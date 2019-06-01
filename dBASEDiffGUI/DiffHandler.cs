﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using dBASE.NET;
using dBASE.NET.Tools;
using Ionic.Zip;

namespace dBASEDiffGUI
{
    public class DiffHandler
    {
        private readonly List<PathPair> _trackedFiles = new List<PathPair>();
        private readonly List<DiffEntry> _diffs = new List<DiffEntry>();
        private readonly string _tempPath = Path.Combine(Path.GetTempPath(), "dBASE_Diff_Files");
        private readonly string _zipPath = Path.Combine(Path.GetTempPath(), "diffs.zip");

        public bool IsTracking { get; set; }

        public string[] Paths => _diffs.Select(d => d.Path).ToArray();

        public void Enumerate(string path)
        {
            _trackedFiles.Clear();

            foreach (string originalFile in Directory.GetFiles(path, "*.dbf", SearchOption.AllDirectories))
            {
                string tempDir = Path.GetTempPath();

                if (!Directory.Exists(_tempPath))
                    Directory.CreateDirectory(_tempPath);


                string copyName = Guid.NewGuid().ToString("B");
                string copyOfPath = Path.Combine(_tempPath, copyName);
                File.Copy(originalFile, copyOfPath);

                string memoPath = Path.ChangeExtension(originalFile, "FPT");

                if (File.Exists(memoPath))
                    File.Copy(memoPath, Path.ChangeExtension(copyOfPath, "FPT"));

                _trackedFiles.Add(new PathPair(copyOfPath, originalFile));
            }
        }

        public void SendResult(string email, string password)
        {
            var fromAddress = new MailAddress(email);
            var toAddress = new MailAddress(email);
            const string subject = "Observed changes in dBASE files.";
            const string body = "This message is autogenerated by dBASE Diff GUI";

            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(fromAddress.Address, password)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
            })
            {
                CreateDiffResult();
                message.Attachments.Add(new Attachment(_zipPath));
                smtp.Send(message);
            }
        }

        public void LoadDiffs(string fileName)
        {
            _diffs.Clear();
            using (var zip = ZipFile.Read(fileName))
            {
                foreach (var entry in zip.Entries)
                {
                    using (var stream = new MemoryStream())
                    {
                        entry.Extract(stream);
                        stream.Seek(0, SeekOrigin.Begin);
                        
                        using (var reader = new StreamReader(stream))
                        {
                            var diff = new DiffEntry(reader.ReadLine(), reader.ReadToEnd());
                            _diffs.Add(diff);
                        }
                    }
                }
            }
        }

        public void ApplyDiffs()
        {
            _diffs.ForEach(d => d.Apply());
        }

        public void SaveResult(string fileName)
        {
            CreateDiffResult();
            File.Copy(_zipPath, fileName, true);
        }

        public void CreateDiffResult()
        {
            File.Delete(_zipPath);
            using (var zip = new ZipFile(_zipPath))
            {
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                foreach (PathPair pair in _trackedFiles)
                {
                    string content = pair.CreateDiff();
                    string diffPath = pair.Original + ".diff";
                    File.WriteAllText(diffPath, $"{pair.Modified}{Environment.NewLine}{content}");
                    zip.AddFile(diffPath, "\\");
                }

                zip.Save();
            }
        }

        public void Cleanup()
        {
            File.Delete(_zipPath);
            if (Directory.Exists(_tempPath))
                Directory.Delete(_tempPath, true);
        }

        private class PathPair
        {
            public PathPair(string pathToOriginal, string pathToModified)
            {
                Original = pathToOriginal;
                Modified = pathToModified;
            }

            public string Original { get; }
            public string Modified { get; }

            public string CreateDiff()
            {
                var encoding = Encoding.GetEncoding(1252);
                var diff = new DbfDiff(new Dbf(Original, encoding), new Dbf(Modified, encoding));

                return diff.Serialize();
            }
        }

        private class DiffEntry
        {
            private readonly string _serializedDiff;

            public DiffEntry(string path, string serializedDiff)
            {
                Path = path;
                _serializedDiff = serializedDiff;
            }

            public string Path { get; }

            public void Apply()
            {
                if (!File.Exists(Path))
                    return;

                var dbf = new Dbf(Path, Encoding.GetEncoding(1252));
                var diff = DbfDiff.Deserialize(dbf.Fields, _serializedDiff);

                diff.ApplyTo(dbf);
                dbf.Save();
            }
        }
    }
}

using dBASE.NET;
using dBASE.NET.Tools;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace dBASEx
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var encoding = Encoding.GetEncoding(1252);

            if (args.Length == 0 || args[0] == "/?" || args[0] == "--help" || args[0] == "-h")
            {
                PrintUsage();
                return;
            }

            for (int i = 0; i < args.Length; ++i)
            {
                string arg = args[i];
                if (arg.StartsWith("-"))
                {
                    switch (arg.Substring(1).ToUpper())
                    {
                        case "DIFF":
                            PrintDiff(args[i + 1], args[i + 2], args[i + 3], encoding);
                            return;
                        case "CSV":
                            PrintCsv(args[i + 1], encoding);
                            return;
                        case "SQL":
                            PrintSchema(args[i + 1], encoding);
                            return;
                        case "PATCH":
                            Patch(args[i + 1], args[i + 2], encoding);
                            return;
                    }
                }
            }
        }

        private static void Patch(string pathToDb, string pathToPatch, Encoding encoding)
        {
            var dbf = new Dbf(pathToDb, encoding);

            DbfDiff diff = DbfDiff.Deserialize(dbf.Fields, File.ReadAllText(pathToPatch));
            diff.ApplyTo(dbf);

            dbf.Save();
        }

        private static void PrintDiff(string dbfName, string pathToOriginal, string pathToModified, Encoding encoding)
        {
            DbfDiff diff = DbfDiff.Create(dbfName, pathToOriginal, pathToModified, encoding);
            Console.WriteLine(diff.Serialize());
        }

        private static void PrintUsage()
        {
            Console.WriteLine("dBASE Tools");
            Console.WriteLine("Options:");
            Console.WriteLine("  -diff  <name> <path to original> <path to modified>");
            Console.WriteLine("  -patch <path to dbf> <path to diff>");
            Console.WriteLine("  -csv   <path>");
            Console.WriteLine("  -sql   <path>\r\n");
        }

        private static void PrintCsv(string dbFilePath, Encoding encoding)
        {
            var csvPath = Path.ChangeExtension(dbFilePath, "csv");
            using (var csvFile = new StreamWriter(csvPath))
            {
                var dbf = new Dbf(dbFilePath, encoding);

                var columnNames = string.Join(",", dbf.Fields.Select(c => c.Name).ToArray());

                csvFile.WriteLine(columnNames);
                Console.WriteLine(columnNames);

                foreach (var record in dbf.Records)
                {
                    csvFile.WriteLine(record);
                    Console.WriteLine(record);
                }
            }
        }

        private static void PrintSchema(string path, Encoding encoding)
        {
            var dbfTable = new Dbf(path, encoding);
            var tableName = Path.GetFileNameWithoutExtension(path);
            Console.WriteLine($"CREATE TABLE [dbo].[{tableName}]");
            Console.WriteLine("(");

            foreach (var dbfColumn in dbfTable.Fields)
            {
                string columnSchema = ColumnSchema(dbfColumn);
                Console.WriteLine($"  {columnSchema},");
            }

            Console.WriteLine("  [IsDeleted] [bit] NULL DEFAULT ((0))");

            Console.WriteLine(")");
        }

        private static string ColumnSchema(DbfField dbfField)
        {
            var schema = string.Empty;
            switch (dbfField.Type)
            {
                case DbfFieldType.Logical:
                    schema = $"[{dbfField.Name}] [bit] NULL DEFAULT ((0))";
                    break;
                case DbfFieldType.Character:
                    schema = $"[{dbfField.Name}] [nvarchar]({dbfField.Length})  NULL";
                    break;
                case DbfFieldType.Currency:
                    schema =
                        $"[{dbfField.Name}] [decimal]({dbfField.Length + dbfField.Precision},{dbfField.Precision}) NULL DEFAULT (NULL)";
                    break;
                case DbfFieldType.Date:
                    schema = $"[{dbfField.Name}] [date] NULL DEFAULT (NULL)";
                    break;
                case DbfFieldType.DateTime:
                    schema = $"[{dbfField.Name}] [datetime] NULL DEFAULT (NULL)";
                    break;
                case DbfFieldType.Double:
                    schema =
                        $"[{dbfField.Name}] [decimal]({dbfField.Length + dbfField.Precision},{dbfField.Precision}) NULL DEFAULT (NULL)";
                    break;
                case DbfFieldType.Float:
                    schema =
                        $"[{dbfField.Name}] [decimal]({dbfField.Length + dbfField.Precision},{dbfField.Precision}) NULL DEFAULT (NULL)";
                    break;
                case DbfFieldType.General:
                    schema = $"[{dbfField.Name}] [nvarchar]({dbfField.Length})  NULL";
                    break;
                case DbfFieldType.Memo:
                    schema = $"[{dbfField.Name}] [ntext]  NULL";
                    break;
                case DbfFieldType.Numeric:
                    if (dbfField.Precision > 0)
                        schema = $"[{dbfField.Name}] [decimal]({dbfField.Length + dbfField.Precision},{dbfField.Precision}) NULL DEFAULT (NULL)";
                    else
                        schema = $"[{dbfField.Name}] [int] NULL DEFAULT (NULL)";
                    break;
                case DbfFieldType.Integer:
                    schema = $"[{dbfField.Name}] [int] NULL DEFAULT (NULL)";
                    break;
            }

            return schema;
        }
    }
}
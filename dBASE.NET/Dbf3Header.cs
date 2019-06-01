﻿using System;
using System.Collections.Generic;
using System.IO;

namespace dBASE.NET
{
    public class Dbf3Header : DbfHeader
    {
        private byte[] _restOfTheHeader = new byte[20];

        internal override void Read(BinaryReader reader)
        {
            Version = (DbfVersion)reader.ReadByte();
            LastUpdate = new DateTime(reader.ReadByte() + 1900, reader.ReadByte(), reader.ReadByte());
            NumRecords = reader.ReadUInt32();
            HeaderLength = reader.ReadUInt16();
            RecordLength = reader.ReadUInt16();
            _restOfTheHeader = reader.ReadBytes(20); // Read rest of header.
        }

        internal override void Write(BinaryWriter writer, List<DbfField> fields, List<DbfRecord> records)
        {
            this.LastUpdate = LastUpdate;
            // Header length = header fields (32b ytes)
            //               + 32 bytes for each field
            //               + field descriptor array terminator (1 byte)
            this.HeaderLength = (ushort)(32 + fields.Count * 32 + 1);
            this.NumRecords = (uint)records.Count;
            this.RecordLength = 1;
            foreach (DbfField field in fields)
            {
                this.RecordLength += field.Length;
            }

            writer.Write((byte)Version);
            writer.Write((byte)(LastUpdate.Year - 1900));
            writer.Write((byte)(LastUpdate.Month));
            writer.Write((byte)(LastUpdate.Day));
            writer.Write(NumRecords);
            writer.Write(HeaderLength);
            writer.Write(RecordLength);
            writer.Write(_restOfTheHeader);
        }
    }
}

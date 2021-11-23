using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SagaLib.VirtualFileSystem.Lpk
{
    public class LpkFileInfo
    {
        private uint headerOffset;
        private uint fileSize;
        private uint uncompressedSize;
        private uint dataOffset;
        private string name;
        private uint crc;

        public uint HeaderOffset { get { return this.headerOffset; } set { this.headerOffset = value; } }

        public uint DataOffset { get { return this.dataOffset; } set { this.dataOffset = value; } }

        public uint FileSize { get { return this.fileSize; } set { this.fileSize = value; } }

        public uint UncompressedSize { get { return this.uncompressedSize; } set { this.uncompressedSize = value; } }

        public uint CRC { get { return this.crc; } set { this.crc = value; } }

        public string Name { get { return this.name; } set { this.name = value; } }

        public static int Size { get { return 16; } }

        public LpkFileInfo()
        {
            
        }

        public LpkFileInfo(Stream stream)
        {
            BinaryReader br = new BinaryReader(stream);
            this.headerOffset = (uint)stream.Position;
            this.dataOffset = br.ReadUInt32() ^ 265851106;
            this.fileSize = br.ReadUInt32() ^ 852870806;
            this.uncompressedSize = br.ReadUInt32() ^ 511060806;
            this.crc = br.ReadUInt32() ^ 987654321;
        }

        public void WriteToStream(Stream stream)
        {
            BinaryWriter bw = new BinaryWriter(stream);
            stream.Position = this.headerOffset;
            bw.Write(dataOffset ^ 265851106);
            bw.Write(fileSize ^ 852870806);
            bw.Write(uncompressedSize ^ 511060806);
            bw.Write(crc ^ 987654321);
        }
    }
}

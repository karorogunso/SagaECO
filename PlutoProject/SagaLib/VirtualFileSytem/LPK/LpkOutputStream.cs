using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace SagaLib.VirtualFileSystem.Lpk
{
    public class LpkOutputStream : Stream
    {
        Stream baseStream;
        LpkFileInfo info;

        GZipStream gzip;

        public LpkOutputStream(Stream lpk, LpkFileInfo file)
        {
            this.baseStream = lpk;
            this.info = file;
            baseStream.Position = file.DataOffset;
            gzip = new GZipStream(baseStream, CompressionMode.Decompress, true);
        }
        
        public override void Close()
        {
            base.Close();
            gzip.Close();
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void Flush()
        {
            throw new NotSupportedException();
        }

        public override long Length
        {
            get { return info.UncompressedSize; }
        }

        public long CompressedLength
        {
            get
            {
                return info.FileSize;
            }
        }

        public override long Position
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return gzip.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }
    }
}

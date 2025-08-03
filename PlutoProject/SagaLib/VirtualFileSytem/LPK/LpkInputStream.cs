using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace SagaLib.VirtualFileSystem.Lpk
{
    public class LpkInputStream : Stream
    {
        Stream baseStream;
        LpkFileInfo info;

        GZipStream gzip;

        public LpkInputStream(Stream lpk, LpkFileInfo file)
        {
            this.baseStream = lpk;
            this.info = file;
            baseStream.Position = file.DataOffset;
            gzip = new GZipStream(baseStream, CompressionMode.Compress, true);
            file.FileSize = 0;
            file.UncompressedSize = 0;
        }
        
        public override void Close()
        {
            base.Close();
            gzip.Close();
            info.FileSize = (uint)this.CompressedLength;
            info.WriteToStream(baseStream);
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Flush()
        {
            baseStream.Flush();
        }

        public override long Length
        {
            get { return info.UncompressedSize; }
        }

        public long CompressedLength
        {
            get
            {
                return baseStream.Position - info.DataOffset;
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
            throw new NotSupportedException();
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
            gzip.Write(buffer, offset, count);
            info.UncompressedSize += (uint)count;
        }
    }
}

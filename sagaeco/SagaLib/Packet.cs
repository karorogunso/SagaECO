using System;
using System.Collections.Generic;
using System.Text;

namespace SagaLib
{
    /// <summary>
    /// Defines the base class of a network packet. Packets are send back and forth between the client 
    /// and server. Different types of packets are used for different purposes. The general packet structure
    /// is: PACKET_SIZE (2 bytes), PACKET_ID (2 bytes), PACKET_DATA (x bytes). The id bytes are considered to
    /// be part of the data bytes.
    /// The size bytes are unencrypted, but the id bytes and all data following are encrypted.
    /// </summary>
    [Serializable]
    public class Packet
    {
        /// <summary>
        /// The size of the packet is equal to the number of data bytes plus 2 bytes for the message id plus 2 bytes for the size.
        /// </summary>
        public uint size;

        /// <summary>
        /// The data bytes (note: these include the id bytes and the size bytes)
        /// </summary>
        public byte[] data;

        /// <summary>
        /// Our current offset in the data array. After creation this will be set to 4 (the first
        /// non ID data byte)
        /// </summary>
        public ushort offset;
        /// <summary>
        /// If true, the data byte array will be cloned before it gets encrypted.
        /// Set it to "true" if you want to send the packet multiple times.
        /// </summary>
        public bool doNotEncryptBuffer;

        internal int length;

        /// <summary>
        /// Create a new packet with the given length. The data bytes are initialized to all zeroes.
        /// </summary>
        /// <param name="length">Length of the data bytes.</param>
        public Packet(uint length)
        {
            this.size = (uint)(length);
            this.data = new byte[length];
            this.offset = 4;
            this.doNotEncryptBuffer = false;
        }

        /// <summary>
        /// Create a new packet with no data bytes at all. The data array has to be initialized manually.
        /// </summary>
        public Packet()
        {
            this.size = 0;
            this.offset = 4;
            this.doNotEncryptBuffer = false;
        }

        /// <summary>
        /// 确保有足够的缓存区，不够则自动扩充
        /// </summary>
        /// <param name="len">长度</param>
        protected void EnsureLength(int len)
        {
            if (len > 20000)
                return;
            int capacity = data.Length;
            bool extend = false;
            while (capacity < len)
            {
                capacity += 1;
                extend = true;
            }
            if (extend)
            {
                byte[] buf = new byte[capacity];
                data.CopyTo(buf, 0);
                data = buf;
            }
            if (length < len)
                length = len;
        }

        /// <summary>
        /// Check to see if a given size is ok for a certain packet.
        /// </summary>
        /// <param name="size">Size to compare with.</param>
        /// <returns>true: size is ok. false: size is not ok.</returns>
        public virtual bool SizeIsOk(uint size) {

            if (this.isStaticSize())
            {
                if (size == this.size) return true;                
            }
            else
            {
                if (size >= this.size) return true;
            }
            return false;
   
        }

        public ushort ID
        {
            get
            {
                return GetUShort(0);
            }
            set
            {
                PutUShort(value, 0);
            }
        }

        /// <summary>
        /// Create a new instance of this packet.
        /// </summary>
        /// <returns></returns>

        public virtual Packet New()
        {
            return new Packet();
        }

        /// <summary>
        /// Parse this packet (only used for GetPackets)
        /// </summary>
        public virtual void Parse(Client client)
        {
            return;
        }

        
        /// <summary>
        /// Write the data length to the first 2 bytes of the packet.
        /// </summary>
        public void SetLength()
        {
            uint tLen = (uint)(data.Length - 4);
            byte[] length = BitConverter.GetBytes(tLen);
            data[0] = length[3];
            data[1] = length[2];
            data[2] = length[1];
            data[3] = length[0];
        }

        public virtual bool isStaticSize() { return true; }

        /// <summary>
        /// Get the Unicode string starting at index.
        /// </summary>
        /// <param name="index">Index of the string.</param>
        /// <returns>String representation.</returns>
        public string GetString(ushort index) {
            ushort end = index;
            while (end < size)
            {
                if (data[end] == 0 && data[end + 1] == 0)
                {
                    if ((end - index) % 2 != 0) end++;
                    break;
                }
                else
                    end++;
            }
            offset = (ushort)(end + 2);            
           
            return Global.Unicode.GetString(data, index, end - index);
        }

        /// <summary>
        /// Get the Unicode string at the current offset.
        /// </summary>
        /// <returns>String representation.</returns>
        public string GetString()
        {
            return GetString(offset);
        }


        public string GetStringFixedSize(ushort index,ushort size)
        {
            if ((index + size) <= this.data.Length)
            {
                offset += size;
                return Global.Unicode.GetString(data, index, size);
            }
            else return "OUT_OF_RANGE";
        }

        /// <summary>
        /// Get the Unicode string at the current offset.
        /// </summary>
        /// <returns>String representation.</returns>
        public string GetStringFixedSize(ushort size)
        {
            return GetStringFixedSize(offset,size);
        }


        /// <summary>
        /// 将Unicode字符串写入指定偏移
        /// </summary>
        /// <param name="s">要写入的字符串.</param>
        /// <param name="index">偏移.</param>
        public void PutString(string s,ushort index)
        {
            byte[] buf = Global.Unicode.GetBytes(s + "\0");
            EnsureLength(index + buf.Length + 1);
            PutByte((byte)buf.Length, index);
            PutBytes(buf, (ushort)(index + 1));
            offset = (ushort)(index + buf.Length + 1);
        }

        /// <summary>
        /// 在当前偏移处写入字符串
        /// </summary>
        /// <param name="s">String to insert.</param>
        public void PutString(string s)
        {
            PutString(s, offset);
        }

        /// <summary>
        /// Get the byte at the given index.
        /// </summary>
        /// <param name="index">Index of the byte.</param>
        /// <returns>Byte at the index.</returns>
        public byte GetByte(ushort index)
        {
            offset = (ushort)(index + 1);
            return data[index];
        }

        /// <summary>
        /// Get the byte at the current offset.
        /// </summary>
        /// <returns>The byte.</returns>
        public byte GetByte()
        {
            return data[offset++];
        }

        /// <summary>
        /// 在指定偏移处写入一个字节
        /// </summary>
        /// <param name="b">字节</param>
        /// <param name="index">偏移</param>
        public void PutByte(byte b, ushort index)
        {
            EnsureLength(index + 1);
            data[index] = b;
            offset = (ushort) (index + 1);
        }

        public void PutByte(byte b, int index)
        {
            EnsureLength(index + 1);
            PutByte(b, (ushort)(index));
        }

        /// <summary>
        /// 在当前位置写入一个字节
        /// </summary>
        /// <param name="b">Byte to insert.</param>
        public void PutByte(byte b)
        {
            EnsureLength(offset + 1);
            data[offset++] = b;
        }

        /// <summary>
        /// 在指定位置取得一个ushort
        /// </summary>
        /// <param name="index">偏移</param>
        /// <returns>The ushort value at the index.</returns>
        public ushort GetUShort(ushort index)
        {
            offset = (ushort)(index + 2);
            byte[] buf = new byte[2];
            buf[0] = data[index + 1];
            buf[1] = data[index];
            return BitConverter.ToUInt16(buf, 0);
        }

        /// <summary>
        /// Get the ushort at the current offset.
        /// </summary>
        /// <returns>The ushort value at the offset.</returns>
        public ushort GetUShort()
        {
            return GetUShort(offset);
        }

        /// <summary>
        /// Put the given ushort at the given index.
        /// </summary>
        /// <param name="s">Ushort to insert.</param>
        /// <param name="index">Index to insert at.</param>
        public void PutUShort(ushort s, ushort index)
        {
            EnsureLength(index + 2);
            byte[] buf = new byte[2];
            buf = BitConverter.GetBytes(s);
            Array.Reverse(buf);
            buf.CopyTo(data, index);
            offset = (ushort) (index + 2);
        }
        public void PutUShort(ushort s, int index)
        {
            PutUShort(s, (ushort)index);
        }
        /// <summary>
        /// Put the given ushort at the current offset.
        /// </summary>
        /// <param name="s"></param>
        public void PutUShort(ushort s)
        {
            PutUShort(s,offset);
        }

        /// <summary>
        /// Get the short at the given index.
        /// </summary>
        /// <param name="index">Index of the short.</param>
        /// <returns>The short value at the index.</returns>
        public short GetShort(ushort index)
        {
            offset = (ushort)(index + 2);
            byte[] buf = new byte[2];
            buf[0] = data[index + 1];
            buf[1] = data[index];
            return BitConverter.ToInt16(buf, 0);
        }

        /// <summary>
        /// Get the short at the current offset.
        /// </summary>
        /// <returns>The short value at the offset.</returns>
        public short GetShort()
        {
            return GetShort(offset);
        }

        /// <summary>
        /// Put the given short at the given index.
        /// </summary>
        /// <param name="s">Short to insert.</param>
        /// <param name="index">Index to insert at.</param>
        public void PutShort(short s, ushort index)
        {
            EnsureLength(index + 2);
            byte[] buf = new byte[2];
            buf = BitConverter.GetBytes(s);
            Array.Reverse(buf);
            buf.CopyTo(data, index);
            offset = (ushort)(index + 2);             
        }
        public void PutShort(short s, int index)
        {
            PutShort(s, (ushort)index);
        }
        /// <summary>
        /// Put the given short at the current offset.
        /// </summary>
        /// <param name="s">Short to insert.</param>
        public void PutShort(short s)
        {
            PutShort(s,offset);
        }

        /// <summary>
        /// Get a set of bytes from a given location.
        /// </summary>
        /// <param name="count">Number of bytes to get.</param>
        /// <param name="index">Indec from where to get bytes.</param>
        /// <returns>Byte array.</returns>
        public byte[] GetBytes(ushort count, ushort index)
        {
            offset = (ushort)(index + count);
            byte[] retBytes = new byte[count];

            if (index + count <= this.data.Length)
            {
                for (ushort i = 0; i < count; i++)
                {
                    retBytes[i] = this.data[index + i];
                }
            }
            return retBytes;
        }

        /// <summary>
        /// Get a certain amount of bytes from the current offset.
        /// </summary>
        /// <param name="count">Number of bytes to read.</param>
        /// <returns>Byte array.</returns>
        public byte[] GetBytes(ushort count)
        {
            return GetBytes(count, offset);
        }

        /// <summary>
        /// Put some given bytes at a given position in the data array.
        /// </summary>
        /// <param name="bdata">bytes to add to the data array</param>
        /// <param name="index">position to add the bytes to</param>
        public void PutBytes(byte[] bdata, ushort index)
        {
            EnsureLength(index + bdata.Length);

            offset = (ushort)(index + bdata.Length);

            if (index + bdata.Length <= this.data.Length)
            {
                for (ushort i = 0; i < bdata.Length; i++)
                {
                    this.data[index + i] = bdata[i];
                }
            }
        }
        public void PutBytes(byte[] bdata, int index)
        {
            PutBytes(bdata, (ushort)index);
        }
        /// <summary>
        /// Put some given bytes at the current offset in the data array.
        /// </summary>
        /// <param name="bdata">bytes to add to the data array</param>
        public void PutBytes(byte[] bdata)
        {
            PutBytes(bdata,offset);
        }

        /// <summary>
        /// Get the int at the given index.
        /// </summary>
        /// <param name="index">Index of the int.</param>
        /// <returns>The int value at the index.</returns>
        public int GetInt(ushort index)
        {
            offset = (ushort)(index + 4);
            byte[] buf = new byte[4];
            buf[0] = data[index + 3];
            buf[1] = data[index + 2];
            buf[2] = data[index + 1];
            buf[3] = data[index];
            return BitConverter.ToInt32(buf, 0);
        }

        /// <summary>
        /// Get the int at the current offset.
        /// </summary>
        /// <returns>The int value at the offset.</returns>
        public int GetInt()
        {
            return GetInt(offset);
        }

        /// <summary>
        /// Put the given int at the given index.
        /// </summary>
        /// <param name="s">Int to insert.</param>
        /// <param name="index">Index to insert at.</param>
        public void PutInt(int s, ushort index)
        {
            EnsureLength(index + 4);
            byte[] buf = new byte[4];
            buf = BitConverter.GetBytes(s);
            Array.Reverse(buf);
            buf.CopyTo(data, index);
            offset = (ushort)(index + 4);
        }

        /// <summary>
        /// Put the given int at the current offset in the data.
        /// </summary>
        /// <param name="s">Int to insert.</param>
        public void PutInt(int s)
        {
            PutInt(s, offset);
        }


        /// <summary>
        /// Get the uint at the given index.
        /// </summary>
        /// <param name="index">Index of the uint.</param>
        /// <returns>The uint value at the index.</returns>
        public uint GetUInt(ushort index)
        {
            offset = (ushort)(index + 4);
            byte[] buf = new byte[4];
            buf[0] = data[index + 3];
            buf[1] = data[index + 2];
            buf[2] = data[index + 1];
            buf[3] = data[index];
            return BitConverter.ToUInt32(buf, 0);
        }

        /// <summary>
        /// Get the uint at the current offset.
        /// </summary>
        /// <returns>The uint value at the offset.</returns>
        public uint GetUInt()
        {
            return GetUInt(offset);
        }

        /// <summary>
        /// Put the given uint at the given index.
        /// </summary>
        /// <param name="s">uint to insert.</param>
        /// <param name="index">Index to insert at.</param>
        public void PutUInt(uint s, ushort index)
        {
            EnsureLength(index + 4);
            byte[] buf = new byte[4];
            buf = BitConverter.GetBytes(s);
            Array.Reverse(buf);
            buf.CopyTo(data, index);
            offset = (ushort)(index + 4);
        }
        public void PutUInt(uint s, int index)
        {
            PutUInt(s, (ushort)index);
        }
        /// <summary>
        /// Put the given uint at the current offset.
        /// </summary>
        /// <param name="s">uint to insert</param>
        public void PutUInt(uint s)
        {
            PutUInt(s,offset);
        }

        public void PutLong(long s)
        {
            PutLong(s, offset);
        }

        public void PutLong(long s, ushort index)
        {
            EnsureLength(index + 8);
            byte[] buf = new byte[8];
            buf = BitConverter.GetBytes(s);
            Array.Reverse(buf);
            buf.CopyTo(data, index);
            offset = (ushort)(index + 8);
        }

        public void PutULong(ulong s)
        {
            PutULong(s, offset);
        }

        public void PutULong(ulong s, ushort index)
        {
            EnsureLength(index + 8);
            byte[] buf = new byte[8];
            buf = BitConverter.GetBytes(s);
            Array.Reverse(buf);
            buf.CopyTo(data, index);
            offset = (ushort)(index + 8);
        }

        public ulong GetULong()
        {
            return GetULong(offset);
        }

        public ulong GetULong(ushort index)
        {
            offset = (ushort)(index + 8);
            byte[] buf = new byte[8];
            buf[0] = data[index + 7];
            buf[1] = data[index + 6];
            buf[2] = data[index + 5];
            buf[3] = data[index + 4];
            buf[4] = data[index + 3];
            buf[5] = data[index + 2];
            buf[6] = data[index + 1];
            buf[7] = data[index];
            return BitConverter.ToUInt64(buf, 0);
        }

        public long GetLong()
        {
            return GetLong(offset);
        }

        public long GetLong(ushort index)
        {
            offset = (ushort)(index + 8);
            byte[] buf = new byte[4];
            buf[0] = data[index + 7];
            buf[1] = data[index + 6];
            buf[2] = data[index + 5];
            buf[3] = data[index + 4];
            buf[4] = data[index + 3];
            buf[5] = data[index + 2];
            buf[6] = data[index + 1];
            buf[7] = data[index];
            return BitConverter.ToInt64(buf, 0);
        }

        /// <summary>
        /// Get the float at the given index.
        /// </summary>
        /// <param name="index">Index of the float.</param>
        /// <returns>The float value at the index.</returns>
        public float GetFloat(ushort index)
        {
            offset = (ushort)(index + 4);
            return BitConverter.ToSingle(data, index);
        }

        /// <summary>
        /// Get the  float at the current offset.
        /// </summary>
        /// <returns>The float value at the offset.</returns>
        public float GetFloat()
        {
            return GetFloat(offset);
        }

        /// <summary>
        /// Put the given float at the given index.
        /// </summary>
        /// <param name="s">Float to insert.</param>
        /// <param name="index">Index to insert at.</param>
        public void PutFloat(float s, ushort index)
        {
            EnsureLength(index + 4);
            BitConverter.GetBytes(s).CopyTo(data, index);
            offset = (ushort)(index + 4);
        }

        /// <summary>
        /// Put the given float at the current offset in the data.
        /// </summary>
        /// <param name="s">Float to insert.</param>
        public void PutFloat(float s)
        {
            PutFloat(s, offset);
        }

        public string DumpData()
        {
            string tmp2 = "";
            for (int i = 0; i < this.data.Length; i++)
            {
                tmp2 += (String.Format("{0:X2} ", this.data[i]));
                if (((i + 1) % 16 == 0) && (i != 0))
                {
                    tmp2 += "\r\n";
                }
            }
            return tmp2;
        }
    }
            
}

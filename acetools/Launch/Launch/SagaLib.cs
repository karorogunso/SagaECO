using System;
using System.Collections.Generic;
using System.Text;

using System.Security.Cryptography;

namespace SagaLib
{
    public class Encryption
    {
        byte[] aesKey = { 2, 3, 3, 74, 56, 5, 20, 0, 2, 13, 2, 13, 2, 13, 2, 13 };
        Rijndael aes;

        public Encryption()
        {
            aes = Rijndael.Create();
            aes.Mode = CipherMode.ECB;
            aes.KeySize = 128;
            aes.Padding = PaddingMode.None;
        }
        public class Files
        {
            public string name;
            public string path;
            public string md5;
        }
        public static class Conversions
        {
            public static string bytes2HexString(byte[] b)
            {
                string tmp = "";
                int i;
                for (i = 0; i < b.Length; i++)
                {
                    string tmp2 = b[i].ToString("X2");
                    tmp = tmp + tmp2;
                }
                return tmp;
            }
        }
        public byte[] Decrypt(byte[] src, int offset)
        {
            if (aesKey == null) return src;
            if (offset == src.Length) return src;
            ICryptoTransform crypt = aes.CreateDecryptor(aesKey, new byte[16]);
            int len = src.Length - offset;
            byte[] buf = new byte[src.Length];
            src.CopyTo(buf, 0);
            //crypt.TransformBlock(src, offset, len, buf, offset);
            return buf;
        }

        public byte[] Encrypt(byte[] src, int offset)
        {
            if (aesKey == null) return src;
            if (offset == src.Length) return src;
            ICryptoTransform crypt = aes.CreateEncryptor(aesKey, new byte[16]);
            int len = src.Length - offset;
            int len_;
            if (src.Length % 16 == 0)
                len_ = len;
            else
                len_ = ((len / 16) + 1) * 16;
            byte[] buf = new byte[len_];
            src.CopyTo(buf, 0);
            crypt.TransformBlock(buf, offset, len_, buf, offset);
            return buf;
        }

    }
}

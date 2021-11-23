using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Cryptography;
using Mono.Math;

namespace SagaLib
{
    public class Encryption
    {
        public static BigInteger Two = new BigInteger((uint)2);
        public static BigInteger Module = new BigInteger("f488fd584e49dbcd20b49de49107366b336c380d451d0f7c88b31c7c5b2d8ef6f3c923c043f0a55b188d8ebb558cb85d38d334fd7c175743a31d186cde33212cb52aff3ce1b1294018118d7c84a70a72d686c40319c807297aca950cd9969fabd00a509b0246d3083d66a45d419f9c7cbd894b221926baaba25ec355e92f78c7");

        BigInteger privateKey = Two;
        byte[] aesKey;
        Rijndael aes;

        public Encryption()
        {
            aes = Rijndael.Create();
            aes.Mode = CipherMode.ECB;
            aes.KeySize = 128;
            aes.Padding = PaddingMode.None;        
        }

        public byte[] AESKey { get { return aesKey; } set { aesKey = value; } }
        
        public void MakePrivateKey()
        {
            SHA1 sha = SHA1.Create();
            byte[] tmp = new byte[40];
            sha.TransformBlock(System.Text.Encoding.ASCII.GetBytes(DateTime.Now.ToString() + DateTime.Now.ToUniversalTime() + DateTime.Now.ToLongDateString()), 0, 40, tmp, 0);
            privateKey = new BigInteger(tmp);
        }

        public byte[] GetKeyExchangeBytes()
        {
            if (privateKey == Two)
                return null;
            return Two.modPow(privateKey, Module).getBytes();
        }

        public void MakeAESKey(string keyExchangeBytes)
        {
            BigInteger A = new BigInteger(keyExchangeBytes);
            byte[] R = A.modPow(privateKey, Module).getBytes();
            aesKey = new byte[16];
            Array.Copy(R, aesKey, 16);
            for (int i = 0; i < 16; i++)
            {
                byte tmp = (byte)(aesKey[i] >> 4);
                byte tmp2 = (byte)(aesKey[i] & 0xF);
                if (tmp > 9)
                    tmp = (byte)(tmp - 9);
                if (tmp2 > 9)
                    tmp2 = (byte)(tmp2 - 9);
                aesKey[i] = (byte)( tmp << 4 | tmp2);
            }
        }

        public bool IsReady
        {
            get
            {
                return aesKey != null;
            }
        }

        public byte[] Encrypt(byte[] src, int offset)
        {
            if (aesKey == null) return src;
            if (offset == src.Length) return src;
            ICryptoTransform crypt = aes.CreateEncryptor(aesKey, new byte[16]);
            int len = src.Length - offset;
            byte[] buf = new byte[src.Length];
            src.CopyTo(buf, 0);
            crypt.TransformBlock(src, offset, len, buf, offset);
            return buf;
        }

        public byte[] Decrypt(byte[] src, int offset)
        {
            if (aesKey == null) return src;
            if (offset == src.Length) return src;
            ICryptoTransform crypt = aes.CreateDecryptor(aesKey, new byte[16]);
            int len = src.Length - offset;
            byte[] buf = new byte[src.Length];
            src.CopyTo(buf, 0);
            int offset2 = 0;
            if (len > 1024)
            {
            }
            while (len > 0)
            {
                int length = len >= 16 ? 16 : len;
                byte[] tmp = new byte[length];
                Array.Copy(src, offset + offset2, tmp, 0, length);
                len -= length;
                crypt.TransformBlock(tmp, 0, length, tmp, 0);
                Array.Copy(tmp, 0, buf, offset + offset2, length);
                offset2 += length;
            }
            return buf;
        }
    }
}

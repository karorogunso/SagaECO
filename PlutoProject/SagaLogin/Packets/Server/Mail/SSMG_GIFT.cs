using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.BBS;

namespace SagaLogin.Packets.Server
{
    public class SSMG_GIFT : Packet
    {
        public SSMG_GIFT()
        {
            this.data = new byte[74];
            this.offset = 2;
            this.ID = 0x01F4;   
        }

        public Gift mails
        {
            set
            {
                byte[] name = Global.Unicode.GetBytes(value.Name + "\0");
                byte[] title = Global.Unicode.GetBytes(value.Title + "\0");
                byte[] buff = new byte[name.Length + title.Length + this.data.Length];
                this.data.CopyTo(buff, 0);
                this.data = buff;

                PutUInt(value.MailID, 2);
                PutUInt((uint)((value.Date - new DateTime(1970, 1, 1)).TotalSeconds), 6);

                PutByte((byte)name.Length, 10);
                PutBytes(name, 11);

                PutByte((byte)title.Length, offset);
                PutBytes(title, offset);

                PutByte(10, offset);

                uint[] itemIDs = new uint[10];
                ushort[] counts = new ushort[10];
                byte count = 0;

                foreach (var item in value.Items)
                {
                    itemIDs[count] = item.Key;
                    counts[count] = item.Value;
                    count++;
                }
                for (int i = 0; i < 10; i++)
                    PutUInt(itemIDs[i], offset);
                PutByte(10, offset);

                for (int i = 0; i < 10; i++)
                    PutUShort(counts[i], offset);
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_SELECT : Packet
    {
        public SSMG_NPC_SELECT()
        {
            this.data = new byte[4];
            this.offset = 2;
            this.ID = 0x05F6;
        }

        public void SetSelect(string title, string confirm, string[] options,bool canCancel)
        {
            if (title != "")
            {
                if (title.Substring(title.Length - 1) != "\0")
                    title += "\0";
            }
            if (confirm != "")
            {
                if (confirm.Substring(confirm.Length - 1) != "\0")
                    confirm += "\0";
            }
            else
            {
                confirm = "\0";
            }
            for (int i = 0; i < options.Length; i++)
            {
                if (options[i].Substring(options[i].Length - 1) != "\0")
                    options[i] += "\0";
            }

            byte[] titleB = Global.Unicode.GetBytes(title);
            byte[] confirmB = Global.Unicode.GetBytes(confirm);
            byte[][] optionsB = new byte[options.Length][];
            int count = 0;
            for (int i = 0; i < options.Length; i++)
            {
                optionsB[i] = Global.Unicode.GetBytes(options[i]);
                count += (optionsB[i].Length + 1);
            }
            count += (titleB.Length + 1);
            count += (confirmB.Length + 1);
            count += 8;
            count += options.Length + 1;
            this.data = new byte[count];
            this.ID = 0x05F6;
            this.offset = 2;

            this.PutByte((byte)titleB.Length);
            this.PutBytes(titleB);
            this.PutByte((byte)options.Length);
            for (int i = 0; i <= options.Length; i++)
            {
                this.PutByte((byte)i);
            }
            foreach (byte[] i in optionsB)
            {
                this.PutByte((byte)i.Length);
                this.PutBytes(i);
            }
            this.PutByte((byte)confirmB.Length);
            if (confirmB.Length != 0)
                this.PutBytes(confirmB);
            if (canCancel)
                this.PutByte(1);
        }
    }
}


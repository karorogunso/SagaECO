using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Synthese;
using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_SYNTHESE_INFO : Packet
    {
        public SSMG_NPC_SYNTHESE_INFO()
        {
            this.data = new byte[42];
            this.offset = 2;
            this.ID = 0x13B6;
        }

        public SyntheseInfo Synthese
        {
            set
            {
                if (value.Materials.Count > 4 || value.Products.Count > 4)
                    throw new ArgumentOutOfRangeException();
                this.PutByte(4, 2);
                int j = 0;
                foreach (ItemElement i in value.Materials)
                {
                    this.PutUInt(i.ID, (ushort)(3 + j * 4));
                    j++;
                }
                this.PutByte(4, 19);
                j = 0;
                foreach (ItemElement i in value.Materials)
                {
                    this.PutUShort(i.Count, (ushort)(20 + j * 2));
                    j++;
                }
                this.PutUInt(value.RequiredTool, 28);
                this.PutUInt(value.ID, 32);
                this.PutUInt(value.Gold, 36);
                this.PutByte(1, 40);
            }
        }
    }
}


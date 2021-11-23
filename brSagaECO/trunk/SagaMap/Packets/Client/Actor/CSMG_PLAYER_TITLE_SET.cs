using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PLAYER_TITLE_SET : Packet
    {
        public CSMG_PLAYER_TITLE_SET()
        {
            this.offset = 2;
        }

        public byte Count
        {
            get
            {
                return this.GetByte(2);
            }
        }

        public List<uint> Titles
        {
            get
            {
                this.offset = 3;
                List<uint> titles = new List<uint>();
                for (int i=0; i<Count;i++)
                {
                    titles.Add(this.GetUInt(offset));
                    offset += 4;
                }
                return titles;
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_PLAYER_TITLE_SET();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnTitleSet(this);
        }
    }
}
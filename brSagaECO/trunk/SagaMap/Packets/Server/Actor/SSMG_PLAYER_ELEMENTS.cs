using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_ELEMENTS : Packet
    {
        public SSMG_PLAYER_ELEMENTS()
        {
            this.data = new byte[32];
            this.offset = 2;
            this.ID = 0x0223;

            this.PutByte(7, 2);
            this.PutByte(7, 17);
        }

        public Dictionary<Elements, int> AttackElements
        {
            set
            {
                int j=0;
                foreach (Elements i in value.Keys)
                {
                    this.PutShort((short)value[i], (ushort)(3 + j++ * 2));                    
                }
            }
        }

        public Dictionary<Elements, int> DefenceElements
        {
            set
            {
                int j = 0;
                foreach (Elements i in value.Keys)
                {
                    this.PutShort((short)value[i], (ushort)(18 + j++ * 2));
                }
            }
        }
    }
}
        

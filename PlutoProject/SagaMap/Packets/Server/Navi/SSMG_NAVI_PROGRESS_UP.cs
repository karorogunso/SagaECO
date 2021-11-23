using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_NAVI_PROGRESS_UP : Packet
    {
        public SSMG_NAVI_PROGRESS_UP()
        {
            this.data = new byte[23];
            this.ID = 0x1EB0;
            this.offset = 2;
        }
        uint id = 0;
        public uint NaviID
        {
            set
            {
                id = value;
                this.PutUInt(value, 2);
                this.PutByte(4, 6);
            }
        }
        public SagaDB.Actor.ActorPC pc
        {
            set
            {
                /*
                this.PutInt(value.Navi.UniqueSteps[id].BelongEvent.DisplaySteps);
                this.PutInt(0);
                this.PutInt(value.Navi.UniqueSteps[id].BelongEvent.FinishedSteps);
                this.PutInt(0);
                */
            }
        }
    }
}

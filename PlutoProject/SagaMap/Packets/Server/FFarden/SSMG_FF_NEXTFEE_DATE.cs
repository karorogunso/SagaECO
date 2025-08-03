using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_FF_NEXTFEE_DATE : Packet
    {
        //下次自动扣费日期 (次回引き落とし日時)  
        public SSMG_FF_NEXTFEE_DATE()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x2023;
        }
        public DateTime UpdateTime
        {
            set
            {
                uint date = (uint)(value - new DateTime(1970, 1, 1)).TotalSeconds;
                this.PutUInt(date, 2);
            }
        }
    }
}

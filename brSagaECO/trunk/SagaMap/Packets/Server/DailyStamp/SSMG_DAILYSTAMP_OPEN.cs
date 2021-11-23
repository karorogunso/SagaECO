using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;


namespace SagaMap.Packets.Server
{
    public class SSMG_DAILYSTAMP_OPEN : Packet
    {
        public SSMG_DAILYSTAMP_OPEN()
        {
            this.data = new byte[9];
            this.ID = 0x1F75;
            this.offset = 2;
        }
        public DailyStamp DailyStamp
        {
            set
            {
                int position = 0;
                foreach (DailyStampSlot slot in Enum.GetValues(typeof(DailyStampSlot)))
                {
                    if (value.Stamps.Test(slot))
                        position++;
                    else
                        break;
                }
                this.PutInt(position, 4);
            }
        }
        public byte Action
        {
            set
            {
                this.PutByte(value, 8);
                // 1 for show
                // 2 for stamp use
            }
        }
    }
}


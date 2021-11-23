using SagaLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.Actor
{
    public enum DailyStampSlot
        {
            One = 1,
            Two ,
            Three,
            Four,
            Five ,
            Six,
            Seven,
            Eight ,
            Nine,
            Ten,
        }
    public class DailyStamp
    {
        BitMask<DailyStampSlot> stamps;
        public DailyStamp()
        {
            stamps = new BitMask<DailyStampSlot>();
        }

        public void Dispose()
        {
            stamps = null;
        }

        public BitMask<DailyStampSlot> Stamps
        {
            get
            {
                if (stamps==null)
                    stamps = new BitMask<DailyStampSlot>();
                return stamps;
            }
        }
    }
}

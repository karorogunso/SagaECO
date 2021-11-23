using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using SagaDB;
using SagaDB.Theater;
using SagaDB.Actor;
using SagaLib;
using SagaMap;
using SagaMap.Manager;
using SagaMap.Scripting;

namespace SagaMap.Network.Client
{
    public partial class MapClient
    {
        public void OnDailyStampOpen(Packets.Client.CSMG_DAILYSTAMP_OPEN p)
        {
            Packets.Server.SSMG_DAILYSTAMP_OPEN p1 = new Packets.Server.SSMG_DAILYSTAMP_OPEN();
            //p1.DailyStamp= this.Character.DailyStamp;
            this.netIO.SendPacket(p1);
            updateDailyStamp();
        }

        void updateDailyStamp()
        {
            if (System.DateTime.Now.Date == this.Character.DailyStampDate.Date)
            {
                foreach (DailyStampSlot slot in Enum.GetValues(typeof(DailyStampSlot)))
                {
                    if (!this.Character.DailyStamp.Stamps.Test(slot))
                    {
                        this.Character.DailyStamp.Stamps.SetValue(slot, true);
                        this.Character.DailyStampDate = System.DateTime.Now;
                        Packets.Server.SSMG_DAILYSTAMP_STAMP_USE p = new Packets.Server.SSMG_DAILYSTAMP_STAMP_USE();
                        p.Slot = (byte)slot;
                        this.netIO.SendPacket(p);
                        return;
                    }
                }
            }
        }
    }
}

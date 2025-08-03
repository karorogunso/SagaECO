using SagaLib;
using SagaDB.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaMap.Network.Client
{
    public partial class MapClient
    {
        public void OnNewMosterDiscover(uint mobID)
        {
            if (SagaDB.Mob.MobFactory.Instance.Mobs.ContainsKey(mobID))
            {
                SagaDB.Mob.MobData mob = SagaDB.Mob.MobFactory.Instance.Mobs[mobID];
                if (mob.guideFlag == 1)
                {
                    Packets.Server.SSMG_MOSTERGUIDE_NEW_RECORD p = new Packets.Server.SSMG_MOSTERGUIDE_NEW_RECORD();
                    p.guideID = mob.guideID;
                    this.netIO.SendPacket(p);
                }
            }
        }
        public void SendMosterGuide()
        {
            Dictionary<short,uint> MobList = (from m in SagaDB.Mob.MobFactory.Instance.Mobs.Values where m.guideFlag == 3 orderby m.guideID select m).ToDictionary(m=>m.guideID,m=>m.id);
            
            //switch m.guideFlag to 1 when enabled
            bool[] boolstates = new bool[MobList.Keys.Max()];
            
            for (short i =0;i< boolstates.Length;i++)
            {
                bool state = false;
                if (MobList.ContainsKey(i))
                {
                    if (this.Character.MosterGuide.ContainsKey(MobList[i]))
                        state = this.Character.MosterGuide[MobList[i]];
                }
                boolstates[i]=state;
            }

            List<BitMask> masks = new List<BitMask>();
            byte index = 0;
            int BitmaskSize = 32;
            int skip = BitmaskSize * index;
            while (skip < boolstates.Length)
            {
                bool[] items = boolstates.Select(x => x).Skip(skip).Take(BitmaskSize).ToArray();
                masks.Add(new BitMask(items));
                index++;
                skip = BitmaskSize * index;
            }

            Packets.Server.SSMG_MOSTERGUIDE_RECORDS p = new Packets.Server.SSMG_MOSTERGUIDE_RECORDS();
            p.Records = masks;
            this.netIO.SendPacket(p);
        }
    }
}
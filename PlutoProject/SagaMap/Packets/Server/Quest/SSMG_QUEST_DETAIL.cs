using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Quests;

namespace SagaMap.Packets.Server
{
    public class SSMG_QUEST_DETAIL : Packet
    {        
        public SSMG_QUEST_DETAIL()
        {
            this.data = new byte[2];
            this.offset = 2;
            this.ID = 0x1992;            
        }

        /// <summary>
        /// 显示任务详细资料
        /// </summary>
        /// <param name="type">任务类型</param>
        /// <param name="name">任务名字</param>
        /// <param name="mapid1">未知地图ID</param>
        /// <param name="mapid2">传送任务起始地图</param>
        /// <param name="mapid3">传送任务目标地图</param>
        /// <param name="info1">未知NPC名</param>
        /// <param name="info2">传送任务起始NPC</param>
        /// <param name="info3">传送任务目标NPC</param>
        /// <param name="unk1">击退任务怪物地图1</param>
        /// <param name="unk2">击退任务怪物地图2</param>
        /// <param name="unk3">击退任务怪物地图3</param>
        /// <param name="item1">物品或怪物ID</param>
        /// <param name="item2">物品或怪物ID</param>
        /// <param name="item3">物品或怪物ID</param>
        /// <param name="amount1">物品或怪物数量</param>
        /// <param name="amount2">物品或怪物数量</param>
        /// <param name="amount3">物品或怪物数量</param>
        /// <param name="time">剩余时间</param>
        /// <param name="unk4"></param>
        public void SetDetail(QuestType type,string name,
            uint mapid1,uint mapid2,uint mapid3,
            string info1,string info2,string info3,
            uint unk1,uint unk2,uint unk3,
            uint item1,uint item2,uint item3,
            uint amount1,uint amount2,uint amount3,
            int time,uint unk4)
        {
            byte[] nameb = Global.Unicode.GetBytes(name + "\0");
            byte[] info1b = Global.Unicode.GetBytes(info1);
            byte[] info2b = Global.Unicode.GetBytes(info2);
            byte[] info3b = Global.Unicode.GetBytes(info3);
            byte[] buff = new byte[68 + nameb.Length + info1b.Length + info2b.Length + info3b.Length];
            this.data.CopyTo(buff, 0);
            this.data = buff;

            this.PutByte((byte)type, 2);
            this.PutByte((byte)nameb.Length, 3);
            this.PutBytes(nameb, 4);
            this.PutByte(3, (ushort)(4 + nameb.Length));
            this.PutUInt(mapid1, (ushort)(5 + nameb.Length));
            this.PutUInt(mapid2, (ushort)(9 + nameb.Length));
            this.PutUInt(mapid3, (ushort)(13 + nameb.Length));
            this.PutByte(3, (ushort)(17 + nameb.Length));
            this.PutByte((byte)info1b.Length, (ushort)(18 + nameb.Length));
            this.PutBytes(info1b, (ushort)(19 + nameb.Length));
            this.PutByte((byte)info2b.Length, (ushort)(19 + nameb.Length + info1b.Length));
            this.PutBytes(info2b, (ushort)(20 + nameb.Length + info1b.Length));
            this.PutByte((byte)info3b.Length, (ushort)(20 + nameb.Length + info1b.Length + info2b.Length));
            this.PutBytes(info3b, (ushort)(21 + nameb.Length + info1b.Length + info2b.Length));
            this.PutByte(3, (ushort)(21 + nameb.Length + info1b.Length + info2b.Length + info3b.Length));
            this.PutUInt(unk1, (ushort)(22 + nameb.Length + info1b.Length + info2b.Length + info3b.Length));
            this.PutUInt(unk2, (ushort)(26 + nameb.Length + info1b.Length + info2b.Length + info3b.Length));
            this.PutUInt(unk3, (ushort)(30 + nameb.Length + info1b.Length + info2b.Length + info3b.Length));
            this.PutByte(3, (ushort)(34 + nameb.Length + info1b.Length + info2b.Length + info3b.Length));
            this.PutUInt(item1, (ushort)(35 + nameb.Length + info1b.Length + info2b.Length + info3b.Length));
            this.PutUInt(item2, (ushort)(39 + nameb.Length + info1b.Length + info2b.Length + info3b.Length));
            this.PutUInt(item3, (ushort)(43 + nameb.Length + info1b.Length + info2b.Length + info3b.Length));
            this.PutByte(3, (ushort)(47 + nameb.Length + info1b.Length + info2b.Length + info3b.Length));
            this.PutUInt(amount1, (ushort)(48 + nameb.Length + info1b.Length + info2b.Length + info3b.Length));
            this.PutUInt(amount2, (ushort)(52 + nameb.Length + info1b.Length + info2b.Length + info3b.Length));
            this.PutUInt(amount3, (ushort)(56 + nameb.Length + info1b.Length + info2b.Length + info3b.Length));
            this.PutInt(time, (ushort)(60 + nameb.Length + info1b.Length + info2b.Length + info3b.Length));
            this.PutUInt(unk4, (ushort)(64 + nameb.Length + info1b.Length + info2b.Length + info3b.Length));            
        }
    }
}


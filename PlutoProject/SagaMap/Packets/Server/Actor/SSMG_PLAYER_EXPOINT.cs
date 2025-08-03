using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    /// <summary>
    /// 角色EX 属性/技能点的情况
    /// </summary>
    public class SSMG_PLAYER_EXPOINT : Packet
    {
        public SSMG_PLAYER_EXPOINT()
        {
            this.data = new byte[13];
            this.offset = 2;
            this.ID = 0x0695;
        }

        public ushort EXStatPoint
        {
            set { this.PutUShort(value, 2); }
        }

        public ushort CanUseStatPoint
        {
            set { this.PutUShort(value, 4); }
        }

        public byte EXSkillPoint
        {
            set { this.PutByte(value, 6); }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Quests;

namespace SagaMap.Packets.Server
{
    public class SSMG_TRADE_START : Packet
    {        
        public SSMG_TRADE_START()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x0A0F;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type">00だと人間? 01だとNPC</param>
        public void SetPara(string name, int type)
        {
            byte[] buf = Global.Unicode.GetBytes(name + "\0");
            byte[] buff = new byte[7 + buf.Length];
            this.data.CopyTo(buff, 0);
            this.data = buff;
            this.PutByte((byte)buf.Length, 2);
            this.PutBytes(buf, 3);
            this.PutInt(type, (ushort)(3 + buf.Length));
        }
    }
}


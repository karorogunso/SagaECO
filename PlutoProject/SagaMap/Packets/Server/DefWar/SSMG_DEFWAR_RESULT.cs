using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.DefWar;


namespace SagaMap.Packets.Server
{
    public class SSMG_DEFWAR_RESULT : Packet
    {

        public SSMG_DEFWAR_RESULT()
        {
            this.data = new byte[17];
            this.offset = 2;
            this.ID = 0x1BC6;
        }

        /// <summary>
        /// 攻略结果1:2为夺还,3为攻略
        /// </summary>
        public byte Result1
        {
            set
            {
                this.PutByte(value, 2);
            }
        }
        /// <summary>
        /// 攻略结果2:0为失败,1为成功,2为大成功,3为完全成功
        /// </summary>
        public byte Result2
        {
            set
            {
                this.PutByte(value, 3);
            }
        }
        public int EXP
        {
            set
            {
                this.PutInt(value, 4);
            }
        }
        public int JOBEXP
        {
            set
            {
                this.PutInt(value, 8);
            }
        }
        public int CP
        {
            set
            {
                this.PutInt(value, 12);
            }
        }
        public byte Unknown
        {
            set
            {
                this.PutByte(value, 16);
            }
        }
    }
}

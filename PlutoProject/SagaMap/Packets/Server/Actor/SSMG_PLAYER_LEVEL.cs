using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    /// <summary>
    /// [00][0E][02][3A]
    ///[2D] //Lv
    ///[2D] //JobLv(1次職)
    ///[01] //JobLv(エキスパート)
    ///[01] //JobLv(テクニカル)
    ///[00][2E] //ボーナスポイント
    ///[00][08] //スキルポイント(1次職)
    ///[00][00] //スキルポイント(エキスパート)
    ///[00][00] //スキルポイント(テクニカル)
    /// </summary>
    public class SSMG_PLAYER_LEVEL : Packet
    {
        public SSMG_PLAYER_LEVEL()
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga17)
                this.data = new byte[19];
            else if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
                this.data = new byte[18];
            else
                this.data = new byte[15];
            this.offset = 2;
            this.ID = 0x023A;
        }

        public byte Level
        {
            set
            {
                this.PutByte(value, 2);
            }
        }

        public byte JobLevel
        {
            set
            {
                this.PutByte(value, 3);
            }
        }

        public byte JobLevel2X
        {
            set
            {
                this.PutByte(value, 4);
            }
        }

        public byte JobLevel2T
        {
            set
            {
                this.PutByte(value, 5);
            }
        }

        public byte JobLevel3
        {
            set
            {
                this.PutByte(value, 6);
            }
        }

        public byte IsDualJob
        {
            set
            {
                this.PutByte(value, 7);
            }
            get
            {
                return this.GetByte(7);
            }
        }

        public byte DualjobLevel
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
                    this.PutByte(value, 8);
            }
        }

        public ushort UseableStatPoint
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga17)
                    this.PutUShort(value, 9);
                else if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
                    this.PutUShort(value, 8);
                else
                    this.PutUShort(value, 7);
            }
        }

        public ushort SkillPoint
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga17)
                    this.PutUShort(value, 11);
                else if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
                    this.PutUShort(value, 10);
                else
                    this.PutUShort(value, 9);
            }
        }

        public ushort Skill2XPoint
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga17)
                    this.PutUShort(value, 13);
                else if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
                    this.PutUShort(value, 12);
                else
                    this.PutUShort(value, 11);
            }
        }

        public ushort Skill2TPoint
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga17)
                    this.PutUShort(value, 15);
                else if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
                    this.PutUShort(value, 14);
                else
                    this.PutUShort(value, 13);
            }
        }

        public ushort Skill3Point
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga17)
                    this.PutUShort(value, 17);
                else if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
                    this.PutUShort(value, 16);
                else
                    this.PutUShort(value, 13);
            }
        }
    }
}
        

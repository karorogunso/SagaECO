using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;

using SagaLib;

namespace SagaLogin.Packets.Server
{
    public class SSMG_FRIEND_CHAR_INFO : Packet
    {
        public SSMG_FRIEND_CHAR_INFO()
        {
            this.data = new byte[20];
            this.ID = 0x00DC;
        }

        public ActorPC ActorPC
        {
            set
            {
                this.PutUInt(value.CharID, 2);
                byte[] buf = Global.Unicode.GetBytes(value.Name + "\0");
                byte[] buff = new byte[20 + buf.Length];
                this.data.CopyTo(buff, 0);
                this.data = buff;
                byte size = (byte)buf.Length;
                this.PutByte(size, 6);
                this.PutBytes(buf, 7);
                if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                {
                    this.PutByte((byte)4, (ushort)(7 + size));
                    this.PutUShort((ushort)value.Job, (ushort)(8 + size));
                    this.PutUShort((ushort)value.Level, (ushort)(10 + size));
                    this.PutUShort((ushort)value.CurrentJobLevel, (ushort)(12 + size));
                }
                else
                {
                    this.PutUShort((ushort)value.Job, (ushort)(7 + size));
                    this.PutUShort((ushort)value.Level, (ushort)(9 + size));
                    this.PutUShort((ushort)value.CurrentJobLevel, (ushort)(11 + size));
                }
            }
        }

        public uint MapID
        {
            set
            {
                byte size = this.GetByte(6);
                if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                    this.PutUInt(value, (ushort)(14 + size));
                else
                    this.PutUInt(value, (ushort)(13 + size));
            }
        }

        public SagaLogin.Network.Client.CharStatus Status
        {
            set
            {
                byte size = this.GetByte(6);
                if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                    this.PutByte((byte)value, (ushort)(18 + size));
                else
                    this.PutByte((byte)value, (ushort)(17 + size));
            }
        }

        public string Comment
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                {
                    byte size = this.GetByte(6);
                    byte[] buf = Global.Unicode.GetBytes(value + "\0");
                    byte[] buff = new byte[20 + size + buf.Length];
                    this.data.CopyTo(buff, 0);
                    this.data = buff;
                    this.PutByte((byte)buf.Length, (ushort)(19 + size));
                    this.PutBytes(buf, (ushort)(20 + size));
                }
                else
                {
                    byte size = this.GetByte(6);
                    byte[] buf = Global.Unicode.GetBytes(value + "\0");
                    byte[] buff = new byte[19 + size + buf.Length];
                    this.data.CopyTo(buff, 0);
                    this.data = buff;
                    this.PutByte((byte)buf.Length, (ushort)(18 + size));
                    this.PutBytes(buf, (ushort)(19 + size));
                }
            }
        }
    }
}


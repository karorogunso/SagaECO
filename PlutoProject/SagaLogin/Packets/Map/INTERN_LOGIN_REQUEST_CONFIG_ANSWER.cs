using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaLogin;
using SagaLogin.Network.Client;

namespace SagaLogin.Packets.Map
{
    public class INTERN_LOGIN_REQUEST_CONFIG_ANSWER : Packet
    {
        public INTERN_LOGIN_REQUEST_CONFIG_ANSWER()
        {
            this.data = new byte[8];
            this.offset = 2;
            this.ID = 0xFFF2;
        }

        public bool AuthOK
        {
            set
            {
                if (value)
                    this.PutByte(1, 2);
                else
                    this.PutByte(0, 2);
            }
        }

        public Dictionary<SagaDB.Actor.PC_RACE, Configurations.StartupSetting> StartupSetting
        {
            set
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bf.Serialize(ms, value);
                ms.Flush();
                byte[] buf = new byte[8 + ms.Length];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                this.PutUInt((uint)ms.Length, 3);
                this.PutBytes(ms.ToArray(), 7);
            }
        }
    }
}
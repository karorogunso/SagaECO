using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.LoginServer;

namespace SagaMap.Packets.Login
{
    public class INTERN_LOGIN_REQUEST_CONFIG_ANSWER : Packet
    {
        public INTERN_LOGIN_REQUEST_CONFIG_ANSWER()
        {
            this.offset = 2;
        }

        public bool AuthOK
        {
            get
            {
                return (GetByte(2) == 1);
            }
        }

        public Dictionary<SagaDB.Actor.PC_RACE, SagaLogin.Configurations.StartupSetting> StartupSetting
        {
            get
            {
                uint len = GetUInt(3);
                byte[] buf;
                buf = GetBytes((ushort)len, 7);
                System.IO.MemoryStream ms = new System.IO.MemoryStream(buf);
                Dictionary<SagaDB.Actor.PC_RACE, SagaLogin.Configurations.StartupSetting> list;
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                list = (Dictionary<SagaDB.Actor.PC_RACE, SagaLogin.Configurations.StartupSetting>)bf.Deserialize(ms);
                return list;
            }
        }
        
        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Login.INTERN_LOGIN_REQUEST_CONFIG_ANSWER();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginSession)(client)).OnGetConfig(this);
        }

    }
}
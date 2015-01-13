using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaWorld.Packets.Server
{
    public class SSMG_VERSION_ACK : Packet
    {
        public enum Result
        {
            OK = 0,
            VERSION_MISSMATCH = -1
        }
        public SSMG_VERSION_ACK()
        {
            this.data = new byte[10];
            this.offset = 14;
            this.ID = 0x0002;           
        }

        public void SetResult(Result res)
        {
            this.PutShort((short)res, 2);
        }

        public void SetVersion(string version)
        {
            this.PutBytes(Conversions.HexStr2Bytes(version), 4);
        }

    }
}


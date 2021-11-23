using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Ring;


namespace SagaMap.Packets.Server
{
    public class SSMG_TEST_EVOLVE_OPEN : Packet
    {
        public SSMG_TEST_EVOLVE_OPEN()
        {
            if (Configuration.Instance.Version>=SagaLib.Version.Saga18)
            {
                this.data = new byte[19];
                //not sure
                this.PutInt(1, 2);
                this.PutInt(1, 6);
                this.PutInt(50, 10);
                this.PutInt(100, 14);
                this.PutByte(0, 18);
            }
            else
            {
                this.data = new byte[3];
                this.PutByte(0, 2);
            }
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x0606;   
        }
    }
}


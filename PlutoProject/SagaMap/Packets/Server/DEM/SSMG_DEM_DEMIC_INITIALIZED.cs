using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;


namespace SagaMap.Packets.Server
{
    public class SSMG_DEM_DEMIC_INITIALIZED : Packet
    {
        public enum Results
        {
            OK,
            FAILED = -1,
            TOO_MANY_ITEMS = -2,
            NOT_ENOUGH_EP = -3,
        }

        public SSMG_DEM_DEMIC_INITIALIZED()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1E4D;
            
        }

        public byte Page
        {
            set
            {
                this.PutByte(value, 2);
            }
        }

        public Results Result
        {
            set
            {
                this.PutByte((byte)value, 3);
            }
        }

        public byte EngageTask
        {
            set
            {
                byte task = value;
                if (task == 255)
                    task = 0;
                else
                    task++;
                this.PutByte(task, 4);
            }
        }
        public byte EngageTask2
        {
            set
            {
                byte task = value;
                if (task == 255)
                    task = 0;
                else
                    task++; 
                this.PutByte(task, 5);
            }
        }
    }
}


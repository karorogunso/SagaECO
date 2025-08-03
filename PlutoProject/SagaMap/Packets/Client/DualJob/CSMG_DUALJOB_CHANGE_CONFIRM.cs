using SagaDB.DualJob;
using SagaLib;
using SagaMap.Network.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaMap.Packets.Client
{
    public class CSMG_DUALJOB_CHANGE_CONFIRM : Packet
    {
        public CSMG_DUALJOB_CHANGE_CONFIRM()
        {
            this.offset = 2;
        }

        public byte DualJobID
        {
            get { return byte.Parse(this.GetShort(offset).ToString()); }
        }

        public uint[] DualJobSkillList
        {
            get
            {
                uint[] skills = new uint[this.GetByte(offset)];

                for (int i = 0; i < skills.Length; i++)
                {
                    var x = this.GetShort(offset).ToString();
                    if (x == "-1")
                        skills[i] = 0;
                    else
                        skills[i] = uint.Parse(x);
                }

                return skills;
            }
        }

        public override Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_DUALJOB_CHANGE_CONFIRM();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnDualChangeRequest(this);
        }
    }
}

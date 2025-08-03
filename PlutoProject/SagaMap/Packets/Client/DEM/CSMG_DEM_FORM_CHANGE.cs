using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_DEM_FORM_CHANGE : Packet
    {
        public CSMG_DEM_FORM_CHANGE()
        {
            this.offset = 2;
        }

        public DEM_FORM Form
        {
            get
            {
                return (DEM_FORM)this.GetByte(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_DEM_FORM_CHANGE();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnDEMFormChange(this);
        }

    }
}
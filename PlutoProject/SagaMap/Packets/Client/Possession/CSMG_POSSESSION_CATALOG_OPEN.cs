
using SagaLib;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_POSSESSION_CATALOG_REQUEST : Packet
    {
        public CSMG_POSSESSION_CATALOG_REQUEST()
        {
            this.offset = 2;
        }

        public PossessionPosition Position
        {
            get
            {
                return (PossessionPosition)this.GetByte(2);
            }
        }
        public ushort Page
        {
            get
            {
                return this.GetUShort(3);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_POSSESSION_CATALOG_REQUEST();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPossessionCatalogRequest(this);
        }

    }
}
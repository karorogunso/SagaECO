using SagaLib;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_ITEM_EXCHANGE_CONFIRM : Packet
    {
        public CSMG_ITEM_EXCHANGE_CONFIRM()
        {
            this.offset = 2;
        }

        public uint ExchangeType
        {
            get { return this.GetUInt(2); }
        }

        public uint InventorySlot
        {
            get { return this.GetUInt(6); }
        }

        public uint ExchangeTargetID
        {
            get { return this.GetUInt(10); }
        }

        public override Packet New()
        {
            return (Packet)new CSMG_ITEM_EXCHANGE_CONFIRM();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnItemExchangeConfirm(this);
        }
    }
}

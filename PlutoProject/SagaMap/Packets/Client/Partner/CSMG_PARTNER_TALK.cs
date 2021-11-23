//Comment this out to deactivate the dead lock check!
using SagaLib;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PARTNER_TALK : Packet
    {
        public CSMG_PARTNER_TALK()
        {
            this.offset = 2;
        }

        public uint ItemID
        {
            get { return this.GetUInt(4); }
        }

        public uint Type
        {
            get { return this.GetUInt(8); }
        }

        public string Msg
        {
            get { return this.GetString(12); }
        }

        public override Packet New()
        {
            return new CSMG_PARTNER_TALK();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPartnerTalk(this);
        }
    }
}
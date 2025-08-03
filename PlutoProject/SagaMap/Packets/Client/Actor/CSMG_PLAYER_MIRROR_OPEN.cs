using SagaLib;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PLAYER_MIRROR_OPEN : Packet
    {
        public CSMG_PLAYER_MIRROR_OPEN()
        {
            this.data = new byte[10];
            this.ID = 0x02B2;
            this.offset = 2;
        }

        public override Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_PLAYER_MIRROR_OPEN();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnMirrorOpenRequire(this);
        }
    }
}

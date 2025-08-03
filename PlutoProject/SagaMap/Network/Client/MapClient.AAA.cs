using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaMap.Network.Client
{
    public partial class MapClient
    {
        public void OnGroupJoin()
        {

        }
        public void OnGroupMemberJoin()
        {

        }
        public void OnGroupMemberKick()
        {

        }
        public void OnGroupLeave()
        {
            if (this.chara.Party.Leader == this.chara)
            {
                Packets.Server.SSMG_AAA_GROUP_DESTROY p2 = new Packets.Server.SSMG_AAA_GROUP_DESTROY();
                this.netIO.SendPacket(p2);
            }
        }
        public void OnGroupSelect()
        {

        }
        public void OnGroupUpdate()
        {

        }
        public void OnGroupChangeState()
        {

        }
        public void OnGroupStart()
        {

        }
        public void OnGroupRestart()
        {

        }
    }
}

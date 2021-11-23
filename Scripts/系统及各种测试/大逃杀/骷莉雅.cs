
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using WeeklyExploration;
using System.Globalization;
namespace SagaScript.M30210000
{
    public class S60000132 : Event
    {
        public S60000132()
        {
            this.EventID = 60000132;
        }
        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "你好呀，现在PK模式正在测试中。$R", "骷莉亚");
            switch(Select(pc,"怎么办呢？","","开启PK模式","关掉PK模式","离开"))
            {
                case 1:
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendPkMode();
                    break;
                case 2:
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendNormalMode();
                    break;
                case 3:
                    if (pc.Account.GMLevel > 30)
                    {
                        SagaMap.Configuration.Instance.PVPDamageRateMagic = 0.1f;
                        SagaMap.Configuration.Instance.PVPDamageRatePhysic = 0.1f;
                    }
                    break;
            }
        }
    }
}
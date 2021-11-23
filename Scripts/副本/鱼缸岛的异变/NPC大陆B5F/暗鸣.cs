
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S80000202 : Event
    {
        public S80000202()
        {
            this.EventID = 80000202;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "他们看起来没有侵略的意思，$R但却很明显的在守护着什么，$R只要有人接近他们的领地$R就会遭到毫不留情的攻击。$R$R为什么我们的岛上会出现这样的怪物？", "暗鸣");
        }
    }
}


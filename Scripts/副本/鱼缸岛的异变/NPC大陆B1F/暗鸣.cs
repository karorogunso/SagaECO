
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
    public class S80000210 : Event
    {
        public S80000210()
        {
            this.EventID = 80000210;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "越接近地表，$R怪物就越是稀少，$R$R似乎大部分的怪物$R都集中在洞穴的最底层。", "暗鸣");
        }
    }
}


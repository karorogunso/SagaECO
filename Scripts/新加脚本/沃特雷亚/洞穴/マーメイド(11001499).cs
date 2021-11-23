using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21003000
{
    public class S11001499 : Event
    {
        public S11001499()
        {
            this.EventID = 11001499;
        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 131, "……。$R;" +
            "（偽裝一下沒有壞處、實際上$R;" +
            "我們以人魚的肉為目標$R;" +
            "怎麼了……？）$R;" +
            "$R（不能大意呢。）$R;", "美人魚");
        }


    }
}



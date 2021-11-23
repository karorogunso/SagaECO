using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30160000
{
    public class S11000241 : Event
    {
        public S11000241()
        {
            this.EventID = 11000241;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "女王陛下是偉大的，$R;" +
                "$R為了保護女王陛下，$R;" +
                "我們會置生死於度外的呀$R;");
        }
    }
}
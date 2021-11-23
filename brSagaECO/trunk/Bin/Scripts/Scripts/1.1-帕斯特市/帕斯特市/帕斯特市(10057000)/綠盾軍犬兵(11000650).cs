using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000650 : Event
    {
        public S11000650()
        {
            this.EventID = 11000650;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "我是軍犬兵！$R;" +
                "$R天天訓練那些狗狗們$R;" +
                "使他們能變成一隊活耀的軍犬$R;");
        }
    }
}
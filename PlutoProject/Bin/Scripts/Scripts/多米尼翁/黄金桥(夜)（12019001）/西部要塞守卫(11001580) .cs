using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12035200
{
    public class S11001580 : Event
    {
        public S11001580()
        {
            this.EventID = 11001580;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11001580, 65535, "喂$R;" +
                                    "你,如果不是执行特殊任务赶快进来避难$R;" +
                                    "外面非常危险！$R;", "西部要塞守卫");
            switch (Select(pc, "回去吗?", "", "回去","还是算了"))
            {
               
                case 1:
                    Warp(pc, 12021000, 209, 127);
                    break;
                case 2:
                    break;

            }
        }
    }
}
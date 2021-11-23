using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001126 : Event
    {
        public S11001126()
        {
            this.EventID = 11001126;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Marionette != null)
            {
                Say(pc, 255, "皮诺！！$R;" +
                    "钟声好烦啊，能不能停止呀？$R;");
                Say(pc, 11001123, 255, "呀呀……$R;" +
                    "钟声停止，我也会停止的。$R;");
                Say(pc, 255, "是吗？！$R;");
                return;
            }
            Say(pc, 255, "训练中$R;");
        }
    }
}
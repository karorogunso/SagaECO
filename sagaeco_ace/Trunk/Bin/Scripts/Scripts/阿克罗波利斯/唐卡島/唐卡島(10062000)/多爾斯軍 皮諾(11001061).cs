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
    public class S11001061 : Event
    {
        public S11001061()
        {
            this.EventID = 11001061;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Marionette != null)
            {
                Say(pc, 131, "哦，辛苦了！$R;");
                return;
            }
            Say(pc, 131, "这里是多尔斯军的本部！$R;");
        }
    }
}
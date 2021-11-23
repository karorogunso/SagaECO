using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M30020001
{
    public class S11000334 : Event
    {
        public S11000334()
        {
            this.EventID = 11000334;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000434, 131, "他的名字是杜利!$R;" +
                "$R是這些家伙們的哥哥$R;" +
                "是富人情味的家伙$R;");
            Say(pc, 111, "汪汪!$R;");
        }
    }
}
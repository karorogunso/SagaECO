using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30102002
{
    public class S11001223 : Event
    {
        public S11001223()
        {
            this.EventID = 11001223;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11001223, 131, "啊啊！$R;" +
                pc.Name + "?$R;" +
                "$R好久不见！！你过的好吗??$R;" +
                "$R找玛莎?$R;" +
                "$R啊…玛莎跟长官正在谈话可能会很久$R好像要等一段时间才可以…$R;");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30102001
{
    public class S11001090 : Event
    {
        public S11001090()
        {
            this.EventID = 11001090;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11001090, 131, "谢谢$R;" +
                "$R两家人（？）团聚都是您的功劳$R;");
        }
    }
}
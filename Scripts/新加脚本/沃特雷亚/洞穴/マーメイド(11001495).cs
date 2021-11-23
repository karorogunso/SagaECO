using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21003000
{
    public class S11001495 : Event
    {
        public S11001495()
        {
            this.EventID = 11001495;
        }

        public override void OnEvent(ActorPC pc)
        {


            Say(pc, 132, "從塔那邊來的人們$R;" +
            "和這個世界的人們$R;" +
            "不一樣呢？$R;", "美人魚");

            Say(pc, 11001494, 132, "那邊的世界$R;" +
            "難道也有美人魚么？$R;", "美人魚");

            Say(pc, 364, "好擔心呢。$R;", "美人魚");

            Say(pc, 11001494, 364, "好擔心。$R;", "美人魚");
        }


    }
}



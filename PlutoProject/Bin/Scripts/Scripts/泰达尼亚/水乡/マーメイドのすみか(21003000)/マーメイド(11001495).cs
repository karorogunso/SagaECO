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


            Say(pc, 132, "从塔那边来的人们$R;" +
            "和这个世界的人们$R;" +
            "不一样呢？$R;", "美人鱼");

            Say(pc, 11001494, 132, "那边的世界$R;" +
            "难道也有美人鱼么？$R;", "美人鱼");

            Say(pc, 364, "好担心呢。$R;", "美人鱼");

            Say(pc, 11001494, 364, "好担心。$R;", "美人鱼");
        }


    }
}



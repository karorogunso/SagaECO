using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30131002
{
    public class S11000807 : Event
    {
        public S11000807()
        {
            this.EventID = 11000807;
        }

        public override void OnEvent(ActorPC pc)
        {
            int a = Global.Random.Next(1, 2);
            if (a == 1)
            {
                Say(pc, 255, "老大，给我缝缝这破掉的地方吧。$R;" +
                    "好…好…$R;");
                return;
            }
            Say(pc, 255, "老大，给我换换里面的棉花吧。$R;" +
                "好…好…$R;");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20146000
{
    public class S11000876 : Event
    {
        public S11000876()
        {
            this.EventID = 11000876;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "回摩戈市是吗？$R;");
            switch (Select(pc, "怎么办呢？", "", "回去", "不回去"))
            {
                case 1:
                    Say(pc, 131, "看看天吧$R;" +
                        "风不大吧？$R;" +
                        "$R现在可以出发了$R;" +
                        "来，快乘坐飞空庭吧$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}
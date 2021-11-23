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
            Say(pc, 131, "回摩根市是嗎？$R;");
            switch (Select(pc, "怎麼辦呢？", "", "回去", "不回去"))
            {
                case 1:
                    Say(pc, 131, "看看天吧$R;" +
                        "風不大吧？$R;" +
                        "$R現在可以出發了$R;" +
                        "來，快乘坐飛空庭吧$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}
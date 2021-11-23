using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20003000
{
    public class S11000103 : Event
    {
        public S11000103()
        {
            this.EventID = 11000103;
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;
            selection = Global.Random.Next(1, 2);
            switch (selection)
            {
                case 1:
                    Say(pc, 131, "『大陸的洞窟』下面$R;" +
                        "聽説有隱藏的地下空間$R;" +
                        "我也想去看看$R;");
                    break;
                case 2:
                    Say(pc, 131, "在這裡的妖怪也$R;" +
                        "都厭倦了$R;" +
                        "更強的妖怪$R;" +
                        "沒有嗎?$R;");
                    break;
            }
        }
    }
}

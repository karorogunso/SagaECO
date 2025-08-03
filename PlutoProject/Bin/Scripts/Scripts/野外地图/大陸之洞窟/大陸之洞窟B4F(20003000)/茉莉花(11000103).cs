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
                    Say(pc, 131, "『大陆的洞窟』下面$R;" +
                        "听说有隐藏的地下空间$R;" +
                        "我也想去看看$R;");
                    break;
                case 2:
                    Say(pc, 131, "已经厌倦了这里$R;" +
                        "的魔物$R;" +
                        "没有更强的$R;" +
                        "魔物吗?$R;");
                    break;
            }
        }
    }
}

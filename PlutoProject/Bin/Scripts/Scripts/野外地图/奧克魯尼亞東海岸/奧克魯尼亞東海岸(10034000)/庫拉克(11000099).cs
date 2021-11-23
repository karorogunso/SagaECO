using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10034000
{
    public class S11000099 : Event
    {
        public S11000099()
        {
            this.EventID = 11000099;
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;
            selection = Global.Random.Next(1, 2);
            switch (selection)
            {
                case 1:
                    Say(pc, 131, "这里是勇敢无惧的冒险家们$R;" +
                         "聚集的『大陆的洞窟』!$R;");
                    break;
                case 2:
                    Say(pc, 131, "入口处很可能会有$R;" +
                        "『鬼火』和『吸血蝙蝠』的$R;" +
                        "$P虽然有着可爱的脸庞、无害的笑容$R;" +
                        "但却是相当粗暴和凶狠的魔物$R;" +
                        "一定小心点!$R;");
                    break;
            }
        }
    }
}

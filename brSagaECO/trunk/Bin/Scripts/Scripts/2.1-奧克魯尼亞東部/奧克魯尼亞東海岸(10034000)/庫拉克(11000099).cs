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
                    Say(pc, 131, "這裡是勇敢無懼的冒險家們$R;" +
                         "聚集的『大陸的洞窟』!$R;");
                    break;
                case 2:
                    Say(pc, 131, "入口處很可能會有$R;" +
                        "『小幽浮』和『德拉奇』的$R;" +
                        "$P雖然有著可愛的臉龐、無害的笑容$R;" +
                        "但卻是相當粗暴和凶狠的魔物$R;" +
                        "一定小心點!$R;");
                    break;
            }
        }
    }
}

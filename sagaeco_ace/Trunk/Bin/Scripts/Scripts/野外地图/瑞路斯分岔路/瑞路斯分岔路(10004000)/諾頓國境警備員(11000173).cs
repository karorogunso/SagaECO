using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10004000
{
    public class S11000173 : Event
    {
        public S11000173()
        {
            this.EventID = 11000173;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "想知道关于什么呢?", "", "北方海角", "冰洁的坑道", "没什么可问的"))
            {
                case 1:
                    Say(pc, 131, "从这边一直往北走就是『北方海角』$R;" +
                        "$R有通往『诺森王国』的巨大的桥$R;" +
                        "$P桥的周边是村庄$R;" +
                        "所以可以在那边休息$R;" +
                        "$R会很冷的用暖和的服装$R;" +
                        "去比较好!!$R;");
                    break;
                case 2:
                    Say(pc, 131, "西边有被叫为『冰洁的坑道』的地牢$R;" +
                        "$R如果不想死的话$R;" +
                        "不要去那里是上策!$R;");
                    break;
                case 3:
                    break;
            }
        }
    }
}

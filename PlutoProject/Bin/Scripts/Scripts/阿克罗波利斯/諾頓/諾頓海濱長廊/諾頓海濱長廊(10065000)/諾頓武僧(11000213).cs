using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10065000
{
    public class S11000213 : Event
    {
        public S11000213()
        {
            this.EventID = 11000213;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "这里是女王陛下居住的$R;" +
                "『诺顿宫殿』$R;");
        }

        void 万圣节(ActorPC pc)
        {

            //EVT1100021301
            //GUIDE OFF
            //SWITCH START

            //,,FLAG ON 0b25 EVT1100021300,
            //,,FLAG ON 0b23 EVT1100021307,
            //,,FLAG ON 0b22 EVT1100021306,
            //,,FLAG ON 0b21 EVT1100021305,
            //,,FLAG ON 0b20 EVT1100021304,
            //,,FLAG ON 0b19 EVT1100021303,
            //,,FLAG ON 0b18 EVT1100021302,
            //SWITCH END
            //GOTO EVT1100021300
            //EVENTEND
            //EVT1100021302
            Say(pc, 131, "女王陛下吩咐您去$R;" +
                "魔法行会总部是吗？$R;" +
                "$P魔法行会总部就在前面$R;" +
                "$R我用箭头指路，$R;" +
                "跟著去就可以了$R;" +
                "$P如果不清楚再问问吧$R;");
            //GUIDE ON 42 67
            //EVENTEND
            //EVT1100021303
            Say(pc, 131, "啊？什么？$R;" +
                "这次调查关于『巴路基石』$R;" +
                "去魔法商店是吗？$R;" +
                "$R魔法商店位于商人地区$R;" +
                "我给您指路吧$R;");
            //GUIDE ON 66 152
            //EVENTEND
            //EVT1100021304
            Say(pc, 131, "熟悉诺森市的人是吗？$R;" +
                "$R啊！有呀$R;" +
                "有一个小子喜欢装做$R;" +
                "熟悉很多传闻或内情阿$R;" +
                "$P可能在桥的附近$R;" +
                "不知道今天在不在$R;" +
                "去找一找吧$R;");
            //EVENTEND
            //EVT1100021305
            Say(pc, 131, "酒馆？$R;" +
                "商人区里有两间酒馆$R;" +
                "给您介绍著名的地方吧$R;");
            //GUIDE ON 66 186
            //EVENTEND
            //EVT1100021306
            Say(pc, 131, "到我们诺森王国军来，$R有什么事情要办吗？$R;" +
                "$R我现在是执勤时间$R;" +
                "请您到本部吧$R;" +
                "$P本部就在附近$R;" +
                "入口处有武僧站岗$R;" +
                "很快就能找到的$R;" +
                "$R西边是男武僧本部$R;" +
                "东边是女武僧本部喔$R;");
            //if (ME.SEX = 1)

            {
                //Call(EVT1100021308);
                return;
            }
            //GUIDE ON 70 5
            //EVENTEND
            //EVT1100021307
            //Say(pc, 131, "事情辦的怎麼樣呢？$R;");
            //EVENTEND
            //EVT1100021308
            //GUIDE ON 33 5
            //EVENTEND
        }
    }
}
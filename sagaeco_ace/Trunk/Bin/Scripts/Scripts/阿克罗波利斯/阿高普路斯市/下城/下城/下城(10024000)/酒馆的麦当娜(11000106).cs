using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:酒馆的麦当娜(11000106) X:200 Y:124
namespace SagaScript.M10024000
{
    public class S11000106 : Event
    {
        public S11000106()
        {
            this.EventID = 11000106;

            this.questTransportSource = "你说帮我搬运吗?$R;" +
                "真的很着急的！拜托了!;";
            this.questTransportDest = "不知道等了多久呢!$R;" +
                "辛苦了;";
            this.questTransportCompleteSrc = "把行李运过去了啊!$R;" +
                "真是感谢!$R;" +
                "报酬请到任务服务台领取;";
            this.questTransportCompleteDest = "辛苦了;";
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000106, 131, "欢迎光临!$R;" +
                                   "$R你好好休息。$R", "酒馆的麦当娜");
        }
    }
}

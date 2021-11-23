using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:卡洛琳奈(11000108) X:72 Y:76
namespace SagaScript.M10024000
{
    public class S11000108 : Event
    {
        public S11000108()
        {
            this.EventID = 11000108;
            this.questTransportSource = "被委托的人是你吗?$R;" +
                    "一把老骨头走不动了,拜托了...;";
            this.questTransportDest = "希望不要过了点!$R;" +
                "辛苦了;";
            this.questTransportCompleteSrc = "谢谢你替我做这么重要的事...$R;" +
                "非常感谢...$R;" +
                "报酬已经送去酒馆了...;";
            this.questTransportCompleteDest = "辛苦了;";
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}

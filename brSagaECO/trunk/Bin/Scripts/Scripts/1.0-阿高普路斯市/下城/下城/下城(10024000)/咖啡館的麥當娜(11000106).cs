using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:咖啡館的麥當娜(11000106) X:200 Y:124
namespace SagaScript.M10024000
{
    public class S11000106 : Event
    {
        public S11000106()
        {
            this.EventID = 11000106;

            this.questTransportSource = "你說幫我搬運嗎?$R;" +
                "真的很著急的！拜託了!;";
            this.questTransportDest = "不知道等了多久呢!$R;" +
                "辛苦了;";
            this.questTransportCompleteSrc = "把行李運過去了啊!$R;" +
                "真是感謝!$R;" +
                "報酬請到任務服務台領取;";
            this.questTransportCompleteDest = "辛苦了;";
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000106, 131, "歡迎光臨!$R;" +
                                   "$R你好好休息。$R", "咖啡館的麥當娜");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30151000
{
    public class S11001298 : Event
    {
        public S11001298()
        {
            this.EventID = 11001298;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*
            if (//ME.JOB > 100
                //ME.JOB < 111
            )
            {
                Say(pc, 190, pc.Name + "、よく來たな。$R;");
                switch (Select(pc, "何をする？", "", "クエストカウンター", "何もしない"))
                {
                    case 1:
                        GOTO EVT1100129801;
                        break;
                    case 2:
                        GOTO EVT1100065699;
                        break;
                }
                return;
            }//*/
            Say(pc, 11001296, 190, "レンジャーマスターって$R;" +
                "どうやって決めているんだ？$R;");
            Say(pc, 11001298, 190, "うちは、指名製だ。$R;" +
                "現マスターが退任するときに$R;" +
                "後任のマスターを指名するんだ。$R;");
            Say(pc, 11001296, 190, "へぇ～、なんだか普通だな。$R;");
            Say(pc, 11001298, 190, "まぁな。$R;");
        }
    }
}
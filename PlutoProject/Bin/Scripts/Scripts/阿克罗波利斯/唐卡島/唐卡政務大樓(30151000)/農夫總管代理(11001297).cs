using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30151000
{
    public class S11001297 : Event
    {
        public S11001297()
        {
            this.EventID = 11001297;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*
            if (//ME.JOB > 90
                //ME.JOB < 101
            )
            {
                Say(pc, 190, pc.Name + "さ～ん～。$R;" +
                    "どうしました～？$R;");
                switch (Select(pc, "何をする？", "", "クエストカウンター", "何もしない"))
                {
                    case 1:
                        GOTO EVT1100129701;
                        break;
                    case 2:
                        GOTO EVT1100065699;
                        break;
                }
                return;
            }//*/
            Say(pc, 11001298, 190, "ファーマーマスターって$R;" +
                "どうやって決めているんだい？$R;");
            Say(pc, 11001297, 190, "うちは～、持ち回りの當番製で～す～。$R;");
            Say(pc, 11001296, 190, "當番製？$R;");
            Say(pc, 11001297, 190, "ほ～ら～。$R;" +
                "$R私たち～、農家だから～。$R;" +
                "あんまり～、畑を～$R;" +
                "放置することは～出來ないんで～。$R;");
            Say(pc, 190, "でも、私。$R;" +
                "あの、のんびりしたマスターさんしか$R;" +
                "見たこと無いんだけど……。$R;");
            Say(pc, 190, "それは～、たまたまですよ～。$R;" +
                "た～ま～た～ま～。$R;" +
                "$Rうふふふふ～$R;");
        }
    }
}
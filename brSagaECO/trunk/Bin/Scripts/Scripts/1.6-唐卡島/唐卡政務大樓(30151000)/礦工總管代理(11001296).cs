using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30151000
{
    public class S11001296 : Event
    {
        public S11001296()
        {
            this.EventID = 11001296;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*
            if (//ME.JOB > 80
                //ME.JOB < 91
            )
            {
                Say(pc, 190,pc.Name +  "じゃんか！$R;" +
                    "どうしたんだ！？$R;");
                switch (Select(pc, "何をする？", "", "クエストカウンター", "何もしない"))
                {
                    case 1:
                        GOTO EVT1100129601;
                        break;
                    case 2:
                        GOTO EVT1100065699;
                        break;
                }
                return;
            }//*/
            Say(pc, 11001299, 190, "タタラベマスターって$R;" +
                "どうやって決めているの？$R;");
            Say(pc, 11001296, 190, "ん、うちはな$R;" +
                "「ＤＯＧＥＺＡ合戦」で決めるぞ！$R;");
            Say(pc, 11001299, 190, "ＤＯＧＥＺＡ合戦～？$R;" +
                "そんなんで、勝負が決まるの？$R;");
            Say(pc, 190, "何言ってるんだ！$R;" +
                "かなり、熱く激しい戦いだぞ！？$R;" +
                "$Pお互い、力を振り絞って$R;" +
                "気絶するまでＤＯＧＥＺＡをするんだ！$R;" +
                "$Pそうすることによって$R;" +
                "他のタタラベに、根性と熱意と$R;" +
                "岩に対する愛情をアピールするんだ！$R;");
            Say(pc, 11001296, 190, "タタラベって、すごいね……。$R;");
        }
    }
}
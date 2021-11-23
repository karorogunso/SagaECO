using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30151000
{
    public class S11001299 : Event
    {
        public S11001299()
        {
            this.EventID = 11001299;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*
            if (//ME.JOB > 110
            )
            {
                Say(pc, 190, pc.Name + "りん、ようこそ$R;");
                switch (Select(pc, "何をする？", "", "クエストカウンター", "何もしない"))
                {
                    case 1:
                        GOTO EVT1100129901;
                        break;
                    case 2:
                        GOTO EVT1100065699;
                        break;
                }
                return;
            }//*/
            Say(pc, 11001297, 190, "マーチャントマスターって$R;" +
                "どうやって決めているんですの～？$R;");
            Say(pc, 11001299, 190, "……うちは選挙をするの。$R;");
            Say(pc, 11001298, 190, "選挙？$R;");
            Say(pc, 11001299, 190, "マスターの任期は、最低３年。$R;" +
                "$R３年ごとにマーチャントギルド$R;" +
                "運営委員會内で$R;" +
                "立候補者を募集して、選挙をするの。$R;");
            Say(pc, 11001296, 190, "な、なんか難しそうだな。$R;");
            Say(pc, 11001299, 190, "そぉ～ね～。$R;" +
                "なるためにはルーランさんや$R;" +
                "他のギルドマスター。$R;" +
                "それに伝說のマーチャント$R;" +
                "ゴルドー氏の支持が必要だし。$R;" +
                "$Pそのためには、手を血に染める…$R;");
            Say(pc, 11001296, 190, "わっ、もういい！$R;" +
                "もういいから、やめてくれ！$R;");
            Say(pc, 11001299, 190, "ふっふっふ……。$R;" +
                "次のマスターの座は、私よ……。$R;");
        }
    }
}
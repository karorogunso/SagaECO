using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20070104
{
    public class S11001382 : Event
    {
        public S11001382()
        {
            this.EventID = 11001382;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 134, "もう無理だろ！時間ねぇーよ！$R;" +
            "どどど、どうしよう！？$R;", "アーク");
            if (Select(pc, "どうする？", "", "無視する", "励ます") == 2)
            {
                Say(pc, 134, "あんた誰だよ！？$R;" +
                "ぐわぁ～もうダメかもしれん！$R;", "アーク");
            }

        }
    }
}
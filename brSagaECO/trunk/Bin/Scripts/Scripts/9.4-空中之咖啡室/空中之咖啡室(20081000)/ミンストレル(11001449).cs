using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M11001448
{
    public class S11001449 : Event
    {
        public S11001449()
        {
            this.EventID = 11001449;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 65535, "" + pc.Name + "。$R;" +
            "私のギターは、あなたのように$R;" +
            "美しい人のためにあります。$R;" +
            "$Rさあ、何でもリクエストしてください。$R;" +
            "心を込めて弾きましょう。$R;", "ミンストレル");
            Select(pc, "リクエストする？", "", "Crispy winey early spring", "blossom shower", "Fireworks!", "The Laughing Man", "Holy day, Holy night", "元の曲に戻す", "しない");

        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10069001
{
    public class S13000288 : Event
    {
        public S13000288()
        {
            this.EventID = 13000288;
        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 13000287, 131, "まったくも〜$R;" +
            "世話が焼けるんだから。$R;", "サクヤ");

            Say(pc, 153, "ほんっとにゴメン！$R;", "バルド");

            Say(pc, 13000287, 131, "むぅ〜。$R;" +
            "ちゃんと反省してる？$R;", "サクヤ");

            Say(pc, 153, "してますしてます！$R;" +
            "$R神様仏様サクヤ様$R;" +
            "どうかご慈悲を！！！$R;", "バルド");
            ShowEffect(pc, 13000287, 4506);

            Say(pc, 13000287, 131, "しかたないなぁ。$R;" +
            "今回だけだからね！$R;" +
            "$P今回だけは課題の失敗$R;" +
            "見逃してあげるけど$R;" +
            "次からはないんだからね！$R;", "サクヤ");

            Say(pc, 153, "ハハァ〜ッ！！！$R;" +
            "$Rそれにしても、優秀な幼馴染が$R;" +
            "監督生になってくれたおかげで$R;" +
            "いろいろ助かるぜ……。$R;", "バルド");
            ShowEffect(pc, 13000287, 4516);

            Say(pc, 13000287, 131, "ん？$R;" +
            "何か言った？$R;", "サクヤ");

            Say(pc, 153, "い、いえ$R;" +
            "滅相もございません！！$R;", "バルド");
        }
    }
}
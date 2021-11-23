using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10024000
{
    public class S18000242 : Event
    {
        public S18000242()
        {
            this.EventID = 18000242;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "『うふふ……アレス$R;" +
            "　今日も遅刻ね？　』$R;", "ルーナのコスプレをした女の子");

            Say(pc, 111, "…………$R;", "ルーナのコスプレをした女の子");

            Say(pc, 134, "ち、ちがうかなぁ……$R;" +
            "ルーナの声はもうちょっとこう$R;" +
            "清楚なかんじで……$R;" +
            "$Pあ……わ～$R;" +
            "恥ずかしいところ$R;" +
            "見られちゃった？？$R;" +
            "$Pこの衣装はね……$R;" +
            "$R「ルナ ～ハーモニー$R;" +
            "　　　　　オブ シルバースター～」$R;" +
            "$R……っていうゲームに出てくる$R;" +
            "「ルーナ」って女の子の$R;" +
            "衣装なの！$R;" +
            "$Pがんばって作ったんだ～♪$R;" +
            "わたし、ルーナみたいに$R;" +
            "なってみたいんだもの！$R;" +
            "$Pいま、そこのシアター３で$R;" +
            "プロモーションビデオを$R;" +
            "観ることができるのよ♪$R;" +
            "$Pルーナがどんな子か、$R;" +
            "あなたも観てきてくれたら$R;" +
            "嬉しいな～$R;" +
            "$P……あ！$R;" +
            "プロモーションビデオの最後には$R;" +
            "とっておきの情報もあるのよ？$R;" +
            "$P最後までちゃんと観てもらえたら$R;" +
            "嬉しいな～$R;", "ルーナのコスプレをした女の子");
        }
    }
}
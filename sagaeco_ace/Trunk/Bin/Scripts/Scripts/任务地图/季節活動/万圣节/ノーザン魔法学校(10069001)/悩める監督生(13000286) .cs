using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10069001
{
    public class S13000286 : Event
    {
        public S13000286()
        {
            this.EventID = 13000286;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 112, "……はぁ。$R;" +
            "$Rソーウェン祭の終わりに$R;" +
            "告白しようと思ってるんだけど$R;" +
            "勇気が出なくて。$R;", "悩める監督生");
            if (Select(pc, "どうする？", "", "何もしない", "応援する") == 2)
            {
                Say(pc, 0, 131, "大丈夫！$R;" +
                "普通に想いを伝えれば$R;" +
                "きっと上手く行きますよ！！$R;", " ");

                Say(pc, 112, "……普通に、か。$R;" +
                "$P……。$R;" +
                "$Pう〜ん。$R;" +
                "やっぱり無理な気がするなぁ。$R;" +
                "$P普通に考えて$R;" +
                "僕を鞭で弄って下さいなんて$R;" +
                "やっぱり断られるよなぁ……。$R;", "悩める監督生");

                Say(pc, 0, 131, "………。$R;" +
                "$P……。$R;" +
                "$P…。$R;" +
                "$P……あ、え、うん。$R;" +
                "$R人にはいろんな趣味$R;" +
                "ありますよね？$R;", " ");

                Say(pc, 112, "ココはやっぱり$R;" +
                "「オーバーニーで踏みつけて♪」$R;" +
                "くらいにしたほうがいいかな？$R;", "悩める監督生");
                ShowEffect(pc, 4508);

                Say(pc, 0, 131, "……。$R;", " ");
            }
        }
    }
}
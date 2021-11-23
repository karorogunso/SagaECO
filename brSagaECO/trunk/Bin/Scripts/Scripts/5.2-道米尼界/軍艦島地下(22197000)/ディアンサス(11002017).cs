using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M22197000
{
    public class S11002017 : Event
    {
        public S11002017()
        {
            this.EventID = 11002017;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11002016, 131, "那個～$R;" +
            "在這之後會變成甚樣呢？$R;", "ミーナ");

            Say(pc, 131, "嚇～。$R;" +
            "你有聽到甚麼嗎！？$R;", "ディアンサス");

            Say(pc, 11002016, 131, "對不起～。$R;", "ミーナ");

            Say(pc, 131, "可以嗎……（喧鬧）$R;", "ディアンサス");

            Say(pc, 11002016, 131, "嚇$R;" +
            "也就是說……。$R;", "ミーナ");

            Say(pc, 131, "唔唔！$R;", "ディアンサス");

            Say(pc, 11002016, 131, "………。$R;" +
            "$P……。$R;" +
            "$P…。$R;" +
            "$P還不是很清楚～。$R;", "ミーナ");

            Say(pc, 131, "（昏倒）$R;", "ディアンサス");
            //
            /*
            Say(pc, 11002016, 131, "あの～$R;" +
            "これからどうなるんでしょう？$R;", "ミーナ");

            Say(pc, 131, "はぁ～。$R;" +
            "あなたは何を聞いていたんですか！？$R;", "ディアンサス");

            Say(pc, 11002016, 131, "すみません～。$R;", "ミーナ");

            Say(pc, 131, "いいですか……（クドクド）$R;", "ディアンサス");

            Say(pc, 11002016, 131, "はぁ$R;" +
            "つまり……。$R;", "ミーナ");

            Say(pc, 131, "うんうん！$R;", "ディアンサス");

            Say(pc, 11002016, 131, "………。$R;" +
            "$P……。$R;" +
            "$P…。$R;" +
            "$Pよくわかりません～。$R;", "ミーナ");

            Say(pc, 131, "（ガクッ）$R;", "ディアンサス");
            */
        }
    }
}


        
   



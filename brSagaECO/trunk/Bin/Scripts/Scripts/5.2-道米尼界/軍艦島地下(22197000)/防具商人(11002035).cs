using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M22197000
{
    public class S11002035 : Event
    {
        public S11002035()
        {
            this.EventID = 11002035;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "いらっしゃい！$R;" +
            "$R実用防具から横流し品まで$R;" +
            "自慢ではございませんが品揃えには$R;" +
            "ちょっとした自信が有りますぞ！$R;", "防具商人");
            switch (Select(pc, "いかがいたしましょう？", "", "防具を買う", "横流しの防具を買う", "物を売る", "何もしない"))
            {
                case 1:
                    OpenShopBuy(pc, 290);
                    Say(pc, 131, "またのごひいき$R;" +
                    "お待ちしておりますぞ。$R;", "防具商人");
                    return;
                case 2:
                    Say(pc, 131, "……ふむぅ。$R;" +
                    "横流し品……ですか。$R;" +
                    "$Pいえいえ、お売りしますよ？$R;" +
                    "ですが、どなたにも気軽にという訳には$R;" +
                    "行かない品ですので……。$R;", "防具商人");

                    Say(pc, 131, "そうですなぁ。$R;" +
                    "$Pこの奥にデムスライトという鉱石が$R;" +
                    "あると聞きます。$R;" +
                    "$Rその破片『デムスライトの破片』を$R;" +
                    "１０個ほどご提供いただけましたら$R;" +
                    "以後、横流し品のお取引をする$R;" +
                    "という条件は如何ですかな？$R;", "防具商人");
                    return;
                case 3:
                    OpenShopSell(pc, 290);
                    Say(pc, 131, "ぜひまたおこし下さい。$R;", "雑貨商人");
                    return;
            }
        }
    }
}


        
   



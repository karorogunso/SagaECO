using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M22197000
{
    public class S11002036 : Event
    {
        public S11002036()
        {
            this.EventID = 11002036;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "いらっしゃいませ。$R;" +
            "$Rいやはやエミルの商人は$R;" +
            "商魂たくましいですなぁ。$R;" +
            "さて、私も負けてはいられません。$R;", "雑貨商人");
            switch (Select(pc, "見て損はさせませんよ！", "", "雑貨を買う", "補給雑貨を買う", "物を売る", "何もしない"))
            {
                case 1:
                    OpenShopBuy(pc, 291);
                    return;
                case 2:
                    Say(pc, 131, "まことに恐縮なのですが$R;" +
                    "まだまだ物資の供給が不安定でして……$R;" +
                    "$P補給雑貨に関しては、対象者を限定$R;" +
                    "させて頂いております。$R;", "雑貨商人");

                    Say(pc, 131, "入手方法は問いませんが$R;" +
                    "『大きなボルト』を１０本ほど$R;" +
                    "ご用意いただければ$R;" +
                    "こちらも勉強させていただきますよ。$R;", "雑貨商人");
                    return;
                case 3:
                    OpenShopSell(pc, 291);
                    return;

            }

        }
    }
}


        
   



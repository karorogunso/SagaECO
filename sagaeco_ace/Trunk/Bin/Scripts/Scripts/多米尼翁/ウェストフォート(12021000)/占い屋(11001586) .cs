using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001586 : Event
    {
        public S11001586()
        {
            this.EventID = 11001586;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.WRPRanking <= 10)
            {
                Say(pc, 131, "ＷＲＰランキング上位者ですね？$R;" +
                "$Pあなた様には$R;" +
                "特別な商品をご用意しておりますよ。$R;", "商人");
                switch (Select(pc, "御用がおありですか？", "", "買い物をする（ランキング上位者用）", "買い物をする", "物を売る", "銀行にお金を預ける", "銀行からお金を引き出す", "倉庫を使う（利用料１００ゴールド）", "何もしない"))
                {
                    case 1:
                        OpenShopBuy(pc, 276);
                        break;
                    case 2:
                        OpenShopBuy(pc, 280);
                        break;
                    case 3:
                        OpenShopSell(pc, 280);
                        break;
                    case 4:
                        Say(pc, 131, "よろしい$R;" +
                        "あなたの評判を占ってみましょう……。$R;" +
                        "$Pただし、それ相応の報酬……。$R;" +
                        "$R100ゴールド$R;" +
                        "支払って頂くことになるけど$R;" +
                        "それでもいいかしら？$R;", "占い屋");
                        Select(pc, "一回100ゴールドかかるけど……？", "", "やめておきます。", "お願いします！");
                        break;
                }
                Say(pc, 131, "ふふふ……、満足してくれた？$R;" +
                "また、来なさい。$R;", "占い屋");
                return;
            }
            switch (Select(pc, "ふふふ、なにか御用かしら？", "", "買い物をする", "物を売る", "自分の評判を知りたい", "何もしない"))
            {
                case 1:
                    OpenShopBuy(pc, 280);
                    break;
                case 2:
                    OpenShopSell(pc, 280);
                    break;
                case 3:
                    Say(pc, 131, "よろしい$R;" +
                    "あなたの評判を占ってみましょう……。$R;" +
                    "$Pただし、それ相応の報酬……。$R;" +
                    "$R100ゴールド$R;" +
                    "支払って頂くことになるけど$R;" +
                    "それでもいいかしら？$R;", "占い屋");
                    Select(pc, "一回100ゴールドかかるけど……？", "", "やめておきます。", "お願いします！");
                    break;
            }
           }

           }
                        
                }
            
            
        
     
    
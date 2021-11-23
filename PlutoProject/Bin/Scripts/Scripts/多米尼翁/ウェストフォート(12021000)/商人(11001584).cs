using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001584 : Event
    {
        public S11001584()
        {
            this.EventID = 11001584;
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
                        OpenShopBuy(pc, 275);

                        break;
                    case 2:
                        OpenShopBuy(pc, 278);

                        break;
                    case 3:
                        OpenShopSell(pc, 278);
                        break;
                    case 4:
                        BankDeposit(pc);
                        break;

                    case 5:
                        BankWithdraw(pc);
                        break;
                }
                return;
            }
            switch (Select(pc, "御用がおありですか？", "", "買い物をする", "物を売る", "銀行にお金を預ける", "銀行からお金を引き出す", "倉庫を使う（利用料５００ゴールド）", "何もしない"))
            {
                case 1:
                    OpenShopBuy(pc, 278);

                    break;
                case 2:
                    OpenShopSell(pc, 278);
                    break;
                case 3:
                    BankDeposit(pc);
                    break;

                case 4:
                    BankWithdraw(pc);
                    break;
            }
        }

           }
                        
                }
            
            
        
     
    
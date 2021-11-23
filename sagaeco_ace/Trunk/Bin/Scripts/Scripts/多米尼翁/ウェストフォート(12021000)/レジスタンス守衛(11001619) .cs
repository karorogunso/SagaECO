using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001619 : Event
    {
        public S11001619()
        {
            this.EventID = 11001619;
        }

        public override void OnEvent(ActorPC pc)
        {
          Say(pc, 131, "ここはレジスタンス本部だ。$R;", "レジスタンス守衛");
          switch (Select(pc, "私になにか用かね？", "", "報酬アイテムが見たい","ＷＲＰとは？","やめる"))
          {
          	case 1:
                switch (Select(pc, "CP褒賞アイテム", "", "やめる", "珍しい物が見たい", "装備品が見たい", "薬品が見たい", "素材を見たい"))
                {
                    case 2:
                        OpenShopBuy(pc, 352);
                        break;
                    case 3:
                        OpenShopBuy(pc, 353);
                        break;
                    case 4:
                        OpenShopBuy(pc, 354);
                        break;
                    case 5:
                        OpenShopBuy(pc, 355);
                        break;
                }
          		break;
          	case 2:
          		Say(pc, 131, "$CTＷ$CDａｒ$R;" + 
                 "$CTＲ$CDｅｃｏｒｄ$R;" + 
                 "$CTＰ$CDｏｉｎｔ$R;" + 
                  "通称、ＷＲＰだ。$R;" + 
                  "$Rつまり、ドミニオン界での$R;" + 
                  "戦績ってことだな。$R;", "レジスタンス守衛");

                   Say(pc, 131, "他に何か聞きたいことはあるか？$R;", "レジスタンス守衛");
                   switch (Select(pc, "どうする？", "", "ＷＲＰの入手について聞く", "自分のＷＲＰについて聞く", "お兄さんかっこいいね", "やっぱり聞かない"))
                   {
                       case 1:
                           Say(pc, 131, "ＷＲＰはなＰｖＰで$R;" +
                           "戦って！戦って！戦って！$R;" +
                           "勝った奴に与えられるポイントだ。$R;" +
                           "$R要は、たくさんＷＲＰを$R;" +
                           "持っている奴は強いってことだな。$R;" +
                           "$Pそれとは他にシティへの$R;" +
                           "貢献次第で入手できる$R;" +
                           "“ＣＰ”ってポイントもあるんだ。$R;" +
                           "$Rまぁこの世界のためにも$R;" +
                           "がんばってくれよな。$R;", "レジスタンス守衛");
                           break;
                       case 2:
                           Say(pc, 131, "自分のＷＲＰは常時$R;" +
                           "メインウィンドウ画面に$R;" +
                           "表示されているからな。$R;" +
                           "$Rそれとシティの入り口に$R;" +
                           "ＷＲＰのランキング掲示板が$R;" +
                           "あるから、自分が今どの程度なのか$R;" +
                           "確認できるぞ。$R;" +
                           "$P掲示板に名前が乗るのは$R;" +
                           "上位１００人までだからな？$R;" +
                           "それ以外はランク外ってことだ。$R;" +
                           "$Pトップ１０に入ると$R;" +
                           "何か良いこともあるらしぞ。$R;", "レジスタンス守衛");
                           break;
                       case 3:
                           Say(pc, 131, "お、おう。$R;" +
                           "よせよ、照れるじゃないか。$R;" +
                           "$Rだが俺にはな、心に決めた人が$R;" +
                           "いるんだ、すまねぇな。$R;" +
                           "$PこのＤＥＭとの戦いが終わったら$R;" +
                           "そいつと結婚しようと思うんだ……。$R;" +
                           "……。$R;" +
                           "って、何言ってんだ俺。$R;" +
                           "いや、その、なんだ$R;" +
                           "今のは聞かなかったことにしてくれ。$R;", "レジスタンス守衛");
                           break;

                   }
          		break;
          }
        }
    }
}
            
            
        
     
    
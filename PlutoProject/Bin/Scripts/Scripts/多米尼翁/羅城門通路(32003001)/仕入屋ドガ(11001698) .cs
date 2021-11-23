using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M32003001
{
    public class S11001698 : Event
    {
        public S11001698()
        {
            this.EventID = 11001698;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "あら！かわいいコね♪$R;" +
            "$Rアタシは仕入屋ドガ。$R;" +
            "ドガちゃんって呼んでねン♪$R;" +
            "$R……ねぇ？$R;" +
            "平和ってタイクツだと思わない？$R;" +
            "アタシの「はぁと」を揺さぶるような$R;" +
            "何か楽しい出来事はないのかしら……？$R;", "仕入屋ドガ");
            switch (Select(pc, "何か見ていく？", "", "やめる", "雑貨", "CP報酬アイテム", "掘り出し物", "売りたい", "雑談する"))
            {
                case 2:
                    Say(pc, 131, "いいわよン♪$R;" +
                    "ゆっくり見ていってネ♪$R;", "仕入屋ドガ");
                    OpenShopBuy(pc, 286);
                    Say(pc, 131, "またいらっしゃい♪$R;", "仕入屋ドガ");
                    break;

                case 3:

                    Say(pc, 131, "こっちは「CP」が必要になるから$R;" +
                    "気をつけてネ♪$R;", "仕入屋ドガ");
                    OpenShopBuy(pc, 350);
                    Say(pc, 131, "また会いに来てネ♪$R;", "仕入屋ドガ");
                    break;


                case 4:
                    Say(pc, 131, "あらん？掘り出し物が見たいの？$R;" +
                    "$Rう～ん……、ここにあるものは$R;" +
                    "何に使うかもわからない$R;" +
                    "実用性のない物ばかりよ……。$R;" +
                    "$Rあなたがコ・コ・ロの底から$R;" +
                    "望むのなら譲ってあげても$R;" +
                    "いいんだケド……。$R;", "仕入屋ドガ");
                    OpenShopBuy(pc, 351);
                    Say(pc, 131, "なぜこんなに高いのかって……？$R;" +
                    "$R……実はあたし、詐欺にあったの。$R;" +
                    "$Rこんな訳の分からないものを$R;" +
                    "凄い高値で売りつけられて……。$R;" +
                    "$R……絶対……許さない……。$R;" +
                    "$Pあの野郎ッ、見つけたら$R;" +
                    "ハラワタ引きずり出しッ！！$R;" +
                    "スキンクの餌にしてやるッ！！$R;", "仕入屋ドガ");
                    break;


            }

        }

    }

}
            
            
        
     
    
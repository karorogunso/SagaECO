using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M31300000
{
    public class S11001724 : Event
    {
        public S11001724()
        {
            this.EventID = 11001724;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "ようこそ、ＥＣＯタウンへ！$R;" +
            "$R右手がショップゾーン。$R;" +
            "左手が温泉ゾーン。$R;" +
            "奥がアトラクションゾーンと$Rなっております。$R;", "総合案内");
            switch (Select(pc, "何でもご相談下さい！", "", "別に用はない", "買い物がしたい！", "温泉でゆっくりしたい！", "ミニゲームで遊びたい！"))
            {
                case 2:
                    Say(pc, 131, "お買い物ですね？$R;" +
                    "$Pお買い物でしたら$R;" +
                    "売店が立ち並ぶ$R;" +
                    "ショップゾーンにお立ち寄り下さい。$R;" +
                    "$P温泉に必需品の「のせタオル」や$R;" +
                    "おみやげに最適な$R;" +
                    "「ＥＣＯタウンＴシャツ」などを$R;" +
                    "販売しております。$R;" +
                    "$Pただし、タウン内では$R;" +
                    "タウン専用の硬貨「ecoin」のみの$R;" +
                    "お取り扱いとなり$R;" +
                    "通常の貨幣は$R;" +
                    "使用することが出来ません。$R;" +
                    "$P「ecoin」は$R;" +
                    "「ecoinカウンター」で$R;" +
                    "ゴールドと交換することが出来ます。$R;" +
                    "$R案内いたしますので$R;" +
                    "矢印に従ってお進み下さい。$R;", "総合案内");
                    Navigate(pc, 19, 45);

                    break;
                case 3:
                    Say(pc, 131, "温泉ですね？$R;" +
                    "$P温泉でしたら左手にございます。$R;" +
                    "$R当タウンの温泉は$R;" +
                    "「体力の湯」と呼ばれ$R;" +
                    "健康促進・体力増強に$R;" +
                    "効果があると言われています。$R;" +
                    "$Pくじら山ダンジョンに行く方は$R;" +
                    "ぜひ出発前にご利用下さい。$R;" +
                    "$R案内いたしますので$R;" +
                    "矢印に従ってお進み下さい。$R;", "総合案内");
                    Navigate(pc, 41, 76);
                    break;
                case 4:
                    Say(pc, 131, "ミニゲームですね？$R;" +
                    "$Pミニゲームでしたら$R;" +
                    "奥のアトラクションゾーンで$R;" +
                    "遊ぶことが出来ます。$R;" +
                    "$P遊ぶためには$R;" +
                    "タウン専用の硬貨「ecoin」が$R;" +
                    "必要になります。$R;" +
                    "$P「ecoin」は$R;" +
                    "「ecoinカウンター」で$R;" +
                    "ゴールドと交換することが出来ます。$R;" +
                    "$R案内いたしますので$R;" +
                    "矢印に従ってお進み下さい。$R;", "総合案内");
                    Navigate(pc, 45, 18);
                    break;
            }
        }


    }
}



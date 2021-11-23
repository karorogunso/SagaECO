using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001582 : Event
    {
        public S11001582()
        {
            this.EventID = 11001582;
        }

        public override void OnEvent(ActorPC pc)
        {
        	if (pc.WRPRanking <=10)
        	{
                Say(pc, 131, "ランキング入りするようなヤツ用には$R;" +
                "それなりの商品を用意してるぜ！$R;", "武器屋");
                switch (Select(pc, "ゆっくり見ていってくれよな！", "", "武器を買う（ランキング上位者用）", "武器を買う", "他に武器を買う", "物を売る", "武具強化", "武具強化の注意点", "合成を依頼する", "何もしない"))
                {
                    case 1:
                        OpenShopBuy(pc, 273);
                        break;
                    case 2:
                        OpenShopBuy(pc, 283);
                        break;
                    case 3:
                        OpenShopBuy(pc, 284);
                        break;
                    case 4:
                        OpenShopSell(pc, 283);
                        break;
                    case 5:
                        Say(pc, 131, "手数料として、１回強化するごとに$R;" +
                        "5000ゴールド頂くぜ！$R;" +
                        "$R金は自動的に減るから$R;" +
                        "残金にはご注意しろよ！$R;", "武器屋");
                        //強化
                        break;
                    case 6:
                        Say(pc, 131, "武具強化はアイテムに$R;" +
                        "かなりの負荷をかける。$R;" +
                        "$P１つの装備品に対して$R;" +
                        "最大『10回』まで武具強化を$R;" +
                        "行うことが出来るが$R;" +
                        "強化すればするほど、壊れやすく$R;" +
                        "なるから注意が必要だ！$R;" +
                        "$Pあと、強化中に壊れた場合$R;" +
                        "代金の返却、アイテムの保証は$R;" +
                        "一切ないからそのつもりで$R;" +
                        "宜しく頼む！$R;" +
                        "$Rとはいっても、最初の１回や２回で$R;" +
                        "壊れることなんて$R;" +
                        "めったにないけどな。$R;" +
                        "$Pあ！それから、強化したい武具は$R;" +
                        "装備から外しておく必要があるぜ！$R;", "武器屋");
                        break;
                }
                Say(pc, 131, "あんたのように強い客は大歓迎だ。$R;" +
                "また来てくれよ！$R;", "武器屋");
                return;
        	}
        	
            switch (Select(pc, "ヘイ！いらっしゃい！", "", "武器を買う", "他に武器を買う", "物を売る", "武具強化", "武具強化の注意点", "合成を依頼する", "何もしない"))
            {
                case 1:
                    OpenShopBuy(pc, 283);
                    break;
                case 2:
                    OpenShopBuy(pc, 284);
                    break;
                case 3:
                    OpenShopSell(pc, 283);
                    break;
                case 4:
                    Say(pc, 131, "手数料として、１回強化するごとに$R;" +
                    "5000ゴールド頂くぜ！$R;" +
                    "$R金は自動的に減るから$R;" +
                    "残金にはご注意しろよ！$R;", "武器屋");
                    //強化
                    break;
                case 5:
                    Say(pc, 131, "武具強化はアイテムに$R;" +
                    "かなりの負荷をかける。$R;" +
                    "$P１つの装備品に対して$R;" +
                    "最大『10回』まで武具強化を$R;" +
                    "行うことが出来るが$R;" +
                    "強化すればするほど、壊れやすく$R;" +
                    "なるから注意が必要だ！$R;" +
                    "$Pあと、強化中に壊れた場合$R;" +
                    "代金の返却、アイテムの保証は$R;" +
                    "一切ないからそのつもりで$R;" +
                    "宜しく頼む！$R;" +
                    "$Rとはいっても、最初の１回や２回で$R;" +
                    "壊れることなんて$R;" +
                    "めったにないけどな。$R;" +
                    "$Pあ！それから、強化したい武具は$R;" +
                    "装備から外しておく必要があるぜ！$R;", "武器屋");
                    break;
            }

           }

           }
                        
                }
            
            
        
     
    
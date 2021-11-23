using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaLib;
namespace SagaScript.M20070050
{
    public class S11004402 : Event
    {
        public S11004402()
        {
            this.EventID = 11004402;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*開發中 進度
            基本資料O
            原文搬運O
            翻譯校對X
            細節修正X
            */
            Say(pc, 0, "あなた、冒険者ね？$R;" +
                "ちょつと聞いてよ！$R;", "？？？");

            switch (Select(pc, "どうする？", "", "言を聞く", "またこんど"))
            {
                case 1:
                    Say(pc, 0, "実は私ね……。$R;", "？？？");
                    Say(pc, 0, "「無限回廊下層」に未知のエリアを$R;" +
                        "発見したのよ！$R;" +
                        "$Pどう？すごいでしょ！！$R;" +
                        "ワクワクしてくるでしょ！$R;" +
                        "$P私はこの階層を「無限回廊裹層」！！$R;" +
                        "と命名して、調查をしようと思うの！$R;" +
                        "$P……でもね、入れる人が$R;" +
                        "限られているみたい$R;" +
                        "$Pいつも宝箱を開けてくれるあの子か$R;" +
                        "あそこでいひられてるあの人か$R;" +
                        "色々な人に協力して貰ったんだけど$R;" +
                        "$Pどうやら$R;" +
                        "$R職業と身体共に$R;" +
                        "成長の限界を迎えた転生者$R;" +
                        "$Rしか入れないみたいなのよ。$R;" +
                        "そんな人は、私のバーティにも$R;" + "$R;" +
                        "居ないのよねぇ。$R;" +
                        "どかかに良い人はいないかんあぁ……？$R;", "？？？");
                    break;
            }
         }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10065000
{
    public class S13000293 : Event
    {
        public S13000293()
        {
            this.EventID = 13000293;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 60121300) >= 1 && CountItem(pc, 50208000) >= 1)
            {
                Say(pc, 131, "お！持ってるね！$R;" +
                "色変える？$R;" +
                "$R$CR※色変えを行うと$R;" +
                "武具の強化値、及びカードスロット$R;" +
                "装着済のイリスカードは消失します。$CD$R;", "見習いサンタ");
                if (Select(pc, "どれにする？", "", "変える", "変えない") == 1)
                {
                    Say(pc, 131, "はい！出来たよ♪$R;" +
                    "いい感じ！$R;" +
                    "戻したくなったら、また声かけてね♪$R;", "見習いサンタ");

                    Say(pc, 0, 131, "「イノセントサンタ」上下の色を$R;" +
                    "変えてもらった$R;", " ");
                    TakeItem(pc, 60121300, 1);
                    TakeItem(pc, 50208000, 1);
                    GiveItem(pc, 60121309, 1);
                    GiveItem(pc, 50208009, 1);
                    return;
                }
                Say(pc, 131, "今のままが良いんだね♪$R;" +
                "気分が変わったらいつでも声かけてね～$R;", "見習いサンタ");
                return;
            }

            Say(pc, 131, "「イノセントサンタ」っていう服を$R;" +
            "持ってきたら、服の色を$R;" +
            "変えてあげちゃう♪$R;" +
            "なんか、問題出すのが好きな$R;" +
            "物好きの３人が居るから、$R;" +
            "その人たちに話しを聞いてみて！$R;", "見習いサンタ");
        }
    }
}
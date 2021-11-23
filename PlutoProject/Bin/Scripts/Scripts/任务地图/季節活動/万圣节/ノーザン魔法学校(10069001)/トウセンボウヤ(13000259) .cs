using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10069001
{
    public class S13000259 : Event
    {
        public S13000259()
        {
            this.EventID = 13000259;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 10031800) > 0)
            {
                Say(pc, 131, "いいもん、みーっけっ！$R;", "トウセンボウヤ");

                Say(pc, 0, 131, "ゾンビパウダーを奪われた。$R;" +
                "$Pトウセンボウヤは$R;" +
                "ゾンビパウダーに夢中だ！！！$R;", " ");
                TakeItem(pc, 10031800, 1);
                Say(pc, 0, 131, "" + pc.Name + "は$R;" +
                "トウセンボウヤの後ろを$R;" +
                "そうっと通り過ぎた。$R;", " ");
                Warp(pc, 10069001, 110, 82);
                ShowEffect(pc, 10405, 4539);
                return;
            }
            if (CountItem(pc, 10031909) > 0)
            {
                Say(pc, 131, "いいもん、みーっけっ！$R;", "トウセンボウヤ");

                Say(pc, 0, 131, "コウモリの羽を奪われた。$R;" +
                "$Pトウセンボウヤは$R;" +
                "ゾンビパウダーに夢中だ！！！$R;", " ");
                TakeItem(pc, 10031909, 1);
                Say(pc, 0, 131, "" + pc.Name + "は$R;" +
                "トウセンボウヤの後ろを$R;" +
                "そうっと通り過ぎた。$R;", " ");
                Warp(pc, 10069001, 110, 82);
                ShowEffect(pc, 10405, 4539);
                return;
            }
            if (CountItem(pc, 10024009) > 0)
            {
                Say(pc, 131, "いいもん、みーっけっ！$R;", "トウセンボウヤ");

                Say(pc, 0, 131, "毒蜘蛛の糸を奪われた。$R;" +
                "$Pトウセンボウヤは$R;" +
                "ゾンビパウダーに夢中だ！！！$R;", " ");
                TakeItem(pc, 10024009, 1);
                Say(pc, 0, 131, "" + pc.Name + "は$R;" +
                "トウセンボウヤの後ろを$R;" +
                "そうっと通り過ぎた。$R;", " ");
                Warp(pc, 10069001, 110, 82);
                ShowEffect(pc, 10405, 4539);
                return;
            }

            Say(pc, 131, "イヒヒッ、イヒヒッ！$R;" +
            "ぼくは、いじわるトウセンボウヤ。$R;" +
            "$Rとうせんぼうしちゃうぞ！$R;", "トウセンボウヤ");
        }
    }
}
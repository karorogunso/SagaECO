using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11000871 : Event
    {
        public S11000871()
        {
            this.EventID = 11000871;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 50030000) >= 1)
            {
                /*
            //50030000, 1);
            if (//ME.WORK0 > 0
            )
            {
                Say(pc, 131, "ラララ～$R;" +
                    "私は染色おばさん。$R;" +
                    "なんでも染めちゃう、素敵なおばさん。$R;" +
                    "$R『トンボ球』持ってない？$R;" +
                    "$P……？？？$R;" +
                    "$Pなんだか、変わった$R;" +
                    "『トンボ球』だねぇ。$R;" +
                    "$Rこれは、染色できないよ～$R;");
                return;
            }
                */
                Say(pc, 131, "ラララ～$R;" +
                    "私は染色おばさん。$R;" +
                    "なんでも染めちゃう、素敵なおばさん。$R;" +
                    "$Rあんたの『トンボ球』$R;" +
                    "２０００ゴールドで染めようか？$R;");
                switch (Select(pc, "２０００ゴールドで染めようか？", "", "染めちゃう、染めちゃう！", "染めない"))
                {
                    case 1:
                        if (CheckInventory(pc, 50030001, 1))
                        {
                            Say(pc, 131, "您的行李太多了$R;");
                            return;
                        }
                        if (pc.Gold > 1999)
                        {
                            Say(pc, 131, "何色に染めちゃう？$R;");
                            switch (Select(pc, "何色に染めちゃう？", "", "赤色に染めちゃう！", "紫色に染めちゃう！", "青色に染めちゃう！", "綠色に染めちゃう！", "橙色に染めちゃう！", "モカに染めちゃう！", "染めない"))
                            {
                                case 1:
                                    GiveItem(pc, 50030001, 1);
                                    break;
                                case 2:
                                    GiveItem(pc, 50030002, 1);
                                    break;
                                case 3:
                                    GiveItem(pc, 50030003, 1);
                                    break;
                                case 4:
                                    GiveItem(pc, 50030005, 1);
                                    break;
                                case 5:
                                    GiveItem(pc, 50030008, 1);
                                    break;
                                case 6:
                                    GiveItem(pc, 50030012, 1);
                                    break;
                                case 7:
                                    return;
                            }
                            TakeItem(pc, 50030000, 1);
                            pc.Gold -= 2000;
                            PlaySound(pc, 2215, false, 100, 50);
                            Fade(pc, FadeType.Out, FadeEffect.Black);
                            Wait(pc, 2000);
                            Fade(pc, FadeType.In, FadeEffect.Black);
                            Say(pc, 131, "簡単、お手軽、染色楽しいわ！$R;");
                            return;
                        }
                        Say(pc, 131, "お金が足りないよ～$R;");
                        break;
                    case 2:
                        break;
                }
                return;
            }
            Say(pc, 131, "ラララ～$R;" +
                "私は染色おばさん。$R;" +
                "なんでも染めちゃう、素敵なおばさん。$R;" +
                "$R『トンボ球』持ってない？$R;");
        }
    }
}
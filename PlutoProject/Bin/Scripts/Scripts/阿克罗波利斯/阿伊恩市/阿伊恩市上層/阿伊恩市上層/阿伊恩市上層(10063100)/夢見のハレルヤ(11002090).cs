using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10063100
{
    public class S11002090 : Event
    {
        public S11002090()
        {
            this.EventID = 11002090;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11001973, 131, "ごきげんよう、$R;" +
            "" + pc.Name + "お嬢様。$R;" +
            "わたくしに何か御用でしょうか？$R;", "夢見のハレルヤ");
            switch (Select(pc, "どうする？", "", "特にない", "カードを交換する", "ＥＸカードを交換する", "貴方は誰？"))
            {
                case 1:
                    Say(pc, 11001973, 131, "さようでございますか。$R;", "夢見のハレルヤ");
                    break;
                case 2:
                    switch (Select(pc, "どれを交換しますか？", "", "ブランクイリスカード", "ブランクイリスカードＵＣ", "ブランクイリスカード５", "ブランクイリスカード１０", "やめる"))
                    {
                        case 1:
                            if (CountItem(pc, 10067300) >= 1)
                            {
                                Say(pc, 11001973, 131, "では、わたくしの右手、左手の$R;" +
                                "どちらで想いを込めましょうか？$R;" +
                                "それとも両手で込めた方が$R;" +
                                "よろしいですかな？$R;", "夢見のハレルヤ");
                                if (Select(pc, "どちらで想いを込めますか？", "", "右手", "左手", "両手", "やっぱりやめる") == 4)
                                {
                                    return;
                                }
                                GiveRandomTreasure(pc, "Irisx4");
                                TakeItem(pc, 10067300, 1);
                                return;
                            }
                            Say(pc, 11001973, 131, "おや、そのブランクイリスカードは$R;" +
                            "お持ちで無いようですね。$R;", "夢見のハレルヤ");
                            break;
                        case 2:
                            Say(pc, 11001973, 131, "おや、そのブランクイリスカードは$R;" +
                            "お持ちで無いようですね。$R;", "夢見のハレルヤ");
                            break;
                        case 3:
                            if (CountItem(pc, 10067310) >= 1)
                            {
                                Say(pc, 11001973, 131, "では、わたくしの右手、左手の$R;" +
                                "どちらで想いを込めましょうか？$R;" +
                                "それとも両手で込めた方が$R;" +
                                "よろしいですかな？$R;", "夢見のハレルヤ");
                                if (Select(pc, "どちらで想いを込めますか？", "", "右手", "左手", "両手", "やっぱりやめる") == 4)
                                {
                                    return;
                                }
                                for (int i = 0; i < 5; i++)
                                {
                                    GiveRandomTreasure(pc, "Irisx4");
                                }
                                TakeItem(pc, 10067310, 1);
                                return;
                            }
                            Say(pc, 11001973, 131, "おや、そのブランクイリスカードは$R;" +
                            "お持ちで無いようですね。$R;", "夢見のハレルヤ");
                            break;
                        case 4:
                            if (CountItem(pc, 10067320) >= 1)
                            {
                                Say(pc, 11001973, 131, "では、わたくしの右手、左手の$R;" +
                                "どちらで想いを込めましょうか？$R;" +
                                "それとも両手で込めた方が$R;" +
                                "よろしいですかな？$R;", "夢見のハレルヤ");
                                if (Select(pc, "どちらで想いを込めますか？", "", "右手", "左手", "両手", "やっぱりやめる") == 4)
                                {
                                    return;
                                }
                                for (int i = 0; i < 10; i++)
                                {
                                    GiveRandomTreasure(pc, "Irisx4");
                                }
                                TakeItem(pc, 10067320, 1);
                                return;
                            }
                            Say(pc, 11001973, 131, "おや、そのブランクイリスカードは$R;" +
                            "お持ちで無いようですね。$R;", "夢見のハレルヤ");
                            break;
                        case 5:
                            Say(pc, 11001973, 131, "さようでございますか。$R;", "夢見のハレルヤ");
                            break;
                    }
                    break;
                case 3:
                    switch (Select(pc, "どれを交換しますか？", "", "ＥＸブランクイリスカード５", "ＥＸブランクイリスカード１０", "やめる"))
                    {
                        case 1:
                            if (CountItem(pc, 16003300) >= 1)
                            {
                                Say(pc, 11001973, 131, "では、わたくしの右手、左手の$R;" +
                                "どちらで想いを込めましょうか？$R;" +
                                "それとも両手で込めた方が$R;" +
                                "よろしいですかな？$R;", "夢見のハレルヤ");
                                if (Select(pc, "どちらで想いを込めますか？", "", "右手", "左手", "両手", "やっぱりやめる") == 4)
                                {
                                    return;
                                }
                                for (int i = 0; i < 5; i++)
                                {
                                    GiveRandomTreasure(pc, "Iris4");
                                }
                                TakeItem(pc, 16003300, 1);
                                
                            }
                            return;
                        case 2:
                            if (CountItem(pc, 16003310) >= 1)
                            {
                                Say(pc, 11001973, 131, "では、わたくしの右手、左手の$R;" +
                                "どちらで想いを込めましょうか？$R;" +
                                "それとも両手で込めた方が$R;" +
                                "よろしいですかな？$R;", "夢見のハレルヤ");
                                if (Select(pc, "どちらで想いを込めますか？", "", "右手", "左手", "両手", "やっぱりやめる") == 4)
                                {
                                    return;
                                }
                                for (int i = 0; i < 10; i++)
                                {
                                    GiveRandomTreasure(pc, "Iris4");
                                }
                                TakeItem(pc, 16003310, 1);
                                
                            }
                            return;
                    }
                    return;
            }
        }
    }
}
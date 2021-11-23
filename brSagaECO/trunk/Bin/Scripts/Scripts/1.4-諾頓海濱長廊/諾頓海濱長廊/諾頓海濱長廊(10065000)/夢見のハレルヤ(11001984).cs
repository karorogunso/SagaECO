using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10065000
{
    public class S11001984 : Event
    {
        public S11001984()
        {
            this.EventID = 11001984;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11001973, 131, "ごきげんよう、$R;" +
            "" + pc.Name + "お嬢様。$R;" +
            "わたくしに何か御用でしょうか？$R;", "夢見のハレルヤ");
            switch (Select(pc, "どうする？", "", "特にない", "ＥＸカードを交換する", "貴方は誰？"))
            {
                case 1:
                    Say(pc, 11001973, 131, "さようでございますか。$R;", "夢見のハレルヤ");
                    break;
                
                case 2:
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
                                    GiveRandomTreasure(pc, "Iris2");
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
                                    GiveRandomTreasure(pc, "Iris2");
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
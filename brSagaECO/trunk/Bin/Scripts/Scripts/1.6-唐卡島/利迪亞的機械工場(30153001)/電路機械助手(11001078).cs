using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30153001
{
    public class S11001078 : Event
    {
        public S11001078()
        {
            this.EventID = 11001078;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 10012300) >= 1)
            {
                Say(pc, 131, "把那個『翠綠橄欖石』給我吧$R;");
                switch (Select(pc, "怎麼辦呢？", "", "不給", "給他吧"))
                {
                    case 1:
                        break;
                    case 2:
                        if (!CheckInventory(pc, 10030100, 1))
                        {
                            Say(pc, 131, "太感謝了$R;" +
                                "不知該說什麼好$R;" +
                                "$R雖然不是貴重的東西，$R;" +
                                "這是我的心意，請收下吧。$R;" +
                                "$R哎呀，行李太多了呀$R;");
                            return;
                        }
                        Say(pc, 131, "太感謝了$R;" +
                            "真心地感謝您，$R;" +
                            "$R這是我的替代部件，$R不知道合不合身，$R;" +
                            "可以的話，請收下吧。$R;");
                        switch (Select(pc, "哪個部件好呀？", "", "發動機", "塔依模型", "超微型電腦"))
                        {
                            case 1:
                                GiveItem(pc, 10030100, 1);
                                TakeItem(pc, 10012300, 1);
                                PlaySound(pc, 2040, false, 100, 50);
                                Say(pc, 131, "得到了『發動機』$R;");
                                break;
                            case 2:
                                GiveItem(pc, 10030002, 1);
                                TakeItem(pc, 10012300, 1);
                                PlaySound(pc, 2040, false, 100, 50);
                                Say(pc, 131, "得到了『塔依模型』$R;");
                                break;
                            case 3:
                                GiveItem(pc, 10030200, 1);
                                TakeItem(pc, 10012300, 1);
                                PlaySound(pc, 2040, false, 100, 50);
                                Say(pc, 131, "得到了『超微型電腦』$R;");
                                break;
                        }
                        break;
                }
                return;
            }
            if (pc.Marionette != null)
            {
                Say(pc, 131, "求，求求您了$R;" +
                    "$R寶石…對！！$R;" +
                    "哪怕只有寶石，心情也許會好起來呢。$R;" +
                    "$P即使是便宜的寶石也好。$R;" +
                    "$R翠綠橄欖石...對了！！$R;" +
                    "給我翠綠橄欖石行嗎？$R;");
                return;
            }
            Say(pc, 131, "哆嗦……$R;");
        }
    }
}
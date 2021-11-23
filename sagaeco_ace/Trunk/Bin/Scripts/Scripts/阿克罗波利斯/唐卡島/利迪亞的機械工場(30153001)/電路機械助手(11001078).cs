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
                Say(pc, 131, "把那个『橄榄石』给我吧$R;");
                switch (Select(pc, "怎么办呢？", "", "不给", "给他吧"))
                {
                    case 1:
                        break;
                    case 2:
                        if (!CheckInventory(pc, 10030100, 1))
                        {
                            Say(pc, 131, "太感谢了$R;" +
                                "不知该说什么好$R;" +
                                "$R虽然不是贵重的东西，$R;" +
                                "这是我的心意，请收下吧。$R;" +
                                "$R哎呀，行李太多了呀$R;");
                            return;
                        }
                        Say(pc, 131, "太感谢了$R;" +
                            "真心地感谢您，$R;" +
                            "$R这是我的替代部件，$R不知道合不合身，$R;" +
                            "可以的话，请收下吧。$R;");
                        switch (Select(pc, "哪个部件好呀？", "", "发动机", "电路机械的模型", "掌上电脑"))
                        {
                            case 1:
                                GiveItem(pc, 10030100, 1);
                                TakeItem(pc, 10012300, 1);
                                PlaySound(pc, 2040, false, 100, 50);
                                Say(pc, 131, "得到了『发动机』$R;");
                                break;
                            case 2:
                                GiveItem(pc, 10030002, 1);
                                TakeItem(pc, 10012300, 1);
                                PlaySound(pc, 2040, false, 100, 50);
                                Say(pc, 131, "得到了『电路机械的模型』$R;");
                                break;
                            case 3:
                                GiveItem(pc, 10030200, 1);
                                TakeItem(pc, 10012300, 1);
                                PlaySound(pc, 2040, false, 100, 50);
                                Say(pc, 131, "得到了『掌上电脑』$R;");
                                break;
                        }
                        break;
                }
                return;
            }
            if (pc.Marionette != null)
            {
                Say(pc, 131, "求，求求您了$R;" +
                    "$R宝石…对！！$R;" +
                    "哪怕只有宝石，心情也许会好起来呢。$R;" +
                    "$P即使是便宜的宝石也好。$R;" +
                    "$R翠绿橄欖石...对了！！$R;" +
                    "给我翠绿橄欖石行吗？$R;");
                return;
            }
            Say(pc, 131, "哆嗦……$R;");
        }
    }
}
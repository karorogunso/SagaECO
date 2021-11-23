using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.M20081000
{
    public class S11001019 : Event
    {
        public S11001019()
        {
            this.EventID = 11001019;
        }


        public override void OnEvent(ActorPC pc)
        {
            if (GetPossibleDungeons(pc).Count == 0)
            {
                if (pc.Quest != null)
                {
                    if (pc.Quest.ID == 26500002)
                    {
                        Weapon90(pc);
                    }
                    else if (pc.Quest.ID == 10032027 || pc.Quest.ID == 10032028 || pc.Quest.ID == 10032029)
                    {
                        OtherQuest(pc);
                    }
                    else Say(pc, 131, "您接的任务不能从这里进入。$R;");
                }
                else
                {
                    Say(pc, 131, "您还没有接任何任务哦。$R;");
                }
            }
            else
            {
                WarpToDungeon(pc);
            }
        }

        void Weapon90(ActorPC pc)
        {
            if (pc.Quest.Status == SagaDB.Quests.QuestStatus.OPEN)
            {
                if (pc.CInt["LV90_Weapon"] == 0)
                {
                    pc.CInt["LV90_Weapon"] = 1;
                    CreateDungeon(pc, pc.Quest.Detail.DungeonID, 20081000, 12, 6);
                    WarpToDungeon(pc);
                }
                else
                {
                    Say(pc, 131, "此任务只允创建一次遗迹！$R;" +
                        "必须完成此任务才能重新创建遗迹！$R;");
                }
            }
            else
            {
                Say(pc, 131, "任务必须是进行中！$R;" +
                    "失败或完成重新接了任务再来吧$R;");
            }
        }

        void OtherQuest(ActorPC pc)
        {
            if (pc.Quest.Status == SagaDB.Quests.QuestStatus.OPEN)
            {
                CreateDungeon(pc, pc.Quest.Detail.DungeonID, 20081000, 12, 6);
                WarpToDungeon(pc);
            }
            else
            {
                Say(pc, 131, "任务必须是进行中！$R;" +
                    "失败或完成重新接任务来吧$R;");
            }
        }
    }
}
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
                    else Say(pc, 131, "您接的任務不能從這裡進入。$R;");
                }
                else
                {
                    Say(pc, 131, "您還沒有接任何任務哦。$R;");
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
                    Say(pc, 131, "此任務只允創建一次遺跡！$R;" +
                        "必須完成此任務才能重新創建遺跡！$R;");
                }
            }
            else
            {
                Say(pc, 131, "任務必須是進行中！$R;" +
                    "失敗或完成重新接了任務再來吧$R;");
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
                Say(pc, 131, "任務必須是進行中！$R;" +
                    "失敗或完成重新接任務來吧$R;");
            }
        }
    }
}
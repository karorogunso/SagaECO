using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:艾尔·夏尔（下层）(11024000)NPC基本信息:10001820
namespace SagaScript
{
    public class yard_entry : Event
    {
        public yard_entry()
        {
            this.EventID = 10001820;
        }
        public override void OnEvent(ActorPC pc)
        {
            if (GetPossibleDungeons(pc).Count == 0)
            {
                if (pc.Quest != null)
                {
                    CreateDungeon(pc, pc.Quest.Detail.DungeonID, 11024000, 117, 200);
                    WarpToDungeon(pc);
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
    }
}

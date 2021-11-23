
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaMap.Mob;
using SagaMap.Skill;
using SagaDB.Mob;
using SagaMap.ActorEventHandlers;
namespace WeeklyExploration
{
    public partial class GQuest : Event
    {
        ActorMob 门卫1;
        ActorMob 门卫2;
        ActorMob 第二关封印;

        void 不死皇城刷怪(uint mapid)
        {
            SagaMap.Map map; map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            门卫1 = map.SpawnCustomMob(10000000, map.ID, 10190100, 0, 0, 158, 178, 0, 1, 0, 骷髅击剑士Info(), 骷髅射手AI(), (MobCallback)门卫1Ondie, 1)[0];
            门卫2 = map.SpawnCustomMob(10000000, map.ID, 10190100, 0, 0, 163, 178, 0, 1, 0, 骷髅击剑士Info(), 骷髅射手AI(), (MobCallback)门卫2Ondie, 1)[0];
            第二关封印 = map.SpawnCustomMob(10000000, map.ID, 10000000, 0, 0, 160, 177, 0, 1, 0, 封印Info(), 封印AI(), (MobCallback)第二关封印Ondie, 1)[0];
        }
        void 门卫1Ondie(MobEventHandler e, ActorPC pc)
        {
            SkillHandler.Instance.ActorSpeak(门卫1, "可...可恶啊！...一定..要守住封印...");
            pc.Party.Leader.TInt["不死皇城第二关变量"] += 1;
            if (pc.Party.Leader.TInt["不死皇城第二关变量"] >= 2)
            {
                第二关封印.HP = 1;
            }
        }
        void 门卫2Ondie(MobEventHandler e, ActorPC pc)
        {
            SkillHandler.Instance.ActorSpeak(门卫2, "请告诉我的麻麻...我...想和光头打架");
            pc.Party.Leader.TInt["不死皇城第二关变量"] += 1;
            if (pc.Party.Leader.TInt["不死皇城第二关变量"] >= 2)
            {
                第二关封印.HP = 1;
            }
        }
        void 第二关封印Ondie(MobEventHandler e, ActorPC pc)
        {
            第三关(pc);
        }
    }
}



using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaDB.Actor;
using SagaMap.Mob;
using SagaDB.Mob;
namespace WeeklyExploration
{
    public partial class MinamiArea4Spawns 
    {
        ActorMob.MobInfo 屠杀机械H00Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "屠杀机械H00";
            info.maxhp = 3528;
            info.speed = 546;
            info.atk_min = 121;
            info.atk_max = 240;
            info.matk_min = 19;
            info.matk_max = 39;
            info.def = 15;
            info.def_add = 79;
            info.mdef = 9;
            info.mdef_add = 50;
            info.hit_critical = 49;
            info.hit_magic = 49;
            info.hit_melee = 150;
            info.hit_ranged = 147;
            info.avoid_critical = 50;
            info.avoid_magic = 24;
            info.avoid_melee = 90;
            info.avoid_ranged = 89;
            info.Aspd = 630;
            info.Cspd = 606;
            info.elements[SagaLib.Elements.Neutral] = 0;
            info.elements[SagaLib.Elements.Fire] = 0;
            info.elements[SagaLib.Elements.Water] = 0;
            info.elements[SagaLib.Elements.Wind] = 0;
            info.elements[SagaLib.Elements.Earth] = 0;
            info.elements[SagaLib.Elements.Holy] = 0;
            info.elements[SagaLib.Elements.Dark] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Confused] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Frosen] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Paralyse] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Poisen] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Silence] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Sleep] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Stone] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Stun] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.鈍足] = 0;
            info.baseExp = info.maxhp;
            info.jobExp = info.maxhp;


            MobData.DropData newDrop = new MobData.DropData();
            newDrop.ItemID = 910000000;
            newDrop.Rate = 300;
            info.dropItems.Add(newDrop);
            return info;
        }
        AIMode 屠杀机械H00AI()
        {
            AIMode ai = new AIMode(0); ai.MobID = 10000000; ai.isNewAI = true;
            return ai;
        }
    }
}


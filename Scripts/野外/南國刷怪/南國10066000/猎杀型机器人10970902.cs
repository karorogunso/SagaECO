
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
        ActorMob.MobInfo 猎杀型机器人Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "猎杀型机器人";
            info.maxhp = 11286;
            info.speed = 546;
            info.atk_min = 144;
            info.atk_max = 290;
            info.matk_min = 24;
            info.matk_max = 48;
            info.def = 17;
            info.def_add = 95;
            info.mdef = 12;
            info.mdef_add = 60;
            info.hit_critical = 60;
            info.hit_magic = 59;
            info.hit_melee = 180;
            info.hit_ranged = 180;
            info.avoid_critical = 59;
            info.avoid_magic = 30;
            info.avoid_melee = 106;
            info.avoid_ranged = 108;
            info.Aspd = 669;
            info.Cspd = 649;
            info.elements[SagaLib.Elements.Neutral] = 0;
            info.elements[SagaLib.Elements.Fire] = 0;
            info.elements[SagaLib.Elements.Water] = 0;
            info.elements[SagaLib.Elements.Wind] = 0;
            info.elements[SagaLib.Elements.Earth] = 0;
            info.elements[SagaLib.Elements.Holy] = 0;
            info.elements[SagaLib.Elements.Dark] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Confused] = 30;
            info.abnormalstatus[SagaLib.AbnormalStatus.Frosen] = 30;
            info.abnormalstatus[SagaLib.AbnormalStatus.Paralyse] = 30;
            info.abnormalstatus[SagaLib.AbnormalStatus.Poisen] = 30;
            info.abnormalstatus[SagaLib.AbnormalStatus.Silence] = 30;
            info.abnormalstatus[SagaLib.AbnormalStatus.Sleep] = 30;
            info.abnormalstatus[SagaLib.AbnormalStatus.Stone] = 30;
            info.abnormalstatus[SagaLib.AbnormalStatus.Stun] = 30;
            info.abnormalstatus[SagaLib.AbnormalStatus.鈍足] = 30;
            info.baseExp = info.maxhp;
            info.jobExp = info.maxhp;



            MobData.DropData newDrop = new MobData.DropData();
            newDrop.ItemID = 910000000;
            newDrop.Rate = 2000;
            info.dropItems.Add(newDrop);
            return info;
        }
        AIMode 猎杀型机器人AI()
        {
            AIMode ai = new AIMode(0); ai.MobID = 10000000; ai.isNewAI = true;
            return ai;
        }
    }
}


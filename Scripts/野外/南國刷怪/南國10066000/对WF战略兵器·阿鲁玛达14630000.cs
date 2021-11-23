
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
        ActorMob.MobInfo 对WF战略兵器阿鲁玛达Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "对WF战略兵器·阿鲁玛达";
            info.maxhp = 943250;
            info.speed = 378;
            info.atk_min = 371;
            info.atk_max = 742;
            info.matk_min = 49;
            info.matk_max = 101;
            info.def = 79;
            info.def_add = 450;
            info.mdef = 39;
            info.mdef_add = 202;
            info.hit_critical = 24;
            info.hit_magic = 200;
            info.hit_melee = 303;
            info.hit_ranged = 300;
            info.avoid_critical = 24;
            info.avoid_magic = 100;
            info.avoid_melee = 61;
            info.avoid_ranged = 62;
            info.Aspd = 561;
            info.Cspd = 750;
            info.elements[SagaLib.Elements.Neutral] = 0;
            info.elements[SagaLib.Elements.Fire] = 0;
            info.elements[SagaLib.Elements.Water] = 0;
            info.elements[SagaLib.Elements.Wind] = 0;
            info.elements[SagaLib.Elements.Earth] = 0;
            info.elements[SagaLib.Elements.Holy] = 0;
            info.elements[SagaLib.Elements.Dark] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Confused] = 90;
            info.abnormalstatus[SagaLib.AbnormalStatus.Frosen] = 90;
            info.abnormalstatus[SagaLib.AbnormalStatus.Paralyse] = 90;
            info.abnormalstatus[SagaLib.AbnormalStatus.Poisen] = 90;
            info.abnormalstatus[SagaLib.AbnormalStatus.Silence] = 90;
            info.abnormalstatus[SagaLib.AbnormalStatus.Sleep] = 90;
            info.abnormalstatus[SagaLib.AbnormalStatus.Stone] = 90;
            info.abnormalstatus[SagaLib.AbnormalStatus.Stun] = 90;
            info.abnormalstatus[SagaLib.AbnormalStatus.鈍足] = 90;
            info.baseExp = info.maxhp;
            info.jobExp = info.maxhp;



            MobData.DropData newDrop = new MobData.DropData();
            newDrop.ItemID = 910000000;
            newDrop.Rate = 10000;
            info.dropItems.Add(newDrop);
            return info;
        }
        AIMode 对WF战略兵器阿鲁玛达AI()
        {
            AIMode ai = new AIMode(0); ai.MobID = 10000000; ai.isNewAI = true;
            return ai;
        }
    }
}


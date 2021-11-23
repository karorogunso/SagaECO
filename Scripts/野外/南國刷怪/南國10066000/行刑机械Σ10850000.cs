
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
        ActorMob.MobInfo 行刑机械ΣInfo()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "行刑机械Σ";
            info.maxhp = 13500;
            info.speed = 420;
            info.atk_min = 117;
            info.atk_max = 235;
            info.matk_min = 118;
            info.matk_max = 237;
            info.def = 21;
            info.def_add = 121;
            info.mdef = 21;
            info.mdef_add = 117;
            info.hit_critical = 24;
            info.hit_magic = 117;
            info.hit_melee = 120;
            info.hit_ranged = 117;
            info.avoid_critical = 23;
            info.avoid_magic = 60;
            info.avoid_melee = 59;
            info.avoid_ranged = 59;
            info.Aspd = 540;
            info.Cspd = 550;
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
        AIMode 行刑机械ΣAI()
        {
            AIMode ai = new AIMode(0); ai.MobID = 10000000; ai.isNewAI = true;
            return ai;
        }
    }
}


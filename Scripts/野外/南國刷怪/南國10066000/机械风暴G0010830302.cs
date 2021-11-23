
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
        ActorMob.MobInfo 机械风暴G00Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "机械风暴G00";
            info.maxhp = 216090;
            info.speed = 336;
            info.atk_min = 360;
            info.atk_max = 705;
            info.matk_min = 40;
            info.matk_max = 78;
            info.def = 43;
            info.def_add = 297;
            info.mdef = 9;
            info.mdef_add = 101;
            info.hit_critical = 29;
            info.hit_magic = 98;
            info.hit_melee = 160;
            info.hit_ranged = 158;
            info.avoid_critical = 30;
            info.avoid_magic = 49;
            info.avoid_melee = 121;
            info.avoid_ranged = 121;
            info.Aspd = 705;
            info.Cspd = 615;
            info.elements[SagaLib.Elements.Neutral] = 0;
            info.elements[SagaLib.Elements.Fire] = 0;
            info.elements[SagaLib.Elements.Water] = 0;
            info.elements[SagaLib.Elements.Wind] = 0;
            info.elements[SagaLib.Elements.Earth] = 0;
            info.elements[SagaLib.Elements.Holy] = 0;
            info.elements[SagaLib.Elements.Dark] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Confused] = 70;
            info.abnormalstatus[SagaLib.AbnormalStatus.Frosen] = 70;
            info.abnormalstatus[SagaLib.AbnormalStatus.Paralyse] = 70;
            info.abnormalstatus[SagaLib.AbnormalStatus.Poisen] = 70;
            info.abnormalstatus[SagaLib.AbnormalStatus.Silence] = 70;
            info.abnormalstatus[SagaLib.AbnormalStatus.Sleep] = 70;
            info.abnormalstatus[SagaLib.AbnormalStatus.Stone] = 70;
            info.abnormalstatus[SagaLib.AbnormalStatus.Stun] = 70;
            info.abnormalstatus[SagaLib.AbnormalStatus.鈍足] = 70;
            info.baseExp = info.maxhp;
            info.jobExp = info.maxhp;


            MobData.DropData newDrop = new MobData.DropData();
            newDrop.ItemID = 910000000;
            newDrop.Rate = 6000;
            info.dropItems.Add(newDrop);
            return info;
        }
        AIMode 机械风暴G00AI()
        {
            AIMode ai = new AIMode(0); ai.MobID = 10000000; ai.isNewAI = true;
            return ai;
        }
    }
}


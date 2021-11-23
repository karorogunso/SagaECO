
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
        ActorMob.MobInfo 步行钻孔机器人Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "步行钻孔机器人";
            info.maxhp = 63483;
            info.speed = 336;
            info.atk_min = 270;
            info.atk_max = 529;
            info.matk_min = 29;
            info.matk_max = 60;
            info.def = 32;
            info.def_add = 225;
            info.mdef = 7;
            info.mdef_add = 74;
            info.hit_critical = 22;
            info.hit_magic = 75;
            info.hit_melee = 117;
            info.hit_ranged = 118;
            info.avoid_critical = 22;
            info.avoid_magic = 37;
            info.avoid_melee = 90;
            info.avoid_ranged = 90;
            info.Aspd = 649;
            info.Cspd = 534;
            info.elements[SagaLib.Elements.Neutral] = 0;
            info.elements[SagaLib.Elements.Fire] = 0;
            info.elements[SagaLib.Elements.Water] = 0;
            info.elements[SagaLib.Elements.Wind] = 0;
            info.elements[SagaLib.Elements.Earth] = 0;
            info.elements[SagaLib.Elements.Holy] = 0;
            info.elements[SagaLib.Elements.Dark] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Confused] = 50;
            info.abnormalstatus[SagaLib.AbnormalStatus.Frosen] = 50;
            info.abnormalstatus[SagaLib.AbnormalStatus.Paralyse] = 50;
            info.abnormalstatus[SagaLib.AbnormalStatus.Poisen] = 50;
            info.abnormalstatus[SagaLib.AbnormalStatus.Silence] = 50;
            info.abnormalstatus[SagaLib.AbnormalStatus.Sleep] = 50;
            info.abnormalstatus[SagaLib.AbnormalStatus.Stone] = 50;
            info.abnormalstatus[SagaLib.AbnormalStatus.Stun] = 50;
            info.abnormalstatus[SagaLib.AbnormalStatus.鈍足] = 50;
            info.baseExp = info.maxhp;
            info.jobExp = info.maxhp;



            MobData.DropData newDrop = new MobData.DropData();
            newDrop.ItemID = 910000000;
            newDrop.Rate = 2000;
            info.dropItems.Add(newDrop);
            return info;
        }
        AIMode 步行钻孔机器人AI()
        {
            AIMode ai = new AIMode(0); ai.MobID = 10000000; ai.isNewAI = true;
            return ai;
        }
    }
}


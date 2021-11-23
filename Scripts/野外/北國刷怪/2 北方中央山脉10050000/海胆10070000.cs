
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
    public partial class KitaArea2Spawns 
    {
        ActorMob.MobInfo 海胆Info()
        {
ActorMob.MobInfo info = new ActorMob.MobInfo();
info.name = "海胆";
info.maxhp = 2623;
info.speed = 504;
info.atk_min = 178;
info.atk_max = 356;
info.matk_min = 151;
info.matk_max = 294;
info.def = 9;
info.def_add = 19;
info.mdef = 13;
info.mdef_add = 49;
info.hit_critical = 29;
info.hit_magic = 49;
info.hit_melee = 121;
info.hit_ranged = 117;
info.avoid_critical = 29;
info.avoid_magic = 24;
info.avoid_melee = 39;
info.avoid_ranged = 40;
info.Aspd = 440;
info.Cspd = 534;
info.elements[SagaLib.Elements.Neutral] = 0;
info.elements[SagaLib.Elements.Fire] = 0;
info.elements[SagaLib.Elements.Water] = 40;
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
            /*---------物理掉落---------*/
            newDrop.ItemID = 910000000;//掉落物品ID
            newDrop.Rate = 3600;//掉落幾率,10000是100%，5000是50%
            info.dropItems.Add(newDrop);

            /*---------物理掉落---------*/

            return info;
        }
        AIMode 海胆AI()
        {
            AIMode ai = new AIMode(0); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            ai.MobID = 10070000;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 3;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 3;//遠程技能表最短釋放間隔，3秒一次

            return ai;
        }
    }
}
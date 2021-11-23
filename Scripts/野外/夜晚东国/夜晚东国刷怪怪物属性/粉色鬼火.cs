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
    public partial class DongguoSpawn
    {
        ActorMob.MobInfo 粉色鬼火Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "粉色鬼火";
            info.level = 70;
            info.maxhp = 18000;
            info.speed = 420;
            info.atk_min = 205;
            info.atk_max = 505;
            info.matk_min = 117;
            info.matk_max = 237;
            info.def = 0;
            info.def_add = 0;
            info.mdef = 0;
            info.mdef_add = 0;
            info.hit_critical = 23;
            info.hit_magic = 118;
            info.hit_melee = 118;
            info.hit_ranged = 120;
            info.avoid_critical = 0;
            info.avoid_magic = 0;
            info.avoid_melee = 0;
            info.avoid_ranged = 0;
            info.Aspd = 540;
            info.Cspd = 540;
            info.elements[SagaLib.Elements.Neutral] = 0;
            info.elements[SagaLib.Elements.Fire] = 0;
            info.elements[SagaLib.Elements.Water] = 70;
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


            /*---------燐粉---------*/
            MobData.DropData newDrop = new MobData.DropData();
            newDrop.ItemID = 10001150;//掉落物品ID
            newDrop.Rate = 3500;//掉落幾率,10000是100%，5000是50%
            info.dropItems.Add(newDrop);

            /*---------灵魂粉末---------*/
            newDrop = new MobData.DropData();
            newDrop.ItemID = 10001200;//掉落物品ID
            newDrop.Rate = 2500;//掉落幾率,10000是100%，5000是50%
            info.dropItems.Add(newDrop);

            /*---------枯树枝---------*/
            newDrop = new MobData.DropData();
            newDrop.ItemID = 910000114;//掉落物品ID
            newDrop.Rate = 10;//掉落幾率,10000是100%，5000是50%
            info.dropItems.Add(newDrop);
            return info;
        }
        AIMode 粉色鬼火AI()
        {
            AIMode ai = new AIMode(1); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            ai.MobID = 10960002;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 3;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 3;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();


            return ai;
        }
    }
}

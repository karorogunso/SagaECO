
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
namespace 东部地牢副本
{
    public partial class 东部地牢
    {
        ActorMob.MobInfo 血腥死神Info(Difficulty diff)
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "血腥死神";
            info.level = 80;
            info.maxhp = 24000;
            info.speed = 420;
            info.atk_min = 758;
            info.atk_max = 908;
            info.matk_min = 470;
            info.matk_max = 560;
            info.def = 3;
            info.def_add = 10;
            info.mdef = 3;
            info.mdef_add = 10;
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



            /*---------死亡之花---------*/
            MobData.DropData newDrop = new MobData.DropData();
            newDrop.ItemID = 110025056;//掉落物品ID
            newDrop.Rate = 2000;//掉落幾率,10000是100%，5000是50%
            info.dropItems.Add(newDrop);

            /*---------血污布片---------*/
            newDrop = new MobData.DropData();
            newDrop.ItemID = 110131608;//掉落物品ID
            newDrop.Rate = 4000;//掉落幾率,10000是100%，5000是50%
            info.dropItems.Add(newDrop);

            /*---------坚硬的树枝---------*/
            newDrop = new MobData.DropData();
            newDrop.ItemID = 10016606;//掉落物品ID
            newDrop.Rate = 1000;//掉落幾率,10000是100%，5000是50%
            info.dropItems.Add(newDrop);

            return info;
        }
        AIMode 血腥死神AI(Difficulty diff)
        {
            AIMode ai = new AIMode(0); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
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


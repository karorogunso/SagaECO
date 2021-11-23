
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
namespace SagaScript.M30210000
{
    public partial class 暗鸣
    {
        ActorMob.MobInfo 迎春宝箱Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "迎春☆宝箱！";
            info.level = 30;
            info.maxhp = 100000;
            info.speed = 420;
            info.atk_min = 480;
            info.atk_max = 700;
            info.matk_min = 317;
            info.matk_max = 437;
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
            info.baseExp = info.maxhp/3;
            info.jobExp = info.maxhp/3;

            MobData.DropData newDrop = new MobData.DropData();
            newDrop.ItemID = 950000030;//掉落物品ID
            newDrop.Rate = 6000;//掉落幾率,10000是100%，5000是50%
            newDrop.Roll = true;
            newDrop.count = (ushort)(Global.Random.Next(3, 12));
            info.dropItems.Add(newDrop);

            newDrop = new MobData.DropData();
            newDrop.ItemID = 950000030;//掉落物品ID
            newDrop.Rate = 6000;//掉落幾率,10000是100%，5000是50%
            newDrop.Roll = true;
            newDrop.count = (ushort)(Global.Random.Next(3, 12));
            info.dropItems.Add(newDrop);

            newDrop = new MobData.DropData();
            newDrop.ItemID = 950000030;//掉落物品ID
            newDrop.Rate = 6000;//掉落幾率,10000是100%，5000是50%
            newDrop.Roll = true;
            newDrop.count = (ushort)(Global.Random.Next(3, 12));
            info.dropItems.Add(newDrop);

            newDrop = new MobData.DropData();
            newDrop.ItemID = 950000030;//掉落物品ID
            newDrop.Rate = 6000;//掉落幾率,10000是100%，5000是50%
            newDrop.Roll = true;
            newDrop.count = (ushort)(Global.Random.Next(3, 12));
            info.dropItems.Add(newDrop);

            newDrop = new MobData.DropData();
            newDrop.ItemID = 910000105;//掉落物品ID
            newDrop.Rate = 2000;//掉落幾率,10000是100%，5000是50%
            newDrop.Roll = true;
            newDrop.count = 1;
            info.dropItems.Add(newDrop);
            return info;
        }
        AIMode 迎春宝箱AI()
        {
            AIMode ai = new AIMode(1); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            ai.MobID = 10960002;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 3;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 3;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 60;//技能CD
            skillinfo.Rate = 200;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfLong.Add(30026, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(30026, skillinfo);//將這個技能加進進程技能表

            return ai;
        }
    }
}


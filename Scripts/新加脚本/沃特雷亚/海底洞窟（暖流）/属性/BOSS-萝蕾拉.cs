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
    public partial class NuanliuSpawn
    {
        public static ActorMob.MobInfo 萝蕾拉Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "萝蕾拉";
            info.level = 85;
            info.maxhp = 8000000;
            info.speed = 620;
            info.range = 2;
            info.atk_min = 700;
            info.atk_max = 1200;
            info.matk_min = 500;
            info.matk_max = 800;
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


            /*---------韵律石---------*/
            MobData.DropData newDrop = new MobData.DropData();
            newDrop.ItemID = 110011300;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率,10000是100%，5000是50%
            newDrop.Public = true;
            info.dropItems.Add(newDrop);

            /*---------蛤蜊---------*/
            newDrop = new MobData.DropData();
            newDrop.ItemID = 10112801;//掉落物品ID
            newDrop.Rate = 3000;//掉落幾率,10000是100%，5000是50%
            info.dropItems.Add(newDrop);

            newDrop = new MobData.DropData();
            newDrop.ItemID = 110011300;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率,10000是100%，5000是50%
            newDrop.Public20 = true;
            info.dropItems.Add(newDrop);


            return info;
        }
        public static AIMode 萝蕾拉AI()
        {
            AIMode ai = new AIMode(1); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            ai.MobID = 10960002;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 3;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 3;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

            /*----------海之狂乱曲----------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 120;//技能CD
            skillinfo.Rate = 200;//釋放概率
            skillinfo.MaxHP = 99;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfLong.Add(31158, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(31158, skillinfo);//將這個技能加進進程技能表

            /*----------死亡狂想曲----------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 56;//技能CD
            skillinfo.Rate = 200;//釋放概率
            skillinfo.MaxHP = 99;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfLong.Add(31159, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(31159, skillinfo);//將這個技能加進進程技能表

            /*----------欢乐时光----------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 75;//技能CD
            skillinfo.Rate = 200;//釋放概率
            skillinfo.MaxHP = 99;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfLong.Add(31160, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(31160, skillinfo);//將這個技能加進進程技能表

            /*----------月光奏鸣曲----------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 90;//技能CD
            skillinfo.Rate = 200;//釋放概率
            skillinfo.MaxHP = 99;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfLong.Add(31161, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(31161, skillinfo);//將這個技能加進進程技能表

            /*----------琴音干扰----------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 29;//技能CD
            skillinfo.Rate = 200;//釋放概率
            skillinfo.MaxHP = 99;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfLong.Add(31162, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(31162, skillinfo);//將這個技能加進進程技能表

            /*----------狂暴曲·腥红之月----------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 1;//技能CD
            skillinfo.Rate = 200;//釋放概率
            skillinfo.MaxHP = 99;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            skillinfo.OverTime = 600;
            ai.SkillOfLong.Add(31163, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(31163, skillinfo);//將這個技能加進進程技能表
            return ai;
        }
    }
}

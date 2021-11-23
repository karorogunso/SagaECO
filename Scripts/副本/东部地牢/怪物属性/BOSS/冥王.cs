
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
    public partial class S100000105
    {
        public static ActorMob.MobInfo 冥王Info(东部地牢.Difficulty diff)
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "冥王";
            info.level = 15;
            info.maxhp = 800000;
            if (diff == 东部地牢.Difficulty.Single_Normal)
                info.maxhp = 320000;
            info.speed = 420;
            info.atk_min = 580;
            info.atk_max = 1000;
            info.matk_min = 317;
            info.matk_max = 437;
            if (diff == 东部地牢.Difficulty.Single_Hard)
            {
                info.maxhp = 640000;
                info.speed = 920;
                info.atk_min = 1080;
                info.atk_max = 1700;
                info.matk_min = 817;
                info.matk_max = 1037;
            }

            info.def = 35;
            info.def_add = 0;
            info.mdef = 35;
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

            /*---------快要腐烂的果实---------*/
            MobData.DropData newDrop = new MobData.DropData();
            newDrop.ItemID = 110003500;//掉落物品ID
            newDrop.LessThanTime = 3000;//战斗少于5分钟才掉落
            newDrop.Rate = 10000;//掉落幾率,10000是100%，5000是50%
            newDrop.Party = true;
            info.dropItems.Add(newDrop);

            /*---------腐烂的果实---------*/
            newDrop = new MobData.DropData();
            newDrop.ItemID = 110003600;//掉落物品ID
            newDrop.GreaterThanTime = 3000;//战斗大于5分钟才掉落
            newDrop.Rate = 10000;//掉落幾率,10000是100%，5000是50%
            newDrop.Party = true;
            info.dropItems.Add(newDrop);

            newDrop = new MobData.DropData();
            newDrop.ItemID = 110070500;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率,10000是100%，5000是50%
            newDrop.Party = true;
            info.dropItems.Add(newDrop);

            /*---------项链强化石---------*/
            newDrop = new MobData.DropData();
            newDrop.ItemID = 960000000;//掉落物品ID
            newDrop.Rate = 500;//掉落幾率,10000是100%，5000是50%
            newDrop.Roll = true;
            info.dropItems.Add(newDrop);

            /*---------武器强化石---------*/
            newDrop = new MobData.DropData();
            newDrop.ItemID = 960000001;//掉落物品ID
            newDrop.Rate = 500;//掉落幾率,10000是100%，5000是50%
            newDrop.Roll = true;
            info.dropItems.Add(newDrop);

            /*---------衣服强化石---------*/
            newDrop = new MobData.DropData();
            newDrop.ItemID = 960000002;//掉落物品ID
            newDrop.Rate = 500;//掉落幾率,10000是100%，5000是50%
            newDrop.Roll = true;
            info.dropItems.Add(newDrop);
            
            
            /*---------KUJIbi---------*/
            newDrop = new MobData.DropData();
            newDrop.ItemID = 950000025;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率,10000是100%，5000是50%
            newDrop.Roll = true;
            info.dropItems.Add(newDrop);

            return info;
        }
        public static AIMode 冥王AI(东部地牢.Difficulty diff)
        {
            AIMode ai = new AIMode(1); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            ai.MobID = 10960002;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 3;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 3;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

            /*----------撕裂----------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 10;//技能CD
            skillinfo.Rate = 200;//釋放概率
            skillinfo.MaxHP = 95;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfLong.Add(31126, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(31126, skillinfo);//將這個技能加進進程技能表

            /*----------穿刺----------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 30;//技能CD
            skillinfo.Rate = 200;//釋放概率
            skillinfo.MaxHP = 95;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfLong.Add(31127, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(31127, skillinfo);//將這個技能加進進程技能表

            /*----------撕咬----------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 45;//技能CD
            skillinfo.Rate = 200;//釋放概率
            skillinfo.MaxHP = 95;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfLong.Add(31128, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(31128, skillinfo);//將這個技能加進進程技能表

            /*----------狼啸----------*/
            /*skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 30;//技能CD
            skillinfo.Rate = 200;//釋放概率
            skillinfo.MaxHP = 95;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfLong.Add(31129, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(31129, skillinfo);//將這個技能加進進程技能表*/

            /*----------黑月----------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 65;//技能CD
            skillinfo.Rate = 200;//釋放概率
            skillinfo.MaxHP = 95;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfLong.Add(31130, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(31130, skillinfo);//將這個技能加進進程技能表

            /*----------会心一击----------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 14;//技能CD
            skillinfo.Rate = 200;//釋放概率
            skillinfo.MaxHP = 99;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfLong.Add(32001, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(32001, skillinfo);//將這個技能加進進程技能表

            /*----------招架----------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 23;//技能CD
            skillinfo.Rate = 200;//釋放概率
            skillinfo.MaxHP = 99;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfLong.Add(32002, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(32002, skillinfo);//將這個技能加進進程技能表
            return ai;
        }
    }
}


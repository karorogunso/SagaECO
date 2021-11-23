
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
    public partial class YugangdaoASpawn
    {
        ActorMob.MobInfo 我兔汉二Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "我兔汉二";
            info.level = 60;
            info.maxhp = 1800529;
            info.speed = 336;
            info.atk_min = 676;
            info.atk_max = 1063;
            info.matk_min = 619;
            info.matk_max = 1039;
            info.def = 0;
            info.def_add = 0;
            info.mdef = 5;
            info.mdef_add = 150;
            info.hit_critical = 14;
            info.hit_magic = 50;
            info.hit_melee = 78;
            info.hit_ranged = 79;
            info.avoid_critical = 14;
            info.avoid_magic = 24;
            info.avoid_melee = 60;
            info.avoid_ranged = 58;
            info.Aspd = 550;
            info.Cspd = 440;
            info.elements[SagaLib.Elements.Neutral] = 0;
            info.elements[SagaLib.Elements.Fire] = 0;
            info.elements[SagaLib.Elements.Water] = 0;
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
            info.baseExp = info.maxhp / 15;
            info.jobExp = info.maxhp / 15;


            MobData.DropData newDrop = new MobData.DropData();
            /*---------物理掉落---------*/
            newDrop.ItemID = 10004200;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率,10000是100%，5000是50%
            newDrop.Public = true;
            info.dropItems.Add(newDrop);


            newDrop = new MobData.DropData();
            newDrop.ItemID = 950000025;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率,10000是100%，5000是50%
            newDrop.Public = true;
            info.dropItems.Add(newDrop);


            newDrop = new MobData.DropData();
            newDrop.ItemID = 950000025;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率,10000是100%，5000是50%
            newDrop.Public20 = true;
            newDrop.count = 2;
            info.dropItems.Add(newDrop);

            newDrop = new MobData.DropData();
            newDrop.ItemID = 10025300;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率,10000是100%，5000是50%
            newDrop.Public20 = true;
            info.dropItems.Add(newDrop);
            /*---------物理掉落---------*/

            return info;
        }
        AIMode 我兔汉二AI()
        {
            AIMode ai = new AIMode(1); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            ai.MobID = 10960002;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 3;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 3;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

            /*---------疾风突刺---------*/
            skillinfo.CD = 15;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(3083, skillinfo);//將這個技能加進進程技能表
            /*---------疾风突刺---------*/

            /*---------疾风突刺---------*/
            skillinfo.CD = 45;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(7697, skillinfo);//將這個技能加進進程技能表
            /*---------疾风突刺---------*/

            /*---------疾风突刺---------*/
            skillinfo.CD = 75;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31091, skillinfo);//將這個技能加進進程技能表
            /*---------疾风突刺---------*/
            return ai;
        }
    }
}


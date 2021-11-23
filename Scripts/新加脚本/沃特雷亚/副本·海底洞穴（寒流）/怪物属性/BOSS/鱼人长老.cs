
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
namespace 海底副本
{
    public partial class 海底副本 : Event
    {
        ActorMob.MobInfo 鱼人长老Info(Difficulty diff)
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "鱼人长老";
            info.level = 15;
            info.maxhp = 1000000;
            if (diff == Difficulty.Single_Normal)
                info.maxhp = 550000;
            info.speed = 420;
            info.atk_min = 600;
            info.atk_max = 900;
            info.matk_min = 317;
            info.matk_max = 437;
            info.def = 50;
            info.def_add = 0;
            info.mdef = 50;
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

            /*---------海水结晶（普通难度掉落）---------*/
            MobData.DropData newDrop = new MobData.DropData();
            newDrop.ItemID = 110140201;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率,10000是100%，5000是50%
            newDrop.Party = true;
            info.dropItems.Add(newDrop);

            return info;
        }
        AIMode 鱼人长老AI(Difficulty diff)
        {
            AIMode ai = new AIMode(1); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            ai.MobID = 10960002;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 3;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 3;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

            /*----------雷霆万钧----------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 30;//技能CD
            skillinfo.Rate = 200;//釋放概率
            skillinfo.MaxHP = 98;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfLong.Add(31144, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(31144, skillinfo);//將這個技能加進進程技能表

            /*----------海啸术----------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 55;//技能CD
            skillinfo.Rate = 200;//釋放概率
            skillinfo.MaxHP = 95;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfLong.Add(31145, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(31145, skillinfo);//將這個技能加進進程技能表

            /*----------波涛汹涌----------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 35;//技能CD
            skillinfo.Rate = 200;//釋放概率
            skillinfo.MaxHP = 95;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfLong.Add(31146, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(31146, skillinfo);//將這個技能加進進程技能表

            /*----------深蓝领域----------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 120;//技能CD
            skillinfo.Rate = 200;//釋放概率
            skillinfo.MaxHP = 95;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfLong.Add(31147, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(31147, skillinfo);//將這個技能加進進程技能表

            /*----------锤击----------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 10;//技能CD
            skillinfo.Rate = 200;//釋放概率
            skillinfo.MaxHP = 95;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfLong.Add(32000, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(32000, skillinfo);//將這個技能加進進程技能表

            /*----------招架----------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 22;//技能CD
            skillinfo.Rate = 200;//釋放概率
            skillinfo.MaxHP = 95;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfLong.Add(32002, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(32002, skillinfo);//將這個技能加進進程技能表
            return ai;
        }
    }
}


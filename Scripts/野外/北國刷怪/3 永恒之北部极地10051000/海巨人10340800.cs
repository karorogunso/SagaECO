
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
    public partial class KitaArea3Spawns 
    {
        ActorMob.MobInfo 海巨人Info()
        {
ActorMob.MobInfo info = new ActorMob.MobInfo();
info.name = "∏海巨人";
info.maxhp = 812175;
info.speed = 336;
info.atk_min = 450;
info.atk_max = 909;
info.matk_min = 249;
info.matk_max = 401;
info.def = 55;
info.def_add = 371;
info.mdef = 12;
info.mdef_add = 126;
info.hit_critical = 37;
info.hit_magic = 123;
info.hit_melee = 196;
info.hit_ranged = 202;
info.avoid_critical = 36;
info.avoid_magic = 61;
info.avoid_melee = 148;
info.avoid_ranged = 147;
info.Aspd = 742;
info.Cspd = 653;
info.elements[SagaLib.Elements.Neutral] = 70;
info.elements[SagaLib.Elements.Fire] = 40;
info.elements[SagaLib.Elements.Water] = 70;
info.elements[SagaLib.Elements.Wind] = 0;
info.elements[SagaLib.Elements.Earth] = 70;
info.elements[SagaLib.Elements.Holy] = 0;
info.elements[SagaLib.Elements.Dark] = 0;
info.abnormalstatus[SagaLib.AbnormalStatus.Confused] = 90;
info.abnormalstatus[SagaLib.AbnormalStatus.Frosen] = 90;
info.abnormalstatus[SagaLib.AbnormalStatus.Paralyse] = 90;
info.abnormalstatus[SagaLib.AbnormalStatus.Poisen] = 90;
info.abnormalstatus[SagaLib.AbnormalStatus.Silence] = 90;
info.abnormalstatus[SagaLib.AbnormalStatus.Sleep] = 90;
info.abnormalstatus[SagaLib.AbnormalStatus.Stone] = 90;
info.abnormalstatus[SagaLib.AbnormalStatus.Stun] = 90;
info.abnormalstatus[SagaLib.AbnormalStatus.鈍足] = 90;
info.baseExp = info.maxhp;
info.jobExp = info.maxhp;

            MobData.DropData newDrop = new MobData.DropData();
            newDrop.ItemID = 10000104;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率
            info.dropItems.Add(newDrop);

            return info;
        }
        AIMode 海巨人AI()
        {
            AIMode ai = new AIMode(0); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            ai.MobID = 10340800;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 3;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 3;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

            /*---------居合斬---------*/
            skillinfo.CD = 10;//技能CD
            skillinfo.Rate = 60;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(2115, skillinfo);//將這個技能加進進程技能表
            /*---------居合斬---------*/

            /*---------寒气漩涡---------*/
            ai.SkillOfShort.Add(7534, skillinfo);//將這個技能加進進程技能表
            /*---------寒气漩涡---------*/

            /*---------水灵精灵的愤怒---------*/
            ai.SkillOfShort.Add(7615, skillinfo);//將這個技能加進進程技能表
            /*---------水灵精灵的愤怒---------*/

            /*---------魔动剑---------*/
            ai.SkillOfShort.Add(3126, skillinfo);//將這個技能加進進程技能表
            /*---------魔动剑---------*/

            /*---------恒星陨落---------*/
            ai.SkillOfShort.Add(3312, skillinfo);//將這個技能加進進程技能表
            /*---------恒星陨落---------*/
            return ai;
        }
    }
}

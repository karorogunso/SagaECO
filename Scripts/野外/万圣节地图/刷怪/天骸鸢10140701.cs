
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
    public partial class 万圣节
    {
        ActorMob.MobInfo 稻草人Info()
        {
ActorMob.MobInfo info = new ActorMob.MobInfo();
info.name = "稻草人";
info.maxhp = 2357543;
info.speed = 462;
info.atk_min = 930;
info.atk_max = 2059;
info.matk_min = 1120;
info.matk_max = 1242;
info.def = 29;
info.def_add = 222;
info.mdef = 34;
info.mdef_add = 272;
info.hit_critical = 22;
info.hit_magic = 264;
info.hit_melee = 176;
info.hit_ranged = 180;
info.avoid_critical = 22;
info.avoid_magic = 133;
info.avoid_melee = 36;
info.avoid_ranged = 37;
info.Aspd = 424;
info.Cspd = 636;
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
            /*---------物理掉落---------*/
            newDrop.ItemID = 950000010;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率,10000是100%，5000是50%
            newDrop.count = 20;
            newDrop.Public = true;
            info.dropItems.Add(newDrop);
            /*---------物理掉落---------*/

            return info;
        }
        AIMode 稻草人AI()
        {
            AIMode ai = new AIMode(32); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            ai.MobID = 10140701;//怪物ID
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

            /*---------冰箭术---------*/
            ai.SkillOfShort.Add(7532, skillinfo);//將這個技能加進進程技能表
            /*---------冰箭术---------*/

            /*---------魔法彈---------*/
            ai.SkillOfLong.Add(3001, skillinfo);//將這個技能加進遠程技能表，遠程
            /*---------魔法彈---------*/

            return ai;
        }
    }
}
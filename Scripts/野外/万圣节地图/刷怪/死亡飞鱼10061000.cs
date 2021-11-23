
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
        ActorMob.MobInfo 海盗BInfo()
        {
ActorMob.MobInfo info = new ActorMob.MobInfo();
info.name = "海盗B";
info.maxhp = 1200000;
info.speed = 420;
info.atk_min = 739;
info.atk_max = 978;
info.matk_min = 360;
info.matk_max = 705;
info.def = 24;
info.def_add = 99;
info.mdef = 60;
info.mdef_add = 294;
info.hit_critical = 40;
info.hit_magic = 297;
info.hit_melee = 235;
info.hit_ranged = 235;
info.avoid_critical = 39;
info.avoid_magic = 151;
info.avoid_melee = 80;
info.avoid_ranged = 78;
info.Aspd = 609;
info.Cspd = 712;
info.elements[SagaLib.Elements.Neutral] = 0;
info.elements[SagaLib.Elements.Fire] = 0;
info.elements[SagaLib.Elements.Water] = 70;
info.elements[SagaLib.Elements.Wind] = 0;
info.elements[SagaLib.Elements.Earth] = 0;
info.elements[SagaLib.Elements.Holy] = 0;
info.elements[SagaLib.Elements.Dark] = 70;
info.abnormalstatus[SagaLib.AbnormalStatus.Confused] = 70;
info.abnormalstatus[SagaLib.AbnormalStatus.Frosen] = 70;
info.abnormalstatus[SagaLib.AbnormalStatus.Paralyse] = 70;
info.abnormalstatus[SagaLib.AbnormalStatus.Poisen] = 70;
info.abnormalstatus[SagaLib.AbnormalStatus.Silence] = 70;
info.abnormalstatus[SagaLib.AbnormalStatus.Sleep] = 70;
info.abnormalstatus[SagaLib.AbnormalStatus.Stone] = 70;
info.abnormalstatus[SagaLib.AbnormalStatus.Stun] = 70;
info.abnormalstatus[SagaLib.AbnormalStatus.鈍足] = 70;
info.baseExp = info.maxhp;
info.jobExp = info.maxhp;


            MobData.DropData newDrop = new MobData.DropData();
            /*---------物理掉落---------*/
            newDrop.ItemID = 950000010;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率,10000是100%，5000是50%
            newDrop.count = 10;
            newDrop.Public = true;
            info.dropItems.Add(newDrop);
            /*---------物理掉落---------*/

            return info;
        }
        AIMode 海盗B()
        {
            AIMode ai = new AIMode(32);//1為主動，0為被動
            ai.MobID = 10061000;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 3;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 3;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

            /*---------神风精灵的愤怒---------*/
            skillinfo.CD = 15;//技能CD
            skillinfo.Rate = 60;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(7671, skillinfo);//將這個技能加進進程技能表
            /*---------神风精灵的愤怒---------*/
            /*---------衰弱---------*/
            ai.SkillOfShort.Add(7750, skillinfo);//將這個技能加進進程技能表
            /*---------魔法彈---------*/
            ai.SkillOfLong.Add(3001, skillinfo);//將這個技能加進遠程技能表，遠程
            /*---------魔法彈---------*/
            return ai;
        }
    }
}
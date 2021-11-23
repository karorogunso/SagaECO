
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
    public partial class KitaArea1Spawns 
    {
        ActorMob.MobInfo 巨型北极熊Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "巨型北极熊";
            info.maxhp = 220500;
            info.speed = 336;
            info.atk_min = 352;
            info.atk_max = 705;
            info.matk_min = 40;
            info.matk_max = 79;
            info.def = 44;
            info.def_add = 303;
            info.mdef = 10;
            info.mdef_add = 100;
            info.hit_critical = 30;
            info.hit_magic = 98;
            info.hit_melee = 156;
            info.hit_ranged = 161;
            info.avoid_critical = 29;
            info.avoid_magic = 49;
            info.avoid_melee = 117;
            info.avoid_ranged = 120;
            info.Aspd = 691;
            info.Cspd = 621;
            info.elements[SagaLib.Elements.Neutral] = 0;
            info.elements[SagaLib.Elements.Fire] = 0;
            info.elements[SagaLib.Elements.Water] = 70;
            info.elements[SagaLib.Elements.Wind] = 0;
            info.elements[SagaLib.Elements.Earth] = 0;
            info.elements[SagaLib.Elements.Holy] = 0;
            info.elements[SagaLib.Elements.Dark] = 0;
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
            newDrop.ItemID = 910000000;//掉落物品ID
            newDrop.Rate = 5000;//掉落幾率,10000是100%，5000是50%
            info.dropItems.Add(newDrop);

            newDrop.ItemID = 940000003;//掉落物品ID
            newDrop.Rate = 5000;//掉落幾率
            newDrop.Party = true;
            info.dropItems.Add(newDrop);
            /*---------物理掉落---------*/

            return info;
        }
        AIMode 巨型北极熊AI()
        {
            AIMode ai = new AIMode(0); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            ai.MobID = 10960002;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 3;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 3;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

            /*---------冰冻毒气---------*/
            skillinfo.CD = 10;//技能CD
            skillinfo.Rate = 60;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(7555, skillinfo);//將這個技能加進進程技能表
            /*---------冰冻毒气---------*/

            /*---------旋風劍---------*/
            ai.SkillOfShort.Add(2116, skillinfo);//將這個技能加進進程技能表
            /*---------旋風劍---------*/

            /*---------魔法彈---------*/
            ai.SkillOfShort.Add(3001, skillinfo);//將這個技能加進遠程技能表，遠程
            /*---------魔法彈---------*/

            /*---------冰箭术---------*/
            ai.SkillOfShort.Add(7532, skillinfo);//將這個技能加進遠程技能表，遠程
            return ai;
        }
    }
}


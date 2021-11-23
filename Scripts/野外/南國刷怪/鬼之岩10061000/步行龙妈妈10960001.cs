
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
    public partial class MinamiArea1Spawns 
    {
        ActorMob.MobInfo 步行龙妈妈Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "步行龙妈妈";
            info.maxhp = 296450;
            info.speed = 504;
            info.atk_min = 441;
            info.atk_max = 891;
            info.matk_min = 371;
            info.matk_max = 750;
            info.def = 24;
            info.def_add = 49;
            info.mdef = 32;
            info.mdef_add = 123;
            info.hit_critical = 74;
            info.hit_magic = 125;
            info.hit_melee = 303;
            info.hit_ranged = 294;
            info.avoid_critical = 75;
            info.avoid_magic = 63;
            info.avoid_melee = 98;
            info.avoid_ranged = 100;
            info.Aspd = 660;
            info.Cspd = 750;
            info.elements[SagaLib.Elements.Neutral] = 0;
            info.elements[SagaLib.Elements.Fire] = 70;
            info.elements[SagaLib.Elements.Water] = 0;
            info.elements[SagaLib.Elements.Wind] = 70;
            info.elements[SagaLib.Elements.Earth] = 0;
            info.elements[SagaLib.Elements.Holy] = 40;
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
            newDrop.ItemID = 940000003;newDrop.Party = true;
            newDrop.Rate = 10000;
            info.dropItems.Add(newDrop);

            return info;
        }
        AIMode 步行龙妈妈AI()
        {
            AIMode ai = new AIMode(0); ai.MobID = 10000000; ai.isNewAI = true;
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 3;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 3;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

            /*---------居合斬---------*/
            skillinfo.CD = 3;//技能CD
            skillinfo.Rate = 60;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(2115, skillinfo);//將這個技能加進進程技能表
            /*---------居合斬---------*/

            /*---------旋風劍---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 3;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(2116, skillinfo);//將這個技能加進進程技能表
            /*---------旋風劍---------*/

            /*---------魔法彈---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 3;//技能CD
            skillinfo.Rate = 30;//釋放概率
            skillinfo.MaxHP = 100;//低於70%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfLong.Add(3001, skillinfo);//將這個技能加進遠程技能表，遠程
            /*---------魔法彈---------*/

            /*---------狂风之刃---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 3;//技能CD
            skillinfo.Rate = 30;//釋放概率
            skillinfo.MaxHP = 100;//低於70%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfLong.Add(3017, skillinfo);//將這個技能加進遠程技能表，遠程
            /*---------狂风之刃---------*/

            /*---------最近累吧---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 3;//技能CD
            skillinfo.Rate = 30;//釋放概率
            skillinfo.MaxHP = 70;//低於70%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfLong.Add(7535, skillinfo);//將這個技能加進遠程技能表，遠程
            /*---------最近累吧---------*/

            return ai;
        }
    }
}


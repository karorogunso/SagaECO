
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
    public partial class MinamiArea7Spawns 
    {
        ActorMob.MobInfo 雷王Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "雷王";
            info.maxhp = 345925;
            info.speed = 482;
            info.atk_min = 122;
            info.atk_max = 247;
            info.matk_min = 378;
            info.matk_max = 742;
            info.def = 40;
            info.def_add = 49;
            info.mdef = 61;
            info.mdef_add = 200;
            info.hit_critical = 86;
            info.hit_magic = 200;
            info.hit_melee = 445;
            info.hit_ranged = 454;
            info.avoid_critical = 87;
            info.avoid_magic = 99;
            info.avoid_melee = 148;
            info.avoid_ranged = 148;
            info.Aspd = 742;
            info.Cspd = 826;
            info.elements[SagaLib.Elements.Neutral] = 0;
            info.elements[SagaLib.Elements.Fire] = 70;
            info.elements[SagaLib.Elements.Water] = 0;
            info.elements[SagaLib.Elements.Wind] = 70;
            info.elements[SagaLib.Elements.Earth] = 0;
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

            newDrop = new MobData.DropData();
            newDrop.ItemID = 940000003;newDrop.Party = true;
            newDrop.Rate = 10000;
            info.dropItems.Add(newDrop);
            return info;
        }
        AIMode 雷王AI()
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


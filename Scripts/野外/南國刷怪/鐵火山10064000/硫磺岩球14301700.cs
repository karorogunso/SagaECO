
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
    public partial class MinamiArea2Spawns 
    {
        ActorMob.MobInfo 硫磺岩球Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "硫磺岩球";
            info.maxhp = 64125;
            info.speed = 336;
            info.atk_min = 272;
            info.atk_max = 534;
            info.matk_min = 29;
            info.matk_max = 60;
            info.def = 33;
            info.def_add = 227;
            info.mdef = 7;
            info.mdef_add = 75;
            info.hit_critical = 22;
            info.hit_magic = 74;
            info.hit_melee = 121;
            info.hit_ranged = 121;
            info.avoid_critical = 22;
            info.avoid_magic = 37;
            info.avoid_melee = 90;
            info.avoid_ranged = 89;
            info.Aspd = 649;
            info.Cspd = 550;
            info.elements[SagaLib.Elements.Neutral] = 0;
            info.elements[SagaLib.Elements.Fire] = 70;
            info.elements[SagaLib.Elements.Water] = 0;
            info.elements[SagaLib.Elements.Wind] = 0;
            info.elements[SagaLib.Elements.Earth] = 70;
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
            newDrop.ItemID = 910000000;
            newDrop.Rate = 7000;
            info.dropItems.Add(newDrop);

            newDrop = new MobData.DropData();
            newDrop.ItemID = 940000003;newDrop.Party = true;
            newDrop.Rate = 10000;
            info.dropItems.Add(newDrop);
            return info;
        }
        AIMode 硫磺岩球AI()
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


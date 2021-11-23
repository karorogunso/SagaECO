
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
    public partial class KitaArea5Spawns 
    {
        ActorMob.MobInfo 阿尔卡纳国王Info()
        {
ActorMob.MobInfo info = new ActorMob.MobInfo();
info.name = "∏阿尔卡纳国王";
info.maxhp = 299475;
info.speed = 504;
info.atk_min = 445;
info.atk_max = 900;
info.matk_min = 375;
info.matk_max = 757;
info.def = 24;
info.def_add = 49;
info.mdef = 31;
info.mdef_add = 122;
info.hit_critical = 74;
info.hit_magic = 126;
info.hit_melee = 300;
info.hit_ranged = 300;
info.avoid_critical = 75;
info.avoid_magic = 63;
info.avoid_melee = 100;
info.avoid_ranged = 99;
info.Aspd = 673;
info.Cspd = 742;
info.elements[SagaLib.Elements.Neutral] = 70;
info.elements[SagaLib.Elements.Fire] = 0;
info.elements[SagaLib.Elements.Water] = 0;
info.elements[SagaLib.Elements.Wind] = 0;
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
            /*---------物理掉落---------*/
            newDrop.ItemID = 940000003;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率
            newDrop.Party = true;
            info.dropItems.Add(newDrop);
            /*---------物理掉落---------*/

            return info;
        }
        AIMode 阿尔卡纳国王AI()
        {
            AIMode ai = new AIMode(1); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            ai.MobID = 10330001;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 3;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 3;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

            /*---------召唤阿尔卡纳侍卫---------*/
            skillinfo.CD = 160;//技能CD
            skillinfo.Rate = 60;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(7595, skillinfo);//將這個技能加進進程技能表
            /*---------召唤阿尔卡纳侍卫---------*/

            AIMode.SkilInfo skillinfo2 = new AIMode.SkilInfo();
            /*---------旋風劍---------*/
            skillinfo.CD = 10;//技能CD
            skillinfo.Rate = 40;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(2116, skillinfo2);//將這個技能加進進程技能表
            /*---------旋風劍---------*/

            /*---------各种十字架---------*/
            ai.SkillOfShort.Add(7674, skillinfo);
            ai.SkillOfShort.Add(7675, skillinfo);
            ai.SkillOfShort.Add(7676, skillinfo);
            ai.SkillOfShort.Add(7677, skillinfo);
            /*---------各种十字架---------*/

            /*---------恐怖咆哮---------*/
            ai.SkillOfShort.Add(7709, skillinfo);

            /*---------铁甲屏障---------*/
            ai.SkillOfShort.Add(7707, skillinfo);
            return ai;
        }
    }
}

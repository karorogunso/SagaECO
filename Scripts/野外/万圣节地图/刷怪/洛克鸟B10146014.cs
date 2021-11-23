
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
        ActorMob.MobInfo 小僵尸Info()
        {
ActorMob.MobInfo info = new ActorMob.MobInfo();
info.name = "小僵尸";
info.maxhp = 21400;
info.speed = 546;
info.atk_min = 542;
info.atk_max = 690;
info.matk_min = 324;
info.matk_max = 648;
info.def = 18;
info.def_add = 94;
info.mdef = 11;
info.mdef_add = 60;
info.hit_critical = 58;
info.hit_magic = 60;
info.hit_melee = 181;
info.hit_ranged = 181;
info.avoid_critical = 58;
info.avoid_magic = 29;
info.avoid_melee = 108;
info.avoid_ranged = 109;
info.Aspd = 676;
info.Cspd = 630;
info.elements[SagaLib.Elements.Neutral] = 0;
info.elements[SagaLib.Elements.Fire] = 0;
info.elements[SagaLib.Elements.Water] = 20;
info.elements[SagaLib.Elements.Wind] = 0;
info.elements[SagaLib.Elements.Earth] = 0;
info.elements[SagaLib.Elements.Holy] = 0;
info.elements[SagaLib.Elements.Dark] = 40;
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


            MobData.DropData newDrop = new MobData.DropData();
            /*---------物理掉落---------*/
            newDrop.ItemID = 910000058;//掉落物品ID
            newDrop.Rate = 8000;//掉落幾率,10000是100%，5000是50%
            info.dropItems.Add(newDrop);
            
            /*---------物理掉落---------*/

            return info;
        }
        AIMode 小僵尸AI()
        {
            AIMode ai = new AIMode(0); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            ai.MobID = 10146014;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 3;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 3;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();
            /*---------旋風劍---------*/
            skillinfo.CD = 10;//技能CD
            skillinfo.Rate = 40;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(2116, skillinfo);//將這個技能加進進程技能表
            /*---------旋風劍---------*/

            /*---------魔法彈---------*/
            ai.SkillOfLong.Add(3001, skillinfo);//將這個技能加進遠程技能表，遠程
            /*---------魔法彈---------*/
            return ai;
        }
    }
}

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
        ActorMob.MobInfo 黑暗之羽Info()
        {
ActorMob.MobInfo info = new ActorMob.MobInfo();
info.name = "黑暗之羽";
info.maxhp = 16636;
info.speed = 546;
info.atk_min = 321;
info.atk_max = 542;
info.matk_min = 419;
info.matk_max = 540;
info.def = 14;
info.def_add = 78;
info.mdef = 10;
info.mdef_add = 50;
info.hit_critical = 50;
info.hit_magic = 50;
info.hit_melee = 147;
info.hit_ranged = 151;
info.avoid_critical = 50;
info.avoid_magic = 24;
info.avoid_melee = 89;
info.avoid_ranged = 88;
info.Aspd = 642;
info.Cspd = 606;
info.elements[SagaLib.Elements.Neutral] = 0;
info.elements[SagaLib.Elements.Fire] = 0;
info.elements[SagaLib.Elements.Water] = 70;
info.elements[SagaLib.Elements.Wind] = 0;
info.elements[SagaLib.Elements.Earth] = 0;
info.elements[SagaLib.Elements.Holy] = 0;
info.elements[SagaLib.Elements.Dark] = 0;
info.abnormalstatus[SagaLib.AbnormalStatus.Confused] = 0;
info.abnormalstatus[SagaLib.AbnormalStatus.Frosen] = 0;
info.abnormalstatus[SagaLib.AbnormalStatus.Paralyse] = 0;
info.abnormalstatus[SagaLib.AbnormalStatus.Poisen] = 0;
info.abnormalstatus[SagaLib.AbnormalStatus.Silence] = 0;
info.abnormalstatus[SagaLib.AbnormalStatus.Sleep] = 0;
info.abnormalstatus[SagaLib.AbnormalStatus.Stone] = 0;
info.abnormalstatus[SagaLib.AbnormalStatus.Stun] = 0;
info.abnormalstatus[SagaLib.AbnormalStatus.鈍足] = 0;
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
        AIMode 黑暗之羽AI()
        {
            AIMode ai = new AIMode(0); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            ai.MobID = 10146000;//怪物ID
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
            ai.SkillOfShort.Add(17108, skillinfo);//將這個技能加進進程技能表
            /*---------居合斬---------*/
            return ai;
        }
    }
}

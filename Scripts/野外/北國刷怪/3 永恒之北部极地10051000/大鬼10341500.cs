
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
        ActorMob.MobInfo 大鬼Info()
        {
ActorMob.MobInfo info = new ActorMob.MobInfo();
info.name = "∏大鬼";
info.maxhp = 222705;
info.speed = 336;
info.atk_min = 360;
info.atk_max = 712;
info.matk_min = 40;
info.matk_max = 80;
info.def = 44;
info.def_add = 297;
info.mdef = 9;
info.mdef_add = 99;
info.hit_critical = 30;
info.hit_magic = 98;
info.hit_melee = 158;
info.hit_ranged = 156;
info.avoid_critical = 30;
info.avoid_magic = 49;
info.avoid_melee = 117;
info.avoid_ranged = 121;
info.Aspd = 698;
info.Cspd = 621;
info.elements[SagaLib.Elements.Neutral] = 70;
info.elements[SagaLib.Elements.Fire] = 70;
info.elements[SagaLib.Elements.Water] = 0;
info.elements[SagaLib.Elements.Wind] = 0;
info.elements[SagaLib.Elements.Earth] = 0;
info.elements[SagaLib.Elements.Holy] = 70;
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
            newDrop.Party = true;
            info.dropItems.Add(newDrop);

            newDrop.ItemID = 940000003;//掉落物品ID
            newDrop.Rate = 5000;//掉落幾率
            newDrop.Party = true;
            info.dropItems.Add(newDrop);
            /*---------物理掉落---------*/

            return info;
        }
        AIMode 大鬼AI()
        {
            AIMode ai = new AIMode(0);
            ai.MobID = 10341500;//怪物ID
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

            return ai;
        }
    }
}

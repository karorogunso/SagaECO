
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
    public partial class KitaArea2Spawns 
    {
        ActorMob.MobInfo 咕噜噜Info()
        {
ActorMob.MobInfo info = new ActorMob.MobInfo();
info.name = "咕噜噜";
info.maxhp = 4207;
info.speed = 420;
info.atk_min = 100;
info.atk_max = 200;
info.matk_min = 101;
info.matk_max = 200;
info.def = 18;
info.def_add = 98;
info.mdef = 18;
info.mdef_add = 99;
info.hit_critical = 19;
info.hit_magic = 101;
info.hit_melee = 98;
info.hit_ranged = 100;
info.avoid_critical = 20;
info.avoid_magic = 49;
info.avoid_melee = 50;
info.avoid_ranged = 50;
info.Aspd = 500;
info.Cspd = 495;
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
            newDrop.ItemID = 910000000;//掉落物品ID
            newDrop.Rate = 3500;//掉落幾率,10000是100%，5000是50%
            info.dropItems.Add(newDrop);

            /*---------物理掉落---------*/

            return info;
        }
        AIMode 咕噜噜AI()
        {
            AIMode ai = new AIMode(0); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            ai.MobID = 10000300;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 3;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 3;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();


            /*---------冰箭术---------*/
            skillinfo.CD = 12;//技能CD
            skillinfo.Rate = 30;//釋放概率
            skillinfo.MaxHP = 70;//低於70%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfLong.Add(7532, skillinfo);//將這個技能加進遠程技能表，遠程
            /*---------冰箭术---------*/
            return ai;
        }
    }
}

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
        ActorMob.MobInfo 木桩四属Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "30万血木桩（四属皆50）";
            info.maxhp = 300000;
            info.speed = 0;
            info.atk_min = 0;
            info.atk_max = 0;
            info.matk_min = 0;
            info.matk_max = 0;
            info.def = 0;
            info.def_add = 0;
            info.mdef = 0;
            info.mdef_add = 0;
            info.hit_critical = 0;
            info.hit_magic = 0;
            info.hit_melee = 0;
            info.hit_ranged = 0;
            info.avoid_critical = 0;
            info.avoid_magic = 0;
            info.avoid_melee = 0;
            info.avoid_ranged = 0;
            info.Aspd = 649;
            info.Cspd = 600;
            info.elements[SagaLib.Elements.Neutral] = 0;
            info.elements[SagaLib.Elements.Fire] = 50;
            info.elements[SagaLib.Elements.Water] = 50;
            info.elements[SagaLib.Elements.Wind] = 50;
            info.elements[SagaLib.Elements.Earth] = 50;
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
            info.baseExp = 1;
            info.jobExp = 1;


            MobData.DropData newDrop = new MobData.DropData();
            /*---------物理掉落---------*/
            /*---------物理掉落---------*/

            return info;
        }
        AIMode 木桩四属AI()
        {
            AIMode ai = new AIMode(6); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            ai.MobID = 10960002;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 3;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 3;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();


            return ai;
        }
    }
}


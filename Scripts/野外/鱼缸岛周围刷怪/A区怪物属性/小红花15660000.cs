
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
    public partial class YugangdaoASpawn
    {
        ActorMob.MobInfo 小红花Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "小红花";
            info.level = 60;
            info.maxhp = 1200;
            info.speed = 420;
            info.atk_min = 120;
            info.atk_max = 240;
            info.matk_min = 117;
            info.matk_max = 237;
            info.def = 0;
            info.def_add = 0;
            info.mdef = 0;
            info.mdef_add = 0;
            info.hit_critical = 23;
            info.hit_magic = 118;
            info.hit_melee = 118;
            info.hit_ranged = 120;
            info.avoid_critical = 0;
            info.avoid_magic = 0;
            info.avoid_melee = 0;
            info.avoid_ranged = 0;
            info.Aspd = 540;
            info.Cspd = 540;
            info.elements[SagaLib.Elements.Neutral] = 0;
            info.elements[SagaLib.Elements.Fire] = 0;
            info.elements[SagaLib.Elements.Water] = 70;
            info.elements[SagaLib.Elements.Wind] = 0;
            info.elements[SagaLib.Elements.Earth] = 0;
            info.elements[SagaLib.Elements.Holy] = 0;
            info.elements[SagaLib.Elements.Dark] = 0;
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



            /*---------シャボタンの花---------*/
            MobData.DropData newDrop = new MobData.DropData();
            newDrop.ItemID = 10092200;//掉落物品ID
            newDrop.Rate = 1000;//掉落幾率,10000是100%，5000是50%
            info.dropItems.Add(newDrop);

            /*---------植物的刺---------*/
            newDrop = new MobData.DropData();
            newDrop.ItemID = 10025205;//掉落物品ID
            newDrop.Rate = 3200;//掉落幾率,10000是100%，5000是50%
            info.dropItems.Add(newDrop);

            /*---------印章---------*/
            newDrop = new MobData.DropData();
            newDrop.ItemID = 10050908;//掉落物品ID
            newDrop.Rate = 50;//掉落幾率,10000是100%，5000是50%
            info.dropItems.Add(newDrop);

            return info;
        }
        AIMode 小红花AI()
        {
            AIMode ai = new AIMode(0); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
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


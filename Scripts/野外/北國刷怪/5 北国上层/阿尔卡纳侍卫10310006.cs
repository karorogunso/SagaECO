
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
        ActorMob.MobInfo 阿尔卡纳侍卫AInfo()
        {
ActorMob.MobInfo info = new ActorMob.MobInfo();
info.name = "阿尔卡纳侍卫";
info.maxhp = 8181;
info.speed = 504;
            info.atk_min = 700;//最低物理攻擊
            info.atk_max = 1000;//最高物理攻擊
            info.matk_min = 300;//最低魔法攻擊
            info.matk_max = 400;//最高物理攻擊
            info.def = 12;
info.def_add = 24;
info.mdef = 15;
info.mdef_add = 60;
info.hit_critical = 35;
info.hit_magic = 60;
info.hit_melee = 144;
info.hit_ranged = 142;
info.avoid_critical = 35;
info.avoid_magic = 29;
info.avoid_melee = 47;
info.avoid_ranged = 48;
info.Aspd = 489;
info.Cspd = 578;
info.elements[SagaLib.Elements.Neutral] = 70;
info.elements[SagaLib.Elements.Fire] = 0;
info.elements[SagaLib.Elements.Water] = 0;
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


            MobData.DropData newDrop = new MobData.DropData();
            /*---------物理掉落---------*/
            newDrop.ItemID = 910000000;//掉落物品ID
            newDrop.Rate = 5000;//掉落幾率,10000是100%，5000是50%
            info.dropItems.Add(newDrop);

            newDrop.ItemID = 10000104;//掉落物品ID
            newDrop.Rate = 5000;//掉落幾率
            info.dropItems.Add(newDrop);
            /*---------物理掉落---------*/

            return info;
        }
        AIMode 阿尔卡纳侍卫AAI()
        {
            AIMode ai = new AIMode(0); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            ai.MobID = 10310006;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
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
            skillinfo.CD = 3;//技能CD
            skillinfo.Rate = 40;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(2116, skillinfo);//將這個技能加進進程技能表
            /*---------旋風劍---------*/
            return ai;
        }
    }
}

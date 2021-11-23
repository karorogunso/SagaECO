
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
    public partial class KitaArea6Spawns 
    {
        ActorMob.MobInfo 樱桃兔Info()
        {
ActorMob.MobInfo info = new ActorMob.MobInfo();
info.name = "樱桃兔";
info.maxhp = 71750;
info.speed = 482;
            info.atk_min = 400;//最低物理攻擊
            info.atk_max = 500;//最高物理攻擊
            info.matk_min = 300;//最低魔法攻擊
            info.matk_max = 700;//最高物理攻擊
            info.def = 24;
info.def_add = 29;
info.mdef = 37;
info.mdef_add = 120;
info.hit_critical = 51;
info.hit_magic = 120;
info.hit_melee = 267;
info.hit_ranged = 264;
info.avoid_critical = 51;
info.avoid_magic = 58;
info.avoid_melee = 89;
info.avoid_ranged = 90;
info.Aspd = 630;
info.Cspd = 737;
info.elements[SagaLib.Elements.Neutral] = 0;
info.elements[SagaLib.Elements.Fire] = 40;
info.elements[SagaLib.Elements.Water] = 0;
info.elements[SagaLib.Elements.Wind] = 0;
info.elements[SagaLib.Elements.Earth] = 0;
info.elements[SagaLib.Elements.Holy] = 40;
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
info.baseExp = info.maxhp/3;
info.jobExp = info.maxhp/3;


            MobData.DropData newDrop = new MobData.DropData();
            /*---------物理掉落---------*/
            newDrop.ItemID = 910000000;//掉落物品ID
            newDrop.Rate = 5000;//掉落幾率,10000是100%，5000是50%
            newDrop.Party = true;
            info.dropItems.Add(newDrop);
            /*---------物理掉落---------*/

            return info;
        }
        AIMode 樱桃兔AI()
        {
            AIMode ai = new AIMode(17); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            ai.MobID = 10110100;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 3;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 3;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

            /*---------居合斬---------*/
            skillinfo.CD = 20;//技能CD
            skillinfo.Rate = 60;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(2115, skillinfo);//將這個技能加進進程技能表
            /*---------居合斬---------*/

            /*---------旋風劍---------*/
            ai.SkillOfShort.Add(2116, skillinfo);//將這個技能加進進程技能表
            /*---------旋風劍---------*/

            /*---------魔法彈---------*/
            ai.SkillOfShort.Add(3001, skillinfo);//將這個技能加進遠程技能表，遠程
            /*---------魔法彈---------*/

            return ai;
        }
    }
}

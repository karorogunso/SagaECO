
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
        ActorMob.MobInfo 椰子兔Info()
        {
ActorMob.MobInfo info = new ActorMob.MobInfo();
info.name = "椰子兔";
info.maxhp = 108405;
info.speed = 420;
            info.atk_min = 400;//最低物理攻擊
            info.atk_max = 500;//最高物理攻擊
            info.matk_min = 300;//最低魔法攻擊
            info.matk_max = 700;//最高物理攻擊
            info.def = 23;
info.def_add = 101;
info.mdef = 60;
info.mdef_add = 303;
info.hit_critical = 40;
info.hit_magic = 294;
info.hit_melee = 240;
info.hit_ranged = 242;
info.avoid_critical = 39;
info.avoid_magic = 148;
info.avoid_melee = 78;
info.avoid_ranged = 78;
info.Aspd = 609;
info.Cspd = 691;
info.elements[SagaLib.Elements.Neutral] = 0;
info.elements[SagaLib.Elements.Fire] = 0;
info.elements[SagaLib.Elements.Water] = 40;
info.elements[SagaLib.Elements.Wind] = 0;
info.elements[SagaLib.Elements.Earth] = 0;
info.elements[SagaLib.Elements.Holy] = 0;
info.elements[SagaLib.Elements.Dark] = 70;
info.abnormalstatus[SagaLib.AbnormalStatus.Confused] = 70;
info.abnormalstatus[SagaLib.AbnormalStatus.Frosen] = 70;
info.abnormalstatus[SagaLib.AbnormalStatus.Paralyse] = 70;
info.abnormalstatus[SagaLib.AbnormalStatus.Poisen] = 70;
info.abnormalstatus[SagaLib.AbnormalStatus.Silence] = 70;
info.abnormalstatus[SagaLib.AbnormalStatus.Sleep] = 70;
info.abnormalstatus[SagaLib.AbnormalStatus.Stone] = 70;
info.abnormalstatus[SagaLib.AbnormalStatus.Stun] = 70;
info.abnormalstatus[SagaLib.AbnormalStatus.鈍足] = 70;
info.baseExp = info.maxhp/3;
info.jobExp = info.maxhp/3;


            MobData.DropData newDrop = new MobData.DropData();
            /*---------物理掉落---------*/
            newDrop.ItemID = 910000000;//掉落物品ID
            newDrop.Rate = 9000;//掉落幾率,10000是100%，5000是50%
            newDrop.Party = true;
            info.dropItems.Add(newDrop);
            /*---------物理掉落---------*/

            return info;
        }
        AIMode 椰子兔AI()
        {
            AIMode ai = new AIMode(17); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            ai.MobID = 10111000;//怪物ID
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

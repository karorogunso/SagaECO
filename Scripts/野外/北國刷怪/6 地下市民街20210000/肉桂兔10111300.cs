
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
        ActorMob.MobInfo 肉桂兔Info()
        {
ActorMob.MobInfo info = new ActorMob.MobInfo();
info.name = "肉桂兔";
info.maxhp = 84250;
info.speed = 420;
            info.atk_min = 400;//最低物理攻擊
            info.atk_max = 500;//最高物理攻擊
            info.matk_min = 300;//最低魔法攻擊
            info.matk_max = 700;//最高物理攻擊
            info.def = 17;
info.def_add = 101;
info.mdef = 18;
info.mdef_add = 98;
info.hit_critical = 19;
info.hit_magic = 98;
info.hit_melee = 100;
info.hit_ranged = 100;
info.avoid_critical = 20;
info.avoid_magic = 49;
info.avoid_melee = 50;
info.avoid_ranged = 49;
info.Aspd = 505;
info.Cspd = 500;
info.elements[SagaLib.Elements.Neutral] = 0;
info.elements[SagaLib.Elements.Fire] = 0;
info.elements[SagaLib.Elements.Water] = 0;
info.elements[SagaLib.Elements.Wind] = 0;
info.elements[SagaLib.Elements.Earth] = 70;
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
        AIMode 肉桂兔AI()
        {
            AIMode ai = new AIMode(17); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            ai.MobID = 10111300;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 3;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 3;//遠程技能表最短釋放間隔，3秒一次

            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 10;//技能CD
            skillinfo.Rate = 30;//釋放概率
            skillinfo.MaxHP = 70;//低於70%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfLong.Add(3001, skillinfo);//將這個技能加進遠程技能表，遠程
            ai.SkillOfLong.Add(2115, skillinfo);//將這個技能加進遠程技能表，遠程
            ai.SkillOfLong.Add(16101, skillinfo);//將這個技能加進遠程技能表，遠程
            return ai;
        }
    }
}

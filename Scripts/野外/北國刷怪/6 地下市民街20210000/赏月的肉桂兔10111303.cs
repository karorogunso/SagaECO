
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
        ActorMob.MobInfo 赏月的肉桂兔Info()
        {
ActorMob.MobInfo info = new ActorMob.MobInfo();
info.name = "赏月的肉桂兔";
info.maxhp = 87725;
info.speed = 336;
            info.atk_min = 400;//最低物理攻擊
            info.atk_max = 500;//最高物理攻擊
            info.matk_min = 300;//最低魔法攻擊
            info.matk_max = 700;//最高物理攻擊
            info.def = 25;
info.def_add = 176;
info.mdef = 6;
info.mdef_add = 60;
info.hit_critical = 18;
info.hit_magic = 60;
info.hit_melee = 96;
info.hit_ranged = 95;
info.avoid_critical = 17;
info.avoid_magic = 29;
info.avoid_melee = 72;
info.avoid_ranged = 71;
info.Aspd = 578;
info.Cspd = 494;
info.elements[SagaLib.Elements.Neutral] = 0;
info.elements[SagaLib.Elements.Fire] = 0;
info.elements[SagaLib.Elements.Water] = 0;
info.elements[SagaLib.Elements.Wind] = 0;
info.elements[SagaLib.Elements.Earth] = 70;
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
        AIMode 赏月的肉桂兔AI()
        {
            AIMode ai = new AIMode(17); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            ai.MobID = 10111303;//怪物ID
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

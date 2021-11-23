
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
        ActorMob.MobInfo 逆袭的肉桂兔Info()
        {
ActorMob.MobInfo info = new ActorMob.MobInfo();
info.name = "∏逆袭的肉桂兔";
info.maxhp = 943250;
info.speed = 378;
info.atk_min = 378;
info.atk_max = 735;
info.matk_min = 349;
info.matk_max = 798;
info.def = 78;
info.def_add = 441;
info.mdef = 40;
info.mdef_add = 196;
info.hit_critical = 25;
info.hit_magic = 200;
info.hit_melee = 303;
info.hit_ranged = 303;
info.avoid_critical = 24;
info.avoid_magic = 100;
info.avoid_melee = 63;
info.avoid_ranged = 62;
info.Aspd = 550;
info.Cspd = 750;
info.elements[SagaLib.Elements.Neutral] = 70;
info.elements[SagaLib.Elements.Fire] = 0;
info.elements[SagaLib.Elements.Water] = 0;
info.elements[SagaLib.Elements.Wind] = 0;
info.elements[SagaLib.Elements.Earth] = 70;
info.elements[SagaLib.Elements.Holy] = 0;
info.elements[SagaLib.Elements.Dark] = 0;
info.abnormalstatus[SagaLib.AbnormalStatus.Confused] = 90;
info.abnormalstatus[SagaLib.AbnormalStatus.Frosen] = 90;
info.abnormalstatus[SagaLib.AbnormalStatus.Paralyse] = 90;
info.abnormalstatus[SagaLib.AbnormalStatus.Poisen] = 90;
info.abnormalstatus[SagaLib.AbnormalStatus.Silence] = 90;
info.abnormalstatus[SagaLib.AbnormalStatus.Sleep] = 90;
info.abnormalstatus[SagaLib.AbnormalStatus.Stone] = 90;
info.abnormalstatus[SagaLib.AbnormalStatus.Stun] = 90;
info.abnormalstatus[SagaLib.AbnormalStatus.鈍足] = 90;
info.baseExp = info.maxhp;
info.jobExp = info.maxhp;


            MobData.DropData newDrop = new MobData.DropData();
            /*---------物理掉落---------*/
            newDrop.ItemID = 940000003;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率
            newDrop.Party = true;
            info.dropItems.Add(newDrop);
            /*---------物理掉落---------*/

            return info;
        }
        AIMode 逆袭的肉桂兔AI()
        {
            AIMode ai = new AIMode(0); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            ai.MobID = 10111302;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 3;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 3;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

            skillinfo.CD = 8;//技能CD
            skillinfo.Rate = 30;//釋放概率
            skillinfo.MaxHP = 70;//低於70%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放

            /*---------大風車---------*/
            ai.SkillOfShort.Add(2503, skillinfo);
            /*---------恒星陨落---------*/
            ai.SkillOfShort.Add(3312, skillinfo);

            /*---------魔法反射---------*/
            ai.SkillOfShort.Add(3313, skillinfo);


            /*---------各种十字架---------*/
            ai.SkillOfShort.Add(7674, skillinfo);
            ai.SkillOfShort.Add(7675, skillinfo);
            ai.SkillOfShort.Add(7676, skillinfo);
            ai.SkillOfShort.Add(7677, skillinfo);
            /*---------各种十字架---------*/

            /*---------恐怖咆哮---------*/
            ai.SkillOfShort.Add(7709, skillinfo);

            /*---------铁甲屏障---------*/
            ai.SkillOfShort.Add(7707, skillinfo);
            return ai;
        }
    }
}

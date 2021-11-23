
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
        ActorMob.MobInfo 泰坦巨人Info()
        {
ActorMob.MobInfo info = new ActorMob.MobInfo();
info.name = "泰坦巨人";
info.maxhp = 216090;
info.speed = 336;
info.atk_min = 356;
info.atk_max = 712;
info.matk_min = 139;
info.matk_max = 280;
info.def = 43;
info.def_add = 297;
info.mdef = 9;
info.mdef_add = 100;
info.hit_critical = 29;
info.hit_magic = 100;
info.hit_melee = 160;
info.hit_ranged = 160;
info.avoid_critical = 29;
info.avoid_magic = 50;
info.avoid_melee = 121;
info.avoid_ranged = 120;
info.Aspd = 712;
info.Cspd = 615;
info.elements[SagaLib.Elements.Neutral] = 70;
info.elements[SagaLib.Elements.Fire] = 0;
info.elements[SagaLib.Elements.Water] = 70;
info.elements[SagaLib.Elements.Wind] = 0;
info.elements[SagaLib.Elements.Earth] = 0;
info.elements[SagaLib.Elements.Holy] = 0;
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
            info.dropItems.Add(newDrop);

            newDrop.ItemID = 10000104;//掉落物品ID
            newDrop.Rate = 4000;//掉落幾率
            newDrop.Party = true;
            info.dropItems.Add(newDrop);
            /*---------物理掉落---------*/

            return info;
        }
        AIMode 泰坦巨人AI()
        {
            AIMode ai = new AIMode(0); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            ai.MobID = 10340304;//怪物ID
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

            /*---------寒气漩涡---------*/
            ai.SkillOfShort.Add(7534, skillinfo);//將這個技能加進進程技能表
            /*---------寒气漩涡---------*/

            /*---------水灵精灵的愤怒---------*/
            ai.SkillOfShort.Add(7615, skillinfo);//將這個技能加進進程技能表
            /*---------水灵精灵的愤怒---------*/

            /*---------魔动剑---------*/
            ai.SkillOfShort.Add(3126, skillinfo);//將這個技能加進進程技能表
            /*---------魔动剑---------*/

            return ai;
        }
    }
}

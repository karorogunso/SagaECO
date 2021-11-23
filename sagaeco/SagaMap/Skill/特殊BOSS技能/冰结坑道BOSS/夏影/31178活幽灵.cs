using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31178
        : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        //活幽灵的属性用的还是活死人的，要注意！
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            byte x = SagaLib.Global.PosX16to8(sActor.X, map.Width);
            byte y = SagaLib.Global.PosY16to8(sActor.Y, map.Height);

            ActorMob mob = map.SpawnCustomMob(10000000, sActor.MapID, 20390051, 0, 0, x, y, 5, 1, 0, Info(), AI(), null, 0)[0];
            ((ActorEventHandlers.MobEventHandler)mob.e).AI.Master = sActor;
            mob.Owner = sActor;
            sActor.Slave.Add(mob);
            SkillHandler.Instance.ShowEffectByActor(mob, 4111);

            mob = map.SpawnCustomMob(10000000, sActor.MapID, 18520000,0,0, x, y, 5, 1, 0, Info(), AI(), null, 0)[0];
            ((ActorEventHandlers.MobEventHandler)mob.e).AI.Master = sActor;
            mob.Owner = sActor;
            sActor.Slave.Add(mob);
            SkillHandler.Instance.ShowEffectByActor(mob, 4111);

        }
       
        #region 活幽灵属性
        ActorMob.MobInfo Info()//怪物属性
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "活幽灵";
            info.maxhp = 30000;
            info.speed = 390;

            info.atk_min = 240;
            info.atk_max = 484;
            info.matk_min = 11400;
            info.matk_max = 13400;
            info.def = 21;
            info.def_add = 220;
            info.mdef = 21;
            info.mdef_add = 118;
            info.hit_critical = 33;
            info.hit_magic = 118;
            info.hit_melee = 118;
            info.hit_ranged = 120;
            info.avoid_critical = 24;
            info.avoid_magic = 59;
            info.avoid_melee = 60;
            info.avoid_ranged = 60;
            info.Aspd = 540;
            info.Cspd = 540;
            info.elements[SagaLib.Elements.Neutral] = 0;
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
            info.baseExp = 100;
            info.jobExp = 100;
            /*---------物理掉落---------*/

            return info;
        }
        AIMode AI()//怪物AI
        {
            AIMode ai = new AIMode(1);//1為主動，0為被動
            ai.AI = 1;
            ai.MobID = 10111302;//怪物ID
            ai.isNewAI = true;
            ai.Distance = 4;
            ai.ShortCD = 3;
            ai.LongCD = 3;
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

            /*---------自爆---------*/
            //skillinfo.CD = 3;//技能CD
            //skillinfo.Rate = 100;//釋放概率
            //skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            //skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            //ai.SkillOfShort.Add(31005, skillinfo);//將這個技能加進進程技能表
            return ai;
        }
        #endregion

        
    }
}

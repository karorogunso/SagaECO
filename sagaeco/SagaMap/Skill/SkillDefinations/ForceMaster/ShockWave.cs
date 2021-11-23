using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.ForceMaster
{
    class ShockWave : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            ShockWaves bs = new ShockWaves(args.skill, sActor, dActor, 2000+1000*level, 0, args);
            Skill.SkillHandler.ApplyAddition(sActor, bs);
        }
        class ShockWaves : DefaultBuff
        {
            public ShockWaves(SagaDB.Skill.Skill skill, Actor sActor, Actor dActor, int lifetime, int damage, SkillArg arg)
                : base(skill, sActor, dActor, "ShockWaves", lifetime, 1000, damage, arg)
            {

                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this.OnUpdate2 += this.TimerUpdate;

            }

            void StartEvent(Actor actor, DefaultBuff skill)
            {
            }

            void EndEvent(Actor actor, DefaultBuff skill)
            {

            }

            void TimerUpdate(Actor sActor, Actor dActor, DefaultBuff skill, SkillArg arg, int damage)
            {
                //测试去除技能同步锁ClientManager.EnterCriticalArea();
                try
                {
                    CreateCat(sActor, dActor, arg, skill.skill.Level);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                //测试去除技能同步锁ClientManager.LeaveCriticalArea();
            }
            void CreateCat(Actor sActor, Actor dActor, SkillArg args, byte level)
            {
                byte X = 0;
                byte Y = 0;
                Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                SkillHandler.Instance.GetTRoundPos(map, sActor, out X, out Y, 1);
                ActorSkill skill = new ActorSkill(args.skill, sactor);
                ActorMob actor = new ActorMob(49510039);
                actor.MapID = sActor.MapID;
                actor.X = SagaLib.Global.PosX8to16(X, map.Width);
                actor.Y = SagaLib.Global.PosY8to16(Y, map.Height);
                actor.e = new ActorEventHandlers.MobEventHandler(actor);

                skill.MapID = sActor.MapID;
                skill.X = SagaLib.Global.PosX8to16(X, map.Width);
                skill.Y = SagaLib.Global.PosY8to16(Y, map.Height);
                skill.e = new ActorEventHandlers.NullEventHandler();

                map.RegisterActor(actor);
                actor.invisble = false;
                map.OnActorVisibilityChange(actor);

                map.RegisterActor(skill);
                skill.invisble = false;
                map.OnActorVisibilityChange(skill);
                ActivatorA timer = new ActivatorA(skill,actor, sActor, dActor, args, level);
                timer.Activate();
            }
        }
        #endregion

    }
    class ActivatorA : MultiRunTask
    {
        ActorSkill BollSkill;
        ActorMob Boll;
        Actor sActor;
        Actor dActor;
        Map map;
        SkillArg arg;
        int count = 0;
        int countMax = 10;
        public ActivatorA(ActorSkill skill, ActorMob actor, Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            this.Boll = actor;
            this.BollSkill = skill;
            this.sActor = sActor;
            this.dActor = dActor;
            this.arg = args.Clone() ;
            map = Manager.MapManager.Instance.GetMap(actor.MapID);
            this.period = 1000;
            this.dueTime = 2000;
        }
        public override void CallBack()
        {
            try
            {
                if (count <= countMax)
                {
                    short DistanceA = Map.Distance(Boll, dActor);
                    if (DistanceA <= 900)//If mob is out the range that FireBolt can cast, skip out.
                    {
                        arg.skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(3001, 1);
                        arg.argType = SkillArg.ArgType.Active;//Configure the skillarg of firebolt, the caster is the skillactor of subsituted groove.
                        arg.sActor = Boll.ActorID;
                        arg.dActor = dActor.ActorID;
                        arg.x = SagaLib.Global.PosX16to8(Boll.X,map.Width);
                        arg.y = SagaLib.Global.PosY16to8(Boll.Y, map.Height);
                        SkillHandler.Instance.MagicAttack(sActor, dActor, arg, Elements.Fire, 0.6f);
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, arg, Boll, true);
                        if (arg.flag.Contains(AttackFlag.DIE | AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT))//If mob died,terminate the proccess.
                        {
                            map.DeleteActor(Boll);
                            map.DeleteActor(BollSkill);
                            this.Deactivate();
                        } 
                    }
                    count++;
                }
                else
                {
                    map.DeleteActor(Boll);
                    map.DeleteActor(BollSkill);
                    this.Deactivate();
                }
            }
            catch (Exception ex)
            {
                map.DeleteActor(Boll);
                map.DeleteActor(BollSkill);
                this.Deactivate();
            }
        }
    }
}

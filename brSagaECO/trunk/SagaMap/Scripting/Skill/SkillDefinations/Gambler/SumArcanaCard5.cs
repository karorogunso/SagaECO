using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaMap.ActorEventHandlers;
namespace SagaMap.Skill.SkillDefinations.Gambler
{
    /// <summary>
    /// 艾卡卡（アルカナカード）[接續技能]
    /// </summary>
    public class SumArcanaCard5 : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 12000;
            uint MobID = 10330006;//艾卡納王
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            ActorMob mob=map.SpawnMob(MobID,
                (short)(sActor.X + SagaLib.Global.Random.Next(1, 11)),
                (short)(sActor.Y + SagaLib.Global.Random.Next(1, 11)),
                2500, sActor);
            MobEventHandler mh = (MobEventHandler)mob.e;
            mh.AI.Mode = new SagaMap.Mob.AIMode(0);
            mh.AI.Mode.EventAttackingSkillRate = 0;
            SumArcanaCardBuff skill = new SumArcanaCardBuff(args, sActor, mob, lifetime);
            SkillHandler.ApplyAddition(sActor, skill);
        }
        public class SumArcanaCardBuff : DefaultBuff
        {
            ActorMob mob;
            SkillArg arg;
            public SumArcanaCardBuff(SkillArg  skill, Actor actor,ActorMob mob, int lifetime)
                : base(skill.skill , actor, "SumArcanaCard", lifetime,6000)
            {
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this.OnUpdate += this.TimeUpdate;
                this.mob = mob;
                arg = skill.Clone();
            }

            void StartEvent(Actor actor, DefaultBuff skill)
            {
                TimeUpdate(actor, skill);
            }
            void TimeUpdate(Actor actor, DefaultBuff skill)
            {
                int rate = 60;
                Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                List<Actor> affected = map.GetActorsArea(mob, 600, false);
                foreach (Actor act in affected)
                {
                    if (SkillHandler.Instance.CheckValidAttackTarget(actor, act))
                    {
                        if (SkillHandler.Instance.CanAdditionApply(actor, act, SkillHandler.DefaultAdditions.Confuse, rate))
                        {
                            Confuse skill2 = new Confuse(arg.skill, act, 3000);
                            SkillHandler.ApplyAddition(act, skill2);
                        }
                    }
                }
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, arg , actor, false);
            }
            void EndEvent(Actor actor, DefaultBuff skill)
            {
                Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                mob.ClearTaskAddition();
                map.DeleteActor(mob);
            }
        }
        #endregion
    }
}

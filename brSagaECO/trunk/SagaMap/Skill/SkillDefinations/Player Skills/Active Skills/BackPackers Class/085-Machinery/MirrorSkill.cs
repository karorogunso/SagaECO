
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Machinery
{
    /// <summary>
    /// 自爆（自爆）
    /// </summary>
    public class MirrorSkill : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 3000 * level;
            MirrorSkillBuff skill = new MirrorSkillBuff(args, dActor, lifetime);
            SkillHandler.ApplyAddition(dActor, skill);
        }
        public class MirrorSkillBuff : DefaultBuff
        {
            private SkillArg args;
            public MirrorSkillBuff(SkillArg args, Actor actor, int lifetime)
                : base(args.skill, actor, "MirrorSkill", lifetime)
            {
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this.args = args.Clone();
            }

            void StartEvent(Actor actor, DefaultBuff skill)
            {
            }

            void EndEvent(Actor actor, DefaultBuff skill)
            {
                float factor = (float)(SagaLib.Global.Random.Next(10, 400) / 100f);
                Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                List<Actor> affected = map.GetActorsArea(actor, 200, false);
                List<Actor> realAffected = new List<Actor>();
                foreach (Actor act in affected)
                {
                    if (SkillHandler.Instance.CheckValidAttackTarget(actor, act))
                    {
                        realAffected.Add(act);
                    }
                }
                SkillHandler.Instance.PhysicalAttack(actor, realAffected, args, SagaLib.Elements.Neutral, factor);
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, args, actor, false);
            }
        }
        #endregion
    }
}

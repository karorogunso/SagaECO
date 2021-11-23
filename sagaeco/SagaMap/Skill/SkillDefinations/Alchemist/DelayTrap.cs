
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Alchemist
{
    /// <summary>
    /// 計時炸彈（ディレイボム）
    /// </summary>
    public class DelayTrap : ISkill
    {
        
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            uint itemID=10022307;//計時炸彈
            if (SkillHandler.Instance.CountItem(sActor, itemID) > 0)
            {
                SkillHandler.Instance.TakeItem(sActor, itemID, 1);
                return 0;
            }
            return -12;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 2000 + 2000 * level;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            DelayTrapBuff skill = new DelayTrapBuff(args, sActor, lifetime);
            SkillHandler.ApplyAddition(sActor, skill);
        }
        public class DelayTrapBuff : DefaultBuff
        {
            private SkillArg args;
            private short x, y;
            public DelayTrapBuff(SkillArg args, Actor actor, int lifetime)
                : base(args.skill, actor, "DelayTrap", lifetime)
            {
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this.args = args.Clone();
            }

            void StartEvent(Actor actor, DefaultBuff skill)
            {
                x = actor.X;
                y = actor.Y;
            }

            void EndEvent(Actor actor, DefaultBuff skill)
            {
                int level = skill.skill.Level;
                
                Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                List<Actor> affected = map.GetActorsArea(x, y, 150, null);
                float factor = 1.0f + 1.0f * level;
                List<Actor> realAffected = new List<Actor>();
                foreach (Actor act in affected)
                {
                    if (SkillHandler.Instance.CheckValidAttackTarget(actor, act))
                    {
                        realAffected.Add(act);
                    }
                }
                factor *= (1f / realAffected.Count);
                SkillHandler.Instance.PhysicalAttack(actor, realAffected, args, SagaLib.Elements.Neutral, factor);
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, args, actor, true);
            }
        }

        #endregion
    }
}

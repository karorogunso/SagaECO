
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Command
{
    /// <summary>
    /// 大型地雷（クレイモアトラップ）
    /// </summary>
    public class ClayMore : Trap
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            uint itemID = 10022308;
            if (SkillHandler.Instance.CountItem(sActor, itemID) > 0)
            {
                SkillHandler.Instance.TakeItem(sActor, itemID, 1);
                return 0;
            }
            return -12;
        }

        public ClayMore()
            : base(true, 300, PosType.sActor)
        {

        }
        public override void BeforeProc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            LifeTime = 20000 + 1000 * level;
        }
        public override void ProcSkill(Actor sActor, Actor mActor, ActorSkill actor, SkillArg args, Map map, int level, float factor)
        {
            int lifetime = 1500;

            ClayMoreBuff skill = new ClayMoreBuff(args, sActor, actor, lifetime);
            SkillHandler.ApplyAddition(sActor, skill);

        }
        public class ClayMoreBuff : DefaultBuff
        {
            Actor sActor;
            SkillArg args;
            public ClayMoreBuff(SkillArg skill, Actor sActor, ActorSkill actor, int lifetime)
                : base(skill.skill, actor, "ClayMore", lifetime)
            {
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this.sActor = sActor;
                args = skill.Clone();
            }

            void StartEvent(Actor actor, DefaultBuff skill)
            {
            }

            void EndEvent(Actor actor, DefaultBuff skill)
            {
                float factor = 2.5f + 0.5f * skill.skill.Level;
                Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                List<Actor> affected = map.GetActorsArea(sActor, 350, false);
                List<Actor> realAffected = new List<Actor>();
                foreach (Actor act in affected)
                {
                    if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                    {
                        realAffected.Add(act);
                    }
                }
                SkillHandler.Instance.PhysicalAttack(sActor, realAffected, args, sActor.WeaponElement, factor);
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, args, actor, false);
            }
        }

    }
}
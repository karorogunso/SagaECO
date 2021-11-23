using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaDB.Mob;
namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// さよならランデヴー
    /// </summary>
    public class GoodByeRendezvous : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int rate = 50;
            float factor = 2.0f;
            int lifetime = 4000;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 300, false);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    realAffected.Add(act);
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, realAffected, args, SagaLib.Elements.Neutral, factor);
            foreach (Actor act in realAffected)
            {
                if (SkillHandler.Instance.isBossMob(act))
                {
                    continue;
                }
                if (act == sActor)
                {
                    continue;
                }
                if (!SkillHandler.Instance.CheckValidAttackTarget(sActor,act))
                {
                    continue;
                }
                if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.Stun, rate))
                {
                    Additions.Global.Stun skill1 = new SagaMap.Skill.Additions.Global.Stun(args.skill, act, lifetime);
                    SkillHandler.ApplyAddition(act, skill1);
                }
                if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.鈍足, rate))
                {
                    Additions.Global.MoveSpeedDown skill2 = new SagaMap.Skill.Additions.Global.MoveSpeedDown(args.skill, act, lifetime);
                    SkillHandler.ApplyAddition(act, skill2);
                }
                if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.Silence, rate))
                {
                    Additions.Global.Silence skill3 = new SagaMap.Skill.Additions.Global.Silence(args.skill, act, lifetime);
                    SkillHandler.ApplyAddition(act, skill3);
                }
                if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.CannotMove, rate))
                {
                    Additions.Global.CannotMove skill4 = new SagaMap.Skill.Additions.Global.CannotMove(args.skill, act, lifetime);
                    SkillHandler.ApplyAddition(act, skill4);
                }
                if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.Confuse, rate))
                {
                    Additions.Global.Confuse skill5 = new SagaMap.Skill.Additions.Global.Confuse(args.skill, act, lifetime);
                    SkillHandler.ApplyAddition(act, skill5);
                }
            }
        }
        #endregion
    }
}

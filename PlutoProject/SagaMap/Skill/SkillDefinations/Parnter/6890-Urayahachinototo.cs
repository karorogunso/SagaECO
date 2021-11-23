using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// 氷剣・ユルヤカヒノミコト
    /// </summary>
    public class Urayahachinototo : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 6.0f;

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 150, false);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    realAffected.Add(act);
                    if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.Frosen, 40))
                    {
                        Additions.Global.Freeze skill = new SagaMap.Skill.Additions.Global.Freeze(args.skill, act, 3000);
                        SkillHandler.ApplyAddition(act, skill);
                    }
                }
            }
            SkillHandler.Instance.PhysicalAttack(sActor, realAffected, args,SagaLib.Elements.Water, factor);
        }
        #endregion
    }
}

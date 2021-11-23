
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Blacksmith
{
    /// <summary>
    /// 大地崩裂（アースクラッシュ）
    /// </summary>
    public class EearthCrash : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 2.8f + 0.7f * level;
            int lifetime = 2000 + 1000 * level;
            int rate = 32 + 4 * level;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 150, false);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    if(SkillHandler.Instance.CanAdditionApply(sActor,act, SkillHandler.DefaultAdditions.Confuse , rate))
                    {
                        Stun skill=new Stun(args.skill,act,lifetime);
                        SkillHandler.ApplyAddition(act,skill);
                    }
                    realAffected.Add(act);
                }
            }
            SkillHandler.Instance.PhysicalAttack(sActor, realAffected, args, sActor.WeaponElement, factor);
        }
        #endregion
    }
}
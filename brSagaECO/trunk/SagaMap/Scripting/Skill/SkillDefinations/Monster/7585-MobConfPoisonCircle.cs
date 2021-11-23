using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;

namespace SagaMap.Skill.SkillDefinations.Monster
{
    /// <summary>
    /// 全方位毒氣
    /// </summary>
    public class MobConfPoisonCircle : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int rate = 30;
            int lifetime = 3000;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 100, false);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.Confuse, rate))
                    {
                        Additions.Global.Confuse skill5 = new SagaMap.Skill.Additions.Global.Confuse(args.skill, act, lifetime);
                        SkillHandler.ApplyAddition(act, skill5);
                    }
                    if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.Poison, rate))
                    {
                        Additions.Global.Poison skill18 = new SagaMap.Skill.Additions.Global.Poison(args.skill, act, lifetime);
                        SkillHandler.ApplyAddition(act, skill18);
                    }
                }
            }
        }
        #endregion
    }
}
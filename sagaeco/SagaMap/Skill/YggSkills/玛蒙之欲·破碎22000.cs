using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S22000: ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("玛蒙之欲")) return -5;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            ActorPC pc = (ActorPC)sActor;
            int damage = pc.TInt["玛蒙之欲伤害"] / 5;
            SkillHandler.Instance.CauseDamage(sActor, dActor, damage);
            SkillHandler.Instance.ShowVessel(sActor, damage);
            pc.TInt["玛蒙之欲未解放"] = 0;
        }
        #endregion
    }
}

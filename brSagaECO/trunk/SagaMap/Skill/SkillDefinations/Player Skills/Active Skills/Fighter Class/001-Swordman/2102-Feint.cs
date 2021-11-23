using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Swordman
{
    /// <summary>
    /// 神隼之眼
    /// </summary>
    public class Feint:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("嘲讽CD"))
                return -30;
            else
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillCD skill2 = new SkillCD(args.skill, sActor, "嘲讽CD", 5000);
            SkillHandler.ApplyAddition(sActor, skill2);
            int[] array = { sActor.Status.max_atk1, sActor.Status.max_atk2, sActor.Status.max_atk3 };
            int damage = (int)(Max(array) * 15f);
            SkillHandler.Instance.AttractMob(sActor, dActor, damage);
            Manager.MapManager.Instance.GetMap(sActor.MapID).SendEffect(dActor, 4539);
        }
        int Max(int[] array)
        {
            int max = Int32.MinValue;
            foreach (int i in array)
            {
                if (i > max) max = i;
            }
            return max;
        }
        #endregion
    }
}

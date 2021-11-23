using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Swordman
{
    public class IaiForWeapon:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckValidAttackTarget(pc, dActor))
            {
                return 0;
            }
            else
            {
                return -14;
            }
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (sActor.EP >= 3)
            {
                sActor.EP -= 3;
                sActor.e.OnHPMPSPUpdate(sActor);


                List<Actor> da = new List<Actor>();
                da.Add(dActor);
                SkillHandler.Instance.PhysicalAttack(sActor, da, args, SkillHandler.DefType.Def, SagaLib.Elements.Neutral, 0, 1.5f, false, 0.05f, false);
            }
            else if (sActor.Status.Additions.ContainsKey("居合姿态启动"))
            {
                sActor.Status.Additions["居合姿态启动"].AdditionEnd();
                sActor.Status.Additions.Remove("居合姿态启动");
            }
        }
        #endregion
    }
}

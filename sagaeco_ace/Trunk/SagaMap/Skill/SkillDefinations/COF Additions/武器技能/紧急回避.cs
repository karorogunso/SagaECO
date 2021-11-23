using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Swordman
{
    public class EmergencyAvoid : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("居合姿态启动"))
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
            if (sActor.Status.Additions.ContainsKey("居合姿态启动"))
            {
                sActor.Status.Additions["居合姿态启动"].AdditionEnd();
                sActor.Status.Additions.Remove("居合姿态启动");
            }
            Skill.Additions.Global.DefaultBuff EmergencyAvoid = new Additions.Global.DefaultBuff(args.skill, sActor, "无敌", 500);
            SkillHandler.ApplyAddition(sActor, EmergencyAvoid);
        }
        #endregion
    }
}

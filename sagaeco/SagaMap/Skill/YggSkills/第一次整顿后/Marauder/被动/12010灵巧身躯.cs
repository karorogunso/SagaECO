using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12010 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = false;
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Job == PC_JOB.HAWKEYE)
                    active = true;

                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "灵巧身躯", active);
                skill.OnAdditionStart += (s, e) =>
                {
                    int AgiUpValue = 4 * level;
                    if (skill.Variable.ContainsKey("AgiUpValue"))
                        skill.Variable.Remove("AgiUpValue");
                    skill.Variable.Add("AgiUpValue", AgiUpValue);
                    sActor.Status.agi_skill += (short)AgiUpValue;

                    pc.TInt["灵巧身躯SP"] = 1 + 3* level;

                    /*pc.TInt["灵巧身躯间隔缩短"] = 5 + level * 5;
                    pc.TInt["灵巧身躯提升"] = level;
                    pc.TInt["灵巧身躯SPHEAL"] = 35 * level;
                    // 35 * level;*/
                };
                skill.OnAdditionEnd += (s, e) =>
                {
                    pc.TInt["灵巧身躯间隔缩短"] = 0;
                    int AgiUpValue = skill.Variable["AgiUpValue"];
                    sActor.Status.agi_skill -= (short)AgiUpValue;

                    pc.TInt["灵巧身躯SP"] = 0;
                };
                SkillHandler.ApplyAddition(sActor, skill);
            }
        }
    }
}

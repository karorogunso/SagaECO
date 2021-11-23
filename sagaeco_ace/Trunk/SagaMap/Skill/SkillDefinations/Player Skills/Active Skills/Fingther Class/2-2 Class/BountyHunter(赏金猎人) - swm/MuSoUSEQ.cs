using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaMap.Skill.SkillDefinations.BountyHunter
{
    public class MuSoUSEQ : ISkill
    {
        public int TryCast(SagaDB.Actor.ActorPC sActor, SagaDB.Actor.Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg args, byte level)
        {
            float[] factors = { 0, 0.2f, 0.45f, 0.7f, 0.95f, 1.2f };
            float factor = factors[level];
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.SLASH;
            if (sActor is ActorPC)
            {
                int lv = 0;
                ActorPC pc = sActor as ActorPC;
                if (pc.Skills3.ContainsKey(1117))
                {
                    lv = pc.Skills3[1117].Level;
                    factor += (0.1f + 0.1f *level);

                    //神速斩
                    SkillHandler.Instance.SetNextComboSkill(sActor, 2527);
                }
            }
            if (sActor.MuSoUCount == 10)
            {
                args.skill.BaseData.nAnim1 = args.skill.BaseData.nAnim2 = 332;
            }
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
            Stiff skill = new Stiff(args.skill, dActor, 1000);
            SkillHandler.ApplyAddition(dActor, skill);
            sActor.MuSoUCount++;
            if (sActor.MuSoUCount == 10)
                SkillHandler.Instance.PushBack(sActor, dActor, 2);
        }
    }
}

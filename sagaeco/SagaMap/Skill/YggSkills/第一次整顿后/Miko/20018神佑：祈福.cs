using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;


namespace SagaMap.Skill.SkillDefinations
{
    class S20018 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //治疗效果
            float healfactor = 2f + 0.5f * level;
            List<ActorPC> pcs = SkillHandler.Instance.GetPartyMembersAround(sActor, 1000, true);
            foreach (var item in pcs)
            {
                OtherAddition autoheal = new OtherAddition(null, item, "神佑：祈福", 30000, 5000);
                autoheal.OnUpdate += (s, e) =>
                {
                    SkillHandler.Instance.DoDamage(false, sActor, item, null, SkillHandler.DefType.IgnoreAll, Elements.Holy, 50, -healfactor);
                };
                SkillHandler.ApplyBuffAutoRenew(item, autoheal);
            }
        }
    }
}
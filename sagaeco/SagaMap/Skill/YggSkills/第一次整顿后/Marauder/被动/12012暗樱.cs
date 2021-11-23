using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12012 : ISkill
    {
        #region ISkill Members

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
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND)&& pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.doubleHand)
                    active = true;
            }
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "暗樱", active);

            skill.OnAdditionStart += (s, e) =>
            {
                int StrUpValue = 5 + 5 * level;
                if (skill.Variable.ContainsKey("StrUpValue"))
                    skill.Variable.Remove("StrUpValue");
                skill.Variable.Add("StrUpValue", StrUpValue);
                sActor.Status.str_skill += (short)StrUpValue;
                sActor.TInt["暗樱提升%"] = 5 + 5 * level;
            };
            skill.OnAdditionEnd += (s, e) =>
            {
                int StrUpValue = skill.Variable["StrUpValue"];
                sActor.Status.str_skill -= (short)StrUpValue;
                sActor.TInt["暗樱提升%"] = 0;
            };
            SkillHandler.ApplyAddition(sActor, skill);
        }

        #endregion
    }
}

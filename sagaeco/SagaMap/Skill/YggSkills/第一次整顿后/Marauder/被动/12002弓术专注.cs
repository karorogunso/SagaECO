using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12002 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = false;
            sActor.TInt["弓术专注暴击伤害"] = 0;
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                {
                    if ((pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BOW) && pc.TInt["斥候远程模式"] == 1)
                    {
                        active = true;
                        sActor.TInt["弓术专注暴击伤害"] = 5 + level * 5;
                    }
                }
            }
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "弓术专注", active);
            skill.OnAdditionStart += (s, e) =>
            {
            };
            skill.OnAdditionEnd += (s, e) =>
            {
            };
            SkillHandler.ApplyAddition(sActor, skill);
        }
    }
}

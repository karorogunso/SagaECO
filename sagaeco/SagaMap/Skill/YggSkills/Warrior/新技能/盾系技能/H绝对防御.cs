using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S18701 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
            {
                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType != SagaDB.Item.ItemType.SHIELD)
                    return -5;
            }
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Freeze fz = new Freeze(args.skill, sActor, 6000, 0);
            SkillHandler.ApplyAddition(sActor, fz);
            DefaultBuff skill = new DefaultBuff(args.skill, sActor, "Invincible", 6000);
            SkillHandler.ApplyAddition(dActor, skill);
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.C1skill
{
    public class SakuraSword : ISkill
    {
        public int TryCast(ActorPC pc, Actor dA, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = false;
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SWORD ||
                    pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHORT_SWORD ||
                    pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.EXSWORD ||
                    pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.CSWAR_MARIO)
                {
                    active = true;
                }
                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "樱花剑法", active);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(sActor, skill);
            }
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.Weapon_add += 0.1f + 0.08f * skill.skill.Level;
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.Weapon_add -= 0.1f + 0.08f * skill.skill.Level;
        }
    }
}

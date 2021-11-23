using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12000 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = false;
            ActorPC pc = (ActorPC)sActor;
            if (sActor.type == ActorType.PC)
            {
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                {
                    if ((pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.GUN ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.DUALGUN) && pc.TInt["斥候远程模式"] == 1)
                        active = true;
                }
            }
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "手枪律动", active);
            skill.OnAdditionStart += (s, e) =>
            {
                sActor.TInt["手枪律动范围"] = level * 10;
                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.DUALGUN)
                    sActor.TInt["手枪律动范围"] += 5;
            };
            skill.OnAdditionEnd += (s, e) =>
            {
                sActor.TInt["手枪律动范围"] = 0;
            };
            SkillHandler.ApplyAddition(sActor, skill);
        }
    }
}

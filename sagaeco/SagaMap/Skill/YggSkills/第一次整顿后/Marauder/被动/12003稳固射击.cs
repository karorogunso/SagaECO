using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12003 : ISkill
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
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                {
                    if ((pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.RIFLE ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.GUN ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.DUALGUN ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BOW)&& pc.TInt["斥候远程模式"] == 1)
                        active = true;
                }
            }
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "稳固射击", active);
            skill.OnAdditionStart += (s, e) =>
            {
                sActor.TInt["稳固射击范围增加"] = level;
                if (sActor.TInt["稳固射击暴击"] == 0)
                {
                    sActor.TInt["稳固射击暴击"] = 15 + 5 * level;
                    sActor.Status.hit_critical_skill += (short)sActor.TInt["稳固射击暴击"];
                }
            };
            skill.OnAdditionEnd += (s, e) =>
            {
                sActor.TInt["稳固射击范围增加"] = 0;
                sActor.Status.hit_critical_skill -= (short)sActor.TInt["稳固射击暴击"];
                sActor.TInt["稳固射击暴击"] = 0;
            };
            SkillHandler.ApplyAddition(sActor, skill);
        }
    }
}

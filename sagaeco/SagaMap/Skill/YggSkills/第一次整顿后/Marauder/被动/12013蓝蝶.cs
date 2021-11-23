using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12013 : ISkill
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
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                {
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.RIFLE ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.GUN ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.DUALGUN ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BOW)
                        active = true;
                }
            }
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "蓝蝶", active);
            skill.OnAdditionStart += (s, e) =>
            {
                int MagUpValue = 5 + 5 * level;
                if (skill.Variable.ContainsKey("MagUpValue"))
                    skill.Variable.Remove("MagUpValue");
                skill.Variable.Add("MagUpValue", MagUpValue);
                sActor.Status.mag_skill += (short)MagUpValue;
                sActor.TInt["蓝蝶提升%"] = MagUpValue; //偷懒！
            };
            skill.OnAdditionEnd += (s, e) =>
            {
                int MagUpValue = skill.Variable["MagUpValue"];
                sActor.Status.mag_skill -= (short)MagUpValue;
                sActor.TInt["蓝蝶提升%"] = 0;
            };
            SkillHandler.ApplyAddition(sActor, skill);
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Archer
{
    public class ConArrow: ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (!pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                return -5;
            else
            {
                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType != SagaDB.Item.ItemType.BOW ||  SkillHandler.Instance.CheckDEMRightEquip(pc, SagaDB.Item.ItemType.PARTS_BLOW))
                    return -5;
                else
                    return 0;
            }
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int combo = 2;
            float factor = 0;
            args.argType = SkillArg.ArgType.Attack;
            switch (level)
            {
                case 1:
                    factor = 1.10f;
                    break;
                case 2:
                    factor = 1.21f;
                    break;
                case 3:
                    factor = 1.32f;
                    break;
                case 4:
                    factor = 1.43f;
                    break;
                case 5:
                    factor = 1.54f;
                    break;
            }

            List<Actor> target = new List<Actor>();
            for (int i = 0; i < combo; i++)
            {
                target.Add(dActor);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, target, args, SagaLib.Elements.Neutral, factor);
        }
        #endregion
    }
}

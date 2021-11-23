using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Knight
{
    public class HolyBlade : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (CheckPossible(pc))
                return 0;
            else
                return -5;
        }

        bool CheckPossible(Actor sActor)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SWORD || SkillHandler.Instance.CheckDEMRightEquip(sActor, SagaDB.Item.ItemType.PARTS_BLOW) || pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.RAPIER)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return true;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0;
            if (CheckPossible(sActor))
            {
                args.type = ATTACK_TYPE.SLASH;
                factor = 1.3f + 0.3f * level;
                if (level == 6)
                {
                    factor = 5f;
                    Skill.Additions.Global.Silence silence = new Additions.Global.Silence(args.skill, dActor, 1500);
                    SkillHandler.ApplyAddition(dActor, silence);
                    SkillHandler.Instance.ShowEffect((ActorPC)sActor, dActor, 4282);
                }
                SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Holy, factor);

            }
        }

        #endregion
    }
}

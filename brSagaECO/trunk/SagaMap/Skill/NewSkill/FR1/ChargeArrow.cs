using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Archer
{
    /// <summary>
    /// 沖擊之箭
    /// </summary>
    public class ChargeArrow: ISkill
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
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BOW ||  SkillHandler.Instance.CheckDEMRightEquip(sActor, SagaDB.Item.ItemType.PARTS_BLOW))
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
            factor = 1.10f + 0.30f * level;
            if (level == 6)
            {
                factor = 3.5f;
                SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
                if (((args.flag[0] & SagaLib.AttackFlag.HP_DAMAGE) != 0))
                {
                    SkillHandler.Instance.PushBack(sActor, dActor, 3);
                    Additions.Global.Stiff skill = new SagaMap.Skill.Additions.Global.Stiff(args.skill, dActor, 2000);
                    SkillHandler.ApplyAddition(dActor, skill);
                }
            }
            else
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
        }
        #endregion
    }
}

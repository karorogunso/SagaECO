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
    public class ChargeArrow : ISkill
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
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BOW || SkillHandler.Instance.CheckDEMRightEquip(sActor, SagaDB.Item.ItemType.PARTS_BLOW))
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
            byte[] push = { 0, 3, 3, 4, 4, 5 };
            if (SkillHandler.Instance.isBossMob(dActor))
            {
                SkillHandler.Instance.PushBack(sActor, dActor, push[level]);
                Additions.Global.硬直 skill = new SagaMap.Skill.Additions.Global.硬直(args.skill, dActor, 500);
                SkillHandler.ApplyAddition(dActor, skill);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
        }
        #endregion
    }
}

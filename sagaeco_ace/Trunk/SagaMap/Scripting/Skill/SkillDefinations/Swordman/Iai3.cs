using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Swordman
{
    /// <summary>
    /// 居和3段
    /// </summary>
    public class Iai3:ISkill
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
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND) || pc.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2).Count > 0)
                    return true;
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
                SkillHandler.Instance.SetNextComboSkill(sActor, 2396);
                SkillHandler.Instance.SetNextComboSkill(sActor, 2109);
                SkillHandler.Instance.SetNextComboSkill(sActor, 2354);
                SkillHandler.Instance.SetNextComboSkill(sActor, 2116);
                SkillHandler.Instance.SetNextComboSkill(sActor, 2187);
                args.type = ATTACK_TYPE.SLASH;
                factor = 1.4f + 0.3f * level;
                SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
                if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.Stun, 2))
                {
                    Additions.Global.Stun skill = new SagaMap.Skill.Additions.Global.Stun(args.skill, dActor, 3000);
                    SkillHandler.ApplyAddition(dActor, skill);
                }
            }
        }

        #endregion
    }
}

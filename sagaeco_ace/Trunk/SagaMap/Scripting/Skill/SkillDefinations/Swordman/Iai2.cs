using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Swordman
{
    /// <summary>
    /// 居和2段
    /// </summary>
    public class Iai2:ISkill
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
                //允许的组合技，居合2段
                SkillHandler.Instance.SetNextComboSkill(sActor, 2202);
                args.type = ATTACK_TYPE.SLASH;
                factor = 1.2f + 0.25f * level;
                SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
            }
        }

        #endregion
    }
}

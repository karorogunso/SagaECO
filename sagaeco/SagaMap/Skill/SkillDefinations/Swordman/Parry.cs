using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaDB.Item;

namespace SagaMap.Skill.SkillDefinations.Swordman
{
    /// <summary>
    /// 防衛板
    /// </summary>
    public class Parry:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            SagaDB.Item.ItemType[] its = { ItemType.SWORD, ItemType.AXE, ItemType.SHORT_SWORD, ItemType.SPEAR };
            if (!SkillHandler.Instance.CheckSkillCanCastForWeapon(pc, args))
            {
            //    if (!SkillHandler.Instance.CheckWeapon(pc, its))
                return -5;
            }
            if (CheckPossible(pc) && args.result != -5)
            {
                DefaultBuff skill = new DefaultBuff(args.skill, pc, "Parry", 2000);
                SkillHandler.ApplyAddition(dActor, skill);
                if (pc.Status.Additions.ContainsKey("Parry"))
                    return 0;
                else return -1;
            }
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
            //如果有防御板技能
            if (sActor.Status.Additions.ContainsKey("Parry"))
                sActor.Status.Additions["Parry"].AdditionEnd();
        }

        #endregion
    }
}

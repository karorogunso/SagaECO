using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaDB.Item;
namespace SagaMap.Skill.SkillDefinations
{
    public class S40302 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
            {
                if(pc.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].BaseData.itemType != ItemType.SHIELD)
                return -5;
            }
            if (args.result != -5)
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


        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //如果有防御板技能
            if (sActor.Status.Additions.ContainsKey("Parry"))
                sActor.Status.Additions["Parry"].AdditionEnd();
        }
        #endregion
    }
}
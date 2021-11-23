
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Fencer
{
    /// <summary>
    /// 盾術修練（シールドマスタリー）
    /// </summary>
    public class ShieldMastery : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (sActor.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
            {
                if (sActor.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHIELD)
                    return 0;
            }
            return -1;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = true;
            ActorPC pc = sActor as ActorPC;
            if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                active = (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHIELD);

            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "ShieldMastery", active);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        public void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int guradadd = 5 * skill.skill.Level;
            if (skill.Variable.ContainsKey("ShieldMastery"))
                skill.Variable.Remove("ShieldMastery");
            skill.Variable.Add("ShieldMastery", guradadd);
            actor.Status.guard_skill += (short)guradadd;
        }
        public void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int value2 = skill.Variable["ShieldMastery"];
            actor.Status.guard_skill -= (short)value2;

        }
        #endregion
    }
}

                                                          
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Wizard
{
    public class WandMaster : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = false;
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND)&&
                    pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.STAFF)
                {
                    active = true;
                }
                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "WandMaster", active);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(sActor, skill);
            }
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            float rate = 0.02f * skill.skill.Level;
            int add = (int)(((ActorPC)actor).Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.matk * rate);
            if (skill.Variable.ContainsKey("WandMasterMatk"))
                skill.Variable.Remove("WandMasterMatk");
            skill.Variable.Add("WandMasterMatk", add);
            actor.Status.min_matk_skill += (short)add;
            actor.Status.max_matk_skill += (short)add;
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.min_matk_skill -= (short)skill.Variable["WandMasterMatk"];
            actor.Status.max_matk_skill -= (short)skill.Variable["WandMasterMatk"];
        }
        #endregion
    }
}

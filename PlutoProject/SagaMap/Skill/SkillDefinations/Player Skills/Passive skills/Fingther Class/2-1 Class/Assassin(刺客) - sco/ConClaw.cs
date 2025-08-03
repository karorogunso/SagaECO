using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Assassin
{
    public class ConClaw : ISkill
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
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.CLAW)
                    {
                        active = true;
                    }
                }
                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "ConClaw", active);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(sActor, skill);
            }
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int level = skill.skill.Level;
            actor.Status.combo_rate_skill += (short)(25 + 5 * level);
            actor.Status.combo_skill = 3;

        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int level = skill.skill.Level;
            actor.Status.combo_rate_skill -= (short)(25 + 5 * level);
            actor.Status.combo_skill = 2;
        }
        #endregion
    }
}

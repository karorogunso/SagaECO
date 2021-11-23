using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Global
{
    public class ShortSwordMastery:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = false;
            bool active2 = false;
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHORT_SWORD)
                    {
                        active = true;
                        DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "ShortSwordMastery", active);
                        skill.OnAdditionStart += this.StartEventHandler;
                        skill.OnAdditionEnd += this.EndEventHandler;
                        SkillHandler.ApplyAddition(sActor, skill);
                    }
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.CLAW)
                    {
                        active2 = true;
                        DefaultPassiveSkill skill2 = new DefaultPassiveSkill(args.skill, sActor, "ConClaw", active2);
                        skill2.OnAdditionStart += this.StartEventHandler2;
                        skill2.OnAdditionEnd += this.EndEventHandler2;
                        SkillHandler.ApplyAddition(sActor, skill2);
                    }
                }



            }
        }
        void StartEventHandler2(Actor actor, DefaultPassiveSkill skill)
        {
            int level = skill.skill.Level;
            actor.Status.combo_rate_skill += (short)(12 * level);
            actor.Status.combo_skill = 3;

        }

        void EndEventHandler2(Actor actor, DefaultPassiveSkill skill)
        {
            int level = skill.skill.Level;
            actor.Status.combo_rate_skill -= (short)(12 * level);
            actor.Status.combo_skill = 2;
        }

        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int level = skill.skill.Level;
            actor.Status.combo_rate_skill += (short)(9 * level);
            actor.Status.combo_skill = 2;
        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int level = skill.skill.Level;
            actor.Status.combo_rate_skill -= (short)(9 * level);
            actor.Status.combo_skill = 1;
        }

        #endregion
    }
}

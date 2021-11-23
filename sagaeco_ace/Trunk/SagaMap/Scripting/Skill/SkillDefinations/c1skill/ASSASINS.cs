using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.C1skill
{
    public class ASSASINS : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
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
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.CLAW 
                        || pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHORT_SWORD)
                    {
                        active = true;
                    }
                }
                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "ASSASINS", active);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(sActor, skill);
            }
        }

        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {

            if (actor.type == ActorType.PC)
            {
                if (((ActorPC)actor).Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.CLAW)
                {
                    int cspd_add;
                    cspd_add = (int)(actor.Status.cspd * (skill.skill.Level * 0.08f));
                    if (skill.Variable.ContainsKey("ASSASINS_CSPD"))
                        skill.Variable.Remove("ASSASINS_CSPD");
                    skill.Variable.Add("ASSASINS_CSPD", cspd_add);
                    actor.Status.cspd_skill += (short)cspd_add;
                }
                else if (((ActorPC)actor).Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHORT_SWORD)
                {
                    int max_atk1, max_atk2, max_atk3, min_atk1, min_atk2, min_atk3;
                    max_atk1 = (int)(actor.Status.max_atk1 * (skill.skill.Level * 0.03f + 0.05f));
                    max_atk2 = (int)(actor.Status.max_atk1 * (skill.skill.Level * 0.03f + 0.05f));
                    max_atk3 = (int)(actor.Status.max_atk1 * (skill.skill.Level * 0.03f + 0.05f));
                    min_atk1 = (int)(actor.Status.max_atk1 * (skill.skill.Level * 0.03f + 0.05f));
                    min_atk2 = (int)(actor.Status.max_atk1 * (skill.skill.Level * 0.03f + 0.05f));
                    min_atk3 = (int)(actor.Status.max_atk1 * (skill.skill.Level * 0.03f + 0.05f));
                    if (skill.Variable.ContainsKey("ASSASINS_max_atk1"))
                        skill.Variable.Remove("ASSASINS_max_atk1");
                    skill.Variable.Add("ASSASINS_max_atk1", max_atk1);
                    if (skill.Variable.ContainsKey("ASSASINS_max_atk2"))
                        skill.Variable.Remove("ASSASINS_max_atk2");
                    skill.Variable.Add("ASSASINS_max_atk1", max_atk2);
                    if (skill.Variable.ContainsKey("ASSASINS_max_atk3"))
                        skill.Variable.Remove("ASSASINS_max_atk3");
                    skill.Variable.Add("ASSASINS_max_atk1", max_atk3);

                    if (skill.Variable.ContainsKey("ASSASINS_min_atk1"))
                        skill.Variable.Remove("ASSASINS_min_atk1");
                    skill.Variable.Add("ASSASINS_max_atk1", min_atk1);
                    if (skill.Variable.ContainsKey("ASSASINS_min_atk2"))
                        skill.Variable.Remove("ASSASINS_min_atk1");
                    skill.Variable.Add("ASSASINS_max_atk1", min_atk2);
                    if (skill.Variable.ContainsKey("ASSASINS_max_atk1"))
                        skill.Variable.Remove("ASSASINS_max_atk1");
                    skill.Variable.Add("ASSASINS_max_atk1", min_atk3);
                }
            }
        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            if (actor.type == ActorType.PC)
            {
                int value = skill.Variable["MasteryATK"];
                actor.Status.min_atk1_skill -= (short)value;
                actor.Status.min_atk2_skill -= (short)value;
                actor.Status.min_atk3_skill -= (short)value;
                value = skill.Variable["MasteryHIT"];
                actor.Status.hit_melee_skill -= (short)value;
            }
        }

        #endregion
    }
}

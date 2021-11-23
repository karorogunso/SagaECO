using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Eraser
{
    public class EraserMaster :ISkill
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
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHORT_SWORD||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.CLAW
                        || pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.GUN
                        )
                    {
                        active = true;
                    }
                }
                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "EraserMaster", active);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(sActor, skill);
            }
        }

        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int value;
            value = 15 + skill.skill.Level * 15;

            if (skill.Variable.ContainsKey("EraserMasteryATK"))
                skill.Variable.Remove("EraserMasteryATK");
            skill.Variable.Add("EraserMasteryATK", value);
            actor.Status.min_atk2_skill += (short)value;
            actor.Status.min_atk3_skill += (short)value;
            actor.Status.max_atk2_skill += (short)value;
            actor.Status.max_atk3_skill += (short)value;

            int value_hit;
            value_hit = 10 + 6 * skill.skill.Level;
            if (skill.Variable.ContainsKey("EraserMasteryHIT"))
                skill.Variable.Remove("EraserMasteryHIT");
            skill.Variable.Add("EraserMasteryHIT", value_hit);
            actor.Status.hit_melee_skill += (short)value_hit;

            short cri;
            cri = (short)(3 * skill.skill.Level);
            if (skill.Variable.ContainsKey("EraserCriUp"))
                skill.Variable.Remove("EraserCriUp");
            skill.Variable.Add("EraserCriUp", cri);
            actor.Status.cri_skill += cri;

            int MartialArtDamUp = 128 + 6 * skill.skill.Level;
            if (skill.Variable.ContainsKey("EraserMasteryMartialArtDamUp"))
                skill.Variable.Remove("EraserMasteryMartialArtDamUp");
            skill.Variable.Add("EraserMasteryMartialArtDamUp", MartialArtDamUp);
            if (actor.Status.Additions.ContainsKey("MartialArtDamUp"))
                (actor.Status.Additions["MartialArtDamUp"] as DefaultPassiveSkill).Variable["MartialArtDamUp"] += MartialArtDamUp;
        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.min_atk2_skill -= (short)skill.Variable["EraserMasteryATK"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["EraserMasteryATK"];
            actor.Status.max_atk2_skill -= (short)skill.Variable["EraserMasteryATK"];
            actor.Status.max_atk3_skill -= (short)skill.Variable["EraserMasteryATK"];
            actor.Status.hit_melee_skill -= (short)skill.Variable["EraserMasteryHIT"];
            actor.Status.cri_skill -= (short)skill.Variable["EraserCriUp"];
            if (actor.Status.Additions.ContainsKey("MartialArtDamUp"))
                (actor.Status.Additions["MartialArtDamUp"] as DefaultPassiveSkill).Variable["MartialArtDamUp"] -= (short)skill.Variable["EraserMasteryMartialArtDamUp"];
            if (skill.Variable.ContainsKey("EraserMasteryMartialArtDamUp"))
                skill.Variable.Remove("EraserMasteryMartialArtDamUp");
        }

        #endregion
    }
}

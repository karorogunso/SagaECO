using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Global
{
    public class SwordMastery : ISkill
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
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SWORD
                        || pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.RAPIER
                        )
                    {
                        active = true;
                    }
                }
                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "SwordMastery", active);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(sActor, skill);
            }
        }

        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int MaxAttack = 5 * skill.skill.Level;
            if (skill.skill.Level == 5) MaxAttack += 5;
            int MinAttack = MaxAttack;


            //最大攻擊
            int max_atk1_add = (int)(MaxAttack);
            if (skill.Variable.ContainsKey("SwordMastery_max_atk1"))
                skill.Variable.Remove("SwordMastery_max_atk1");
            skill.Variable.Add("SwordMastery_max_atk1", max_atk1_add);
            actor.Status.max_atk1_skill += (short)max_atk1_add;

            //最大攻擊
            int max_atk2_add = (int)(MaxAttack);
            if (skill.Variable.ContainsKey("SwordMastery_max_atk2"))
                skill.Variable.Remove("SwordMastery_max_atk2");
            skill.Variable.Add("SwordMastery_max_atk2", max_atk2_add);
            actor.Status.max_atk2_skill += (short)max_atk2_add;

            //最大攻擊
            int max_atk3_add = (int)(MaxAttack);
            if (skill.Variable.ContainsKey("SwordMastery_max_atk3"))
                skill.Variable.Remove("SwordMastery_max_atk3");
            skill.Variable.Add("SwordMastery_max_atk3", max_atk3_add);
            actor.Status.max_atk3_skill += (short)max_atk3_add;

            //最小攻擊
            int min_atk1_add = (int)(MinAttack);
            if (skill.Variable.ContainsKey("SwordMastery_min_atk1"))
                skill.Variable.Remove("SwordMastery_min_atk1");
            skill.Variable.Add("SwordMastery_min_atk1", min_atk1_add);
            actor.Status.min_atk1_skill += (short)min_atk1_add;

            //最小攻擊
            int min_atk2_add = (int)(MinAttack);
            if (skill.Variable.ContainsKey("SwordMastery_min_atk2"))
                skill.Variable.Remove("SwordMastery_min_atk2");
            skill.Variable.Add("SwordMastery_min_atk2", min_atk2_add);
            actor.Status.min_atk2_skill += (short)min_atk2_add;

            //最小攻擊
            int min_atk3_add = (int)(MinAttack);
            if (skill.Variable.ContainsKey("SwordMastery_min_atk3"))
                skill.Variable.Remove("SwordMastery_min_atk3");
            skill.Variable.Add("SwordMastery_min_atk3", min_atk3_add);
            actor.Status.min_atk3_skill += (short)min_atk3_add;

            //近命中
            int hit_melee_add = 2 * skill.skill.Level;
            if (skill.skill.Level == 5) hit_melee_add += 2;
            if (skill.Variable.ContainsKey("SwordMastery_hit_melee"))
                skill.Variable.Remove("SwordMastery_hit_melee");
            skill.Variable.Add("SwordMastery_hit_melee", hit_melee_add);
            actor.Status.hit_melee_skill += (short)hit_melee_add;



            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_STATUS, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            //最大攻擊
            actor.Status.max_atk1_skill -= (short)skill.Variable["SwordMastery_max_atk1"];

            //最大攻擊
            actor.Status.max_atk2_skill -= (short)skill.Variable["SwordMastery_max_atk2"];

            //最大攻擊
            actor.Status.max_atk3_skill -= (short)skill.Variable["SwordMastery_max_atk3"];

            //最小攻擊
            actor.Status.min_atk1_skill -= (short)skill.Variable["SwordMastery_min_atk1"];

            //最小攻擊
            actor.Status.min_atk2_skill -= (short)skill.Variable["SwordMastery_min_atk2"];

            //最小攻擊
            actor.Status.min_atk3_skill -= (short)skill.Variable["SwordMastery_min_atk3"];

            //近命中
            actor.Status.hit_melee_skill -= (short)skill.Variable["SwordMastery_hit_melee"];


            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_STATUS, null, actor, true);
        }

        #endregion
    }
}

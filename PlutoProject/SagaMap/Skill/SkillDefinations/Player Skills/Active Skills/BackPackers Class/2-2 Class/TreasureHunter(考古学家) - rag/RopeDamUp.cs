
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.TreasureHunter
{
    /// <summary>
    /// 鞭子修練（ウィップマスタリー）
    /// </summary>
    public class RopeDamUp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (Skill.SkillHandler.Instance.isEquipmentRight(sActor, SagaDB.Item.ItemType.ROPE) || sActor.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2).Count > 0)
            {
                return 0;
            }
            return -5;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = false;
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.ROPE)
                    {
                        active = true;
                    }
                }
                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "RopeDamUp", active);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(sActor, skill);
            }

        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int level = skill.skill.Level;
            //最小攻擊
            int min_atk1_add = (int)(25 + 5 * level);
            if (skill.Variable.ContainsKey("RopeDamUp_min_atk1"))
                skill.Variable.Remove("RopeDamUp_min_atk1");
            skill.Variable.Add("RopeDamUp_min_atk1", min_atk1_add);
            actor.Status.min_atk1_skill += (short)min_atk1_add;

            //最小攻擊
            int min_atk2_add = (int)(25 + 5 * level);
            if (skill.Variable.ContainsKey("RopeDamUp_min_atk2"))
                skill.Variable.Remove("RopeDamUp_min_atk2");
            skill.Variable.Add("RopeDamUp_min_atk2", min_atk2_add);
            actor.Status.min_atk2_skill += (short)min_atk2_add;

            //最小攻擊
            int min_atk3_add = (int)(25 + 5 * level);
            if (skill.Variable.ContainsKey("RopeDamUp_min_atk3"))
                skill.Variable.Remove("RopeDamUp_min_atk3");
            skill.Variable.Add("RopeDamUp_min_atk3", min_atk3_add);
            actor.Status.min_atk3_skill += (short)min_atk3_add;

            //百分比近命中
            float hit_up = new float[] { 0, 0.02f, 0.03f, 0.05f, 0.08f, 0.12f }[level];
            int hit_melee_up = (int)(actor.Status.hit_melee_bs * hit_up);
            if (skill.Variable.ContainsKey("RopeDamUp_hit_melee"))
                skill.Variable.Remove("RopeDamUp_hit_melee");
            skill.Variable.Add("RopeDamUp_hit_melee", hit_melee_up);
            actor.Status.hit_melee_skill += (short)hit_melee_up;

            //近命中
            int hit_melee_add = (int)(2 * level);
            if (skill.Variable.ContainsKey("RopeDamUp_hit_melee_add"))
                skill.Variable.Remove("RopeDamUp_hit_melee_add");
            skill.Variable.Add("RopeDamUp_hit_melee_add", hit_melee_add);
            actor.Status.hit_melee_skill += (short)hit_melee_add;



        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            //最小攻擊
            actor.Status.min_atk1_skill -= (short)skill.Variable["RopeDamUp_min_atk1"];

            //最小攻擊
            actor.Status.min_atk2_skill -= (short)skill.Variable["RopeDamUp_min_atk2"];

            //最小攻擊
            actor.Status.min_atk3_skill -= (short)skill.Variable["RopeDamUp_min_atk3"];

            //百分比近命中
            actor.Status.hit_melee_skill -= (short)skill.Variable["RopeDamUp_hit_melee"];

            //近命中
            actor.Status.hit_melee_skill -= (short)skill.Variable["RopeDamUp_hit_melee_add"];

        }
        #endregion
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Gunner
{
    /// <summary>
    /// 步槍射擊練習（ライフルマスタリー）
    /// </summary>
    public class RifleGunDamUp : ISkill
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
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.RIFLE)
                    {
                        active = true;
                    }
                }
                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "RifleGunDamUp", active);
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
            if (skill.Variable.ContainsKey("RifleGunDamUp_min_atk1"))
                skill.Variable.Remove("RifleGunDamUp_min_atk1");
            skill.Variable.Add("RifleGunDamUp_min_atk1", min_atk1_add);
            actor.Status.min_atk1_skill += (short)min_atk1_add;

            //最小攻擊
            int min_atk2_add = (int)( 25 + 5 * level);
            if (skill.Variable.ContainsKey("RifleGunDamUp_min_atk2"))
                skill.Variable.Remove("RifleGunDamUp_min_atk2");
            skill.Variable.Add("RifleGunDamUp_min_atk2", min_atk2_add);
            actor.Status.min_atk2_skill += (short)min_atk2_add;

            //最小攻擊
            int min_atk3_add = (int)( 25 + 5 * level);
            if (skill.Variable.ContainsKey("RifleGunDamUp_min_atk3"))
                skill.Variable.Remove("RifleGunDamUp_min_atk3");
            skill.Variable.Add("RifleGunDamUp_min_atk3", min_atk3_add);
            actor.Status.min_atk3_skill += (short)min_atk3_add;

            //遠命中
            int hit_ranged_add = (int)(Math.Floor(actor.Status.hit_ranged_bs * (0.01 * level + 1)) + level);
            if (skill.Variable.ContainsKey("RifleGunDamUp_hit_ranged"))
                skill.Variable.Remove("RifleGunDamUp_hit_ranged");
            skill.Variable.Add("RifleGunDamUp_hit_ranged", hit_ranged_add);
            actor.Status.hit_ranged_skill += (short)hit_ranged_add;
         
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            //最小攻擊
            actor.Status.min_atk1_skill -= (short)skill.Variable["RifleGunDamUp_min_atk1"];

            //最小攻擊
            actor.Status.min_atk2_skill -= (short)skill.Variable["RifleGunDamUp_min_atk2"];

            //最小攻擊
            actor.Status.min_atk3_skill -= (short)skill.Variable["RifleGunDamUp_min_atk3"];

            //遠命中
            actor.Status.hit_ranged_skill -= (short)skill.Variable["RifleGunDamUp_hit_ranged"];
                
        }
        #endregion
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Trader
{
    /// <summary>
    /// 手提包修練（バッグマスタリー）
    /// </summary>
    public class BagDamUp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = false;
            if (SkillHandler.Instance.isEquipmentRight(sActor, SagaDB.Item.ItemType.LEFT_HANDBAG, SagaDB.Item.ItemType.HANDBAG))
            {
                active = true;
            }
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "BagDamUp", active);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int level = skill.skill.Level;
            int[] ATK = { 0, 5, 10, 15,20, 30 };
            int[] HIT = { 0, 2, 2, 2, 2, 3 };
            //最大攻擊
            int max_atk1_add = ATK[level];
            if (skill.Variable.ContainsKey("BagDamUp_max_atk1"))
                skill.Variable.Remove("BagDamUp_max_atk1");
            skill.Variable.Add("BagDamUp_max_atk1", max_atk1_add);
            actor.Status.max_atk1_skill += (short)max_atk1_add;

            //最大攻擊
            int max_atk2_add = ATK[level];
            if (skill.Variable.ContainsKey("BagDamUp_max_atk2"))
                skill.Variable.Remove("BagDamUp_max_atk2");
            skill.Variable.Add("BagDamUp_max_atk2", max_atk2_add);
            actor.Status.max_atk2_skill += (short)max_atk2_add;

            //最大攻擊
            int max_atk3_add = ATK[level];
            if (skill.Variable.ContainsKey("BagDamUp_max_atk3"))
                skill.Variable.Remove("BagDamUp_max_atk3");
            skill.Variable.Add("BagDamUp_max_atk3", max_atk3_add);
            actor.Status.max_atk3_skill += (short)max_atk3_add;

            //最小攻擊
            int min_atk1_add = ATK[level];
            if (skill.Variable.ContainsKey("BagDamUp_min_atk1"))
                skill.Variable.Remove("BagDamUp_min_atk1");
            skill.Variable.Add("BagDamUp_min_atk1", min_atk1_add);
            actor.Status.min_atk1_skill += (short)min_atk1_add;

            //最小攻擊
            int min_atk2_add = ATK[level];
            if (skill.Variable.ContainsKey("BagDamUp_min_atk2"))
                skill.Variable.Remove("BagDamUp_min_atk2");
            skill.Variable.Add("BagDamUp_min_atk2", min_atk2_add);
            actor.Status.min_atk2_skill += (short)min_atk2_add;

            //最小攻擊
            int min_atk3_add = ATK[level];
            if (skill.Variable.ContainsKey("BagDamUp_min_atk3"))
                skill.Variable.Remove("BagDamUp_min_atk3");
            skill.Variable.Add("BagDamUp_min_atk3", min_atk3_add);
            actor.Status.min_atk3_skill += (short)min_atk3_add;

            //近命中
            int hit_melee_add = HIT[level];
            if (skill.Variable.ContainsKey("BagDamUp_hit_melee"))
                skill.Variable.Remove("BagDamUp_hit_melee");
            skill.Variable.Add("BagDamUp_hit_melee", hit_melee_add);
            actor.Status.hit_melee_skill += (short)hit_melee_add;
         
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            //最大攻擊
            actor.Status.max_atk1_skill -= (short)skill.Variable["BagDamUp_max_atk1"];

            //最大攻擊
            actor.Status.max_atk2_skill -= (short)skill.Variable["BagDamUp_max_atk2"];

            //最大攻擊
            actor.Status.max_atk3_skill -= (short)skill.Variable["BagDamUp_max_atk3"];

            //最小攻擊
            actor.Status.min_atk1_skill -= (short)skill.Variable["BagDamUp_min_atk1"];

            //最小攻擊
            actor.Status.min_atk2_skill -= (short)skill.Variable["BagDamUp_min_atk2"];

            //最小攻擊
            actor.Status.min_atk3_skill -= (short)skill.Variable["BagDamUp_min_atk3"];

            //近命中
            actor.Status.hit_melee_skill -= (short)skill.Variable["BagDamUp_hit_melee"];
    
        }
        #endregion
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaDB.Item;

namespace SagaMap.Skill.SkillDefinations.Hawkeye
{
    /// <summary>
    /// シューティングマスタリー
    /// </summary>
    public class HawkeyeMaster : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.isEquipmentRight(pc, ItemType.BOW, ItemType.GUN, ItemType.EXGUN, ItemType.DUALGUN))
            {
                return 0;
            }
            return -14;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, dActor, "HawkeyeMaster", true);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int atk_add = 15 + 15 * skill.skill.Level;
            int hit_add = 10 + 6 * skill.skill.Level;
            int cri_add = 3 * skill.skill.Level;

            if (skill.Variable.ContainsKey("HawkeyeMaster_atk"))
                skill.Variable.Remove("HawkeyeMaster_atk");
            skill.Variable.Add("HawkeyeMaster_atk", atk_add);
            actor.Status.max_atk1_skill += (short)atk_add;
            actor.Status.max_atk2_skill += (short)atk_add;
            actor.Status.max_atk3_skill += (short)atk_add;
            actor.Status.min_atk1_skill += (short)atk_add;
            actor.Status.min_atk2_skill += (short)atk_add;
            actor.Status.min_atk3_skill += (short)atk_add;

            if (skill.Variable.ContainsKey("HawkeyeMaster_hit"))
                skill.Variable.Remove("HawkeyeMaster_hit");
            skill.Variable.Add("HawkeyeMaster_hit", hit_add);
            actor.Status.hit_melee_skill += (short)hit_add;

            if (skill.Variable.ContainsKey("HawkeyeMaster_cri"))
                skill.Variable.Remove("HawkeyeMaster_cri");
            skill.Variable.Add("HawkeyeMaster_cri", cri_add);
            actor.Status.hit_critical_skill += (short)cri_add;
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.max_atk1_skill -= (short)skill.Variable["HawkeyeMaster_atk"];
            actor.Status.max_atk2_skill -= (short)skill.Variable["HawkeyeMaster_atk"];
            actor.Status.max_atk3_skill -= (short)skill.Variable["HawkeyeMaster_atk"];
            actor.Status.min_atk1_skill -= (short)skill.Variable["HawkeyeMaster_atk"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["HawkeyeMaster_atk"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["HawkeyeMaster_atk"];
            actor.Status.hit_melee_skill -= (short)skill.Variable["HawkeyeMaster_hit"];
            actor.Status.hit_critical_skill -= (short)skill.Variable["HawkeyeMaster_cri"];
        }
        #endregion
    }
}

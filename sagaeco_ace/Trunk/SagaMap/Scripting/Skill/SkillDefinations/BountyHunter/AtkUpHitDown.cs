using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.BountyHunter
{
    /// <summary>
    /// 心靈鼓動（アジテイト）
    /// </summary>
    public class AtkUpHitDown : ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = (45 - 5 * level) * 1000;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "AtkUpHitDown", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            int max_atk1_add = 0, min_atk1_add = 0, max_atk2_add = 0, min_atk2_add = 0, max_atk3_add = 0, min_atk3_add = 0;
            int hit_range_down = -(int)(actor.Status.hit_ranged  * (0.3f * 0.1f * level));
            int hit_melee_down = -(int)(actor.Status.hit_melee  * (0.3f * 0.1f * level));
            max_atk1_add = (int)(actor.Status.max_atk_ori * (0.15f * 0.05f * level));
            min_atk1_add = (int)(actor.Status.min_atk_ori * (0.15f * 0.05f * level));
            max_atk2_add = (int)(actor.Status.max_atk_ori * (0.15f * 0.05f * level));
            min_atk2_add = (int)(actor.Status.min_atk_ori * (0.15f * 0.05f * level));
            max_atk3_add = (int)(actor.Status.max_atk_ori * (0.15f * 0.05f * level));
            min_atk3_add = (int)(actor.Status.min_atk_ori * (0.15f * 0.05f * level));
            //avo_range_down
            if (skill.Variable.ContainsKey("AtkUpHitDown_hit_range_down"))
                skill.Variable.Remove("AtkUpHitDown_hit_range_down");
            skill.Variable.Add("AtkUpHitDown_hit_range_down", hit_range_down);
            actor.Status.hit_ranged_skill  += (short)hit_range_down;
            //avo_melee_down
            if (skill.Variable.ContainsKey("AtkUpHitDown_hit_melee_down"))
                skill.Variable.Remove("AtkUpHitDown_hit_melee_down");
            skill.Variable.Add("AtkUpHitDown_hit_melee_down", hit_melee_down);
            actor.Status.hit_melee_skill  += (short)hit_melee_down;
            //大傷
            if (skill.Variable.ContainsKey("AtkUpHitDown_max_atk1_add"))
                skill.Variable.Remove("AtkUpHitDown_max_atk1_add");
            skill.Variable.Add("AtkUpHitDown_max_atk1_add", max_atk1_add);
            actor.Status.max_atk1_skill += (short)max_atk1_add;

            if (skill.Variable.ContainsKey("AtkUpHitDown_max_atk2_add"))
                skill.Variable.Remove("AtkUpHitDown_max_atk2_add");
            skill.Variable.Add("AtkUpHitDown_max_atk2_add", max_atk2_add);
            actor.Status.max_atk2_skill += (short)max_atk2_add;

            if (skill.Variable.ContainsKey("AtkUpHitDown_max_atk3_add"))
                skill.Variable.Remove("AtkUpHitDown_max_atk3_add");
            skill.Variable.Add("AtkUpHitDown_max_atk3_add", max_atk3_add);
            actor.Status.max_atk3_skill += (short)max_atk3_add;
            //小傷
            if (skill.Variable.ContainsKey("AtkUpHitDown_min_atk1_add"))
                skill.Variable.Remove("AtkUpHitDown_min_atk1_add");
            skill.Variable.Add("AtkUpHitDown_min_atk1_add", min_atk1_add);
            actor.Status.min_atk1_skill += (short)min_atk1_add;

            if (skill.Variable.ContainsKey("AtkUpHitDown_min_atk2_add"))
                skill.Variable.Remove("AtkUpHitDown_min_atk2_add");
            skill.Variable.Add("AtkUpHitDown_min_atk2_add", min_atk2_add);
            actor.Status.min_atk2_skill += (short)min_atk2_add;

            if (skill.Variable.ContainsKey("AtkUpHitDown_min_atk3_add"))
                skill.Variable.Remove("AtkUpHitDown_min_atk3_add");
            skill.Variable.Add("AtkUpHitDown_min_atk3_add", min_atk3_add);
            actor.Status.min_atk3_skill += (short)min_atk3_add;

            actor.Buff.最小攻撃力上昇 = true;
            actor.Buff.最大攻撃力上昇 = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.hit_ranged_skill -= (short)skill.Variable["AtkUpHitDown_hit_range_down"];
            actor.Status.hit_melee_skill -= (short)skill.Variable["AtkUpHitDown_hit_melee_down"];
            actor.Status.max_atk1_skill -= (short)skill.Variable["AtkUpHitDown_max_atk1_add"];
            actor.Status.max_atk2_skill -= (short)skill.Variable["AtkUpHitDown_max_atk2_add"];
            actor.Status.max_atk3_skill -= (short)skill.Variable["AtkUpHitDown_max_atk3_add"];
            actor.Status.min_atk1_skill -= (short)skill.Variable["AtkUpHitDown_min_atk1_add"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["AtkUpHitDown_min_atk2_add"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["AtkUpHitDown_min_atk3_add"];

            actor.Buff.最小攻撃力上昇 = false;
            actor.Buff.最大攻撃力上昇 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}

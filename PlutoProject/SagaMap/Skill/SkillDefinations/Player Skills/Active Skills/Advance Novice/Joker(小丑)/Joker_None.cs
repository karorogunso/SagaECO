using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// 小丑
    /// </summary>
    public class JokerNone : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int life = 60000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, sActor, "Joker1", life);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
            
                
        

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            int hp = (int)(actor.MaxHP * 0.2f * level);
            int mp = (int)(actor.MaxMP * 0.2f * level);
            int sp = (int)(actor.MaxSP * 0.2f * level);
            int max_atk1_add = (int)(actor.Status.max_atk_bs * (0.5f * level));
            int max_atk2_add = (int)(actor.Status.max_atk_bs * (0.5f * level));
            int max_atk3_add = (int)(actor.Status.max_atk_bs * (0.5f * level));
            int min_atk1_add = (int)(actor.Status.min_atk_bs * (0.5f * level));
            int min_atk2_add = (int)(actor.Status.min_atk_bs * (0.5f * level));
            int min_atk3_add = (int)(actor.Status.min_atk_bs * (0.5f * level));
            int max_matk_add = (int)(actor.Status.max_matk_bs * (0.5f * level));
            int min_matk_add = (int)(actor.Status.min_matk_bs * (0.5f * level));

            if (skill.Variable.ContainsKey("Joker1_HP"))
                skill.Variable.Remove("Joker1_HP");
            skill.Variable.Add("Joker1_HP", hp);
            actor.Status.hp_skill += (short)hp;

            if (skill.Variable.ContainsKey("Joker1_MP"))
                skill.Variable.Remove("Joker1_MP");
            skill.Variable.Add("Joker1_MP", mp);
            actor.Status.mp_skill += (short)mp;

            if (skill.Variable.ContainsKey("Joker1_SP"))
                skill.Variable.Remove("Joker1_SP");
            skill.Variable.Add("Joker1_SP", sp);
            actor.Status.sp_skill += (short)sp;

            //大傷
            if (skill.Variable.ContainsKey("Joker1_MAX_ATK1"))
                skill.Variable.Remove("Joker1_MAX_ATK1");
            skill.Variable.Add("Joker1_MAX_ATK1", max_atk1_add);
            actor.Status.max_atk1_skill += (short)max_atk1_add;

            if (skill.Variable.ContainsKey("Joker1_MAX_ATK2"))
                skill.Variable.Remove("Joker1_MAX_ATK2");
            skill.Variable.Add("Joker1_MAX_ATK2", max_atk2_add);
            actor.Status.max_atk2_skill += (short)max_atk2_add;

            if (skill.Variable.ContainsKey("Joker1_MAX_ATK3"))
                skill.Variable.Remove("Joker1_MAX_ATK3");
            skill.Variable.Add("Joker1_MAX_ATK3", max_atk3_add);
            actor.Status.max_atk3_skill += (short)max_atk3_add;

            //小伤
            if (skill.Variable.ContainsKey("Joker1_MIN_ATK1"))
                skill.Variable.Remove("Joker1_MIN_ATK1");
            skill.Variable.Add("Joker1_MIN_ATK1", min_atk1_add);
            actor.Status.min_atk1_skill += (short)min_atk1_add;

            if (skill.Variable.ContainsKey("Joker1_MIN_ATK2"))
                skill.Variable.Remove("Joker1_MIN_ATK2");
            skill.Variable.Add("Joker1_MIN_ATK2", min_atk2_add);
            actor.Status.min_atk2_skill += (short)min_atk2_add;

            if (skill.Variable.ContainsKey("Joker1_MIN_ATK3"))
                skill.Variable.Remove("Joker1_MIN_ATK3");
            skill.Variable.Add("Joker1_MIN_ATK3", min_atk3_add);
            actor.Status.min_atk3_skill += (short)min_atk3_add;

            //魔伤
            if (skill.Variable.ContainsKey("Joker1_MAX_MATK"))
                skill.Variable.Remove("Joker1_MAX_MATK");
            skill.Variable.Add("Joker1_MAX_MATK", max_matk_add);
            actor.Status.max_matk_skill += (short)max_matk_add;

            if (skill.Variable.ContainsKey("Joker1_MIN_MATK"))
                skill.Variable.Remove("Joker1_MIN_MATK");
            skill.Variable.Add("Joker1_MIN_MATK", min_matk_add);
            actor.Status.min_matk_skill += (short)min_matk_add;

            actor.Buff.MaxHPUp = true;
            actor.Buff.MaxSPUp = true;
            actor.Buff.MaxMPUp = true;
            actor.Buff.MaxAtkUp = true;
            actor.Buff.MinAtkUp = true;
            actor.Buff.MinMagicAtkUp = true;
            actor.Buff.MaxMagicAtkUp = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //大傷
            actor.Status.max_atk1_skill -= (short)skill.Variable["Joker_MAX_ATK1"];
            actor.Status.max_atk2_skill -= (short)skill.Variable["Joker_MAX_ATK2"];
            actor.Status.max_atk3_skill -= (short)skill.Variable["Joker_MAX_ATK3"];
            //小傷
            actor.Status.min_atk1_skill -= (short)skill.Variable["Joker_MIN_ATK1"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["Joker_MIN_ATK2"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["Joker_MIN_ATK3"];

            //魔伤
            actor.Status.max_matk_skill -= (short)skill.Variable["Joker_MAX_MATK"];
            actor.Status.min_matk_skill -= (short)skill.Variable["Joker_MIN_MATK"];

            actor.Buff.MaxHPUp = false;
            actor.Buff.MaxSPUp = false;
            actor.Buff.MaxMPUp = false;
            actor.Buff.MaxAtkUp = false;
            actor.Buff.MinAtkUp = false;
            actor.Buff.MinMagicAtkUp = false;
            actor.Buff.MaxMagicAtkUp = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        #endregion
    }
}

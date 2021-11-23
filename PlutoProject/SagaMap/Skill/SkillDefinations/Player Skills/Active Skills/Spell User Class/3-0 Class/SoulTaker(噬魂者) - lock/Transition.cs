using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;


namespace SagaMap.Skill.SkillDefinations.SoulTaker
{
    class Transition : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("ModeChange"))
            {
                return -13;
            }
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 30000 + 15000 * level;
            if (!dActor.Status.Additions.ContainsKey("Transition"))
            {
                DefaultBuff skill = new DefaultBuff(args.skill, dActor, "Transition", lifetime);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(dActor, skill);
            }
            else
                dActor.Status.Additions["Transition"].OnTimerEnd();
        }
        #endregion
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            //int max_number = actor.Status.max_matk - actor.Status.max_atk1;
            //int min_number = actor.Status.min_matk - actor.Status.min_atk1;

            ////actor.Status.min_atk1_skill = 0;
            ////actor.Status.max_atk1_skill = 0;
            ////actor.Status.min_matk_skill = 0;
            ////actor.Status.max_matk_skill = 0;

            //if (skill.Variable.ContainsKey("Transition_Max_atk"))
            //    skill.Variable.Remove("Transition_Max_atk");
            //skill.Variable.Add("Transition_Max_atk", max_number);
            //actor.Status.max_atk1_skill += (short)max_number;
            //actor.Status.max_matk_skill -= (short)max_number;

            //if (skill.Variable.ContainsKey("Transition_Min_atk"))
            //    skill.Variable.Remove("Transition_Min_atk");
            //skill.Variable.Add("Transition_Min_atk", min_number);
            //actor.Status.min_atk1_skill += (short)min_number;
            //actor.Status.min_matk_skill -= (short)min_number;



            //int change_aspd = actor.Status.aspd - actor.Status.cspd;
            //if (skill.Variable.ContainsKey("Transition_Aspd"))
            //    skill.Variable.Remove("Transition_Aspd");
            //skill.Variable.Add("Transition_Aspd", change_aspd);
            //actor.Status.aspd_skill -= (short)change_aspd;
            //actor.Status.cspd_skill += (short)change_aspd;
            ActorPC pc = (ActorPC)actor;
            int change_sm = pc.Str - pc.Mag;
            if (skill.Variable.ContainsKey("Change_str"))
                skill.Variable.Remove("Change_str");
            skill.Variable.Add("Change_str", change_sm);
            actor.Status.str_skill -= (short)change_sm;
            actor.Status.mag_skill += (short)change_sm;

            int change_da = pc.Dex - pc.Agi;
            if (skill.Variable.ContainsKey("Change_dex"))
                skill.Variable.Remove("Change_dex");
            skill.Variable.Add("Change_dex", change_da);
            actor.Status.dex_skill -= (short)change_da;
            actor.Status.agi_skill += (short)change_da;
            actor.Buff.三转ATK与MATK互换 = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.str_skill += (short)skill.Variable["Change_str"];
            actor.Status.mag_skill -= (short)skill.Variable["Change_str"];
            actor.Status.dex_skill += (short)skill.Variable["Change_dex"];
            actor.Status.agi_skill -= (short)skill.Variable["Change_dex"];
            //actor.Status.max_atk1_skill -= (short)skill.Variable["Transition_Max_atk"];
            //actor.Status.min_atk1_skill -= (short)skill.Variable["Transition_Min_atk"];
            //actor.Status.max_matk_skill += (short)skill.Variable["Transition_Max_atk"];
            //actor.Status.min_matk_skill += (short)skill.Variable["Transition_Min_atk"];
            //actor.Status.aspd_skill += (short)skill.Variable["Transition_Aspd"];
            //actor.Status.cspd_skill -= (short)skill.Variable["Transition_Aspd"];
            actor.Buff.三转ATK与MATK互换 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
//if (i.Status.Additions.ContainsKey("イレイザー") 
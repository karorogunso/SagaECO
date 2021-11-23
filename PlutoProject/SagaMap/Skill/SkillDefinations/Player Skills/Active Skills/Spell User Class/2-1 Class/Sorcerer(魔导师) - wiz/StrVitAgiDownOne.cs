using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Sorcerer
{
    /// <summary>
    /// 弱化（ディビリテイト）
    /// </summary>
    public class StrVitAgiDownOne : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int rate = 10 + 10 * level;
            int lifetime = new int[] { 0, 15000, 20000, 25000, 27000, 30000 }[level];
            if (SagaLib.Global.Random.Next(0, 99) < rate)
            {
                DefaultBuff skill = new DefaultBuff(args.skill, dActor, "StrVitAgiDownOne", lifetime);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            if (actor is ActorPC)
            {
                //STR
                int str_add = new int[] { 0, 5, 6, 7, 8, 10 }[level];
                if (skill.Variable.ContainsKey("StrVitAgiDownOne_str"))
                    skill.Variable.Remove("StrVitAgiDownOne_str");
                skill.Variable.Add("StrVitAgiDownOne_str", str_add);
                actor.Status.str_skill -= (short)str_add;

                //AGI
                int agi_add = new int[] { 0, 9, 12, 14, 16, 18 }[level];
                if (skill.Variable.ContainsKey("StrVitAgiDownOne_agi"))
                    skill.Variable.Remove("StrVitAgiDownOne_agi");
                skill.Variable.Add("StrVitAgiDownOne_agi", agi_add);
                actor.Status.agi_skill -= (short)agi_add;

                //VIT
                int vit_add = new int[] { 0, 6, 7, 8, 11, 12 }[level];
                if (skill.Variable.ContainsKey("StrVitAgiDownOne_vit"))
                    skill.Variable.Remove("StrVitAgiDownOne_vit");
                skill.Variable.Add("StrVitAgiDownOne_vit", vit_add);
                actor.Status.vit_skill -= (short)vit_add;
                actor.Buff.STRDown = true;
                actor.Buff.AGIDown = true;
                actor.Buff.DefDown = true;
            }
            else if (actor is ActorMob)
            {
                int min_atk1_add = (int)(actor.Status.min_atk1 * (0.1f + 0.04f * level));
                int min_atk2_add = (int)(actor.Status.min_atk2 * (0.1f + 0.04f * level));
                int min_atk3_add = (int)(actor.Status.min_atk3 * (0.1f + 0.04f * level));
                int max_atk1_add = (int)(actor.Status.max_atk1 * (0.1f + 0.04f * level));
                int max_atk2_add = (int)(actor.Status.max_atk2 * (0.1f + 0.04f * level));
                int max_atk3_add = (int)(actor.Status.max_atk3 * (0.1f + 0.04f * level));
                int savoid_add = (int)(actor.Status.avoid_melee * (0.1f + 0.04f * level));
                int def_add = 10 + 4 * level;

                if (skill.Variable.ContainsKey("StrVitAgiDownOne_minatk1"))
                    skill.Variable.Remove("StrVitAgiDownOne_minatk1");
                skill.Variable.Add("StrVitAgiDownOne_minatk1", min_atk1_add);

                if (skill.Variable.ContainsKey("StrVitAgiDownOne_minatk2"))
                    skill.Variable.Remove("StrVitAgiDownOne_minatk2");
                skill.Variable.Add("StrVitAgiDownOne_minatk2", min_atk2_add);

                if (skill.Variable.ContainsKey("StrVitAgiDownOne_minatk3"))
                    skill.Variable.Remove("StrVitAgiDownOne_minatk3");
                skill.Variable.Add("StrVitAgiDownOne_minatk3", min_atk3_add);

                if (skill.Variable.ContainsKey("StrVitAgiDownOne_maxatk1"))
                    skill.Variable.Remove("StrVitAgiDownOne_maxatk1");
                skill.Variable.Add("StrVitAgiDownOne_maxatk1", max_atk1_add);

                if (skill.Variable.ContainsKey("StrVitAgiDownOne_maxatk2"))
                    skill.Variable.Remove("StrVitAgiDownOne_maxatk2");
                skill.Variable.Add("StrVitAgiDownOne_maxatk2", max_atk2_add);

                if (skill.Variable.ContainsKey("StrVitAgiDownOne_maxatk3"))
                    skill.Variable.Remove("StrVitAgiDownOne_maxatk3");
                skill.Variable.Add("StrVitAgiDownOne_maxatk3", max_atk3_add);

                if (skill.Variable.ContainsKey("StrVitAgiDownOne_savoid"))
                    skill.Variable.Remove("StrVitAgiDownOne_savoid");
                skill.Variable.Add("StrVitAgiDownOne_savoid", savoid_add);

                if (skill.Variable.ContainsKey("StrVitAgiDownOne_def"))
                    skill.Variable.Remove("StrVitAgiDownOne_def");
                skill.Variable.Add("StrVitAgiDownOne_def", def_add);

                actor.Status.min_atk1_skill -= (short)min_atk1_add;
                actor.Status.min_atk2_skill -= (short)min_atk2_add;
                actor.Status.min_atk3_skill -= (short)min_atk3_add;
                actor.Status.max_atk1_skill -= (short)max_atk1_add;
                actor.Status.max_atk2_skill -= (short)max_atk2_add;
                actor.Status.max_atk3_skill -= (short)max_atk3_add;
                actor.Status.avoid_melee_skill -= (short)savoid_add;
                actor.Status.def_skill -= (short)def_add;
                actor.Buff.MinAtkDown = true;
                actor.Buff.MaxAtkDown = true;
                actor.Buff.ShortDodgeDown = true;
                actor.Buff.DefRateDown = true;
            }
            
            if(actor is ActorPC)
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            else
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, false);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            if (actor is ActorPC)
            {
                //STR
                actor.Status.str_skill += (short)skill.Variable["StrVitAgiDownOne_str"];

                //AGI
                actor.Status.agi_skill += (short)skill.Variable["StrVitAgiDownOne_agi"];

                //VIT
                actor.Status.vit_skill += (short)skill.Variable["StrVitAgiDownOne_vit"];
                actor.Buff.STRDown = false;
                actor.Buff.AGIDown = false;
                actor.Buff.DefDown = false;
            }
            else if (actor is ActorMob)
            {
                actor.Status.min_atk1_skill += (short)skill.Variable["StrVitAgiDownOne_minatk1"];
                actor.Status.min_atk2_skill += (short)skill.Variable["StrVitAgiDownOne_minatk2"];
                actor.Status.min_atk3_skill += (short)skill.Variable["StrVitAgiDownOne_minatk3"];
                actor.Status.max_atk1_skill += (short)skill.Variable["StrVitAgiDownOne_maxatk1"];
                actor.Status.max_atk2_skill += (short)skill.Variable["StrVitAgiDownOne_maxatk2"];
                actor.Status.max_atk3_skill += (short)skill.Variable["StrVitAgiDownOne_maxatk3"];
                actor.Status.avoid_melee_skill += (short)skill.Variable["StrVitAgiDownOne_savoid"];
                actor.Status.def_skill += (short)skill.Variable["StrVitAgiDownOne_def"];
                actor.Buff.MinAtkDown = false;
                actor.Buff.MaxAtkDown = false;
                actor.Buff.ShortDodgeDown = false;
                actor.Buff.DefRateDown = false;
            }
            
            if (actor is ActorPC)
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            else
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, false);
        }
        #endregion
    }
}

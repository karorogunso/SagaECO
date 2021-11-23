
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Gladiator
{
    /// <summary>
    /// 鬼人の構え
    /// </summary>
    public class DevilStance : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int[] totals = new int[] { 0, 45, 60, 75, 90, 120 };
            if (!dActor.Status.Additions.ContainsKey("DevilStance"))
            {
                int lifetime = 1000 * (totals[level]);
                DefaultBuff skill = new DefaultBuff(args.skill, dActor, "DevilStance", lifetime);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(dActor, skill);
            }
            else
            {
                dActor.Status.Additions["DevilStance"].OnTimerEnd();
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int Def = 0;
            if (actor.Status.def_add > 300)
                Def = 300;
            else
                Def = actor.Status.def_add;

            if (skill.Variable.ContainsKey("DevilStance_def"))
                skill.Variable.Remove("DevilStance_def");
            skill.Variable.Add("DevilStance_def", Def);

            //最大攻擊
            actor.Status.max_atk1_skill += (short)Def;

            //最大攻擊
            actor.Status.max_atk2_skill += (short)Def;

            //最大攻擊
            actor.Status.max_atk3_skill += (short)Def;

            //最小攻擊
            actor.Status.min_atk1_skill += (short)Def;

            //最小攻擊
            actor.Status.min_atk2_skill += (short)Def;

            //最小攻擊

            actor.Status.min_atk3_skill += (short)Def;

            //右防禦
            actor.Status.def_add_skill -= (short)Def;
            actor.Buff.DevilStance = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //最大攻擊
            actor.Status.max_atk1_skill -= (short)skill.Variable["DevilStance_def"];

            //最大攻擊
            actor.Status.max_atk2_skill -= (short)skill.Variable["DevilStance_def"];

            //最大攻擊
            actor.Status.max_atk3_skill -= (short)skill.Variable["DevilStance_def"];

            //最小攻擊
            actor.Status.min_atk1_skill -= (short)skill.Variable["DevilStance_def"];

            //最小攻擊
            actor.Status.min_atk2_skill -= (short)skill.Variable["DevilStance_def"];

            //最小攻擊
            actor.Status.min_atk3_skill -= (short)skill.Variable["DevilStance_def"];

            //右防禦
            actor.Status.def_add_skill += (short)skill.Variable["DevilStance_def"];
            actor.Buff.DevilStance = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}

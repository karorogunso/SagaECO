using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Assassin
{
    /// <summary>
    /// アサルト
    /// </summary>
    public class Concentricity : ISkill 
    {
        bool MobUse;
        public Concentricity()
        {
            this.MobUse = false;
        }
        public Concentricity(bool MobUse)
        {
            this.MobUse = MobUse;
        }
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (MobUse)
            {
                level = 5;
            }
            int lifetime = 15000 + 15000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "Concentricity", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            short def_rate = (short)((float)actor.Status.def * 0.3f);
            if (skill.Variable.ContainsKey("Concentricity"))
                skill.Variable.Remove("Concentricity");
            skill.Variable.Add("Concentricity", def_rate);
            actor.Status.def_skill -= def_rate;

            actor.Status.cri_skill += 30;
            actor.Buff.DefDown = true;
            actor.Buff.CriticalRateUp = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.DefDown = false;
            actor.Buff.CriticalRateUp = false;

            actor.Status.cri_skill  -= 30;
            actor.Status.def_skill += (short)skill.Variable["Concentricity"];

            if (skill.Variable.ContainsKey("Concentricity"))
                skill.Variable.Remove("Concentricity");

            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}

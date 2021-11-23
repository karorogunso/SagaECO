using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.TestingAddition
{
    /// <summary>
    /// 金刚不坏
    /// </summary>
    public class MentalStrength : ISkill
    {
        bool MobUse = false;
        public MentalStrength()
        {
            this.MobUse = false;
        }
        public MentalStrength(bool mobUse)
        {
            this.MobUse = mobUse;
        }
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (MobUse)
            {
                args.skill.Level = 5;
                level = 5;
            }
            int lifetime = 10 * 1000 * level;
            DefaultBuff buff = new DefaultBuff(args.skill, dActor, "MentalStrength", lifetime);
            buff.OnAdditionStart += Buff_OnAdditionStart;
            buff.OnAdditionEnd += Buff_OnAdditionEnd;
            SkillHandler.ApplyAddition(dActor, buff);
        }

        private void Buff_OnAdditionEnd(Actor actor, DefaultBuff skill)
        {
            int reducerate = 20 * skill.skill.Level - 1;

            if (skill.Variable.ContainsKey("MentalStrength"))
                skill.Variable.Remove("MentalStrength");
            skill.Variable.Add("MentalStrength", reducerate);
            actor.Buff.ParalysisResist = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        private void Buff_OnAdditionStart(Actor actor, DefaultBuff skill)
        {
            if (skill.Variable.ContainsKey("MentalStrength"))
                skill.Variable.Remove("MentalStrength");
            actor.Buff.ParalysisResist = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}

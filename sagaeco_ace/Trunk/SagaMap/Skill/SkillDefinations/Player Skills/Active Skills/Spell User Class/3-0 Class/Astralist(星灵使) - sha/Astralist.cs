using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Astralist
{
    /// <summary>
    /// アストラリスト
    /// </summary>
    public class Astralist : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;

        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 30000 + 30000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "Astralist", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            //float up = 0.5f * skill.skill.Level;
            int attackvalue = 200 + 20 * skill.skill.Level;
            if (skill.Variable.ContainsKey("Astralist"))
                skill.Variable.Remove("Astralist");
            skill.Variable.Add("Astralist", attackvalue);

            //actor.Status.ElementDamegeUp_rate += up;
            actor.Buff.MainSkillPowerUp3RD = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //actor.Status.ElementDamegeUp_rate = 0;
            if (skill.Variable.ContainsKey("Astralist"))
                skill.Variable.Remove("Astralist");
            actor.Buff.MainSkillPowerUp3RD = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}

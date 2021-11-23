using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaMap.Skill.Additions.Global;
using SagaLib;
using SagaMap;

namespace SagaMap.Skill.SkillDefinations.Elementaler
{
    /// <summary>
    /// 魔法極大化（ゼン）
    /// </summary>
    public class Zen:ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("ZensssCD"))
                return -30;
            else
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillCD skill2 = new SkillCD(args.skill, dActor, "ZensssCD", 60000);
            SkillHandler.ApplyAddition(dActor, skill2);
            if (sActor.type == ActorType.PC)
            {
                DefaultBuff skill = new DefaultBuff(args.skill, dActor, "极大", 26000 - 2000 * level);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            short maxmatk = (short)(actor.Status.max_matk * (float)(0.7f + skill.skill.Level * 0.3f));
            short minmatk = (short)(actor.Status.min_matk * (float)(0.7f + skill.skill.Level * 0.3f));

            if (skill.Variable.ContainsKey("ZenMaxMatk"))
                skill.Variable.Remove("ZenMaxMatk");
            skill.Variable.Add("ZenMaxMatk", maxmatk);
            actor.Status.max_matk_skill += (short)maxmatk;

            /*if (skill.Variable.ContainsKey("ZenMinMatk"))
                skill.Variable.Remove("ZenMinMatk");
            skill.Variable.Add("ZenMinMatk", minmatk);
            actor.Status.min_matk_skill += (short)minmatk;*/
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.max_matk_skill -= (short)skill.Variable["ZenMaxMatk"];
            /*actor.Status.min_matk_skill -= (short)skill.Variable["ZenMinMatk"];*/
        }
    #endregion
    }
}

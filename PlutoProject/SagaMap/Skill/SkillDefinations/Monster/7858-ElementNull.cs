using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Monster
{
    /// <summary>
    /// 属性攻击无效
    /// </summary>
    public class ElementNull : ISkill, MobISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void BeforeCast(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            return;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 30000;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "ElementNull", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.ElementDamegeDown_rate = 100;
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.ElementDamegeDown_rate = 0;
        }
    }
}

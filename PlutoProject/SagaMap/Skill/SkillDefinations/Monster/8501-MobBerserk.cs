using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Monster
{
    /// <summary>
    /// 光戰士
    /// </summary>
    public class MobBerserk : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = false;
            if (dActor.HP < (uint)(dActor.MaxHP * 0.6f))
            {
                int rate = 90;
                if (SagaLib.Global.Random.Next(0, 99) < rate)
                {
                    active = true;
                }
            }
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "MobBerserk", active);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            Berserk skill2 = new Berserk(skill.skill, actor, 60000);
            SkillHandler.ApplyAddition(actor, skill2);
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
        }
        #endregion
    }
}
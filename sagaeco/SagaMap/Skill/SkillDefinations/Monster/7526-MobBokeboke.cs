using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;

namespace SagaMap.Skill.SkillDefinations.Monster
{
    /// <summary>
    /// 嗚嗚嗚啊~
    /// </summary>
    public class MobBokeboke : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Actor act = dActor;
            int lifetime = 3000;
            int rate = 4;
            if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.Stone, rate))
            {
                Additions.Global.Stone skill16 = new SagaMap.Skill.Additions.Global.Stone(args.skill, act, lifetime);
                SkillHandler.ApplyAddition(act, skill16);
            }
            if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.Frosen, rate))
            {
                Additions.Global.Freeze skill17 = new SagaMap.Skill.Additions.Global.Freeze(args.skill, act, lifetime);
                SkillHandler.ApplyAddition(act, skill17);
            }
            rate = 6;
            if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.Poison , rate))
            {
                Additions.Global.Poison1 skill18 = new SagaMap.Skill.Additions.Global.Poison1(args.skill, act, lifetime);
                SkillHandler.ApplyAddition(act, skill18);
            }
            if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.Confuse, rate))
            {
                Additions.Global.Confuse skill5 = new SagaMap.Skill.Additions.Global.Confuse(args.skill, act, lifetime);
                SkillHandler.ApplyAddition(act, skill5);
            }
            rate = 8;
            if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.Stun, rate))
            {
                Additions.Global.Stun skill1 = new SagaMap.Skill.Additions.Global.Stun(args.skill, act, lifetime);
                SkillHandler.ApplyAddition(act, skill1);
            }
            if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.Sleep, rate))
            {
                Additions.Global.Sleep skill4 = new SagaMap.Skill.Additions.Global.Sleep(args.skill, act, lifetime);
                SkillHandler.ApplyAddition(act, skill4);
            }
            rate = 10;
            if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.鈍足, rate))
            {
                Additions.Global.鈍足 skill2 = new SagaMap.Skill.Additions.Global.鈍足(args.skill, act, lifetime);
                SkillHandler.ApplyAddition(act, skill2);
            }
            if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.Silence, rate))
            {
                Additions.Global.Silence skill3 = new SagaMap.Skill.Additions.Global.Silence(args.skill, act, lifetime);
                SkillHandler.ApplyAddition(act, skill3);
            }            
        }
        #endregion
    }
}
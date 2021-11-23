using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Sage
{
    /// <summary>
    /// 能量轉移（エナジーフリーク）
    /// </summary>
    public class EnergyFreak :ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = (2 + level) * 1000;
            float factor = 0.8f + 0.2f * level;
            int rate =  10 * level ;
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
            if (SagaLib.Global.Random.Next(0, 99) < rate && !SkillHandler.Instance.isBossMob(dActor))
            {
                Additions.Global.Silence skill3 = new SagaMap.Skill.Additions.Global.Silence(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, skill3);
                Additions.Global.Poison1 skill5 = new SagaMap.Skill.Additions.Global.Poison1(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, skill5);
                Additions.Global.CannotMove skill4 = new SagaMap.Skill.Additions.Global.CannotMove(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, skill4);
            }
        }
        #endregion
    }
}

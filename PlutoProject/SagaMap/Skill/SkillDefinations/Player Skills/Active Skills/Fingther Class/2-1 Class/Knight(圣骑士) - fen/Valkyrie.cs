
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Knight
{
    /// <summary>
    /// 神聖的一擊（ヴァルキリー）
    /// </summary>
    
    #region ISkill Members
    public class Valkyrie : ISkill
    {
        
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float[] factors = { 0f, 2.0f, 3.4f, 4.8f, 6.2f, 7.6f };
            List<Actor> affected = new List<Actor>();
            affected.Add(dActor);
            SkillHandler.Instance.PhysicalAttack(sActor, affected, args, SkillHandler.DefType.IgnoreRight, SagaLib.Elements.Holy, 0, factors[level], false);
            Stiff skills = new Stiff(args.skill, dActor, 500);
            SkillHandler.ApplyAddition(dActor, skills);
        }
    }
}
#endregion

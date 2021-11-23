
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
    public class Valkyrie : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float[] factors = {0f,1.0f,1.12f,1.25f,1.37f,1.5f};
            uint LowHP = (uint)(dActor.MaxHP*(100 - 10 * level)/100f);
            if (LowHP < dActor.MaxHP)
            {
                List<Actor> affected =new List<Actor>();
                affected.Add(dActor);
                SkillHandler.Instance.PhysicalAttack(sActor, affected, args, SkillHandler.DefType.IgnoreDefRight, SagaLib.Elements.Holy, 0, factors[level], false);
                硬直 skills = new 硬直(args.skill, dActor, 500);
                SkillHandler.ApplyAddition(dActor, skills);
            }           
        }
        #endregion
    }
}
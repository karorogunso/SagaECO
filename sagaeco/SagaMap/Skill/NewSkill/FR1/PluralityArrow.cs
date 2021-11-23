using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Striker
{
    /// <summary>
    /// 多連箭（バラージアロー）
    /// </summary>
    public class PluralityArrow : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int times = 3;
            float factor = 0.8f + level * 0.1f;
            args.delayRate = 5f;
            args.argType = SkillArg.ArgType.Attack;

            if (sActor.Status.Additions.ContainsKey("ConArrow"))
            {
                sActor.Status.Additions["ConArrow"].AdditionEnd();
                SagaMap.Skill.Additions.Global.DefaultBuff db = new Additions.Global.DefaultBuff(args.skill, sActor, "PluralityArrow", 5000);
                SkillHandler.ApplyAddition(sActor, db);
                factor = factor * 1.5f;
                if (sActor.type == ActorType.PC)
                    SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)sActor).nextCombo.Add(2206);
            }

            List<Actor> target = new List<Actor>();

            for (int i = 0; i < times; i++)
            {
                target.Add(dActor);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, target, args, SkillHandler.DefType.Def, SagaLib.Elements.Neutral, 0, 0, false, 0, false, -10, 0);
        }
        #endregion
    }
}

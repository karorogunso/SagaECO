
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Assassin
{
    /// <summary>
    /// 死神晚宴 | 背刺（バックアタック）
    /// </summary>
    public class BackAtk : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0;
            //背面
            if (SkillHandler.Instance.GetIsBack(sActor, dActor))
                factor = new float[] { 0, 2.1f, 2.5f, 3.0f, 3.5f, 4.0f }[level];
            else
                factor = 1.1f;

            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
            if(SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.硬直, 95))
            {
                Skill.Additions.Global.Stiff stiff = new Additions.Global.Stiff(args.skill, dActor, 3000);
                SkillHandler.ApplyAddition(dActor, stiff);
            }
        }
        #endregion
    }
}
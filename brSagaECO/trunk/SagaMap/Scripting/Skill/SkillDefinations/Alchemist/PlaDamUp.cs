using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Alchemist
{
    /// <summary>
    /// 植物傷害增加（植物系ダメージ上昇）
    /// </summary>
    public class PlaDamUp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //ushort[] Values = { 0, 3, 6, 9, 12, 15 };//%

            //ushort value = Values[level];

            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "异常UPUPUP", true);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            actor.Ac_add_rate += (byte)(skill.skill.Level * 8);
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            actor.Ac_add_rate -= (byte)(skill.skill.Level * 8);
        }
        #endregion
    }
}

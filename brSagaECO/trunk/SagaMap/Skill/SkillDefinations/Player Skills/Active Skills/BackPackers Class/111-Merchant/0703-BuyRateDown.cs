
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Merchant
{
    /// <summary>
    /// 折扣（ビートダウン）
    /// </summary>
    public class BuyRateDown : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = true;
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "BuyRateDown", active);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int rate = -skill.skill.Level * 2;
            skill["BuyRate"] = rate;
            actor.Status.buy_rate += (short)rate;
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.buy_rate -= (short)(skill["BuyRate"]);
        }
        #endregion
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Gambler
{
    /// <summary>
    /// 意志昂揚（ダブルアップ）
    /// </summary>
    public class DoubleUp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
                bool active = true;
                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "DoubleUp", active);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.doubleUpList.Add(2127);//連續投擲
            actor.Status.doubleUpList.Add(3286);//擲骰子
            actor.Status.doubleUpList.Add(3287);//幸福輪盤
            actor.Status.doubleUpList.Add(2374);//卡飛旋鏢
            actor.Status.doubleUpList.Add(2375);//一擲千金
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.doubleUpList.Clear();
        }
        #endregion
    }
}

                                                          
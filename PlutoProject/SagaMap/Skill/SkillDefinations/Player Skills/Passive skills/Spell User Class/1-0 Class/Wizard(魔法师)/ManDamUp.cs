﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Wizard
{
    /// <summary>
    /// 魔法生物傷害增加
    /// </summary>
    public class ManDamUp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //创建一个默认被动技能处理对象
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "居合姿态", true);
            //设置OnAdditionStart事件处理过程
            skill.OnAdditionStart += this.StartEventHandler;
            //设置OnAdditionEnd事件处理过程
            skill.OnAdditionEnd += this.EndEventHandler;
            //对指定Actor附加技能效果
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
        }
        #endregion
    }
}

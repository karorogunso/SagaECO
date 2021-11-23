using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Gladiator
{
    /// <summary>
    /// 不屈の闘志
    /// </summary>
    public class Volunteers : ISkill
    {
        #region ISkill 成員

        public int TryCast(SagaDB.Actor.ActorPC sActor, SagaDB.Actor.Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg args, byte level)
        {
            //创建一个默认被动技能处理对象
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, dActor, "不屈の闘志", true);
            //设置OnAdditionStart事件处理过程
            skill.OnAdditionStart += this.StartEventHandler;
            //设置OnAdditionEnd事件处理过程
            skill.OnAdditionEnd += this.EndEventHandler;
            //对指定Actor附加技能效果
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            float factor = 0;
            switch (skill.skill.Level)
            {
                case 1:
                    factor = 0.3f;
                    break;
                case 2:
                    factor = 0.6f;
                    break;
                case 3:
                    factor = 0.9f;
                    break;
                case 4:
                    factor = 0.12f;
                    break;
                case 5:
                    factor = 0.15f;
                    break;
            }
            //将此被动技能所增加的值存到临时变量中，以便取消效果时调用
            actor.Status.absorb_hp = factor;
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.absorb_hp = 0;
        }
        #endregion
    }
}

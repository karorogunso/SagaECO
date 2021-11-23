using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Global
{
    public class MaxHPUp:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //创建一个默认被动技能处理对象
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "MaxHPUp", true);
            //设置OnAdditionStart事件处理过程
            skill.OnAdditionStart += this.StartEventHandler;
            //设置OnAdditionEnd事件处理过程
            skill.OnAdditionEnd += this.EndEventHandler;
            //对指定Actor附加技能效果
            SkillHandler.ApplyAddition(sActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int value;
            float factor = 0;
            switch (skill.skill.Level)
            {
                case 1:
                    factor = 0.01f;
                    break;
                case 2:
                    factor = 0.03f;
                    break;
                case 3:
                    factor = 0.06f;
                    break;
                case 4:
                    factor = 0.10f;
                    break;
                case 5:
                    factor = 0.15f;
                    break;
            }
            value = (int)(actor.MaxHP * factor);
            if (value < (skill.skill.Level * 10))
            {
                value = skill.skill.Level * 10;
            }
            //将此被动技能所增加的值存到临时变量中，以便取消效果时调用
            if (skill.Variable.ContainsKey("HPValue"))
                skill.Variable.Remove("HPValue");
            skill.Variable.Add("HPValue", value);
            actor.Status.hp_skill += (short)value;
        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int value = skill.Variable["HPValue"];
            actor.Status.hp_skill -= (short)value;
        }

        #endregion
    }
}

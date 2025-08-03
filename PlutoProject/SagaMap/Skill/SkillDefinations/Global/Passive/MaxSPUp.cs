using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Global
{
    public class MaxSPUp : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //创建一个默认被动技能处理对象
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "MaxSPUp", true);
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
            float factor = new float[] { 0, 0.01f, 0.03f, 0.06f, 0.10f, 0.15f }[skill.skill.Level];

            value = (int)(actor.MaxSP * factor);

            ushort[] minspup = new ushort[] { 0, 10, 20, 30, 40, 60 };
            value = Math.Max(value, minspup[skill.skill.Level]);
            //将此被动技能所增加的值存到临时变量中，以便取消效果时调用
            if (skill.Variable.ContainsKey("SPValue"))
                skill.Variable.Remove("SPValue");
            skill.Variable.Add("SPValue", value);
            actor.Status.sp_skill += (short)value;
        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int value = skill.Variable["SPValue"];
            actor.Status.sp_skill -= (short)value;
        }

        #endregion
    }
}

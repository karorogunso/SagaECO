using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaMap.Skill.Additions.Global;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// CD类BUFF定义接口
    /// </summary>
    public interface ICDBuff
    {
        /// <summary>
        /// 赋予定义的BUFF
        /// </summary>
        /// <param name="actor">目标</param>
        /// <param name="lifetime">时间（毫秒）</param>
        void ApplyBuff(Actor actor, int lifetime);
    }
}

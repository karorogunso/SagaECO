using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 技能定义接口
    /// </summary>
    public interface ISkill
    {
        /// <summary>
        /// 技能处理过程
        /// </summary>
        /// <param name="sActor">源Actor</param>
        /// <param name="dActor">目标Actor</param>
        /// <param name="args">技能参数</param>
        /// <param name="level">技能等级</param>
        void Proc(Actor sActor, Actor dActor, SkillArg args, byte level);

        /// <summary>
        /// 尝试释放某技能，并返回结果
        /// </summary>
        /// <param name="sActor">源Actor</param>
        /// <param name="dActor">目标Actor</param>
        /// <returns>0表示可释放，小于0则为错误代码</returns>
        int TryCast(ActorPC sActor, Actor dActor, SkillArg args);
    }
}

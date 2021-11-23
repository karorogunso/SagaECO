
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Sorcerer
{
    /// <summary>
    /// 魔力吸收（ディジスト）
    /// </summary>
    public class Desist : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 35000 - 5000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "Desist", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
            /*
             * 以受到的魔法傷害恢復MP
             * 目標：自己
             * 施展時間：0.5秒
             * 冷卻時間：3秒
             * 憑依時使用：可
             * Lv       1   2   3   4   5 
             * MP消耗   10  20  30  40  50 
             * 吸收量   10% 20% 30% 40% 50% 
             * 憑依時則恢復被憑依者
             * 
             */
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
        }
        #endregion
    }
}

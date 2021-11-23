
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Gardener
{
    /// <summary>
    /// ウェザーコントロール
    /// </summary>
    public class WeatherControl : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            /*
             * ウェザーコントロール †
                Active 
                習得JOBLV：28 
                効果：飛空庭周囲の空間を歪め、天候を自在に操作することが出来る。(憑依中使用不可) 
                他人の飛空庭でも自由に使うことが出来る。 
                変更できる天気は【雨】と【雪】

             */
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Gardener
{
    /// <summary>
    /// ギャザリング
    /// </summary>
    public class Gathering : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            /*
             * ギャザリング †
                Active 
                習得JOBLV：5 
                効果：地面に多くの種を植え、植物を生やす。
                ファーマースキル「栽培」と専用のアイテムが必要。 
                消費は種×8個のみ（SP・MP消費無し） 
                栽培エリアは自分の周囲8マス 
                同じ種類の種を8個以上持っていない場合はスキルを使用できない
            */
        }
        #endregion
    }
}
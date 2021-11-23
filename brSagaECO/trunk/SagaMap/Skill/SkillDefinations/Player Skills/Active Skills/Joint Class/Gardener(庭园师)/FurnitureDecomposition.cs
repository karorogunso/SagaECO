
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Gardener
{
    /// <summary>
    /// 家具分解
    /// </summary>
    public class FurnitureDecomposition : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            /*
             * 家具分解 †
                Active 
                習得JOBLV：30 
                効果：不必要な家具を分解し、そこから新たな素材を得ることができる。(憑依中使用不可) 
                対象：自分 
                家具なら何でも分解できる。 
                家具扱いだがモンスターフィギュアは分解不可能。
                実装当初は、安価な設置用キューブを分解することによって、精錬結晶や大理石等の高価な素材を楽に入手することができた。 
                現在でも同様の手段は相変わらず可能なものの、高額素材の出現率は大幅に下げられた。
                (修正後は結晶が0.1%未満、大理石が0.5%程度)
             */
        }
        #endregion
    }
}
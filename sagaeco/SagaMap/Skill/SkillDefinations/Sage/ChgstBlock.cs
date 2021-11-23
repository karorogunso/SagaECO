using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Sage
{
    /// <summary>
    /// 解放異常狀態（エンチャントブロック）
    /// </summary>
    public class ChgstBlock:ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
                return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int rate = 30 + 10 * level;
            if (SagaLib.Global.Random.Next(0, 99) < rate)
            {
                List<string> adds = new List<string>();
                foreach (System.Collections.Generic.KeyValuePair<string,SagaDB.Actor.Addition> ad in dActor.Status.Additions)
                {
                    adds.Add(ad.Value.Name);
                }
                foreach (string adn in adds)
                {
                    SkillHandler.RemoveAddition(dActor, adn);
                }
            }
            //尚須加上不能被賦予狀態
        }
        #endregion
    }
}

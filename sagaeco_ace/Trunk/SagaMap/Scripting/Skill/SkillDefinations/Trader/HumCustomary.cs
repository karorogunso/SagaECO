
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Mob;
namespace SagaMap.Skill.SkillDefinations.Trader
{
    /// <summary>
    /// 賞金（チップ）
    /// </summary>
    public class HumCustomary : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            ActorPet pet = SkillHandler.Instance.GetPet(sActor);
            if (pet == null)
            {
                return;
            }
            if (SkillHandler.Instance.CheckMobType(pet, "HUMAN"))
            {
                uint PastMoney =(uint)(30 * level);
                ActorPC pc = (ActorPC)sActor;
                if (pc.Gold >= PastMoney)
                {
                    pc.Gold -= (int)PastMoney;
                    MobAI ai = SkillHandler.Instance.GetMobAI(pet);
                    ai.CastSkill(6401, level, dActor);
                }
            }
        }
        #endregion
    }
}
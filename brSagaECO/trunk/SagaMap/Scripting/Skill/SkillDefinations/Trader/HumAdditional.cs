
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Mob;
namespace SagaMap.Skill.SkillDefinations.Trader
{
    /// <summary>
    /// 支付追加費（アディショナルチャージ）
    /// </summary>
    public class HumAdditional : ISkill
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
                uint[] PastMoney = { 0, 50, 80, 50, 120, 100 };
                ActorPC pc = (ActorPC)sActor;
                if (pc.Gold >= PastMoney[level])
                {
                    pc.Gold -= (int)PastMoney[level];
                    MobAI ai = SkillHandler.Instance.GetMobAI(pet);
                    ai.CastSkill(6403, level, dActor);
                }
            }
        }
        #endregion
    }
}
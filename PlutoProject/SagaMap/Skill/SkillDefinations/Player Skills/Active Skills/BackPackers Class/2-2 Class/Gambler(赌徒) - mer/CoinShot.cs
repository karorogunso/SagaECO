
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Gambler
{
    /// <summary>
    /// 一擲千金（コインシュート）
    /// </summary>
    public class CoinShot : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = new float[] { 0, 2.5f, 3.5f, 4.5f, 5.5f, 7.5f }[level];
            ActorPC pc = (ActorPC)sActor;
            uint[] gold = { 0, 500, 700, 900, 1100, 1500 };
            if (pc.Gold > gold[level])
            {
                pc.Gold -= (int)gold[level];
                SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, factor);
            }
        }
        #endregion
    }
}
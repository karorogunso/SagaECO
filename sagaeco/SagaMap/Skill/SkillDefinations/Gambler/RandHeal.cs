
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Gambler
{
    /// <summary>
    /// 擲骰子（ランダムヒーリング）
    /// </summary>
    public class RandHeal : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0.1f - 0.6f * level;
            int attack_rate = 40 - 10 * level;
            if (SagaLib.Global.Random.Next(0, 99) < attack_rate)
            {
                SkillHandler.Instance.MagicAttack(sActor, sActor, args, SagaLib.Elements.Holy, -factor);
            }
            else
            {
                SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Holy, factor);
            }
            uint MPSP_add = (uint)(25 + 5 * level);
            if (dActor.MP + MPSP_add <= dActor.MaxMP)
            {
                dActor.MP += MPSP_add;
            }
            else
            {
                dActor.MP = dActor.MaxMP;
            }
            if (dActor.SP + MPSP_add <= dActor.MaxSP)
            {
                dActor.SP += 0;
            }
            else
            {
                dActor.SP = dActor.MaxSP;
            }
            args.affectedActors.Clear();
            args.affectedActors.Add(dActor);
            args.Init();
            args.mp[0] =(int)MPSP_add;
            args.sp[0] = (int)MPSP_add;
            args.flag[0] |= SagaLib.AttackFlag.MP_HEAL | SagaLib.AttackFlag.SP_HEAL | SagaLib.AttackFlag.NO_DAMAGE;
            if (SagaLib.Global.Random.Next(0, 99) < MPSP_add)
            {
                RemoveAddition(dActor, "Poison");
                RemoveAddition(dActor, "鈍足");
                RemoveAddition(dActor, "Stone");
                RemoveAddition(dActor, "Silence");
                RemoveAddition(dActor, "Stun");
                RemoveAddition(dActor, "Frosen");
                RemoveAddition(dActor, "Confuse");
            }
        }
        public void RemoveAddition(Actor actor, String additionName)
        {
            if (actor.Status.Additions.ContainsKey(additionName))
            {
                Addition addition = actor.Status.Additions[additionName];
                actor.Status.Additions.Remove(additionName);
                if (addition.Activated)
                {
                    addition.AdditionEnd();
                }
                addition.Activated = false;
            }
        }
        #endregion
    }
}
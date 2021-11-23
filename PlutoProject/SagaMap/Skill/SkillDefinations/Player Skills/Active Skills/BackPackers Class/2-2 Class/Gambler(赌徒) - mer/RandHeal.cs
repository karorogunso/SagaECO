
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
            if (dActor.Buff.NoRegen)
            {
                return -1;
            }
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int min = 0, max = 0;
            if ((SagaLib.Global.Random.Next(0, 99)) < 40)
            {
                switch (level)
                {
                    case 1:
                        min = (int)((sActor.Status.max_matk * 0.36f) + 18);
                        max = (int)((sActor.Status.max_matk * 0.65f) + 32);
                        break;
                    case 2:
                        min = (int)((sActor.Status.max_matk * 0.45f) + 25);
                        max = (int)((sActor.Status.max_matk * 1.16f) + 52);
                        break;
                    case 3:
                        min = (int)((sActor.Status.max_matk * 0.6f) + 30);
                        max = (int)((sActor.Status.max_matk * 1.60f) + 80);
                        break;

                }
                uint calc = (uint)(SagaLib.Global.Random.Next(min, max));
                uint healhp = 0, healsp = 0, healmp = 0;
                healhp = calc;
                sActor.HP += healhp;
                if (sActor.HP > sActor.MaxHP)
                    sActor.HP = sActor.MaxHP;
                if ((SagaLib.Global.Random.Next(0, 99)) < 40)
                {
                    healsp = calc;
                    healmp = calc;
                    sActor.MP += healmp;
                    sActor.SP += healsp;
                    if (sActor.SP > sActor.MaxSP)
                        sActor.SP = sActor.MaxSP;
                    if (sActor.MP > sActor.MaxMP)
                        sActor.MP = sActor.MaxMP;
                    args.affectedActors.Clear();
                    args.affectedActors.Add(dActor);
                    args.Init();
                    args.hp[0] = (int)healhp;
                    args.mp[0] = (int)healmp;
                    args.sp[0] = (int)healsp;
                    args.flag[0] |= SagaLib.AttackFlag.MP_HEAL | SagaLib.AttackFlag.HP_HEAL | SagaLib.AttackFlag.SP_HEAL | SagaLib.AttackFlag.NO_DAMAGE;
                }
                else
                {
                    args.affectedActors.Clear();
                    args.affectedActors.Add(dActor);
                    args.Init();
                    args.hp[0] = (int)healhp;
                    args.flag[0] |= SagaLib.AttackFlag.HP_HEAL | SagaLib.AttackFlag.NO_DAMAGE;
                }


                if (SagaLib.Global.Random.Next(0, 99) < 30)
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
            else
            {
                if (sActor.HP > sActor.Status.min_matk)
                    SkillHandler.Instance.FixAttack(sActor, sActor, args, SagaLib.Elements.Neutral, sActor.Status.min_matk);
                else if (sActor.HP == sActor.Status.min_matk)
                    SkillHandler.Instance.FixAttack(sActor, sActor, args, SagaLib.Elements.Neutral, sActor.Status.min_matk - 1);
                else
                    SkillHandler.Instance.FixAttack(sActor, sActor, args, SagaLib.Elements.Neutral, sActor.HP - 1);
            }

            /*

            float factor = 0.1f - 0.6f * level;
            int attack_rate = 40 - 10 * level;
            factor += sActor.Status.Cardinal_Rank;
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
            */
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
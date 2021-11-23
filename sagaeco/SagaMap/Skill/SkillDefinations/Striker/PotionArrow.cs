using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;

namespace SagaMap.Skill.SkillDefinations.Striker
{
    /// <summary>
    /// 復活之箭（ポーションアロー）
    /// </summary>
    public class PotionArrow : ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //90+2*BASELv./3 180+4*BASELv./3 270+6*BASELv./3 
            uint HP_add = (uint)(dActor.MaxHP * 0.2f);
            uint MP_add = (uint)(dActor.MaxMP * 0.2f);
            uint SP_add = (uint)(dActor.MaxSP * 0.2f);
            int[] combo = { 0, 1, 1, 1 };
            List<Actor> target = new List<Actor>();
            for (int i = 0; i < combo[level]; i++)
            {
                target.Add(dActor);
            }
            
            if (!dActor.Status.Additions.ContainsKey("Sacrifice"))
            {
                if (dActor.HP + HP_add < dActor.MaxHP)
                {
                    dActor.HP += HP_add;
                }
                else
                {
                    dActor.HP = dActor.MaxHP;
                }
                if (dActor.MP + MP_add < dActor.MaxMP)
                {
                    dActor.MP += MP_add;
                }
                else
                {
                    dActor.MP = dActor.MaxMP;
                }
                if (dActor.SP + SP_add < dActor.MaxSP)
                {
                    dActor.SP += SP_add;
                }
                else
                {
                    dActor.SP = dActor.MaxSP;
                }
            }
            args.flag[0] |= AttackFlag.HP_HEAL | AttackFlag.MP_HEAL | AttackFlag.SP_HEAL | AttackFlag.NO_DAMAGE;
            SkillHandler.Instance.PhysicalAttack(sActor, target, args, SagaLib.Elements.Holy, 0.0f);
        }
        #endregion
    }
}

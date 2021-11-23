using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.BountyHunter
{
    /// <summary>
    /// 利劍語意（ソードアセイル）
    /// </summary>
    public class SwordAssail : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, dActor))
            {
                return 0;
            }
            else
            {
                return -14;
            }

        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            sActor.SwordACount = 0;
            args.argType = SkillArg.ArgType.Cast;
            for (int i = 1; i <= 5; i++)
            {
                AutoCastInfo aci = SkillHandler.Instance.CreateAutoCastInfo(2397, level, 2000);
                args.autoCast.Add(aci);
            }
            //float[] factor = { 0, 3.15f, 3.67f, 4.2f, 4.72f, 5.25f };
            //args.argType = SkillArg.ArgType.Attack;
            //args.type = ATTACK_TYPE.BLOW;
            //List<Actor> dest = new List<Actor>();
            //for (int i = 0; i < 4; i++)
            //{
            //    sActor.Status.aspd_skill -= 800;
            //    dest.Add(dActor);
            //    sActor.Status.aspd_skill += 800;
            //}
            //SkillHandler.Instance.PhysicalAttack(sActor, dest, args, sActor.WeaponElement, factor[level]);
        }
        #endregion
    }
}

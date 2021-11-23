using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaMap.Skill.SkillDefinations.BountyHunter
{


    public class SwordAssailSEQ : ISkill
    {
        public int TryCast(SagaDB.Actor.ActorPC sActor, SagaDB.Actor.Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg args, byte level)
        {
            sActor.Status.aspd_skill -= 800;
            float factor = 0;
            //float[] factors = 2.0f + 0.4f * level;
            //float factor = 2.0f + 0.4f * level;
            args.argType = SkillArg.ArgType.Attack;
            //args.type = ATTACK_TYPE.SLASH;
            if (sActor.SwordACount < 3)
            {
                factor = 2.0f + 0.4f * level;
                //args.skill.BaseData.nAnim1 = args.skill.BaseData.nAnim2 = 332;
            }
            else
            {
                factor = 4.5f + 0.9f * level;
            }
            if (sActor.SwordACount != 0)
                SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, factor);
            Stiff skill = new Stiff(args.skill, dActor, 1000);
            SkillHandler.ApplyAddition(dActor, skill);
            sActor.Status.aspd_skill += 800;
            sActor.SwordACount++;
            //if (sActor.SwordACount == 4)
            //    SkillHandler.Instance.PushBack(sActor, dActor, 2);
        }
    }
    /// <summary>
    /// 利劍語意（ソードアセイル）最终段
    /// </summary>
    //public class SordAssailSEQ : ISkill
    //{
    //    #region ISkill Members
    //    public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
    //    {
    //        return 0;

    //    }


    //    public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
    //    {


    //        if (SkillHandler.Instance.CheckValidAttackTarget(sActor, dActor))
    //        {
    //            float factor = 2.0f + 0.4f * level;
    //            args.argType = SkillArg.ArgType.Attack;
    //            args.type = ATTACK_TYPE.BLOW;
    //            List<Actor> dest = new List<Actor>();
    //            dest.Add(dActor);
    //            args.delayRate = 4.5f;
    //            SkillHandler.Instance.PhysicalAttack(sActor, dest, args, sActor.WeaponElement, factor);
    //        }

    //    }
    //    #endregion
    //}
}

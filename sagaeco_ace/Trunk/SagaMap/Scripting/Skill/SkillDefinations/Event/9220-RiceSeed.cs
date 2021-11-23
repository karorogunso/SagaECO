
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.Event
{
    /// <summary>
    /// 種もみ
    /// </summary>
    public class RiceSeed : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 9999.0f;
            SkillHandler.Instance.FixAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
            dActor.MP += 9999;
            if (dActor.MP > dActor.MaxMP)
            {
                dActor.MP = dActor.MaxMP;
            }
            dActor.SP += 9999;
            if (dActor.SP > dActor.MaxSP)
            {
                dActor.SP = dActor.MaxSP;
            }
            args.Init();
            args.flag[0] = SagaLib.AttackFlag.HP_HEAL | AttackFlag.MP_HEAL | AttackFlag.SP_HEAL;
        }
        #endregion
    }
}
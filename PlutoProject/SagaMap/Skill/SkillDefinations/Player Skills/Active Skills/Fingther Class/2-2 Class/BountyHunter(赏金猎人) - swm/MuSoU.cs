using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.BountyHunter
{
    /// <summary>
    /// 參擊無雙（斬撃無双）
    /// </summary>
    public class MuSoU : ISkill
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
            sActor.MuSoUCount = 0;

            for (int i = 1; i <= 10; i++)
            {
                AutoCastInfo aci = SkillHandler.Instance.CreateAutoCastInfo(2402, level, 250);
                args.autoCast.Add(aci);
            }
            //SkillHandler.Instance.PushBack(sActor, dActor, 2);
        }
        #endregion
    }
}

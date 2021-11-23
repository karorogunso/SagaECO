using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S40011 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckSkillCanCastForWeapon(pc, args))
                return 5;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, dActor))
            {
                args.argType = SkillArg.ArgType.Attack;
                args.delayRate = 0.2f;
                SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, Elements.Water, 6f);

                Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                sActor.EP += (uint)(500);
                if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
            }
        }
        #endregion
    }
}

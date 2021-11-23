using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S43202 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            uint mp = (uint)(sActor.MaxMP * 0.15f);
            uint sp = (uint)(sActor.MaxSP * 0.15f);
            sActor.MP += mp;
            if (sActor.MP > sActor.MaxMP) sActor.MP = sActor.MaxMP;
            sActor.SP += sp;
            if (sActor.SP > sActor.MaxSP) sActor.SP = sActor.MaxSP;

            SkillHandler.Instance.ShowVessel(sActor, 0, (int)-mp, (int)-sp);

            Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, args, sActor, true);
        }
        #endregion
    }
}
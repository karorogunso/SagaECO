using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public partial class S21100 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("瘴气兵装CD"))
                return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int delay = 0;
            for (uint i = 5625; i < 5629; i++)
            {
                P_瘴气兵装 Effect = new P_瘴气兵装(sActor, i, delay, false);
                Effect.Activate();
                delay += 200;
            }
            P_瘴气兵装 buff = new P_瘴气兵装(sActor, 0, 900, true);
            buff.Activate();
            sActor.EP += 15;
            if (sActor.EP > sActor.MaxEP)
                sActor.EP = sActor.MaxEP;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }
    }
}

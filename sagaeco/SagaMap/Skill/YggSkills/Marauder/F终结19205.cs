using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S19205:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
             return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 6f;
            int count = 0;
            if (dActor.Status.Additions.ContainsKey("ASPDDOWN")) count++;
            if (dActor.Status.Additions.ContainsKey("CSPDDOWN")) count++;
            if (dActor.Status.Additions.ContainsKey("HITMELLEDOWN")) count++;
            if (dActor.Status.Additions.ContainsKey("HITRANGEDOWN")) count++;
            if (dActor.Status.Additions.ContainsKey("DEFDOWN")) count++;
            if (dActor.Status.Additions.ContainsKey("MDEFDOWN")) count++;
            if (dActor.Status.Additions.ContainsKey("ATKDOWN")) count++;
            if (dActor.Status.Additions.ContainsKey("ATKDOWN")) count++;
            if (dActor.Status.Additions.ContainsKey("Poison1")) count++;
            for (int i = 0; i < count; i++)
            {
                factor += 0.8f;
                sActor.EP += 250;
                if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
            }
            if (count >= 1)
                SkillHandler.Instance.ShowEffectByActor(dActor, 5171);
            if(count >= 9)
                SkillHandler.Instance.ShowEffectByActor(dActor, 5164);

            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.SLASH;
            args.delayRate = 10f;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, factor);
            SkillHandler.Instance.ShowEffectOnActor(dActor, 5275);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }
        #endregion
    }
}

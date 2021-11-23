using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S40201 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckSkillCanCastForWeapon(pc, args))
                return 0;
            return -5;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 3.2f;
            //SkillHandler.Instance.ShowEffectByActor(sActor, 4126);
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.BLOW;
            args.delayRate = 1f;
            List<Actor> dest = new List<Actor>();
            for (int i = 0; i < 7; i++)
            {
                dest.Add(dActor);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, dest, args, SagaLib.Elements.Neutral, factor);
            sActor.EP += 1050;
            if (sActor.EP >= sActor.MaxEP)
                sActor.EP = sActor.MaxEP;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }
        #endregion
    }
}

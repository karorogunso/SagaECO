using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.Hawkeye
{
    public class TimeBomb : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return SkillHandler.Instance.CheckPcLongAttack(pc);
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.Instance.PcArrowAndBulletDown(sActor);
            args.argType = SkillArg.ArgType.Attack;
            //args.type = ATTACK_TYPE.STAB;
            float factor = 2.0f + 0.3f * level;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, factor);
            Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ATTACK, null, sActor, false);
            args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(2515, level, 1000));
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S18504 : ISkill
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
            dActor = SkillHandler.Instance.GetdActor(sActor,args);
            if (dActor == null) return;

            float factor = 3.2f;
            switch (level)
            {
                case 1:
                    factor = 1.4f;
                    break;
                case 2:
                    factor = 1.6f;
                    break;
                case 3:
                    factor = 1.6f;
                    break;
            }
            //SkillHandler.Instance.ShowEffectByActor(sActor, 4126);
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.BLOW;
            args.delayRate = 0.3f;
            List<Actor> dest = new List<Actor>();
            for (int i = 0; i < 2; i++)
            {
                dest.Add(dActor);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, dest, args, SagaLib.Elements.Neutral, factor);
            sActor.EP += 200;
            if (sActor.EP >= sActor.MaxEP)
                sActor.EP = sActor.MaxEP;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);

            SkillHandler.Instance.SetNextComboSkill(sActor, 18504);
        }
        #endregion
    }
}

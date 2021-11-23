using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Wizard
{
    public class EnergyOne:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckValidAttackTarget(pc, dActor))
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
            float factor = 1f;
            ActorPC pc = (ActorPC)sActor;
            if (pc.Skills.ContainsKey(14008))
            {
                byte lv = pc.Skills[14008].Level;
                //factor *= 1f + (sActor.EP / 1000f);
                float rate = lv * 1f;
                
                int epheal =(int)(rate * pc.Int);
                if (sActor.EP > epheal)
                    sActor.EP -= (uint)epheal;
                else
                    sActor.EP = 0;
                Manager.MapManager.Instance.GetMap(pc.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
            }

            SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
        }

        #endregion
    }
}

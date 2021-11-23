using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S19001 : ISkill
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
            if (sActor.type != ActorType.MOB)
            {
                Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                SkillHandler.Instance.ChangdeWeapons(sActor, 0);
                float factor = 5f;
                if (sActor.Status.Additions.ContainsKey("Invisible"))//隐身加成
                {
                    factor += 5f;
                    sActor.EP += 500;
                }
                if (SkillHandler.Instance.GetIsBack(sActor, dActor))//背刺加成
                {
                    SkillHandler.Instance.ShowEffectByActor(dActor, 5150);
                    factor += 5f;
                    sActor.EP += 500;
                }

                if (factor > 17f)
                    SkillHandler.Instance.ShowEffectByActor(dActor, 5368);

                SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);

                sActor.EP += 500;
                if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
               
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
            }
            else
                SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, 5f);
        }
        #endregion
    }
}

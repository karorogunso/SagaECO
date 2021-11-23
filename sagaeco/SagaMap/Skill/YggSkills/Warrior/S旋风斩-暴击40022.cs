using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S40022 : ISkill
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
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> targets = map.GetActorsArea(sActor, 350, true);
            if (targets.Count < 1) return;
            List<Actor> dactors = new List<Actor>() ;
            foreach (var item in targets)
            {
                if(SkillHandler.Instance.CheckValidAttackTarget(sActor,item))
                {
                    //SkillHandler.Instance.PushBack(sActor, item, 1);
                    dactors.Add(item);
                }
            }
            if (dactors.Count < 1) return;
            SkillHandler.Instance.PhysicalAttack(sActor, dactors, args, Elements.Wind, 10f);
            SkillHandler.Instance.ShowEffectByActor(sActor, 4160);
            
            sActor.EP += 500;
            if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }
        #endregion
    }
}

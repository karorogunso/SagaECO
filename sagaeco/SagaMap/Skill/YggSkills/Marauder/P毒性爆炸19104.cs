using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S19104:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
             return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> dactors = map.GetActorsArea(sActor, 1000, false);
            List<Actor> targets = new List<Actor>();
            foreach (var item in dactors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    targets.Add(item);
                }
            }
            foreach (var item in targets)
            {
                if(item.Status.Additions.ContainsKey("Poison1"))
                {
                    int resttime = item.Status.Additions["Poison1"].RestLifeTime;
                    SkillHandler.RemoveAddition(item, "Poison1");
                    float factor = (float)(resttime / 1000.0f) * 3f;
                    SkillHandler.Instance.DoDamage(true, sActor, item, args, SkillHandler.DefType.Def, SagaLib.Elements.Neutral, 0, factor);
                    SkillHandler.Instance.ShowEffectByActor(item, 5282);
                }
            }

            if(targets.Count == 1)
            {
                sActor.EP += 500;
                if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
            }
            else if(targets.Count > 1)
            {
                sActor.EP += 1500;
                if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
            }

            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }
        #endregion
    }
}

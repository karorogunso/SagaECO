using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 光之惩戒：小范围aoe
    /// </summary>
    public class S43001 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 3.5f;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            if (sActor.Status.Additions.ContainsKey("属性契约"))
            {
                if (((OtherAddition)(sActor.Status.Additions["属性契约"])).Variable["属性契约"] == (int)Elements.Holy)
                {
                    factor = 6f;
                    sActor.EP += 300;
                    if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
                }
            }
            List<Actor> targets = map.GetActorsArea(dActor, 200, true);
            List<Actor> dactors = new List<Actor>();
            foreach (var item in targets)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    dactors.Add(item);
                }
            }
            if (dactors.Count < 1) return;
            SkillHandler.Instance.MagicAttack(sActor, dactors, args, Elements.Holy, factor);
        }
        #endregion
    }
}

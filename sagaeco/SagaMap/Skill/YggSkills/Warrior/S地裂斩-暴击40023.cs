using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S40023 : ISkill
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
            List<Actor> targets = map.GetActorsArea(dActor, 300, true);
            List<Actor> dactors = new List<Actor>();
            foreach (var item in targets)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    dactors.Add(item);
                }
            }
            if (dactors.Count < 1) return;
            SkillHandler.Instance.PhysicalAttack(sActor, dactors, args, Elements.Earth, 10f);

            sActor.EP += 500;
            if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }
        #endregion
    }
}
/*int dx0 = dActor.X - sActor.X;
int dy0 = dActor.Y - sActor.Y;
List<Actor> dactors = new List<Actor>();
foreach (var item in map.Actors)
{
    if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item.Value))
    {
        int dx1 = item.Value.X - sActor.X;
        int dy1 = item.Value.Y - sActor.Y;
        double dl = Math.Abs(dx0 * dx1 + dy0 * dy1) / Math.Sqrt(dx0 ^ 2 + dy0 ^ 2);
        double dn = Math.Sqrt(dx1 * dx1 + dy1 * dy1 - dl * dl);
        if (dl < 150 && dn < 50)
        {
            dactors.Add(item.Value);
        }
    }
}
if (dactors.Count < 1) return;*/

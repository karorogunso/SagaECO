using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S43108 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 3.5f;

            List<Actor> actors = Manager.MapManager.Instance.GetMap(dActor.MapID).GetActorsArea(dActor, 100, true);
            List<Actor> affected = new List<Actor>();
            List<Actor> Darksaffected = new List<Actor>();
            //取得有效Actor（即怪物）
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                {
                    affected.Add(i);
                    if (dActor.Darks == 1)
                    {
                        Darksaffected.Add(i);
                    }
                    dActor.Darks = 0;

                }
            }
            if (sActor.Status.Additions.ContainsKey("属性契约"))
            {
                if (((OtherAddition)(sActor.Status.Additions["属性契约"])).Variable["属性契约"] == (int)Elements.Dark)
                {
                    Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                    factor = 5f;
                    sActor.EP += 500;
                    if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
                    if (Darksaffected.Count > 0)
                    {
                        Manager.MapManager.Instance.GetMap(sActor.MapID).SendEffect(dActor, 5202);
                        foreach (var item in Darksaffected)
                        {
                            SkillHandler.Instance.DoDamage(false, sActor, item, args, SkillHandler.DefType.MDef, Elements.Dark, 50, 3f);
                        }
                    }
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, affected, args, SagaLib.Elements.Dark, factor);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;


namespace SagaMap.Skill.SkillDefinations
{
    public class S43107 : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
             return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 4f;
            float suck = 0f;
            List<Actor> ac = new List<Actor>();
            ac.Add(dActor);
            if (sActor.Status.Additions.ContainsKey("属性契约"))
            {
                if (((OtherAddition)(sActor.Status.Additions["属性契约"])).Variable["属性契约"] == (int)Elements.Dark)
                {
                    factor = 6f;
                    suck = 0.1f;
                    if (dActor.Darks != 1)
                    {
                        Manager.MapManager.Instance.GetMap(sActor.MapID).SendEffect(dActor, 5081);
                        dActor.Darks = 1;
                    }
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, ac, args, SkillHandler.DefType.MDef, SagaLib.Elements.Dark, 50, factor, 0, false, false, suck);
        }
        #endregion
    }
}

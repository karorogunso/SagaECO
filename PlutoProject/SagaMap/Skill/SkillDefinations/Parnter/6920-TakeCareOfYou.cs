using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;

namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// 手当ていたします。
    /// </summary>
    public class TakeCareOfYou : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0;
            factor = -5.3f;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 500, false);
            List<Actor> realAffected = new List<Actor>();
            Actor ActorlowHP = sActor;
            foreach (Actor act in affected)
            {
                if (act.type == ActorType.PARTNER || act.type == ActorType.PC || act.type == ActorType.PET)
                {
                    if ((float)(act.HP / act.MaxHP) < (float)(ActorlowHP.HP / ActorlowHP.MaxHP) && act.type == ActorType.MOB)
                    {
                        ActorlowHP = act;
                    }
                }

            }
            SkillHandler.Instance.MagicAttack(sActor, ActorlowHP, args, SkillHandler.DefType.IgnoreAll, SagaLib.Elements.Holy, factor);
            //SkillHandler.Instance.(sActor, ActorlowHP, args, SagaLib.Elements.Holy, factor);
        }
        #endregion
    }
}
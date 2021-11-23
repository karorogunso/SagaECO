using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Scripting;

namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// ▲伊邪那美式抜刀術　千斬り
    /// </summary>
    public class Thousand : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }



        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 11.0f;
            List<Actor> actors = Manager.MapManager.Instance.GetMap(sActor.MapID).GetActorsArea(dActor, 100, true);
            List<Actor> affected = new List<Actor>();
            foreach (var item in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                    affected.Add(item);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, affected, args, sActor.WeaponElement, factor);


        }
        #endregion
    }
}

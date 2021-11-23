using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Monster
{
    /// <summary>
    /// ソーマミラージュ (索玛幻影?)
    /// </summary>
    public class SomaMirage : ISkill, MobISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void BeforeCast(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            return;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            List<Actor> affected = new List<Actor>();
            List<Actor> inrange = Manager.MapManager.Instance.GetMap(dActor.MapID).GetActorsArea(dActor, 500, false);
            foreach (var item in inrange)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                    affected.Add(item);
            }
            SkillHandler.Instance.FixAttack(sActor, affected, args, SagaLib.Elements.Neutral, 5000);
        }
    }
}

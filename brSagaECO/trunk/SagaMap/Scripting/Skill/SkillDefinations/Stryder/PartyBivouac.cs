
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Stryder
{
    public class PartyBivouac : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(sActor, 300, true);
            List<Actor> affected = new List<Actor>();
            foreach (Actor i in actors)
            {
                HPRecovery skill = new HPRecovery(args.skill, sActor, 300000, 5000);
                SkillHandler.ApplyAddition(sActor, skill);
            }
        }
        #endregion
    }
}
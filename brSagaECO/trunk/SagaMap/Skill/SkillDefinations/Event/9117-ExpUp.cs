
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Event
{
    /// <summary>
    /// X 領域（エキスパンド）
    /// </summary>
    public class ExpUp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 300000;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 100, true);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (act.type== ActorType.PC )
                {
                    SagaMap.Skill.Additions.Global.ExpUp skill = new SagaMap.Skill.Additions.Global.ExpUp(args.skill , act, lifetime);
                    SkillHandler.ApplyAddition(act, skill);
                }
            }
        }
        #endregion
    }
}
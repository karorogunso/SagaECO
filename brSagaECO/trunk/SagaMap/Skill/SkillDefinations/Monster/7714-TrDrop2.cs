using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Monster
{
    /// <summary>
    /// 解除憑依
    /// </summary>
    public class TrDrop2 : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //List<Actor> affected = map.GetActorsArea(sActor, 150, false);
            List<Actor> affected = map.GetActorsArea(sActor, 750, false);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (act.type == ActorType.PC)
                {
                    SkillHandler.Instance.PossessionCancel((ActorPC)act, SagaLib.PossessionPosition.NONE);
                }
            }
        }
        #endregion
    }
}





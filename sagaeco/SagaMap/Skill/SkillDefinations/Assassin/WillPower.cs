using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Assassin
{
    /// <summary>
    ///  氣合（気合）
    /// </summary>
    public class WillPower : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            ActorPC actorPC = (ActorPC)sActor;
            try
            {
                List<string> WillBeRemove = new List<string>();
                foreach (KeyValuePair<string, Addition> s in actorPC.Status.Additions)
                {
                    Addition addition = (Addition)s.Value;
                    WillBeRemove.Add(s.Key);
                    if (addition.Activated)
                    {
                        addition.AdditionEnd();
                    }
                    addition.Activated = false;

                }
                foreach (var AdditionName in WillBeRemove)
                {
                    actorPC.Status.Additions.Remove(AdditionName);
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion
    }
}

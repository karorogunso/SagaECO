using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Cardinal
{
    public class CureAll : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (sActor.Party != null) return 0;
            else return -12;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            List<Actor> realAffected = new List<Actor>();
            ActorPC sPC = (ActorPC)sActor;
            int[] cureRate = new int[] { 0, 40, 60, 60, 60, 60, 100 }; 
            foreach (ActorPC act in sPC.Party.Members.Values)
            {
                if (act.Online)
                {
                    if (act.Party.ID != 0 && !act.Buff.Dead && act.MapID == sActor.MapID)
                    {
                        if (SagaLib.Global.Random.Next(0, 100) <= cureRate[level])
                        {
                            RemoveAddition(dActor, "Poison");
                            RemoveAddition(dActor, "鈍足");
                            RemoveAddition(dActor, "Stone");
                            RemoveAddition(dActor, "Silence");
                            RemoveAddition(dActor, "Stun");
                            RemoveAddition(dActor, "Sleep");
                            RemoveAddition(dActor, "Frosen");
                            RemoveAddition(dActor, "Confuse");
                        }
                    }
                }
            }
        }
        public void RemoveAddition(Actor actor, String additionName)
        {
            if (actor.Status.Additions.ContainsKey(additionName))
            {
                Addition addition = actor.Status.Additions[additionName];
                actor.Status.Additions.Remove(additionName);
                if (addition.Activated)
                {
                    addition.AdditionEnd();
                }
                addition.Activated = false;
            }
        }
    }
}

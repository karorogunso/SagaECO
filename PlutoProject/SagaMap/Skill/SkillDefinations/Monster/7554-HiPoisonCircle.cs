using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;


namespace SagaMap.Skill.SkillDefinations.Monster
{
    public class HiPoisonCircie : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 5000;
            args.dActor = 0;

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 300, false);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    realAffected.Add(act);
                }
            }
            foreach (Actor i in realAffected)
            {
                Additions.Global.Poison skill = new SagaMap.Skill.Additions.Global.Poison(args.skill, i, lifetime);
                SkillHandler.ApplyAddition(i, skill);
                Additions.Global.Stun skill1 = new SagaMap.Skill.Additions.Global.Stun(args.skill, i, lifetime);
                SkillHandler.ApplyAddition(i, skill);
            }
        }
    }
}
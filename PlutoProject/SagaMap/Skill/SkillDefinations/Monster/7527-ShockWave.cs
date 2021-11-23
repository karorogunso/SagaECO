using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Monster
{
    public class ShockWave : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //float factor = 0;
            //factor = 1.5f;
            //Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //List<Actor> actors = map.GetActorsArea(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), 200, null);
            //List<Actor> realAffected = new List<Actor>();
            //foreach (Actor act in actors)
            //{
            //    if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
            //    {
            //        realAffected.Add(act);
            //    }
            //}
            //SkillHandler.Instance.PhysicalAttack(sActor, realAffected, args, SagaLib.Elements.Neutral, factor);

            float factor = 1.5f;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(sActor, 200, false);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act)&&(act.type == ActorType.PC || act.type == ActorType.PET || act.type == ActorType.PARTNER))
                {
                    realAffected.Add(act);
                }
            }
            SkillHandler.Instance.PhysicalAttack(sActor, realAffected, args, sActor.WeaponElement, factor);
        }
    }
}

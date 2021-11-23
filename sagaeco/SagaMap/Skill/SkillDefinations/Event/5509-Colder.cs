using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.ActorEventHandlers;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Event
{
    /// <summary>
    /// 魅惑腳踢
    /// </summary>
    public class Colder : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            Manager.MapManager.Instance.GetMap(dActor.MapID).SendEffect(dActor, 5089);
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 200, false);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                Freeze skill = new Freeze(args.skill, act, 5000);
                SkillHandler.ApplyAddition(act, skill);
                if(SkillHandler.Instance.CheckValidAttackTarget(sActor,dActor))
                    realAffected.Add(act);
            }
            SkillHandler.Instance.MagicAttack(sActor, realAffected, args, SagaLib.Elements.Water, 2f);
        }
        #endregion
    }
}
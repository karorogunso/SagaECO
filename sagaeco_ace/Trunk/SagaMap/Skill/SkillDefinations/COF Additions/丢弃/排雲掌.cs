using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;


namespace SagaMap.Skill.SkillDefinations.X
{
    class Rowofcloudpalm : MobISkill
    {
        #region ISkill Members

        public void BeforeCast(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(sActor, 1000, false, true);
            List<Actor> realAffected = new List<Actor>();
            List<Actor> FixActors = new List<Actor>();
            List<Actor> NormalActors = new List<Actor>();
            foreach (Actor act in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                    realAffected.Add(act);
            }

            foreach (Actor item in realAffected)
            {
                if (item.Status.Additions.ContainsKey("Frosen"))
                    FixActors.Add(item);
                else
                    NormalActors.Add(item);
            }

            SkillArg arg2 = new SkillArg();
            arg2 = args.Clone();
            SkillHandler.Instance.FixAttack(sActor, FixActors, arg2, Elements.Neutral, 500);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, arg2, sActor, true);

            SkillHandler.Instance.PhysicalAttack(sActor, NormalActors, args, Elements.Neutral, 1f);
        }
        #endregion
    }
}

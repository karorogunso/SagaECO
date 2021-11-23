using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S40401 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            List<Actor> targets = new List<Actor>();
            if(sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Party == null) targets.Add(pc);
                else
                {
                    Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                    List<Actor> actors = map.GetActorsArea(sActor, 500, true);
                    foreach (var item in actors)
                    {
                        if(item.type == ActorType.PC)
                        {
                            if (((ActorPC)item).Party == pc.Party)
                                targets.Add(item);
                        }
                    }
                }
                foreach (var item in targets)
                {
                    DefUp buff41 = new DefUp(args.skill, item, 60000, 15);
                    MDefUp buff42 = new MDefUp(args.skill, item, 60000, 15);
                    SkillHandler.ApplyAddition(item, buff41);
                    SkillHandler.ApplyAddition(item, buff42);
                    SkillHandler.Instance.ShowEffectOnActor(item, 5010);
                }
            }
        }
        #endregion
    }
}

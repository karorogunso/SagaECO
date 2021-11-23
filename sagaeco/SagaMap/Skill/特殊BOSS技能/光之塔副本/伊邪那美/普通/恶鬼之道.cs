using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31021 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);

            SkillHandler.Instance.ShowEffectOnActor(sActor, 5082);

            List<Actor> da = map.GetActorsArea(sActor, 2000, false);
            List<Actor> targets = new List<Actor>();
            foreach (var item in da)
            {
                if(SkillHandler.Instance.CheckValidAttackTarget(sActor,item))
                    targets.Add(item);
            }

            if(targets.Count > 0)
            {
                Actor target;
                if (targets.Count == 1)
                    target = targets[0];
                else
                target = targets[SagaLib.Global.Random.Next(0, targets.Count - 1)];

                恶鬼缠身 skill = new 恶鬼缠身(sActor, target);
                skill.Activate();
            }
            SkillHandler.Instance.ActorSpeak(sActor, "怯惧畏妄，恶鬼缠身！");
        }
    }
}

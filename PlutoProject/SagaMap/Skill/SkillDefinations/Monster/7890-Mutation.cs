using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Monster
{
    /// <summary>
    /// ミューテイション(突变)
    /// </summary>
    public class Mutation : MobISkill
    {
        public void BeforeCast(Actor sActor, Actor dActor, SkillArg args, byte level)
        {

        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            ActorMob mob = sActor as ActorMob;
            //Manager.MapClientManager.EnterCriticalArea();
            foreach (var item in sActor.AttackElements.Keys)
                sActor.Status.elements_skill[item] = 0;

            int rnd = SagaLib.Global.Random.Next(1, 100);
            if (rnd > 0 && rnd <= 20)
                sActor.Status.elements_skill[SagaLib.Elements.Fire] = 100;
            else if (rnd > 20 && rnd <= 40)
                sActor.Status.elements_skill[SagaLib.Elements.Water] = 100;
            else if (rnd > 40 && rnd <= 60)
                sActor.Status.elements_skill[SagaLib.Elements.Wind] = 100;
            else if (rnd > 60 && rnd <= 80)
                sActor.Status.elements_skill[SagaLib.Elements.Earth] = 100;
            else
                sActor.Status.elements_skill[SagaLib.Elements.Dark] = 100;
            //Manager.MapClientManager.LeaveCriticalArea();
            Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, sActor, false);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;
using SagaDB.Item;


namespace SagaMap.Skill.SkillDefinations.Royaldealer
{
    class TimeIsMoney : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            ActorPC pc = sActor as ActorPC;
            float factor = new float[] { 0, 5.0f, 6.0f, 7.0f, 8.5f, 10.0f }[level];
            if (pc.Gold >= 90000000)
                factor += 25.0f;
            else if (pc.Gold >= 50000000)
                factor += 22.0f;
            else if (pc.Gold >= 5000000)
                factor += 19.0f;
            else if (pc.Gold >= 500000)
                factor += 14.0f;
            else if (pc.Gold >= 50000)
                factor += 10.0f;

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> acts = map.GetActorsArea(dActor, 250, true);
            List<Actor> realaffected = new List<Actor>();
            foreach (var item in acts)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                    realaffected.Add(item);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, realaffected, args, SkillHandler.DefType.Def, Elements.Neutral, 0, factor, false, 0, false, 20, 0);
        }
    }
}

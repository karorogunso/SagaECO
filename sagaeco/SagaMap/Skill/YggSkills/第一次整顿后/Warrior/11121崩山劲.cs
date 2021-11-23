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
    public class S11121 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 6.4f + 0.6f * level;

            if (sActor.Status.Additions.ContainsKey("重锤火花"))
                factor += factor * sActor.TInt["重锤火花提升%"] / 100f;


            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> targets = map.GetActorsArea(sActor, 300, true);
            List<Actor> dest = new List<Actor>();
            foreach (var item in targets)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    dest.Add(item);
                    if (!item.Status.Additions.ContainsKey("崩山劲晕眩CD"))
                    {
                        OtherAddition skill = new OtherAddition(null, item, "崩山劲晕眩CD", 45000);
                        SkillHandler.ApplyAddition(item, skill);
                        if (!item.Status.Additions.ContainsKey("Stun"))
                        {
                            Stun stun = new Stun(null, item, 4000 + level * 1000);
                            SkillHandler.ApplyAddition(item, stun);
                        }
                    }
                }
            }

            SkillHandler.Instance.PhysicalAttack(sActor, dest, args, SkillHandler.DefType.Def, Elements.Neutral, 0, factor, true, 0, true);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31165 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {

            SkillHandler.Instance.ShowEffectOnActor(dActor, 5050);
            float damage = 2.5f;
            if (dActor.HP < dActor.MaxHP / 2)
                damage = 5f;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, Elements.Water, damage);

            OtherAddition skill = new OtherAddition(args.skill, dActor, "冷灰的大镰", 8000);
            skill.OnAdditionEnd += (s, e) =>
            {
                Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
                List<Actor> actors = map.GetActorsArea(dActor, 300, true);
                List<Actor> Targets = new List<Actor>();
                foreach (var target in actors)
                    if (SkillHandler.Instance.CheckValidAttackTarget(sActor, target))
                        Targets.Add(target);
                if (Targets.Count == 0)
                    return;
                foreach (var target in Targets)
                {
                    Freeze fz = new Freeze(null, target, 4000/ Targets.Count);
                    SkillHandler.ApplyAddition(target, fz);
                }
            };

            SkillHandler.ApplyAddition(dActor, skill);
            if (dActor.HP <= 0)//如果造成伤害后目标死亡
            {
                if (!sActor.Status.Additions.ContainsKey("瘴气兵装"))
                {
                    瘴气兵装 buff = new 瘴气兵装(null, sActor, 15000);
                    SkillHandler.ApplyAddition(sActor, buff);
                }
            }
        }
    }
}

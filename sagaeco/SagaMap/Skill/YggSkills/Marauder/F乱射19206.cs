using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S19206 : ISkill
    {

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckSkillCanCastForWeapon(pc, args))
                return 0;
            return -5;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 2.5f;
            //SkillHandler.Instance.ShowEffectByActor(sActor, 4126);
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.STAB;
            args.delayRate = 4f;
            List<Actor> dest = new List<Actor>();
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(sActor, 700, false);
            List<Actor> targets = new List<Actor>();
            foreach (var item in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    targets.Add(item);
                }
            }
            if (targets.Count == 0) return;
            for (int i = 0; i < 10; i++)
            {
                Actor a = targets[SagaLib.Global.Random.Next(0, targets.Count - 1)];
                if (a == null || targets.Count == 0) continue;
                if (SagaLib.Global.Random.Next(0, 100) < 20)
                    RandomApplyDebuff(dActor);
                dest.Add(a);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, dest, args, SagaLib.Elements.Neutral, factor);
        }
        void RandomApplyDebuff(Actor dactor)
        {
            //待补充
        }
    }
}

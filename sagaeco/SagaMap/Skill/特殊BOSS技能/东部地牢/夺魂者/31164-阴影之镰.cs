using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31164 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            /*-------------------获取随机目标-----------------*/

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(sActor, 5000, false);
            List<Actor> Targets = new List<Actor>();
            foreach (var target in actors)
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, target) && target != dActor && target.HP > target.MaxHP * 0.9f)
                    Targets.Add(target);
            if (Targets.Count == 0)
                Targets.Add(dActor);
            int ran = SagaLib.Global.Random.Next(0, Targets.Count - 1);
            Actor Target = Targets[ran];
            /*-------------------获取随机目标完成-----------------*/

            short[] pos = map.GetRandomPosAroundPos(Target.X, Target.Y, 100);
            map.TeleportActor(sActor, pos[0], pos[1]);
            //SkillHandler.Instance.ShowEffectOnActor(sActor, 4178);
            SkillHandler.Instance.PhysicalAttack(sActor, Target, args, Elements.Dark, 2f);//对目标造成伤害
            SkillHandler.Instance.ShowEffectOnActor(Target, 5080, sActor);

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

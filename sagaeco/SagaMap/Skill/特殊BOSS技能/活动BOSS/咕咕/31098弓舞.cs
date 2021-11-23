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
    public class S31099 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(sActor, 700, false);

            foreach (var item in actors)
            {
                if(SkillHandler.Instance.CheckValidAttackTarget(sActor,item))
                {
                    Silence sil = new Silence(null, item, 5000);
                    SkillHandler.ApplyAddition(item, sil);
                    SkillHandler.Instance.DoDamage(true, sActor, item, args, SkillHandler.DefType.IgnoreAll, Elements.Neutral, 100, 3f);
                    SkillHandler.Instance.ShowEffectOnActor(item, 5111);
                }
            }
            SkillHandler.Instance.ActorSpeak(sActor, "明明说过马上到的，大家却都在说我坏话，闭嘴啦你们！");
        }
    }
}

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
    public class S31098 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(dActor, 800, true);
            
            foreach (var item in actors)
            {
                float f = 2f;
                if(SkillHandler.Instance.CheckValidAttackTarget(sActor,item))
                {
                    if (item == dActor) f = 5f;
                    SkillHandler.Instance.DoDamage(true, sActor, item, args, SkillHandler.DefType.IgnoreAll, Elements.Neutral, 100, f);
                }
            }
            SkillHandler.Instance.ActorSpeak(sActor, "你们对咕咕鸡的力量一无所知，咕咕鸡才不是鸽子啊！");
        }
    }
}

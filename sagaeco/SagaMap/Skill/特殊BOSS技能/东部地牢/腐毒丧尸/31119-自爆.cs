using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31119 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
            SkillHandler.Instance.ShowEffectOnActor(sActor, 5205);
            sActor.HP = 1;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
            List<Actor> actors;//定一个actor的列表，用来装释放者周围的所有Actor的
            actors = map.GetActorsArea(sActor, 5000, false);//获取sActor周围10格内的所有Actor，并装在actors里
            foreach (var item in actors)//遍历刚刚获得的actors
            {
                if(SkillHandler.Instance.CheckValidAttackTarget(sActor,item))
                {
                    SkillHandler.Instance.CauseDamage(sActor, item, (int)(item.HP - 1));
                    SkillHandler.Instance.ShowVessel(item, (int)(item.HP - 1));
                    SkillHandler.Instance.ShowEffectOnActor(item, 5021);
                }
            }
            硬直 skill = new 硬直(null, sActor, 10000);
            SkillHandler.ApplyAddition(sActor, skill);
        }
    }
}

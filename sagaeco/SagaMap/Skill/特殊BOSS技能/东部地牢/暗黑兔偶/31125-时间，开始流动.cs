using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;
using SagaMap.ActorEventHandlers;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31125 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(sActor.MapID);//根据释放者的地图ID，获取地图数据，保存在map里
            List<Actor> actors;//定一个actor的列表，用来装释放者周围的所有Actor的
            actors = map.GetActorsArea(sActor, 1000, false);//获取sActor周围10格内的所有Actor，并装在actors里
            List<Actor> Targets = new List<Actor>();//再定一个Actor的列表，用来装可以供释放者攻击的所有Actor
            SkillHandler.Instance.ShowEffectOnActor(sActor, 4172);
            foreach (var item in actors)//遍历刚刚获得的actors
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))//检查sActor是否可以攻击遍历的item
                    Targets.Add(item);//如果可以攻击，就加进Targets里
            }

            if (sActor.type == ActorType.MOB)
            {
                ActorMob mob = (ActorMob)sActor;
                MobEventHandler e = (MobEventHandler)mob.e;
                foreach (var item in Targets)//遍历刚刚获得的actors
                {
                    if(item.Status.Additions.ContainsKey("Stun"))
                    {
                        SkillHandler.RemoveAddition(item, "Stun");
                        SkillHandler.Instance.CauseDamage(sActor, item, 66666);
                        SkillHandler.Instance.ShowVessel(item, 66666);
                        SkillHandler.Instance.ShowEffectOnActor(item, 5336);
                    }
                    else if(e.AI.DamageTable.ContainsKey(item.ActorID))
                    {
                        int damage = e.AI.DamageTable[item.ActorID] / 100;
                        e.AI.DamageTable[item.ActorID] = 1;
                        SkillHandler.Instance.CauseDamage(sActor, item, damage);
                        SkillHandler.Instance.ShowVessel(item, damage);
                        SkillHandler.Instance.ShowEffectOnActor(item, 5288);
                    }
                }
            }
        }
    }
}

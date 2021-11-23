using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31149 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(sActor,3000,false);
            foreach (var item in actors)
            {
                if(SkillHandler.Instance.CheckValidAttackTarget(sActor,item))
                {
                    if (item.Status.Additions.ContainsKey("AtkUp")) continue;
                    SkillHandler.Instance.ShowEffectOnActor(item, 5109);
                    if(!item.Status.Additions.ContainsKey("深海毒液"))
                    {
                        OtherAddition skill = new OtherAddition(null, item, "深海毒液", 10000,1000);
                        skill.OnAdditionStart += (s, e) =>
                        {
                            item.Buff.Poison = true;
                            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, item, true);
                        };
                        skill.OnAdditionEnd += (s, e) =>
                        {
                            item.Buff.Poison = false;
                            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, item, true);
                        };
                        skill.OnUpdate += (s, e) =>
                        {
                            int damage = (int)(item.MaxHP * 0.05f);
                            /*if (item.Status.Additions.ContainsKey("漆黑之火"))
                                damage *= 2;*/
                            SkillHandler.Instance.CauseDamage(sActor, item, damage);
                            SkillHandler.Instance.ShowVessel(item, damage);
                        };
                        SkillHandler.ApplyAddition(item, skill);
                    }
                    else
                        ((OtherAddition)item.Status.Additions["深海毒液"]).endTime = DateTime.Now + new TimeSpan(0, 0, 0, 10);
                }
            }
        }
    }
}

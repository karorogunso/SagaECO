using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31118 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, Elements.Dark, 3f);
            SkillHandler.Instance.ShowEffectOnActor(dActor, 5021);
            if (dActor.Status.Additions.ContainsKey("腐毒丧尸感染"))
            {
                SkillHandler.Instance.ShowEffectOnActor(dActor, 5072);
                List<Actor> actors = map.GetActorsArea(dActor, 500, false);
                foreach (var item in actors)
                {
                    if(SkillHandler.Instance.CheckValidAttackTarget(sActor,item))
                        感染毒(item);
                }
            }
            感染毒(dActor);
        }
        void 感染毒(Actor dActor)
        {
            Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
            if (dActor.Status.Additions.ContainsKey("腐毒丧尸感染"))
            {
                Addition 腐毒丧尸感染 = dActor.Status.Additions["腐毒丧尸感染"];
                TimeSpan span = new TimeSpan(0, 0, 0, 0, 10000);
                ((OtherAddition)腐毒丧尸感染).endTime = DateTime.Now + span;
            }
            else
            {
                OtherAddition skill = new OtherAddition(null, dActor, "腐毒丧尸感染", 10000);
                skill.OnAdditionStart += (s, e) =>
                {
                    dActor.Buff.Poison = true;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, dActor, true);
                };
                skill.OnAdditionEnd += (s, e) =>
                {
                    dActor.Buff.Poison = false;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, dActor, true);
                };
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
    }
}

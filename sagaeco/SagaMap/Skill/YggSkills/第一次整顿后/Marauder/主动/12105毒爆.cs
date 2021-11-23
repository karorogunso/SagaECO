using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12105 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float MaxFactor = 4f + 4f * level;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> dactors = map.GetActorsArea(sActor, 1000, false);
            List<Actor> targets = new List<Actor>();
            foreach (var item in dactors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    targets.Add(item);
                }
            }
            foreach (var item in targets)
            {
                if (item.Status.Additions.ContainsKey("Poison1"))
                {
                    int resttime = item.Status.Additions["Poison1"].RestLifeTime;
                    SkillHandler.RemoveAddition(item, "Poison1");

                    float factor = MaxFactor;
                    int MaxStunTime = 500 + level * 1500;
                    int StunTime = MaxStunTime;
                    if (!item.Status.Additions.ContainsKey("毒爆晕眩CD"))
                    {
                        OtherAddition skill = new OtherAddition(null, item, "毒爆晕眩CD", 60000);
                        SkillHandler.ApplyAddition(item, skill);
                        if (!item.Status.Additions.ContainsKey("Stun"))
                        {
                            Stun stun = new Stun(null, item, StunTime);
                            SkillHandler.ApplyAddition(item, stun);
                        }
                    }
                    else if (factor < 3f) factor = 3f;


                    if (factor > MaxFactor) factor = MaxFactor;
                    SkillHandler.Instance.DoDamage(false, sActor, item, args, SkillHandler.DefType.MDef, SagaLib.Elements.Neutral, 0, factor);



                    SkillHandler.Instance.ShowEffectByActor(item, 5282);
                }
            }
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }
    }
}

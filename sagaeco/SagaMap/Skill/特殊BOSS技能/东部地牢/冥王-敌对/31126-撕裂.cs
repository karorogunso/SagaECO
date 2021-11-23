using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31126 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, 1.5f);
            if(dActor.Status.Additions.ContainsKey("撕裂流血"))
            {
                Addition 撕裂流血 = dActor.Status.Additions["撕裂流血"];
                TimeSpan span = new TimeSpan(0, 0, 0, 0, 10000);
                ((OtherAddition)撕裂流血).endTime = DateTime.Now + span;
                if (dActor.TInt["撕裂流血层数"] < 5)
                    dActor.TInt["撕裂流血层数"]++;
            }
            else
            {
                dActor.TInt["撕裂流血层数"] = 1;
                OtherAddition 撕裂流血 = new OtherAddition(null, dActor, "撕裂流血", 20000, 2000);
                撕裂流血.OnUpdate += (s, e) =>
                 {
                     float rate = dActor.TInt["撕裂流血层数"] * 0.02f;
                     int damage = (int)(dActor.MaxHP * rate);
                     SkillHandler.Instance.CauseDamage(sActor, dActor, damage,true);
                     SkillHandler.Instance.ShowVessel(dActor, damage);
                 };
                SkillHandler.ApplyAddition(dActor, 撕裂流血);
            }
        }
    }
}

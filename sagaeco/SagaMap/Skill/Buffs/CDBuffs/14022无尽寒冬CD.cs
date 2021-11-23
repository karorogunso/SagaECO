using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaMap.Skill.SkillDefinations;

namespace SagaMap.Skill.Additions.Global
{
    public class 无尽寒冬CD : ICDBuff
    {
        void ICDBuff.ApplyBuff(Actor actor, int lifetime)
        {
            string BuffName = "无尽寒冬CD";
            if (!actor.Status.Additions.ContainsKey(BuffName))
            {
                StableAddition buff = new StableAddition(null, actor, BuffName, lifetime);
                buff.OnAdditionEnd += (s, e) =>
                {
                    SkillHandler.Instance.ShowEffectOnActor(actor, 5084);
                    Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("[幻界·无尽寒冬] 可以再次使用了");
                };
                SkillHandler.ApplyAddition(actor, buff);
            }
            else
                (actor.Status.Additions[BuffName] as StableAddition).endTime = DateTime.Now + new TimeSpan(0, 0, 0, 0, lifetime);
        }

    }
}

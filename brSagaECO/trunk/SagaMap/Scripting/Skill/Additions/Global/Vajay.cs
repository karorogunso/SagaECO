using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class Vajay : DefaultBuff
    {
        public Vajay(SagaDB.Skill.Skill skill, Actor actor, int lifetime)
            : base(skill, actor, "Vajay", lifetime, 1000)
        {
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("春哥状态已进入");
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("春哥状态消失了！");
        }
    }
}

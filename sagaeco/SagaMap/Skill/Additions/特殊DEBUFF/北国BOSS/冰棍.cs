using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class 冰棍 : DefaultBuff
    {
        public 冰棍(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int plies)
            : base(skill, actor, "冰棍", lifetime, 1000)
        {
            this.OnAdditionStart += this.StartEventForSpeedDown;
            this.OnAdditionEnd += this.EndEventForSpeedDown;
            actor.Plies = (byte)plies;
            if (actor.type == ActorType.PC && actor.Plies == 1)
                Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("好冷哦——[冰棍层数1/3]");
            else if (actor.type == ActorType.PC && actor.Plies == 2)
                Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("好冷啊...快要变成冰棍了！[冰棍层数2/3]");
        }

        void StartEventForSpeedDown(Actor actor, DefaultBuff skill)
        {
            SkillHandler.Instance.ShowEffectByActor(actor, 5050); 
        }

        void EndEventForSpeedDown(Actor actor, DefaultBuff skill)
        {
            actor.Plies = 0;
            //Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("冰棍DEBUFF消失了！");
        }
    }
}

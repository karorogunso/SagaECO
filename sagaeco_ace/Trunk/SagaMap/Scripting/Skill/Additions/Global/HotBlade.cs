using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class HotBlade : DefaultBuff
    {
        public HotBlade(SagaDB.Skill.Skill skill, Actor actor, int lifetime)
            : base(skill, actor, "HotBlade", lifetime)
        {
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            byte count = (byte)(1 + skill.skill.Level * 2);
            if (actor.HotBlade < count)
                actor.HotBlade += 1;
            actor.HotBladeMark = 0;

            if (actor.HotBlade == count)
            {
                EffectArg arg = new EffectArg();
                arg.effectID = 5113;
                arg.actorID = actor.ActorID;
                if (actor.type == ActorType.PC)
                    Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, actor, true);
            }
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            if (actor.HotBladeMark == 0)
                actor.HotBlade = 0;
        }
    }
}

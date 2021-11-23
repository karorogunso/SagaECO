using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaDB.Skill;
namespace SagaMap.Skill.Additions.Global
{
    /// <summary>
    /// 居合姿态
    /// </summary>
    public class AttackRhythm : DefaultBuff
    {
        public AttackRhythm(SagaDB.Skill.Skill skill, Actor actor, int lifetime)
            : base(skill, actor, "AttackRhythm", lifetime)
        {
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            if (actor.AttackRhythm < 10)
            {
                if(actor.AttackRhythm == 0)
                    SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("进入进攻节奏！");
                actor.AttackRhythm++;
                if (actor.AttackRhythm == 10)
                    SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("进攻节奏达到了最大层数！");
            }
            actor.Status.aspd_skill_perc += (float)(actor.AttackRhythm * 0.1f);
            SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)actor).SendStatusExtend();
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            actor.Status.aspd_skill_perc -= (float)(actor.AttackRhythm * 0.1f);
            
            if (skill.endTime < DateTime.Now)
            {
                actor.AttackRhythm = 0;
                SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("效果消失了。");
                SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)actor).SendStatusExtend();
            }
        }
    }
}

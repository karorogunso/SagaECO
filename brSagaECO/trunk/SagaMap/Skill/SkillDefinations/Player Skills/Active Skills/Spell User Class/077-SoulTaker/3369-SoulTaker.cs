using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.SoulTaker
{
    public class SoulTaker : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = (30 + level * 5)*1000;
            int rate = 175 + level * 25;
            SoulTakerBuff skill = new SoulTakerBuff(args.skill, dActor, lifetime, rate);
            SkillHandler.ApplyAddition(sActor, skill);
        }
        public class SoulTakerBuff : DefaultBuff
        {
            public SoulTakerBuff(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int rate)
                : base(skill, actor, "SoulTaker", lifetime)
            {
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this["rate"] = rate;
            }
            void StartEvent(Actor actor, DefaultBuff skill)
            {
                int level = skill.skill.Level;
                actor.Buff.MainSkillPowerUp3RD = true;
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            }
            void EndEvent(Actor actor, DefaultBuff skill)
            {
                actor.Buff.MainSkillPowerUp3RD = false;
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            }
        }
    }
}
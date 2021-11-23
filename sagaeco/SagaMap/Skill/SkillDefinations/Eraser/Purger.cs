using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Eraser
{
    public class Purger : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public ushort speed_old;
        public void Proc(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg args, byte level)
        {
            ActorPC pc = (ActorPC)sActor;
            ApplySkill(pc, args);
        }
        public void ApplySkill(ActorPC dActor, SkillArg args)
        {
            int[] lifetimes = new int[] { 0, 60000, 90000, 120000, 150000, 180000 };
            int lifetime = lifetimes[args.skill.Level];
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "イレイザー", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.Purger_Lv = skill.skill.Level;
            actor.Status.speed_skill += 310;
            actor.Buff.三转未知强力增强 = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.Purger_Lv = 0;
            actor.Status.speed_skill -= 310;
            actor.Buff.三转未知强力增强 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}

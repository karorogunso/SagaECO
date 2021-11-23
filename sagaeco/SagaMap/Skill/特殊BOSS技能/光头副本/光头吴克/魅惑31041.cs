using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations
{
    public class S31041 : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            硬直 skill2 = new 硬直(args.skill, sActor, 5000);
            SkillHandler.ApplyAddition(sActor, skill2);

            List<Actor> targets = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(sActor, 3000);

            foreach (var item in targets)
            {
                OtherAddition skill = new OtherAddition(args.skill, item, "魅惑", 5000);
                skill.OnAdditionStart += Skill_OnAdditionStart;
                skill.OnAdditionEnd += Skill_OnAdditionEnd;
                SkillHandler.ApplyAddition(item, skill);
            }
        }

        private void Skill_OnAdditionEnd(Actor actor, OtherAddition skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            if (actor.type ==  ActorType.PC)
            {
                ActorPC pc = (ActorPC)actor;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.PLAYER_MODE, null, pc, true);
            }
            actor.Buff.九尾狐魅惑 = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        private void Skill_OnAdditionStart(Actor actor, OtherAddition skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            if (actor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)actor;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.PLAYER_MODE, null, pc, true);
            }
            actor.Buff.九尾狐魅惑 = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}


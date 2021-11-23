using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31162: ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            OtherAddition skill = new OtherAddition(args.skill, sActor, "琴音干扰", 4000, 0);
            skill.OnAdditionStart += (s, e) =>
            {
                sActor.Buff.九尾狐魅惑 = true;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, sActor, true);
            };
            skill.OnAdditionEnd += (s, e) =>
            {
                sActor.Buff.九尾狐魅惑 = false;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, sActor, true);
            };
            SkillHandler.ApplyAddition(sActor, skill);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S19007:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
                return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            OtherAddition oa = new OtherAddition(args.skill, sActor, "AllAvoid", 3000);
            oa.OnAdditionStart += Oa_OnAdditionStart;
            oa.OnAdditionEnd += Oa_OnAdditionEnd;
            SkillHandler.ApplyAddition(sActor, oa);
        }

        private void Oa_OnAdditionStart(Actor actor, OtherAddition skill)
        {
            actor.Buff.Spirit = true;
            actor.Buff.三转見切り = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        private void Oa_OnAdditionEnd(Actor actor, OtherAddition skill)
        {
            actor.Buff.Spirit = false;
            actor.Buff.三转見切り = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}

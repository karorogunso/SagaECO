using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S19200:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
             return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            ((ActorPC)sActor).TInt["Scorponok暴击"] = 0;
            OtherAddition oa = new OtherAddition(args.skill, sActor, "Scorponok", 60000);
            oa.OnAdditionStart += Oa_OnAdditionStart;
            oa.OnAdditionEnd += Oa_OnAdditionEnd;
            SkillHandler.ApplyAddition(sActor, oa);

            sActor.MP = sActor.MaxMP;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }

        private void Oa_OnAdditionStart(Actor actor, OtherAddition skill)
        {
            ((ActorPC)actor).TInt["Scorponok暴击"] = 0;
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.不知道5 = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        private void Oa_OnAdditionEnd(Actor actor, OtherAddition skill)
        {
            ((ActorPC)actor).TInt["Scorponok暴击"] = 0;
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.不知道5 = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        #endregion
    }
}

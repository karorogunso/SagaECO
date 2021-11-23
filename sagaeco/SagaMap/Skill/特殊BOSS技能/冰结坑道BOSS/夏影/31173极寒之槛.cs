using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;


namespace SagaMap.Skill.SkillDefinations
{
    public class S31173 : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            OtherAddition skill = new OtherAddition(null, dActor, "极寒之槛", 180000);
            skill.OnAdditionStart += (s, e) =>
            {
                dActor.Buff.Frosen = true;
                //dActor.Buff.CannotMove = true;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, dActor, true);
            };
            skill.OnAdditionEnd += (s, e) =>
            {
                dActor.Buff.Frosen = false;
                //dActor.Buff.CannotMove = false;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, dActor, true);
            };
            SkillHandler.ApplyBuffAutoRenew(dActor, skill);

        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S2571 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return -21;//关闭技能
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            OtherAddition oa = new OtherAddition(args.skill, sActor, "书页解放效果", 180000);
            oa.OnAdditionStart += (s, e) =>
            {
                sActor.Buff.不知道13 = true;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, sActor, true);
                PC.StatusFactory.Instance.CalcStatus((ActorPC)sActor);
            };
            oa.OnAdditionEnd += (s, e) =>
            {
                sActor.Buff.不知道13 = false;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, sActor, true);
                PC.StatusFactory.Instance.CalcStatus((ActorPC)sActor);
            };
            SkillHandler.ApplyAddition(sActor, oa);

        }
        #endregion
    }
}

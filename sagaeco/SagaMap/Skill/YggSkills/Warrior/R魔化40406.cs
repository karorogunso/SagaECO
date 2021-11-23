using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S40406 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.HP > pc.MaxHP * 0.5f)
                return 0;
            return -5;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            OtherAddition oa = new OtherAddition(args.skill, sActor, "魔化", 25000);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            sActor.HP = (uint)(sActor.HP * 0.2f);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
            oa.OnAdditionStart += (s, e) =>
            {
                s.Buff.魂之手 = true;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, s, true);
            };
            oa.OnAdditionEnd += (s, e) =>
            {
                s.Buff.魂之手 = false;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, s, true);
            };
            SkillHandler.ApplyAddition(sActor, oa);
        }

        #endregion
    }
}

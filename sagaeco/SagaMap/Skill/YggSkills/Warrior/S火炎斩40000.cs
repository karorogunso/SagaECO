using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S40000 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckSkillCanCastForWeapon(pc, args))
                return 0;
            return -5;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (sActor.Status.Additions.ContainsKey("地裂斩")) SkillHandler.RemoveAddition(sActor, "地裂斩");
            if (sActor.Status.Additions.ContainsKey("寒冰斩")) SkillHandler.RemoveAddition(sActor, "寒冰斩");
            if (sActor.Status.Additions.ContainsKey("旋风斩")) SkillHandler.RemoveAddition(sActor, "旋风斩");
            if (sActor.Status.Additions.ContainsKey("圣光之矛")) SkillHandler.RemoveAddition(sActor, "圣光之矛");
            OtherAddition oa = new OtherAddition(args.skill, sActor, "炎龙斩", 60000);
            SkillHandler.ApplyAddition(sActor, oa);
            sActor.MP = sActor.MaxMP;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }
        #endregion
    }
}

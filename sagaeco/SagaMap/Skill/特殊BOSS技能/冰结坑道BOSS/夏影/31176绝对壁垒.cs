    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Mob;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations
{
    class S31176: ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);

            OtherAddition skill = new OtherAddition(null, sActor, "神圣光界", 10000);
            skill.OnAdditionStart += (s, e) =>
            {
                sActor.TInt["续命开关"] =1;
                //sActor.MaxSP = 5000000;
                sActor.SP = 5000000;
                OtherAddition skill2 = new OtherAddition(null, sActor, "不可打断", 10000);
                SkillHandler.ApplyAddition(sActor, skill2);
            };
            skill.OnAdditionEnd += (s, e) =>
            {
                //actor.Status.Additions.ContainsKey("不可打断");
                sActor.Status.Additions.Remove("不可打断");
                SkillHandler.Instance.CancelSkillCast(sActor);
                sActor.TInt["续命开关"] = 0;
            };
            SkillHandler.ApplyAddition(sActor, skill);

            AutoCastInfo info = SkillHandler.Instance.CreateAutoCastInfo(31177, 1, 500);
            args.autoCast.Add(info);
        }
        #endregion
    }
}

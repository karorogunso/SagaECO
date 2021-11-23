using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12102 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("替身术CD"))
                return -30;
            if (pc.AInt["接受了搬运任务"] == 1)
            {
                SkillHandler.SendSystemMessage(pc, "由于接受了搬运任务，你无法使用该技能。");
                return -30;
            }
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //short[] pos = map.GetRandomPosAroundPos(sActor.X, sActor.Y, 100);

            sActor.TInt["替身术记录X"] = sActor.X;
            sActor.TInt["替身术记录Y"] = sActor.Y;

            OtherAddition cd = new OtherAddition(null, sActor, "替身术CD", 20000);
            SkillHandler.ApplyAddition(sActor, cd);

            OtherAddition ts = new OtherAddition(null, sActor, "替身术", 500 + level * 500);
            ts.OnAdditionEnd += (s, e) =>
            {
                sActor.TInt["替身术记录X"] = 0;
                sActor.TInt["替身术记录Y"] = 0;
            };
            SkillHandler.ApplyAddition(sActor, ts);
        }
    }
}

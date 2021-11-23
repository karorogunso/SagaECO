using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;


namespace SagaMap.Skill.SkillDefinations
{
    public class S13104 : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 2f + 0.5f * level;
            float factor2 = 1.3f + 0.5f * level;
            factor += factor2 * (sActor.BeliefDark / 5000f);

            List<Actor> d = new List<Actor> { dActor };

            if (dActor.Status.Additions.ContainsKey("暗刻"))
            {
                SkillHandler.RemoveAddition(dActor, "暗刻");
                SkillHandler.Instance.ShowEffectOnActor(dActor, 5293, sActor);
                factor *= 1.8f;

                if (dActor.Status.Additions.ContainsKey("侵蚀感染"))
                {
                    Addition 侵蚀感染 = dActor.Status.Additions["侵蚀感染"];
                    TimeSpan span = new TimeSpan(0, 0, 0, 0, 5000);
                    ((OtherAddition)侵蚀感染).endTime = DateTime.Now + span;
                }
                else
                {
                    OtherAddition skill = new OtherAddition(null, dActor, "侵蚀感染", 5000);
                    SkillHandler.Instance.ShowEffectByActor(dActor, 5091);
                    SkillHandler.ApplyAddition(dActor, skill);
                }
                if (level == 4)
                {
                    OtherAddition skill2 = new OtherAddition(null, sActor, "施法时间减半", 3000);
                    SkillHandler.ApplyBuffAutoRenew(sActor, skill2);
                    SkillHandler.Instance.ShowEffectOnActor(dActor, 5237, sActor);
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, d, args, SkillHandler.DefType.MDef, SagaLib.Elements.Dark, 50, factor, 0, false, false);


            if (!sActor.Status.Additions.ContainsKey("意志坚定"))
            {
                if (sActor.EP < 300)
                    sActor.EP = 0;
                else sActor.EP -= 300;
            }
            Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }
        #endregion
    }
}

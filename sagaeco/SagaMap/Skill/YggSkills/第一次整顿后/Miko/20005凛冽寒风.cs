using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;


namespace SagaMap.Skill.SkillDefinations
{
    class S20005 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("凛冽寒风CD"))
                return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //取得目标、造成伤害
            float factor = 5f + 2f * level;
            Map map = SkillHandler.GetActorMap(sActor);
            List<Actor> targets = SkillHandler.Instance.GetAreaActorByPosWhoCanBeAttackedTargets(sActor, args.x, args.y, 500);
            SkillHandler.Instance.MagicAttack(sActor, targets, args, Elements.Wind, factor);

            //技能CD
            OtherAddition cd = new OtherAddition(null, sActor, "凛冽寒风CD", 30000);
            SkillHandler.ApplyBuffAutoRenew(sActor, cd);

            //使目标降低速度、受到冰属性伤害提升100%
            foreach (var i in targets)
            {
                OtherAddition sd = new OtherAddition(null, i, "凛冽寒风减速", 5000);
                sd.OnAdditionStart += (s, e) =>
                {
                    i.TInt["凛冽寒风减速点"] = i.Speed / 3;
                    i.Speed -= (ushort)i.TInt["凛冽寒风减速点"];
                    i.Buff.SpeedDown = true;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, i, true);
                };
                sd.OnAdditionEnd += (s, e) =>
                {
                    i.Speed += (ushort)i.TInt["凛冽寒风减速点"];
                    i.Buff.SpeedDown = false;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, i, true);
                };
                SkillHandler.ApplyAddition(i, sd);
            }
            #endregion
        }
    }
}

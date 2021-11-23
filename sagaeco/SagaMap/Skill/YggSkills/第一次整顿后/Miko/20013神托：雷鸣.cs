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
    class S20013 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("神托CD"))
                return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int cdtime = 30000;
            int lifetime = 5000;
            int reduce = 10 + 30 * level;

            //技能CD
            OtherAddition cd = new OtherAddition(null, sActor, "神托CD", cdtime);
            SkillHandler.ApplyBuffAutoRenew(sActor, cd);

            //赋予BUFF
            Map map = SkillHandler.GetActorMap(sActor);
            List<ActorPC> pcs = SkillHandler.Instance.GetPartyMembersAround(sActor, 500);
            foreach (var item in pcs)
            {
                SkillHandler.Instance.ShowEffectOnActor(sActor, 5107);
                OtherAddition buff = new OtherAddition(null, item, "神托：雷鸣", lifetime);
                buff.OnAdditionStart += (s, e) =>
                {
                    item.Buff.ShieldWind = true;
                    item.TInt["风属性伤害降低"] = reduce;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, item, true);
                    SkillHandler.SendSystemMessage(item, "受到来自 " + sActor.Name + " 的神托：雷鸣效果，受到风属性伤害减少。");
                };
                buff.OnAdditionEnd += (s, e) =>
                {
                    item.Buff.ShieldWind = false;
                    item.TInt["风属性伤害降低"] = 0;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, item, true);
                    SkillHandler.SendSystemMessage(item, "神托：雷鸣效果效果消失了。");
                };
                SkillHandler.ApplyBuffAutoRenew(item, buff);
            }
        }
    }
}

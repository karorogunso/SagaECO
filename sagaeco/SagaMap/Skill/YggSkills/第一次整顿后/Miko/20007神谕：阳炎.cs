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
    class S20007 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("神谕CD"))
                return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int cdtime = 0;
            int lifetime = 15000;
            short uprate = (short)(100 + 10 * level);

            //技能CD
            OtherAddition cd = new OtherAddition(null, sActor, "神谕CD", cdtime);
            SkillHandler.ApplyBuffAutoRenew(sActor, cd);

            //赋予BUFF
            Map map = SkillHandler.GetActorMap(sActor);
            List<ActorPC> pcs = SkillHandler.Instance.GetPartyMembersAround(sActor, 500);
            foreach (var item in pcs)
            {
                SkillHandler.Instance.ShowEffectOnActor(sActor, 4480);
                OtherAddition buff = new OtherAddition(null, item, "神谕：阳炎", lifetime);
                buff.OnAdditionStart += (s, e) =>
                {
                    item.Buff.AtkMinUp = true;
                    item.Buff.AtkMaxUp = true;
                    item.Buff.MAtkMaxUp = true;
                    item.Buff.MAtkMinUp = true;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, item, true);
                    item.Status.min_atk1_rate_skill = uprate;
                    item.Status.min_atk2_rate_skill = uprate;
                    item.Status.min_atk3_rate_skill = uprate;
                    item.Status.min_matk_rate_skill = uprate;
                    item.Status.max_atk1_rate_skill = uprate;
                    item.Status.max_atk2_rate_skill = uprate;
                    item.Status.max_atk3_rate_skill = uprate;
                    item.Status.max_matk_rate_skill = uprate;
                    SkillHandler.SendSystemMessage(item, "受到来自 " + sActor.Name + " 的神諭：阳炎效果，攻击力提升了");
                };
                buff.OnAdditionEnd += (s, e) =>
                {
                    item.Buff.AtkMinUp = false;
                    item.Buff.AtkMaxUp = false;
                    item.Buff.MAtkMaxUp = false;
                    item.Buff.MAtkMinUp = false;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, item, true);
                    item.Status.min_atk1_rate_skill = 100;
                    item.Status.min_atk2_rate_skill = 100;
                    item.Status.min_atk3_rate_skill = 100;
                    item.Status.min_matk_rate_skill = 100;
                    item.Status.max_atk1_rate_skill = 100;
                    item.Status.max_atk2_rate_skill = 100;
                    item.Status.max_atk3_rate_skill = 100;
                    item.Status.max_matk_rate_skill = 100;
                    SkillHandler.SendSystemMessage(item, "神諭：阳炎效果消失了。");
                };
                SkillHandler.ApplyBuffAutoRenew(item, buff);
            }
        }
    }
}
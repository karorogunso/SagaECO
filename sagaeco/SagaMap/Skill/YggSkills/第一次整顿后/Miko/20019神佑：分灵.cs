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
    class S20019 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Party == null)
            {
                SkillHandler.SendSystemMessage(pc, "周围没有可以分灵的队友。");
                return -150;
            }
            List<ActorPC> pcs = SkillHandler.Instance.GetPartyMembersAround(pc, 1000, true);
            if (pcs.Count <= 1)
            {
                SkillHandler.SendSystemMessage(pc, "周围没有可以分灵的队友。");
                return -150;
            }
            if (pc.Status.Additions.ContainsKey("神佑：分灵CD"))
                return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //技能CD
            int CD = 90000;
            StableAddition cd = new StableAddition(null, sActor, "神佑：分灵CD", CD);
            cd.OnAdditionEnd += (s, e) =>
            {
                SkillHandler.SendSystemMessage(dActor, "『神佑：分灵』可以再次使用了。");
            };
            SkillHandler.ApplyBuffAutoRenew(sActor, cd);
            //注：分灵只针对来自敌人的伤害
            //当受到伤害时，所有的队友受到等量“来自自身的“分灵”状态造成的伤害”
            //伤害来自于“自己”的“分灵”状态而不是敌人。
            List<ActorPC> pcs = SkillHandler.Instance.GetPartyMembersAround(sActor, 1000, true);
            foreach (var item in pcs)
            {
                OtherAddition skill = new OtherAddition(null, item, "BUFF_神佑：分灵", 10000);
                skill.OnAdditionStart += (s, e) =>
                {
                    SkillHandler.SendSystemMessage(dActor, "受到了『神佑：分灵』效果，受到的伤害将由你与周围队友平摊");
                    item.OnBuffCallBackList.Add("神佑：分灵", (tsActor, tdActor, tDamage) =>
                    {
                        List<ActorPC> tpcs = SkillHandler.Instance.GetPartyMembersAround(item, 1000, false).Where(tpc => tpc.Status.Additions.ContainsKey("BUFF_神佑：分灵")).ToList();
                        if (tsActor != tdActor && tpcs.Count > 0)
                        {
                            tDamage /= tpcs.Count + 1;//tpcs不包括自己，自己受到的仍然是原本的“伤害”而不是分灵造成的伤害！
                            //SkillHandler.Instance.ShowEffectOnActor(tdActor, 1);
                            foreach (var item2 in tpcs)
                            {
                                SkillHandler.Instance.CauseDamage(item2, item2, tDamage);
                                SkillHandler.Instance.ShowVessel(item2, tDamage);
                            }
                        }
                        return tDamage;
                    });
                };
                skill.OnAdditionEnd += (s, e) =>
                {
                    SkillHandler.SendSystemMessage(dActor, "『神佑：分灵』效果消失了");
                    item.OnBuffCallBackList.Remove("神佑：分灵");
                };

                SkillHandler.ApplyBuffAutoRenew(dActor, skill);
            }
        }
    }
}
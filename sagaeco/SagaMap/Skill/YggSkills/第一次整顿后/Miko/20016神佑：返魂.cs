using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;
using SagaMap.Network.Client;

namespace SagaMap.Skill.SkillDefinations
{
    class S20016 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("神佑：返魂CD"))
                return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int CD = 12000000;

            //技能CD
            StableAddition cd = new StableAddition(null, sActor, "神佑：返魂CD", CD);
            cd.OnAdditionEnd += (s, e) =>
            {
                SkillHandler.SendSystemMessage(dActor, "『神佑：返魂』可以再次使用了。");
            };
            SkillHandler.ApplyBuffAutoRenew(sActor, cd);

            //复活已死亡队友
            List<ActorPC> pcs = SkillHandler.Instance.GetPartyMembersAround(sActor, 3000, false);
            foreach (var m in pcs)
                if(m.Buff.Dead)
                {
                    m.Buff.紫になる = true;
                    MapClient.FromActorPC(m).Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, m, true);
                    m.TInt["Revive"] = 1;
                    MapClient.FromActorPC(m).EventActivate(0xF1000000);
                    m.TStr["Revive"] = sActor.Name;
                    MapClient.FromActorPC(m).SendSystemMessage(string.Format("玩家 {0} 正在请求你复活", sActor.Name));
                }
                    
        }

    }
}
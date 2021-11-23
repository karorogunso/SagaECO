using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12007 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            uint mpcost = (uint)(pc.MaxMP * 0.33f);
            if (pc.MP < mpcost)
                return -1;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC Me = (ActorPC)sActor;

                uint mpcost = (uint)(sActor.MaxMP * 0.33f);
                if (sActor.MP > mpcost)
                    sActor.MP -= mpcost;
                else
                    sActor.MP = 0;
                Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
                int Lv = 1;
                float factor = 2f + 2f * Lv;
                SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
                if (!sActor.Status.Additions.ContainsKey("绽放重置"))
                {
                    OtherAddition 绽放重置 = new OtherAddition(args.skill, Me, "绽放重置", 50000);
                    绽放重置.OnAdditionEnd += (s, e) =>
                    {

                        if (Me.TInt["绽放次数"] > 0)
                        {
                            SkillHandler.Instance.ShowEffectOnActor(sActor, 5273);
                            Network.Client.MapClient.FromActorPC((ActorPC)sActor).SendSystemMessage("『绽放』的累计重置了。");
                        }
                        Me.TInt["绽放次数"] = 0;
                    };
                    SkillHandler.ApplyAddition(sActor, 绽放重置);
                }

                Me.TInt["绽放次数"]++;
                if (Me.TInt["绽放次数"] == 5)
                {
                    SkillHandler.Instance.ShowEffectOnActor(sActor, 5294);
                    Network.Client.MapClient.FromActorPC((ActorPC)sActor).SendSystemMessage("达到了『落幕』的使用要求！");
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12008 : ISkill
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
            uint mpcost = (uint)(sActor.MaxMP * 0.33f);
            if (sActor.MP > mpcost)
                sActor.MP -= mpcost;
            else
                sActor.MP = 0;
            if (sActor.type == ActorType.PC)
            {
                ActorPC Me = (ActorPC)sActor;
                //if (Me.Skills.ContainsKey(12006))
                {
                    int Lv = 1;
                    float factor = 5f + 5f * Lv;
                    if (Me.TInt["绽放次数"] >= 5 || Me.Status.Playman > 0)
                    {
                        Me.TInt["绽放次数"] = 0;
                        if (sActor.Status.Additions.ContainsKey("绽放重置"))
                            SkillHandler.RemoveAddition(sActor, "绽放重置");
                        SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
                        if (!sActor.Status.Additions.ContainsKey("绝唱伤害加成") && !sActor.Status.Additions.ContainsKey("完美谢幕伤害提升"))
                        {
                            Map map = Manager.MapManager.Instance.GetMap(Me.MapID);
                            OtherAddition 绝唱伤害加成 = new OtherAddition(args.skill, sActor, "绝唱伤害加成", 35000);
                            绝唱伤害加成.OnAdditionStart += (s, e) =>
                            {
                                Me.Buff.不知道5 = true;
                                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, Me, true);
                            };
                            绝唱伤害加成.OnAdditionEnd += (s, e) =>
                            {
                                //Network.Client.MapClient.FromActorPC((ActorPC)sActor).SendSystemMessage("『绝唱』的伤害加成结束了。");
                                SkillHandler.Instance.ShowEffectOnActor(sActor, 5154);
                                Me.Buff.不知道5 = false;
                                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, Me, true);
                            };
                            SkillHandler.ApplyAddition(sActor, 绝唱伤害加成);
                        }
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S25008 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("老毒瘤CD"))
                return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            if (sActor.TInt["老毒瘤"]>=3)
            {
                List<Actor> actors = map.GetActorsArea(sActor, 4000, false);//获取周围所有的目标
                foreach (Actor i in actors)//遍历目标，不需要检查合法性，mob、玩家一起处理。
                {
                    if (i.type == ActorType.SKILL && i.Name == "老毒瘤" + sActor.ActorID.ToString())//检查引爆目标
                    {
                        List<Actor> targets = map.GetActorsArea(i, 200, false);//获取周围3*3的目标
                        foreach (Actor j in targets)//遍历目标，不需要检查合法性，mob、玩家一起处理。
                        {
                            if (j.type == ActorType.PC || j.type == ActorType.MOB)//检查目标
                            {
                                if (!j.Status.Additions.ContainsKey("丰饶之土"))
                                {
                                    SkillHandler.Instance.PushBack(i, j, 4);//击退4格
                                    //Stun skill = new Stun(null, item, 1000);
                                    //SkillHandler.ApplyAddition(item, skill);
                                }
                            }
                        }
                        SkillHandler.Instance.ShowEffectOnActor(i, 4289);
                        map.DeleteActor(i);
                        
                    }
                }
                Network.Client.MapClient.FromActorPC((ActorPC)sActor).SendSystemMessage("引爆了所有『老毒瘤』！");
                sActor.TInt["老毒瘤"] = 0;

                int cdtime = 30000;
                if (sActor.Status.Additions.ContainsKey("涌动之水"))
                    cdtime /= 2;
                OtherAddition cd = new OtherAddition(null, sActor, "老毒瘤CD", cdtime);
                cd.OnAdditionEnd += (s, e) =>
                {
                    Network.Client.MapClient.FromActorPC((ActorPC)sActor).SendSystemMessage("『老毒瘤』冷却完毕。");
                };
                SkillHandler.ApplyAddition(sActor, cd);
            }
            else
            {
                ActorSkill actor = new ActorSkill(SagaDB.Skill.SkillFactory.Instance.GetSkill(25011, 1), sActor)
                {
                    Name = "老毒瘤" + sActor.ActorID.ToString(),
                    MapID = sActor.MapID,
                    X = sActor.X,
                    Y = sActor.Y,
                    e = new ActorEventHandlers.NullEventHandler(),
                };
                map.RegisterActor(actor);
                actor.invisble = false;
                actor.Stackable = false;
                map.OnActorVisibilityChange(actor);
                sActor.TInt["老毒瘤"]++;
                Network.Client.MapClient.FromActorPC((ActorPC)sActor).SendSystemMessage("设置了一枚『老毒瘤』：" + sActor.TInt["老毒瘤"].ToString() + "/3");
                int cdtime = 1000;
                if (sActor.Status.Additions.ContainsKey("涌动之水"))
                    cdtime /= 2;
                OtherAddition cd = new OtherAddition(null, sActor, "老毒瘤CD", cdtime);
                cd.OnAdditionEnd += (s, e) =>
                {
                    //Network.Client.MapClient.FromActorPC((ActorPC)sActor).SendSystemMessage("『老毒瘤』冷却完毕。");
                };
                SkillHandler.ApplyAddition(sActor, cd);
            }

        }
    }
}

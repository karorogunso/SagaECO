using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S9301 : ISkill
    {

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("绝对壁垒CD")) return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 30000;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            ActorPC me = (ActorPC)sActor;


            List<Actor> affected = map.GetActorsArea(sActor, 500, true);
            foreach (Actor act in affected)
            {
                if (act == null) continue;
                if (act.type == ActorType.PC && act.Buff.Dead != true && !act.Status.Additions.ContainsKey("绝对壁垒"))
                {
                    OtherAddition skill = new OtherAddition(null, act, "绝对壁垒", lifetime);
                    skill.OnAdditionStart += (s, e) =>
                    {
                        act.TInt["绝对壁垒"] = 3;
                        act.OnBuffCallBackList.Add("绝对壁垒", (x, y, z) =>
                        {
                            if (y.Status.Additions.ContainsKey("绝对壁垒"))
                            {
                                z = 0;
                                SkillHandler.Instance.ShowEffectOnActor(y, 4173);
                                y.TInt["绝对壁垒"]-=1;
                            }
                            if (y.TInt["绝对壁垒"]<=0)
                                SkillHandler.RemoveAddition(y, "绝对壁垒");
                            return z;
                        });
                        act.TInt["绝对壁垒"] = 3;
                        if (s.type==ActorType.PC)
                            Network.Client.MapClient.FromActorPC(me).SendSystemMessage("进入了『绝对壁垒』状态。");
                    };
                    skill.OnAdditionEnd += (s, e) =>
                    {
                        act.OnBuffCallBackList.Remove("绝对壁垒");
                        act.TInt["绝对壁垒"] = 0;
                    };
                    SkillHandler.ApplyAddition(act, skill);
                }
            }
        }
    }
}


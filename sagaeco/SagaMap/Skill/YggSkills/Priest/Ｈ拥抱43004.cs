using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Network.Client;
using SagaMap.Skill.Additions.Global;
using SagaLib;

namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 拥抱：群体小治疗，并提高群体内所有友军的3p自然恢复速度
    /// </summary>
    public class S43004 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("属性契约"))
            {
                if (((OtherAddition)(pc.Status.Additions["属性契约"])).Variable["属性契约"] == (int)Elements.Holy)
                {
                    return 0;
                }
                return -2;
            }
            else
            {
                return -2;
            }
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 3.0f;
            int lifetime = 30000;
            int recup = 100;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), 400, true);
            List<Actor> realAffected = new List<Actor>();
            if (sActor.Status.Additions.ContainsKey("属性契约"))
            {
                if (((OtherAddition)(sActor.Status.Additions["属性契约"])).Variable["属性契约"] == (int)Elements.Holy)
                {
                    factor = 4.5f;
                    foreach (Actor act in affected)
                    {
                        if (act.type == ActorType.PC)
                        {
                            ActorPC m = (ActorPC)act;
                            if (m.Mode == ((ActorPC)sActor).Mode)
                            {
                                if (m.Buff.Dead != true)
                                {
                                    realAffected.Add(act);
                                    if (!m.Status.Additions.ContainsKey("MPRecUp"))
                                        SkillHandler.Instance.ShowEffectOnActor(m, 4015);
                                    HPRecUp buff1 = new HPRecUp(args.skill, act, lifetime, recup);
                                    MPRecUp buff2 = new MPRecUp(args.skill, act, lifetime, recup);
                                    SPRecUp buff3 = new SPRecUp(args.skill, act, lifetime, recup);
                                    SkillHandler.ApplyAddition(act, buff1);
                                    SkillHandler.ApplyAddition(act, buff2);
                                    SkillHandler.ApplyAddition(act, buff3);
                                }
                            }
                        }
                    }
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, realAffected, args, SkillHandler.DefType.IgnoreAll, SagaLib.Elements.Holy, -factor);
        }
    }
}

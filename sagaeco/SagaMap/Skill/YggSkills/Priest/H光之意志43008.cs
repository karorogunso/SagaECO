using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 光之意志：获得一个持续5秒的状态，此状态时间内如果受到使自己hp降至低于x%的伤害则该次伤害变成将自己hp降至x%。 待实装
    /// </summary>
    public class S43008 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("光之意志CD"))
            {
                Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("『光之意志』冷却中");
                return -30;
            }
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
            int cd = 900000;

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 400, true);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (act.type == ActorType.PC)
                {
                    ActorPC m = (ActorPC)act;
                    if (m.Mode == ((ActorPC)sActor).Mode)
                    {
                        if (m.Buff.Dead != true && !m.Status.Additions.ContainsKey("光之意志CD") && !m.Status.Additions.ContainsKey("HolyVolition"))
                        {
                            DefaultBuff skill = new DefaultBuff(args.skill, m, "HolyVolition", 6000);
                            skill.OnAdditionStart += (s, e) =>
                            {
                                m.Buff.フェニックス = true;
                                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, m, true);
                                Network.Client.MapClient.FromActorPC(m).SendSystemMessage("获得『光之意志』效果");

                            } ;
                            skill.OnAdditionEnd += (s, e) =>
                            {
                                m.Buff.フェニックス = false;
                                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, m, true);
                                Network.Client.MapClient.FromActorPC(m).SendSystemMessage("『光之意志』效果消失");
                            };
                            SkillHandler.ApplyAddition(m, skill);
                            SkillHandler.Instance.ShowEffect(map, m, 4390);

                            DefaultBuff skill2 = new DefaultBuff(args.skill, m, "光之意志CD", cd);
                            SkillHandler.ApplyAddition(m, skill2);
                        }
                    }
                }
            }
        }
        #endregion
    }
}

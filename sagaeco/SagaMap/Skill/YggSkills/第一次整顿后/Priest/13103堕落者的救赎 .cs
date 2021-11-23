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
    public class S13103 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("堕落者的救赎CD2"))
            {
                Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("『堕落者的救赎』冷却中");
                return -30;
            }
            if (pc.BeliefDark>0)
                return -12;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int cd = 120000;
            if (sActor.Status.Additions.ContainsKey("堕落者的救赎CD"))
                cd = (int)(((DefaultBuff)sActor.Status.Additions["堕落者的救赎CD"]).endTime - DateTime.Now).TotalMilliseconds;
            DefaultBuff skill3 = new DefaultBuff(args.skill, sActor, "堕落者的救赎CD2", cd);
            SkillHandler.ApplyAddition(sActor, skill3);

            cd = 120000;
            int lifetime = 3000 + 2000 * level;
            bool heal = false;
            bool party = false;
            if (sActor.EP + 5000 - sActor.Status.BeliefBalace >= 4000)
            {
                heal = true;
                if (level == 3)
                {
                    ActorPC pc = (ActorPC)sActor;
                    if (pc.MapID != 10054000)
                        Network.Client.MapClient.FromActorPC(pc).TitleProccess(pc, 32, 1);
                }
            }
            if (sActor.EP + 5000 - sActor.Status.BeliefBalace >= 1000)
            {
                party = true;
                SkillHandler.Instance.ShowEffectOnActor(sActor, 5176);
            }
            if (sActor.Status.Additions.ContainsKey("神圣祷告"))
                SkillHandler.RemoveAddition(sActor, "神圣祷告");
            if (sActor.Status.Additions.ContainsKey("意志坚定"))
                SkillHandler.RemoveAddition(sActor, "意志坚定");
            sActor.EP = 5000;
            //sActor.SP = 0;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
            List<Actor> affected = map.GetActorsArea(sActor, 400, true);
            if (!party) affected = new List<Actor> { sActor };
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (act.type == ActorType.PC)
                {
                    ActorPC m = (ActorPC)act;
                    if (m.Mode == ((ActorPC)sActor).Mode)
                    {
                        if (m.Buff.Dead != true && !m.Status.Additions.ContainsKey("堕落者的救赎CD") && !m.Status.Additions.ContainsKey("HolyVolition"))
                        {
                            DefaultBuff skill = new DefaultBuff(args.skill, m, "HolyVolition", lifetime);
                            skill.OnAdditionStart += (s, e) =>
                            {
                                m.Buff.フェニックス = true;
                                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, m, true);
                                Network.Client.MapClient.FromActorPC(m).SendSystemMessage("获得『堕落者的救赎』效果");

                            } ;
                            skill.OnAdditionEnd += (s, e) =>
                            {
                                m.Buff.フェニックス = false;
                                if(heal && m.HP > 0 && !m.Buff.Dead)
                                {
                                    SkillHandler.Instance.ShowEffect(map, m, 5143);
                                    uint value = (uint)(m.MaxHP * 0.9f);
                                    m.HP += value;
                                    if (m.HP > m.MaxHP)
                                        m.HP = m.MaxHP;
                                    SkillHandler.Instance.ShowVessel(m, (int)-value);
                                }
                                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, m, true);
                                Network.Client.MapClient.FromActorPC(m).SendSystemMessage("『堕落者的救赎』效果消失");
                            };
                            SkillHandler.ApplyAddition(m, skill);
                            SkillHandler.Instance.ShowEffect(map, m, 4390);

                            DefaultBuff skill2 = new DefaultBuff(args.skill, m, "堕落者的救赎CD", cd);
                            skill2.OnAdditionEnd += (s, e) =>
                            {
                                SkillHandler.Instance.ShowEffect(map, m, 5174);
                                Network.Client.MapClient.FromActorPC(m).SendSystemMessage("『堕落者的救赎』冷却结束！");
                            };
                            SkillHandler.ApplyAddition(m, skill2);
                        }
                    }
                }
            }
        }
        #endregion
    }
}

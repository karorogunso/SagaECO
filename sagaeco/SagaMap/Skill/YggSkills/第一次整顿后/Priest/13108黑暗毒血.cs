using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;


namespace SagaMap.Skill.SkillDefinations
{
    public class S13108 : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 1.9f + 0.6f * level;
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, Elements.Dark, factor);

            if (!sActor.Status.Additions.ContainsKey("意志坚定"))
                sActor.EP -= 500;

            if (dActor.Status.Additions.ContainsKey("暗刻"))
            {
                SkillHandler.RemoveAddition(dActor, "暗刻");
                Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);

                int lifetime = 3000 + 2000 * level;
                if (sActor.BeliefDark > 3000)
                    lifetime *= 2;

                if (sActor.type == ActorType.PC)
                {
                    List<Actor> Actors = map.GetActorsArea(sActor, 900, true);
                    SkillArg arg2 = new SkillArg();
                    arg2.skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(13199, 1);
                    arg2.sActor = sActor.ActorID;
                    arg2.argType = SkillArg.ArgType.Active;
                    ActorPC pc = (ActorPC)sActor;
                    foreach (var item in Actors)
                    {
                        if (item.type == ActorType.PC)
                        {
                            ActorPC player = (ActorPC)item;
                            if (player.HP > 0 && player.Mode == pc.Mode)
                            {
                                if(!player.Status.Additions.ContainsKey("黑暗毒血吸血BUFF"))
                                {
                                    arg2.affectedActors.Add(item);
                                    arg2.flag.Add(AttackFlag.UNKNOWN1);
                                    arg2.hp.Add(0);
                                    arg2.mp.Add(0);
                                    arg2.sp.Add(0);
                                    player.Buff.ライフテイク = true;
                                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, player, true);
                                    OtherAddition skill = new OtherAddition(null, player, "黑暗毒血吸血BUFF", lifetime);
                                    skill.OnAdditionEnd += (s, e) =>
                                    {
                                        player.Buff.ライフテイク = false;
                                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, player, true);
                                    };
                                    SkillHandler.ApplyAddition(player, skill);
                                }
                                else
                                {
                                    OtherAddition skill = (OtherAddition)player.Status.Additions["黑暗毒血吸血BUFF"];
                                    if(skill.endTime < DateTime.Now + new TimeSpan(0,0,0,0,lifetime))
                                    {
                                        arg2.affectedActors.Add(item);
                                        arg2.flag.Add(AttackFlag.UNKNOWN1);
                                        arg2.hp.Add(0);
                                        arg2.mp.Add(0);
                                        arg2.sp.Add(0);
                                        skill.endTime = DateTime.Now + new TimeSpan(0, 0, 0, 0, lifetime);
                                    }
                                }
                            }
                        }
                    }
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, arg2, sActor, true);
                }
            }
            #endregion
        }
    }
}

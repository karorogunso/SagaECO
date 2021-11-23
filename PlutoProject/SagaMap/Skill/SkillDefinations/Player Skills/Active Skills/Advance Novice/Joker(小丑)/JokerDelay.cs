using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// 小丑延迟取消
    /// </summary>
    public class JokerDelay : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }


        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            args.dActor = 0;//不显示效果
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 300, true);
            List<Actor> realAffected = new List<Actor>();
            ActorPC sPC = (ActorPC)sActor;
            if (sPC.Party != null)
            {
                foreach (Actor act in affected)
                {
                    if (act.type == ActorType.PC)
                    {
                        ActorPC aPC = (ActorPC)act;
                        if (aPC.Party != null && sPC.Party != null)
                        {
                            if ((aPC.Party.ID == sPC.Party.ID) && aPC.Party.ID != 0 && !aPC.Buff.Dead && aPC.PossessionTarget == 0)
                            {
                                if (act.Buff.NoRegen) continue;

                                if (aPC.Party.ID == sPC.Party.ID)
                                {
                                    realAffected.Add(act);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                realAffected.Add(sPC);
            }
            int life = 180000;
            foreach (Actor rAct in realAffected)
            {
                if (rAct.ActorID == sActor.ActorID)
                {
                    Actor realdActor = SkillHandler.Instance.GetPossesionedActor((ActorPC)sActor);
                    DefaultBuff skill = new DefaultBuff(args.skill, realdActor, "JokerDelay", life);
                    skill.OnAdditionStart += this.StartEventHandler;
                    skill.OnAdditionEnd += this.EndEventHandler;
                    skill.OnCheckValid += this.ValidCheck;
                    SkillHandler.ApplyAddition(realdActor, skill);
                }
                else
                {
                    DefaultBuff skill = new DefaultBuff(args.skill, rAct, "JokerDelay", life);
                    skill.OnAdditionStart += this.StartEventHandler;
                    skill.OnAdditionEnd += this.EndEventHandler;
                    skill.OnCheckValid += this.ValidCheck;
                    SkillHandler.ApplyAddition(rAct, skill);
                }

            }

        }

        void ValidCheck(ActorPC pc, Actor dActor, out int result)
        {
            result = TryCast(pc, dActor, null);
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.aspd_skill_perc += (float)(0.2f * skill.skill.Level);

            actor.Buff.JSpeed3RD = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            float raspd_skill_perc_restore = (float)(0.2f * skill.skill.Level);
            if (actor.Status.aspd_skill_perc > raspd_skill_perc_restore + 1)
            {
                actor.Status.aspd_skill_perc -= raspd_skill_perc_restore;
            }
            else
            {
                actor.Status.aspd_skill_perc = 1;
            }

            actor.Buff.JSpeed3RD = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        #endregion
    }
}

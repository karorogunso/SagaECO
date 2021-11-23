using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Sorcerer
{
    /// <summary>
    /// 透明化（インビジブル）
    /// </summary>
    public class Invisible:ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 100, true);
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
                                if (aPC.Party.ID == sPC.Party.ID)
                                {
                                    DefaultBuff skill = new DefaultBuff(args.skill, act, "Invisible", 30000);
                                    skill.OnAdditionStart += this.StartEventHandler;
                                    skill.OnAdditionEnd += this.EndEventHandler;
                                    SkillHandler.ApplyAddition(act, skill);
                                }
                            }
                        }
                    }
                }
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.Transparent = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.Transparent = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}

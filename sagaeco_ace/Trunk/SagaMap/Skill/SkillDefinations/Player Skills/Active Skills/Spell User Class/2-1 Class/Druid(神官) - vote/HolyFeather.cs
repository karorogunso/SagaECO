using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Druid
{
    /// <summary>
    /// ホーリーフェザー
    /// </summary>
    public class HolyFeather : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 300, true, false );
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
                            if ((aPC.Party.ID == sPC.Party.ID) && aPC.Party.ID != 0 && !aPC.Buff.Dead && aPC.PossessionTarget==0 )
                            {
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
                realAffected.Add(sActor);
            }
            args.affectedActors = realAffected;
            args.Init();
            int[] lifetimes = { 0,60000, 75000, 90000, 105000, 120000 };
            int lifetime = lifetimes[level];
            foreach (Actor rAct in realAffected)
            {
                Additions.Global.MPRecovery skill1 = new SagaMap.Skill.Additions.Global.MPRecovery(args.skill, rAct, lifetime, 5000);
                SkillHandler.ApplyAddition(rAct, skill1);
                Additions.Global.SPRecovery skill2 = new SagaMap.Skill.Additions.Global.SPRecovery(args.skill, rAct, lifetime, 5000);
                SkillHandler.ApplyAddition(rAct, skill2);
                DefaultBuff skill = new DefaultBuff(args.skill, rAct, "HolyFeather", lifetime);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(rAct, skill);
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.mp_recover_skill += 15;
            actor.Status.hp_recover_skill += 15;
            actor.Status.sp_recover_skill += 15;
            actor.Buff.HolyFeather = true;
            actor.Buff.HPRegenUp = true;
            actor.Buff.SPRegenUp = true;
            actor.Buff.MPRegenUp = true;
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.mp_recover_skill -= 15;
            actor.Status.hp_recover_skill -= 15;
            actor.Status.sp_recover_skill -= 15;
            actor.Buff.HolyFeather = false;
            actor.Buff.HPRegenUp = false;
            actor.Buff.SPRegenUp = false;
            actor.Buff.MPRegenUp = false;
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}

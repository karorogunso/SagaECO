using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// ホーリーフェザー
    /// </summary>
    public class NoFlashPlayer : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 200, true);
            List<Actor> realAffected = new List<Actor>();
            ActorPC sPC;
            ActorPartner pet = (ActorPartner)sActor;
            sPC = pet.Owner;
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
                realAffected.Add(sActor);
                realAffected.Add(sPC);
            }
            args.affectedActors = realAffected;
            args.Init();
            int lifetime = 60000;
            foreach (Actor rAct in realAffected)
            {
                Additions.Global.MPRecovery skill1 = new SagaMap.Skill.Additions.Global.MPRecovery(args.skill, rAct, lifetime, 5000);
                SkillHandler.ApplyAddition(rAct, skill1);
                Additions.Global.SPRecovery skill2 = new SagaMap.Skill.Additions.Global.SPRecovery(args.skill, rAct, lifetime, 5000);
                SkillHandler.ApplyAddition(rAct, skill2);
                DefaultBuff skill = new DefaultBuff(args.skill, rAct, "NoFlashPlayer", lifetime);
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
            actor.Buff.HPRegenUp = false;
            actor.Buff.SPRegenUp = false;
            actor.Buff.MPRegenUp = false;
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}

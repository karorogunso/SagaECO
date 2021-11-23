using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Druid
{
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
            List<Actor> affected = map.GetActorsArea(sActor, 500, true, false);
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
            int lifetime = 35000 + 5000 * level;
            foreach (Actor rAct in realAffected)
            {
                Additions.Global.MPRecovery skill1 = new SagaMap.Skill.Additions.Global.MPRecovery(args.skill, rAct, lifetime, 5000);
                SkillHandler.ApplyAddition(rAct, skill1);
                Additions.Global.SPRecovery skill2 = new SagaMap.Skill.Additions.Global.SPRecovery(args.skill, rAct, lifetime, 5000);
                SkillHandler.ApplyAddition(rAct, skill2);
                HolyFeather2 hf2 = new HolyFeather2(args.skill, sActor, rAct, lifetime, 0);
                SkillHandler.ApplyAddition(rAct, hf2);
            }
        }
        class HolyFeather2 : DefaultBuff
        {
            public HolyFeather2(SagaDB.Skill.Skill skill, Actor sActor, Actor dActor, int lifetime, int damage)
                : base(skill, sActor, dActor, "HolyFeather2", lifetime, 5000, damage)
            {

                this.OnAdditionStart += this.StartEventHandler;
                this.OnAdditionEnd += this.EndEventHandler;
                this.OnUpdate2 += this.TimerUpdate;

            }

            void TimerUpdate(Actor sActor, Actor dActor, DefaultBuff skill, SkillArg arg, int damage)
            {
                //测试去除技能同步锁ClientManager.EnterCriticalArea();
                try
                {
                    if (dActor.HP > 0 && !dActor.Buff.Dead)
                    {
                        Map map = SagaMap.Manager.MapManager.Instance.GetMap(sActor.MapID);
                        int mpadd = (int)(dActor.MaxMP * 0.04f + 0.01f * skill.skill.Level);
                        int spadd = (int)(dActor.MaxSP * 0.04f + 0.01f * skill.skill.Level);
                        if (dActor.MP + mpadd > dActor.MaxMP) dActor.MP = dActor.MaxMP;
                        else dActor.MP += (uint)mpadd;
                        if (dActor.SP + mpadd > dActor.MaxSP) dActor.SP = dActor.MaxSP;
                        else dActor.SP += (uint)spadd;
                        SkillHandler.Instance.ShowVessel(dActor, 0, -mpadd, -spadd);
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, dActor, true);
                    }
                }
                catch (Exception ex)
                {
                    SagaLib.Logger.ShowError(ex);
                }
                //测试去除技能同步锁ClientManager.LeaveCriticalArea();
            }
            void StartEventHandler(Actor actor, DefaultBuff skill)
            {
                actor.Status.mp_recover_skill += 15;
                actor.Status.hp_recover_skill += 15;
                actor.Status.sp_recover_skill += 15;
                actor.Buff.HolyFeather = true;
                actor.Buff.HPRecUp = true;
                actor.Buff.SPRecUp = true;
                actor.Buff.MPRecUp = true;
                Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            }
            void EndEventHandler(Actor actor, DefaultBuff skill)
            {
                actor.Status.mp_recover_skill -= 15;
                actor.Status.hp_recover_skill -= 15;
                actor.Status.sp_recover_skill -= 15;
                actor.Buff.HolyFeather = false;
                actor.Buff.HPRecUp = false;
                actor.Buff.SPRecUp = false;
                actor.Buff.MPRecUp = false;
                Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            }
        #endregion
        }
    }
}

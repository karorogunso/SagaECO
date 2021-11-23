
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations
{
    public class S43304 : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (sActor.Status.Additions.ContainsKey("乐章CD")) return -30;
            if (SkillHandler.Instance.CheckSkillCanCastForWeapon(sActor, args))
            {
                if (sActor.Buff.演奏中)
                {
                    return -8;
                }
                return 0;
            }
            return -5;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            actor.MapID = sActor.MapID;
            actor.X = sActor.X;
            actor.Y = sActor.Y;
            actor.e = new ActorEventHandlers.NullEventHandler();
            map.RegisterActor(actor);
            actor.invisble = false;
            map.OnActorVisibilityChange(actor);
            actor.Stackable = false;
            timer = new Activator(sActor, actor, args, level);
            timer.Activate();

            DefaultBuff cd = new DefaultBuff(sActor, "乐章CD", 60000);
            SkillHandler.ApplyAddition(sActor, cd);

            OtherAddition skill = new OtherAddition(args.skill, sActor, "沙月之歌", 15000);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            skill.OnCheckValid += this.ValidCheck;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        Activator timer;
        void ValidCheck(ActorPC pc, Actor dActor, out int result)
        {
            if (pc.Buff.演奏中)
            {
                if (Skill.SkillHandler.Instance.isEquipmentRight(pc, SagaDB.Item.ItemType.STRINGS))
                    result = 0;
                else
                {
                    result = -5;
                    timer.Deactivate();
                    timer.map.DeleteActor(timer.actor);
                }
            }
            else result = 0;
        }
        void StartEventHandler(Actor actor, OtherAddition skill)
        {
            actor.Buff.演奏中 = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, OtherAddition skill)
        {
            actor.Buff.演奏中 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        private class Activator : MultiRunTask
        {
            public ActorSkill actor;
            Actor caster;
            SkillArg skill;
            public Map map;
            int countMax = 5, count = 0;
            int lifeTime = 0;
            public Activator(Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 3000;
                this.dueTime = 0;
                this.lifeTime = 15000;
                countMax = lifeTime / period;
            }
            public override void CallBack()
            {
                try
                {
                    ActorPC me = (ActorPC)(caster);
                    if (!caster.Buff.演奏中)
                    {
                        Deactivate();
                        map.DeleteActor(actor);
                    }
                    if (count < countMax)
                    {
                        caster.EP = 0;
                        List<Actor> actors = map.GetActorsArea(actor, 300, true);
                        List<Actor> affected = new List<Actor>();
                        skill.affectedActors.Clear();
                        foreach (Actor act in actors)
                        {
                            if (act.type == ActorType.PC)
                            {
                                ActorPC target = (ActorPC)(act);
                                if ((target.Party == me.Party && me.Party != null) || (target == me))
                                {
                                    act.SP += 200;
                                    act.MP += 200;
                                    if ((target != me))
                                        act.EP += 2000;
                                    if (act.MP >= act.MaxMP) act.MP = act.MaxMP;
                                    if (act.SP >= act.MaxSP) act.SP = act.MaxSP;
                                    if (act.EP >= act.MaxEP) act.EP = act.MaxEP;
                                    AspdUp buff1 = new AspdUp(skill.skill, act, 10000, 200);
                                    CspdUp buff2 = new CspdUp(skill.skill, act, 10000, 200);
                                    SkillHandler.ApplyAddition(target, buff1);
                                    SkillHandler.ApplyAddition(target, buff2);
                                }
                            }
                        }
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, actor, false);
                        count++;
                    }
                    else
                    {
                        this.Deactivate();
                        map.DeleteActor(actor);
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
            void StartEventHandler(Actor actor, DefaultBuff skill)
            {
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_STATUS, null, actor, true);
            }
            void EndEventHandler(Actor actor, DefaultBuff skill)
            {
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_STATUS, null, actor, true);
            }
            void TimerEventHandler(Actor actor, DefaultBuff skill)
            {
                if (!SkillHandler.Instance.isInRange(this.actor, actor, 200))
                {
                    skill.AdditionEnd();
                }
            }
        }
        #endregion
    }
}

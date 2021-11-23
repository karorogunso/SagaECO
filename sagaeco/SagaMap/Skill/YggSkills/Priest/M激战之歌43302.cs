
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 激战之歌：7×7范围内友军攻防提升
    /// </summary>
    public class S43302 : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
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

            OtherAddition skill = new OtherAddition(args.skill, sActor, "激战之歌", 30000);
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
            int countMax = 3, count = 0;
            int lifeTime = 0;
            public Activator(Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 1000;
                this.dueTime = 0;
                this.lifeTime = 30000;
                countMax = lifeTime / period;
            }
            public override void CallBack()
            {
                try
                {
                    if (!caster.Buff.演奏中)
                    {
                       Deactivate();
                       map.DeleteActor(actor);
                    }
                    if (count < countMax && caster.SP >= 300)
                    {
                        caster.SP -= 300;
                        List<Actor> actors = map.GetActorsArea(actor, 300, false);
                        List<Actor> affected = new List<Actor>();
                        skill.affectedActors.Clear();
                        ActorPC me = (ActorPC)(caster);
                        foreach (Actor act in actors)
                        {
                            if (act.type == ActorType.PC)
                            {
                                ActorPC target = (ActorPC)(act);
                                if ((target.Party == me.Party && me.Party != null) || (target == me))
                                {
                                    AtkUp buff1 = new AtkUp(skill.skill, target, 10000, 60);
                                    MAtkUp buff2=new MAtkUp(skill.skill, target, 10000, 60);
                                    DefUp buff3=new DefUp(skill.skill, target, 10000, 15);
                                    MDefUp buff4=new MDefUp(skill.skill, target, 10000, 15);
                                    HitCriUp buff5 = new HitCriUp(skill.skill, target, 10000, 50);
                                    SkillHandler.ApplyAddition(target, buff1);
                                    SkillHandler.ApplyAddition(target, buff2);
                                    SkillHandler.ApplyAddition(target, buff3);
                                    SkillHandler.ApplyAddition(target, buff4);
                                    SkillHandler.ApplyAddition(target, buff5);
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

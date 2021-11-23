
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations
{
    public class S13202 : ISkill
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
            actor.Speed = 670;
            actor.e = new ActorEventHandlers.NullEventHandler();
            actor.Name = "NOT_SHOW_DISAPPEAR";
            map.RegisterActor(actor);
            actor.invisble = false;
            map.OnActorVisibilityChange(actor);
            timer = new Activator(sActor, actor, args, level);
            timer.Activate();

            /*OtherAddition skill = new OtherAddition(args.skill, sActor, "活力之幻想曲", 30000);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            skill.OnCheckValid += this.ValidCheck;
            SkillHandler.ApplyAddition(sActor, skill);*/

        }
        Activator timer;
        void ValidCheck(ActorPC pc, Actor dActor, out int result)
        {
            if (pc.Buff.演奏中)
            {
                if (SkillHandler.Instance.isEquipmentRight(pc, SagaDB.Item.ItemType.STRINGS))
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
            int countMax = 300, count = 0;
            int lifeTime = 0;
            int val = 10;
            public Activator(Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 50;
                this.dueTime = 0;
                this.lifeTime = 30000;
                countMax = lifeTime / period;
                val = 5 + 15 * level;
                caster.Buff.演奏中 = true;
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, caster, true);
            }
            public override void CallBack()
            {
                try
                {
                    if (!caster.Buff.演奏中)
                    {
                       Deactivate();
                       map.DeleteActor(actor);
                        return;
                    }
                    if (count < countMax && caster.SP >= 40)
                    {
                        /*Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, caster, true);
                        short[] pos = new short[2];
                        pos[0] = caster.X;
                        pos[1] = caster.Y;
                        map.MoveActor(Map.MOVE_TYPE.START, actor, pos, 0, 650);*/
                        if (count % 10 == 0)
                        {
                            List<Actor> actors = map.GetActorsArea(actor, 300, false);
                            List<Actor> affected = new List<Actor>();
                            skill.affectedActors.Clear();
                            ActorPC me = (ActorPC)(caster);
                            caster.SP -= 40;
                            foreach (Actor act in actors)
                            {
                                if (act.type == ActorType.PC)
                                {
                                    ActorPC target = (ActorPC)(act);
                                    if ((target.Party == me.Party && me.Party != null) || (target == me))
                                    {
                                        if (!target.Status.Additions.ContainsKey("活力之幻想曲Buff"))
                                        {
                                            OtherAddition skill2 = new OtherAddition(null, target, "活力之幻想曲Buff", 2200);
                                            skill2.OnAdditionStart += (s, e) =>
                                            {
                                                target.TInt["活力之幻想曲Value"] = val;
                                                SkillHandler.Instance.ShowEffectOnActor(target, 4268, caster);
                                            };
                                            skill2.OnAdditionEnd += (s, e) =>
                                            {
                                                target.TInt["活力之幻想曲Value"] = 0;
                                            };
                                            SkillHandler.ApplyAddition(target, skill2);
                                        }
                                        else
                                        {
                                            Addition skill2 = target.Status.Additions["活力之幻想曲Buff"];
                                            ((OtherAddition)skill2).endTime = DateTime.Now + new TimeSpan(0, 0, 0, 0, 2200);
                                        }
                                    }
                                }
                            }
                            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, actor, false);
                        }
                        count++;
                    }
                    else
                    {
                        caster.Buff.演奏中 = false;
                        Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, caster, true);
                        this.Deactivate();
                        map.DeleteActor(actor);
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
        }
        #endregion
    }
}

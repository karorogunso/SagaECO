using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;


namespace SagaMap.Skill.SkillDefinations.Elementaler
{
    class IcicleTempest : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            Map map = Manager.MapManager.Instance.GetMap(pc.MapID);
            if (map.CheckActorSkillInRange(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), 300))
            {
                return -17;
            }
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            actor.MapID = sActor.MapID;
            actor.X = SagaLib.Global.PosX8to16(args.x, map.Width);
            actor.Y = SagaLib.Global.PosY8to16(args.y, map.Height);
            actor.e = new ActorEventHandlers.NullEventHandler();
            //設置系
            actor.Stackable = false;
            map.RegisterActor(actor);
            actor.invisble = false;
            map.OnActorVisibilityChange(actor);
            Activator timer = new Activator(sActor, actor, args, level);
            timer.Activate();
        }

        #endregion

        #region Timer

        private class Activator : MultiRunTask
        {
            ActorSkill actor;
            Actor caster;
            SkillArg skill;
            Map map;
            int countMax = 3, count = 0;
            float factor = 1.0f;
            int TotalLv = 0;


            public Activator(Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 500;
                this.dueTime = 0;
                ActorPC Me = (ActorPC)caster;

                switch (level)
                {
                    case 1:
                        factor *= 1f;
                        countMax = 3;
                        break;
                    case 2:
                        factor *= 1.2f;
                        countMax = 4;
                        break;
                    case 3:
                        factor *= 1.4f;
                        countMax = 4;
                        break;
                    case 4:
                        factor *= 1.6f;
                        countMax = 5;
                        break;
                    case 5:
                        factor *= 1.8f;
                        countMax = 5;
                        break;
                }

                if (Me.Skills2.ContainsKey(3036))
                {
                    TotalLv = Me.Skills2[3036].BaseData.lv;
                    if (TotalLv == 2 || TotalLv == 1)
                        factor += 0.3f;
                    else if (TotalLv == 4 || TotalLv == 3)
                        factor += 0.6f;
                    else if (TotalLv == 5)
                        factor += 0.9f;
                }
                if (Me.SkillsReserve.ContainsKey(3036))
                {
                    TotalLv = Me.SkillsReserve[3036].BaseData.lv;
                    if (TotalLv == 2 || TotalLv == 1)
                        factor += 0.3f;
                    else if (TotalLv == 4 || TotalLv == 3)
                        factor += 0.6f;
                    else if (TotalLv == 5)
                        factor += 0.9f;
                }
                if (Me.Skills2.ContainsKey(3025))
                {
                    TotalLv = Me.Skills2[3025].BaseData.lv;
                    if (TotalLv == 3 || TotalLv == 2)
                        factor += 0.3f;
                    else if (TotalLv == 5 || TotalLv == 4)
                        factor += 0.6f;
                }
                if (Me.SkillsReserve.ContainsKey(3025))
                {
                    TotalLv = Me.SkillsReserve[3025].BaseData.lv;
                    if (TotalLv == 3 || TotalLv == 2)
                        factor += 0.3f;
                    else if (TotalLv == 5 || TotalLv == 4)
                        factor += 0.6f;
                }


            }

            public override void CallBack()
            {
                //测试去除技能同步锁
                ClientManager.EnterCriticalArea();
                try
                {
                    if (count < countMax)
                    {
                        List<Actor> actors = map.GetActorsArea(actor, 300, false);
                        List<Actor> affected = new List<Actor>();
                        skill.affectedActors.Clear();
                        foreach (Actor i in actors)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, i))
                            {
                                Additions.Global.Stiff Stiff = new SagaMap.Skill.Additions.Global.Stiff(skill.skill, i, 400);//Mob can not move as soon as attacked.
                                SkillHandler.ApplyAddition(i, Stiff);
                                affected.Add(i);
                            }
                        }

                        SkillHandler.Instance.MagicAttack(caster, affected, skill, Elements.Water, factor);
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
                //测试去除技能同步锁
                ClientManager.LeaveCriticalArea();
            }
        }
        #endregion
    }
}

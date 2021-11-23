using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31087 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Activator timer = new Activator(sActor, args, level);
            timer.Activate();
        }

        #endregion

        #region Timer

        private class Activator : MultiRunTask
        {
            Actor caster;
            SkillArg skill;
            Map map;
            List<short[]> paths = new List<short[]>();
            int countMax = 30, count = 0;
            public Activator(Actor caster, SkillArg args, byte level)
            {
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 50;
                this.dueTime = 0;

            }
            public override void CallBack()
            {
                ClientManager.EnterCriticalArea();
                try
                {
                    if(count == 10)
                    {
                        if (caster.type == ActorType.MOB)
                            SkillHandler.Instance.ActorSpeak(caster, "……");
                    }
                    if (count < countMax)
                    {
                        ActorSkill actor = new ActorSkill(this.skill.skill, caster);
                        Map map = Manager.MapManager.Instance.GetMap(caster.MapID);
                        actor.MapID = caster.MapID;
                        short[] pos = map.GetRandomPosAroundPos(caster.X, caster.Y, 2000);
                        actor.X = pos[0];
                        actor.Y = pos[1];
                        actor.Speed = 500;
                        actor.e = new ActorEventHandlers.NullEventHandler();
                        map.RegisterActor(actor);
                        actor.invisble = false;
                        map.OnActorVisibilityChange(actor);
                        actor.Stackable = false;
                        Activator2 timer = new Activator2(caster, actor, this.skill);
                        timer.Activate();
                        count++;
                    }
                    else
                    {
                        this.Deactivate();
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    this.Deactivate();
                }
                //解开同步锁
                ClientManager.LeaveCriticalArea();
            }
        }
        #endregion
        private class Activator2 : MultiRunTask
        {
            ActorSkill actor;
            Actor caster;
            Actor dactor;
            SkillArg skill;
            Map map;
            int countMax = 30, count = 0;
            public Activator2(Actor caster, ActorSkill skillActor, SkillArg args)
            {
                this.actor = skillActor;
                this.caster = caster;
                this.skill = args.Clone();
                this.skill.dActor = 0xffffffff;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 320;
                this.dueTime = 3000;
                List<Actor> actors = map.GetActorsArea(actor, 2000, false);
                List<Actor> affected = new List<Actor>();
                foreach (var item in actors)
                {
                    if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                        affected.Add(item);
                }
                if (affected.Count > 0)
                    this.dactor = affected[SagaLib.Global.Random.Next(0, affected.Count - 1)];
                else this.dactor = null;
            }
            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                ClientManager.EnterCriticalArea();
                try
                {
                    List<Actor> actors = map.GetActorsArea(actor, 100, false);
                    List<Actor> affected = new List<Actor>();
                    foreach (var item in actors)
                    {
                        if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                            affected.Add(item);
                    }
                    if (affected.Count > 0 || dactor == null || count == countMax || dactor.Buff.Dead)
                    {
                        byte x = SagaLib.Global.PosX16to8(actor.X, map.Width);
                        byte y = SagaLib.Global.PosY16to8(actor.Y, map.Height);
                        SkillArg s = this.skill.Clone();
                        s.x = x;
                        s.y = y;
                        EffectArg arg = new EffectArg();
                        arg.effectID = 5205;
                        arg.actorID = 0xFFFFFFFF;
                        arg.x = s.x;
                        arg.y = s.y;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, actor, true);
                        List<Actor> actors2 = map.GetRoundAreaActors(actor.X, actor.Y, 300);
                        foreach (Actor j in actors2)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, j))
                            {
                                int damage = SkillHandler.Instance.CalcDamage(true, caster, j, skill, SkillHandler.DefType.MDef, SagaLib.Elements.Fire, 1, 2f);
                                SkillHandler.Instance.CauseDamage(caster, j, damage);
                                SkillHandler.Instance.ShowVessel(j, damage);
                                SkillHandler.Instance.ShowEffect(SagaMap.Manager.MapManager.Instance.GetMap(map.ID), j, 5075);
                                Burning burn = new Burning(null, j, 6000, 90);
                                SkillHandler.ApplyAddition(j, burn);
                                if (!j.Status.Additions.ContainsKey("BOSS_大地之怒"))
                                {
                                    OtherAddition skill2 = new OtherAddition(null, j, "BOSS_红莲炎", 10000);
                                    skill2.OnAdditionEnd += (i, e) =>
                                    {
                                        i.TInt["BOSS_红莲炎层数"] = 0;
                                        Network.Client.MapClient.FromActorPC((ActorPC)i).SendSystemMessage("【红莲炎种】层数清空了。");
                                    };
                                    SkillHandler.ApplyAddition(j, skill2);
                                }
                                if (j.TInt["BOSS_红莲炎层数"] < 5)
                                {
                                    //SkillHandler.Instance.ShowEffectOnActor(j, 5041);
                                    j.TInt["BOSS_红莲炎层数"]++;
                                    Network.Client.MapClient.FromActorPC((ActorPC)j).SendSystemMessage("你被红莲炎炸中了！[层数:" + j.TInt["BOSS_红莲炎层数"].ToString() + "/5]");
                                }
                                else
                                {

                                    Network.Client.MapClient.FromActorPC((ActorPC)j).SendSystemMessage("【红莲炎】将你吞噬了。[层数:" + j.TInt["BOSS_红莲炎层数"].ToString() + "/5]");
                                    SkillHandler.Instance.ShowEffectOnActor(j, 5421);
                                    List<Actor> actors3 = map.GetRoundAreaActors(j.X, j.Y, 500);
                                    foreach (var item in actors3)
                                    {
                                        if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                                        {
                                            SkillHandler.Instance.CauseDamage(caster, item, (int)item.MaxHP);
                                            SkillHandler.Instance.ShowVessel(item, (int)item.MaxHP);
                                            SkillHandler.Instance.ShowEffectOnActor(item, 5075);
                                            if (item != j)
                                                Network.Client.MapClient.FromActorPC((ActorPC)item).SendSystemMessage("附近的【红莲炎】将你吞噬了。");
                                        }
                                    }
                                }
                            }
                        }
                        this.Deactivate();
                        map.DeleteActor(actor);
                        count = countMax;
                    }
                    else
                    {
                        Mob.MobAI ai = new MobAI(actor, true);
                        List<MapNode> path = ai.FindPath(SagaLib.Global.PosX16to8(actor.X, map.Width), SagaLib.Global.PosY16to8(actor.Y, map.Height),
                            SagaLib.Global.PosX16to8(dactor.X, map.Width), SagaLib.Global.PosY16to8(dactor.Y, map.Height));
                        int deltaX = path[0].x;
                        int deltaY = path[0].y;
                        MapNode node = new MapNode();
                        node.x = (byte)deltaX;
                        node.y = (byte)deltaY;
                        path.Add(node);
                        short[] pos = new short[2];
                        pos[0] = SagaLib.Global.PosX8to16(path[0].x, map.Width);
                        pos[1] = SagaLib.Global.PosY8to16(path[0].y, map.Height);
                        map.MoveActor(Map.MOVE_TYPE.START, actor, pos, 0, 200);

                    }
                    count++;
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    this.Deactivate();
                    map.DeleteActor(actor);
                }
                //解开同步锁
                ClientManager.LeaveCriticalArea();
            }
        }
    }
}

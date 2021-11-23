using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31130 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            印记 timer = new 印记(sActor, args, level);
            timer.Activate();
        }

        private class 印记 : MultiRunTask
        {
            Actor caster;
            SkillArg skill;
            Map map;
            List<short[]> paths = new List<short[]>();
            int countMax = 105, count = 0;
            public 印记(Actor caster, SkillArg args, byte level)
            {
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = SagaLib.Global.Random.Next(50, 130);
                this.dueTime = 1500;

            }
            public override void CallBack()
            {
                //ClientManager.EnterCriticalArea();
                try
                {
                    if (count < countMax)
                    {
                        short[] pos;
                        //获取范围内随机坐标
                        pos = map.GetRandomPosAroundPos(caster.X, caster.Y, 1800);
                        SkillHandler.Instance.ShowEffect(map, caster, SagaLib.Global.PosX16to8(pos[0], map.Width), SagaLib.Global.PosY16to8(pos[1], map.Height), 5081);
                        雷电 timer = new 雷电(caster, skill, pos);
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
                    Deactivate();
                }
                //解开同步锁
                //ClientManager.LeaveCriticalArea();
            }
        }

        private class 雷电 : MultiRunTask
        {
            Actor caster;
            SkillArg skill;
            Map map;
            short[] paths;
            public 雷电(Actor caster, SkillArg args, short[] paths)
            {
                this.caster = caster;
                this.skill = args.Clone();
                this.skill.dActor = 0xffffffff;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 0;
                this.dueTime = SagaLib.Global.Random.Next(1000,2000);
                this.paths = paths;
            }
            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                //ClientManager.EnterCriticalArea();
                try
                {
                    byte x = SagaLib.Global.PosX16to8(paths[0], map.Width);
                    byte y = SagaLib.Global.PosY16to8(paths[1], map.Height);
                    SkillArg s = this.skill.Clone();
                    s.x = x;
                    s.y = y;
                    EffectArg arg = new EffectArg();
                    arg.effectID = 5003;
                    arg.actorID = 0xFFFFFFFF;
                    arg.x = s.x;
                    arg.y = s.y;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, caster, true);
                    List<Actor> actors = map.GetRoundAreaActors(paths[0], paths[1], 150);
                    List<Actor> affected = new List<Actor>();
                    s.affectedActors.Clear();
                    foreach (Actor j in actors)
                    {
                        if (SkillHandler.Instance.CheckValidAttackTarget(caster, j))
                        {
                            int damage = 0;
                            if (j.Status.Additions.ContainsKey("Stun"))
                            {
                                damage = 0;
                            }
                            else
                            {
                                damage = SkillHandler.Instance.CalcDamage(true, caster, j, skill, SkillHandler.DefType.MDef, SagaLib.Elements.Dark, 1, 5f);
                                Stun stun = new Stun(null, j, 5000);
                                SkillHandler.ApplyAddition(j, stun);

                                /*-------------------魔法阵的技能体-----------------*/
                                ActorSkill actor2 = new ActorSkill(SagaDB.Skill.SkillFactory.Instance.GetSkill(31138, 1), caster);
                                actor2.Name = "黑月小暗魔法阵";
                                actor2.MapID = caster.MapID;
                                actor2.X = paths[0];
                                actor2.Y = paths[1];
                                actor2.e = new ActorEventHandlers.NullEventHandler();
                                map.RegisterActor(actor2);
                                actor2.invisble = false;
                                map.OnActorVisibilityChange(actor2);
                                actor2.Stackable = false;
                                /*-------------------魔法阵的技能体-----------------*/

                                黑月 hy = new 黑月(caster, actor2);
                                hy.Activate();
                            }

                            SkillHandler.Instance.CauseDamage(caster, j, damage);
                            SkillHandler.Instance.ShowVessel(j, damage);
                            SkillHandler.Instance.ShowEffect(SagaMap.Manager.MapManager.Instance.GetMap(map.ID), j, 4321);
                        }
                    }
                    this.Deactivate();
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    Deactivate();
                }
                //解开同步锁
                //ClientManager.LeaveCriticalArea();
            }
        }

        private class 黑月 :MultiRunTask
        {
            Actor caster;
            ActorSkill actorSkill;
            Map map;
            public 黑月(Actor caster, ActorSkill skill)
            {
                this.caster = caster;
                actorSkill = skill;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                dueTime = 3000;
            }
            public override void CallBack()
            {
                try
                {
                    List<Actor> actors = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(caster, actorSkill, 150);
                    SkillHandler.Instance.ShowEffect(map, caster, SagaLib.Global.PosX16to8(actorSkill.X, map.Width), SagaLib.Global.PosY16to8(actorSkill.Y, map.Height), 5044);
                    map.DeleteActor(actorSkill);//删除魔法阵效果
                    foreach (var item in actors)
                    {
                        int damage = (int)(item.MaxHP * 6f);
                        SkillHandler.Instance.CauseDamage(caster, item, damage);
                        SkillHandler.Instance.ShowVessel(item, damage);
                        SkillHandler.Instance.ShowEffectOnActor(item, 5261);
                    }
                    Deactivate();
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    Deactivate();
                }
            }
        }
    }
}

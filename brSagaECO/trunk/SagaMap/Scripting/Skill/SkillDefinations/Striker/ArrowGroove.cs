using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap.Mob;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Striker
{
    /// <summary>
    /// 飛鑽箭頭（クリーブアロー）
    /// </summary>
    public class ArrowGroove : ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            this.ProcSub(sActor, dActor, args, level, SagaLib.Elements.Neutral );
        }
        protected void ProcSub(Actor sActor, Actor dActor, SkillArg args, byte level, SagaLib.Elements element)
        {
            //创建设置型技能技能体
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //设定技能体位置            
            actor.MapID = sActor.MapID;
            actor.X = sActor.X;
            actor.Y = sActor.Y;
            actor.Speed = 500;

            //创建AI类
            Mob.MobAI ai = new SagaMap.Mob.MobAI(actor, true);
            //寻路
            List<MapNode> path = ai.FindPath(SagaLib.Global.PosX16to8(sActor.X, map.Width), SagaLib.Global.PosY16to8(sActor.Y, map.Height), args.x, args.y);

            if (path.Count >= 2)
            {
                //根据现有路径推算一步
                int deltaX = path[path.Count - 1].x - path[path.Count - 2].x;
                int deltaY = path[path.Count - 1].y - path[path.Count - 2].y;
                deltaX = path[path.Count - 1].x + deltaX;
                deltaY = path[path.Count - 1].y + deltaY;
                MapNode node = new MapNode();
                node.x = (byte)deltaX;
                node.y = (byte)deltaY;
                path.Add(node);
            }
            if (path.Count == 1)
            {
                //根据现有路径推算一步
                int deltaX = path[path.Count - 1].x - SagaLib.Global.PosX16to8(sActor.X, map.Width);
                int deltaY = path[path.Count - 1].y - SagaLib.Global.PosY16to8(sActor.Y, map.Height);
                deltaX = path[path.Count - 1].x + deltaX;
                deltaY = path[path.Count - 1].y + deltaY;
                MapNode node = new MapNode();
                node.x = (byte)deltaX;
                node.y = (byte)deltaY;
                path.Add(node);
            }
            //设定技能体的事件处理器，由于技能体不需要得到消息广播，因此创建个空处理器
            actor.e = new ActorEventHandlers.NullEventHandler();
            //在指定地图注册技能体Actor
            map.RegisterActor(actor);
            //设置Actor隐身属性为非
            actor.invisble = false;
            //广播隐身属性改变事件，以便让玩家看到技能体
            map.OnActorVisibilityChange(actor);
            //创建技能效果处理对象

            Activator timer = new Activator(sActor, actor, args, path, element);
            timer.Activate();
        }

        #region Timer

        private class Activator : MultiRunTask
        {
            ActorSkill actor;
            Actor caster;
            SkillArg skill;
            Map map;
            List<MapNode> path;
            int count = 0;
            float factor = 1f;
            Elements element;
            bool stop = false;

            public Activator(Actor caster, ActorSkill actor, SkillArg args, List<MapNode> path, Elements element)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 200;
                this.dueTime = 200;
                this.path = path;
                factor = CalcFactor(args.skill.Level);
                this.element = element;
            }

            /// <summary>
            /// 计算伤害加成
            /// </summary>
            /// <param name="level">技能等级</param>
            /// <returns>伤害加成</returns>
            float CalcFactor(byte level)
            {
                switch (level)
                {
                    case 1:
                        return 1.9f;
                    case 2:
                        return 1.35f;
                    case 3:
                        return 1.85f;
                    case 4:
                        return 1.45f;
                    case 5:
                        return 2.15f;
                    default:
                        return 1f;
                }
            }

            public override void CallBack(object o)
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                //测试去除技能同步锁ClientManager.EnterCriticalArea();
                try
                {
                    if ((path.Count <= count + 1) || (count > skill.skill.Level + 2))
                    {
                        this.Deactivate();
                        //在指定地图删除技能体（技能效果结束）
                        map.DeleteActor(actor);
                    }
                    else
                    {
                        try
                        {
                            short[] pos = new short[2];
                            short[] pos2 = new short[2];
                            pos[0] = SagaLib.Global.PosX8to16(path[count].x, map.Width);
                            pos[1] = SagaLib.Global.PosY8to16(path[count].y, map.Height);
                            pos2[0] = SagaLib.Global.PosX8to16(path[count + 1].x, map.Width);
                            pos2[1] = SagaLib.Global.PosY8to16(path[count + 1].y, map.Height);
                            map.MoveActor(Map.MOVE_TYPE.START, actor, pos, 0, 200);

                            //取得当前格子内的Actor
                            List<Actor> list = map.GetActorsArea(actor, 50, false);
                            List<Actor> affected = new List<Actor>();

                            //筛选有效对象
                            foreach (Actor i in list)
                            {
                                if (SkillHandler.Instance.CheckValidAttackTarget(caster, i))
                                    affected.Add(i);
                            }
                            if (map.GetActorsArea(pos2[0], pos2[1], 50).Count != 0 || map.Info.walkable[path[count + 1].x, path[count + 1].y] != 2)
                            {
                                if (stop)
                                {
                                    this.Deactivate();
                                    //在指定地图删除技能体（技能效果结束）
                                    map.DeleteActor(actor);
                                    //return 前必须解锁
                                    ClientManager.LeaveCriticalArea();
                                    return;
                                }
                                else
                                    stop = true;
                            }

                            foreach (Actor i in affected)
                            {
                                //CannotMove addition = new CannotMove(skill.skill, i, 400);
                                Additions.Global.硬直 addition = new 硬直(skill.skill, i, 400);
                                SkillHandler.ApplyAddition(i, addition);
                                map.MoveActor(Map.MOVE_TYPE.START, i, pos2, 500, 500, true);
                                if (i.type == ActorType.MOB || i.type == ActorType.PET || i.type == ActorType.SHADOW)
                                {
                                    ActorEventHandlers.MobEventHandler mob = (ActorEventHandlers.MobEventHandler)i.e;
                                    mob.AI.OnPathInterupt();
                                }
                            }

                            skill.affectedActors.Clear();
                            SkillHandler.Instance.PhysicalAttack(caster, affected, skill, element, factor);
                            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, actor, false);
                        }
                        catch { }
                        count++;
                    }


                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                //解开同步锁
                //测试去除技能同步锁ClientManager.LeaveCriticalArea();
            }
        }
        #endregion

        #endregion
    }
}

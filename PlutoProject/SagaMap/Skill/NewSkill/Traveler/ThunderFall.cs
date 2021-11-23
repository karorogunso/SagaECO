using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;

namespace SagaMap.Skill.SkillDefinations.Traveler
{
    public class ThunderFall : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //创建设置型技能技能体
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //设定技能体位置
            actor.MapID = sActor.MapID;
            actor.X = SagaLib.Global.PosX8to16(args.x, map.Width);
            actor.Y = SagaLib.Global.PosY8to16(args.y, map.Height);
            //设定技能体的事件处理器，由于技能体不需要得到消息广播，因此创建个空处理器
            actor.e = new ActorEventHandlers.NullEventHandler();
            //在指定地图注册技能体Actor
            map.RegisterActor(actor);
            //设置Actor隐身属性为非
            actor.invisble = false;
            //广播隐身属性改变事件，以便让玩家看到技能体
            map.OnActorVisibilityChange(actor);
            //設置系
            actor.Stackable = false;
            //创建技能效果处理对象
            Activator timer = new Activator(sActor, actor, args, level);
            timer.Activate();
            /*
            uint NextSkillID = 22000;
            args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(NextSkillID, 1, 0));
            */
            
        }

        #endregion

        #region Timer

        private class Activator : MultiRunTask
        {
            public List<Point> GetStraightPath(byte fromx, byte fromy, byte tox, byte toy)
            {

                List<Point> path = new List<Point>();
                if (fromx == tox && fromy == toy)
                    return path;
                double k;// 
                double nowx = fromx;
                double nowy = fromy;
                int x = fromx;
                int y = fromy;
                sbyte addx, addy;
                if (Math.Abs(toy - fromy) <= Math.Abs(tox - fromx))
                {
                    if (tox == fromx)
                    {
                        if (fromy < toy)
                        {
                            for (int i = fromy + 1; i <= toy; i++)
                            {
                                Point t = new Point();
                                t.x = fromx;
                                t.y = (byte)i;
                                path.Add(t);
                            }
                        }
                        else
                        {
                            for (int i = fromy - 1; i <= toy; i--)
                            {
                                Point t = new Point();
                                t.x = fromx;
                                t.y = (byte)i;
                                path.Add(t);
                            }
                        }
                    }
                    else
                    {
                        k = Math.Abs((double)(toy - fromy) / (tox - fromx));
                        if (toy < fromy)
                            addy = -1;
                        else
                            addy = 1;
                        if (tox < fromx)
                            addx = -1;
                        else
                            addx = 1;
                        while (Math.Round(nowx) != tox)
                        {
                            x += addx;
                            if (Math.Round(nowy) != Math.Round(nowy + k * addy))
                                y += addy;
                            nowx += addx;
                            nowy += k * addy;

                            Point t = new Point();
                            t.x = (byte)x;
                            t.y = (byte)y;
                            path.Add(t);
                        }
                    }
                }
                else
                {
                    if (toy == fromy)
                    {
                        if (fromx < tox)
                        {
                            for (int i = fromx + 1; i <= tox; i++)
                            {
                                Point t = new Point();
                                t.x = (byte)i;
                                t.y = fromy;
                                path.Add(t);
                            }
                        }
                        else
                        {
                            for (int i = fromx - 1; i <= tox; i--)
                            {
                                Point t = new Point();
                                t.x = (byte)i;
                                t.y = fromy;
                                path.Add(t);
                            }
                        }
                    }
                    else
                    {
                        k = Math.Abs((double)(tox - fromx) / (toy - fromy));
                        if (toy < fromy)
                            addy = -1;
                        else
                            addy = 1;
                        if (tox < fromx)
                            addx = -1;
                        else
                            addx = 1;
                        while (Math.Round(nowy) != toy)
                        {
                            y += addy;
                            if (Math.Round(nowx) != Math.Round(nowx + k * addx))
                                x += addx;
                            nowy += addy;
                            nowx += k * addx;

                            Point t = new Point();
                            t.x = (byte)x;
                            t.y = (byte)y;
                            path.Add(t);
                        }
                    }
                }
                return path;
            }

            public class Point
            {
                public byte x, y;

            }
            ActorSkill actor;
            Actor caster;
            SkillArg skill;
            Map map;
            float factor = 1.0f;
            int count = 0;
            List<Point> path;

            public Activator(Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                this.skill.skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(23003,1);
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 200;
                this.dueTime = 0;
                path = GetStraightPath(SagaLib.Global.PosX16to8(caster.X, map.Width), SagaLib.Global.PosY16to8(caster.Y, map.Height), args.x, args.y);
                ActorPC Me = (ActorPC)caster;

            }



            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问ClientManager.EnterCriticalArea();
                try
                {
                    if (count < path.Count)
                    {
                        skill.x = path[count].x;
                        skill.y = path[count].y;

                        List<Actor> actors = map.GetRoundAreaActors(SagaLib.Global.PosX8to16(skill.x,map.Width), SagaLib.Global.PosY8to16(skill.y,map.Height), 150);
                        List<Actor> affected = new List<Actor>();
                        skill.affectedActors.Clear();
                        foreach (Actor i in actors)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, i))
                            {
                                affected.Add(i);
                            }
                        }
                        SkillHandler.Instance.MagicAttack(caster, affected, skill, Elements.Wind, factor);

                        //广播技能效果
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, actor, false);
                        count++;
                    }
                    else
                    {
                        
                        this.Deactivate();
                        //在指定地图删除技能体（技能效果结束）
                        map.DeleteActor(actor);
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                //解开同步锁ClientManager.LeaveCriticalArea();
            }
        }
        #endregion
    }
}

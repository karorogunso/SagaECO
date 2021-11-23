using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;

namespace SagaMap.Skill.SkillDefinations
{
    public class S30015 : ISkill
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
            SkillHandler.Instance.ActorSpeak(sActor, "你可走过这座奈何桥？");
        }

        #endregion

        #region Timer

        private class Activator : MultiRunTask
        {
            ActorSkill actor;
            Actor caster;
            SkillArg skill;
            Map map;
            float factor = 1.0f;
            int count = 0;
            List<SkillHandler.Point> path;

            public Activator(Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                //this.skill.skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(23003, 1);
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 50;
                this.dueTime = 3000;
                byte x = SagaLib.Global.PosX16to8(caster.X, map.Width);
                byte y = SagaLib.Global.PosY16to8(caster.Y, map.Height);
               short[] p = map.GetRandomPos(); ;

                

                path =SkillHandler.Instance.GetStraightPath(SagaLib.Global.PosX16to8(p[0], map.Width), SagaLib.Global.PosY16to8(p[1], map.Height), x, y);

            }



            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                ClientManager.EnterCriticalArea();
                try
                {
                    if (count < path.Count)
                    {
                        if (count % 3 == 0)
                        {
                            skill.x = path[count].x;
                            skill.y = path[count].y;
                            map.SendEffect(caster, skill.x, skill.y, 5103);
                            List<Actor> actors = map.GetRoundAreaActors(SagaLib.Global.PosX8to16(skill.x, map.Width), SagaLib.Global.PosY8to16(skill.y, map.Height), 500);
                            List<Actor> affected = new List<Actor>();
                            skill.affectedActors.Clear();
                            foreach (Actor i in actors)
                            {
                                if (SkillHandler.Instance.CheckValidAttackTarget(caster, i))
                                {
                                    int damage = 0;
                                    if (i.HP > 1)
                                        damage = (int)(i.HP - 1);
                                    SkillHandler.Instance.CauseDamage(caster, i, damage);
                                    SkillHandler.Instance.ShowVessel(i, damage);
                                    SkillHandler.Instance.ShowEffect(SagaMap.Manager.MapManager.Instance.GetMap(i.MapID), i, 8050);
                                    Skill.Additions.Global.MaxHPDown hpd = new Additions.Global.MaxHPDown(skill.skill, i, 10000);//死亡毒素
                                    SkillHandler.ApplyAddition(i, hpd);
                                }
                            }
                            //广播技能效果
                            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, actor, false);

                        } count++;
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
                    this.Deactivate();
                    map.DeleteActor(actor);
                }
                //解开同步锁
                ClientManager.LeaveCriticalArea();
            }
        }
        #endregion
    }
}

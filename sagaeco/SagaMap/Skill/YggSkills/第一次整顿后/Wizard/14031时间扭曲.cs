using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 时间扭曲：弱鸡技能
    /// </summary>
    public class S14031
        : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("时间扭曲CD")) return -30;
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
            SkillHandler.Instance.ShowEffect(map, sActor, args.x, args.y, 5198);
        }

        #endregion

        #region Timer

        private class Activator : MultiRunTask
        {
            ActorSkill actor;
            Actor caster;
            ActorPC Me;
            SkillArg skill;
            Map map;
            float factor = 1.5f;
            int countMax = 10, count = 0;
            int cdtime = 300000;
            List<string> cds = new List<string>() {  "袈裟斩CD", "雲切CD", };

            public Activator(Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 1000;
                this.dueTime = 0;

                Me = (ActorPC)caster;

                switch(level)
                {
                    case 1:
                        countMax = 20;
                        break;
                    case 2:
                        countMax = 30;
                        break;
                    case 3:
                        countMax = 40;
                        break;
                }
                DefaultBuff cd = new DefaultBuff(caster, "时间扭曲CD", cdtime);
                SkillHandler.ApplyAddition(caster, cd);
            }
            public override void CallBack()
            {
                try
                {
                    if (count < countMax)
                    {
                        List<Actor> actors = map.GetActorsArea(actor, 3000, false);
                        List<Actor> affected = new List<Actor>();
                        skill.affectedActors.Clear();
                        foreach (Actor i in actors)
                        {
                            if(i.type == ActorType.PC)
                            {
                                ActorPC pc = (ActorPC)i;
                                if(Me.Mode == pc.Mode)
                                {
                                    if(pc.Status.Additions.ContainsKey("时间扭曲"))
                                    {
                                        Addition sd = i.Status.Additions["时间扭曲"];
                                        TimeSpan span = new TimeSpan(0, 0, 0, 0, 1200);
                                        ((OtherAddition)sd).endTime = DateTime.Now + span;

                                        foreach (var item in cds)
                                        {
                                            if (pc.Status.Additions.ContainsKey(item))
                                                SkillHandler.RemoveAddition(pc, item);
                                        }
                                    }
                                    else if(!pc.Status.Additions.ContainsKey("时间扭曲CD2"))
                                    {
                                        OtherAddition skill2 = new OtherAddition(null, pc, "时间扭曲", 1200);
                                        SkillHandler.ApplyAddition(pc, skill2);
                                        SkillHandler.Instance.ShowEffectOnActor(pc, 5133);
                                        OtherAddition cd = new OtherAddition(null, pc, "时间扭曲CD2", 300000);
                                        SkillHandler.ApplyAddition(pc, cd);
                                    }
                                }
                            }
                        }
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
        }
        #endregion
    }
}

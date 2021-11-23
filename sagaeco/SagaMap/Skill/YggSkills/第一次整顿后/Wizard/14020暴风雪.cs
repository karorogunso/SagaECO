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
    /// 寒冰湍流：7×7水属性设置多段魔法攻击，附带颤栗
    /// </summary>
    public class S14020
        : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("寒冰湍流CD")) return -30;
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

            if(sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                pc.TInt["水属性魔法释放"] = 1;
                if (pc.TInt["火属性魔法释放"] == 1)
                    pc.TInt["水属性魔法释放"] = 2;
            }
        }

        #endregion

        #region Timer

        private class Activator : MultiRunTask
        {
            ActorSkill actor;
            Actor caster;
            SkillArg skill;
            Map map;
            float factor = 2.5f;
            int countMax = 5, count = 0;
            byte Lv = 1;
            public Activator(Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 200;
                this.dueTime = 0;
                Lv = level;
                ActorPC Me = (ActorPC)caster;

                switch(level)
                {
                    case 1:
                        countMax = 5;
                        break;
                    case 2:
                        countMax = 10;
                        break;
                    case 3:
                        countMax = 15;
                        break;
                    case 4:
                        countMax = 15;
                        factor = 3.2f;
                        break;
                }
                int cdtime = 10000;
                switch (Me.TInt["降温"])
                {
                    case 1:
                        cdtime = 8000;
                        break;
                    case 2:
                        cdtime = 6500;
                        break;
                    case 3:
                        cdtime = 5000;
                        break;
                }
                OtherAddition cd = new OtherAddition(null,caster, "寒冰湍流CD", cdtime);
                SkillHandler.ApplyAddition(caster, cd);
                //caster.TInt["魔导师普攻"] = 1;
            }
            public override void CallBack()
            {
                try
                {
                    bool Fortified = false;
                    if (count < countMax)
                    {
                        List<Actor> actors = map.GetActorsArea(actor, 300, false);
                        List<Actor> affected = new List<Actor>();
                        skill.affectedActors.Clear();
                        foreach (Actor i in actors)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, i))
                            {
                                affected.Add(i);
                            }
                        }
                        if(affected.Count > 0)
                        {
                            Actor i = null;
                            if (affected.Count > 1)
                                i = affected[SagaLib.Global.Random.Next(0, affected.Count - 1)];
                            else
                                i = affected[0];
                            if(i == null)
                            {
                                Deactivate();
                                map.DeleteActor(actor);
                            }
                            if (i.Status.Additions.ContainsKey("空间震") && caster.type == ActorType.PC)
                            {
                                ActorPC Me = (ActorPC)caster;
                                if (Me.Skills.ContainsKey(14007))
                                {
                                    byte lv = Me.Skills[14007].Level;
                                    float fup = 1.25f + lv * 0.05f;
                                    if (!Fortified)
                                    {
                                        factor *= fup;
                                        Fortified = true;
                                    }
                                    SkillHandler.RemoveAddition(i, "空间震");
                                    SkillHandler.Instance.ShowEffectOnActor(i, 5266, caster);
                                }
                            }
                            if (i.Status.Additions.ContainsKey("暴风雪减速"))
                            {
                                Addition sd = i.Status.Additions["暴风雪减速"];
                                TimeSpan span = new TimeSpan(0, 0, 0, 0, 8000);
                                ((OtherAddition)sd).endTime = DateTime.Now + span;
                            }
                            else
                            {
                                OtherAddition sd = new OtherAddition(null, i, "暴风雪减速", 8000);
                                sd.OnAdditionStart += (s, e) =>
                                {
                                    i.TInt["暴风雪减速点"] = i.Speed / 2;
                                    i.Speed -= (ushort)i.TInt["暴风雪减速点"];
                                    i.Buff.SpeedDown = true;
                                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, i, true);
                                };
                                sd.OnAdditionEnd += (s, e) =>
                                {
                                    i.Speed += (ushort)i.TInt["暴风雪减速点"];
                                    i.Buff.SpeedDown = false;
                                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, i, true);
                                };
                                SkillHandler.ApplyAddition(i, sd);
                            }
                            if (Lv >= 4)
                            {
                                if (SagaLib.Global.Random.Next(0, 100) < 5)
                                {
                                    if (!i.Status.Additions.ContainsKey("暴风雪冻结CD"))
                                    {
                                        Freeze fz = new Freeze(null, i, 5000);
                                        SkillHandler.ApplyAddition(i, fz);
                                        OtherAddition sd = new OtherAddition(null, i, "暴风雪冻结CD", 30000);
                                        SkillHandler.ApplyAddition(i, sd);
                                    }
                                }
                            }
                            SkillHandler.Instance.MagicAttack(caster, i, skill, Elements.Water, factor);
                            SkillHandler.Instance.ShowEffectOnActor(i, 5284);
                        }
                        //factor /= affected.Count;
                       

                        //广播技能效果
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
        }
        #endregion
    }
}

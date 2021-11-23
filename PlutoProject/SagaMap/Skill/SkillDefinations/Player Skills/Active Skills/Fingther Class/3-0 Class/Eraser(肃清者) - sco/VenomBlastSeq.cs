using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaMap.Skill.SkillDefinations.Eraser
{
    /// <summary>
    /// ヴェノムブラスト [后续技能]
    /// </summary>
    public class VenomBlastSeq : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }



        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //创建设置型技能技能体
            //if (dActor.HP == 0)
            //{
            //    return;
            //}
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
            //设定技能体位置
            actor.MapID = dActor.MapID;
            //actor.X = SagaLib.Global.PosX8to16((byte)sActor.X, map.Width);
            //actor.Y = SagaLib.Global.PosY8to16((byte)sActor.Y, map.Height);
            //更改设定位置为怪物本身位置
            actor.X = dActor.X;
            actor.Y = dActor.Y;
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


        }

        #endregion

        #region Timer

        private class Activator : MultiRunTask
        {
            ActorSkill actor;
            Actor caster;
            SkillArg skill;
            Map map;
            byte skilllevel;

            //float[] factors = new float[] { 0f, 0.02f, 0.04f, 0.01f, 0.04f, 0.05f, 100f };//治疗量=(使用者的)百分比比例
            float factor;

            public Activator(Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                skilllevel = level;
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                factor = 2.0f + 0.5f * level;
                //这里取副职的毒雾等级
                var duallv = 0;
                ActorPC pc = caster as ActorPC;
                if (pc.SkillsReserve.ContainsKey(2142) || pc.Skills2_1.ContainsKey(2142) || pc.DualJobSkill.Exists(x => x.ID == 2142))
                {
                    if (pc.DualJobSkill.Exists(x => x.ID == 2142))
                        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 2142).Level;

                    //这里取主职的毒雾等级
                    var mainlv = 0;
                    if (pc.Skills2_1.ContainsKey(2142))
                        mainlv = pc.Skills2_1[2142].Level;

                    //这里取保留技能中的毒雾等级
                    var Reservelv = 0;
                    if (pc.SkillsReserve.ContainsKey(2142))
                        Reservelv = pc.SkillsReserve[2142].Level;


                    //这里取等级最高的毒雾等级参与运算
                    int tmp = Math.Max(duallv, mainlv);
                    factor += 5.0f + 0.3f * Math.Max(Reservelv, tmp);
                }

                //this.period = periods[level];
                this.dueTime = 1000;

            }

            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                //ClientManager.EnterCriticalArea();
                try
                {
                    Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                    List<Actor> affected = map.GetActorsArea(actor, 200, true);
                    List<Actor> realAffected = new List<Actor>();
                    foreach (Actor act in affected)
                    {
                        if (SkillHandler.Instance.CheckValidAttackTarget(caster, act))
                        {
                            realAffected.Add(act);
                            if (SkillHandler.Instance.CanAdditionApply(caster, act, SkillHandler.DefaultAdditions.Poison, 100))
                            {
                                Additions.Global.Poison skill = new SagaMap.Skill.Additions.Global.Poison(actor.Skill, act, (int)15000);
                                SkillHandler.ApplyAddition(act, skill);
                            }
                            if (SkillHandler.Instance.CanAdditionApply(caster, act, SkillHandler.DefaultAdditions.Silence, 100))
                            {
                                if (skill.skill.Level >= 3)
                                {
                                    Additions.Global.Silence skill = new SagaMap.Skill.Additions.Global.Silence(actor.Skill, act, (int)15000);
                                    SkillHandler.ApplyAddition(act, skill);
                                }
                            }
                            if (SkillHandler.Instance.CanAdditionApply(caster, act, SkillHandler.DefaultAdditions.鈍足, 100))
                            {
                                if (skill.skill.Level >= 4)
                                {
                                    Additions.Global.鈍足 skill = new SagaMap.Skill.Additions.Global.鈍足(actor.Skill, act, (int)15000);
                                    SkillHandler.ApplyAddition(act, skill);
                                }
                            }
                            if (SkillHandler.Instance.CanAdditionApply(caster, act, SkillHandler.DefaultAdditions.Stun, 100))
                            {
                                if (skill.skill.Level == 5)
                                {
                                    Additions.Global.Stun skill = new SagaMap.Skill.Additions.Global.Stun(actor.Skill, act, (int)15000);
                                    SkillHandler.ApplyAddition(act, skill);
                                }
                            }

                            //(リカバリーブロック習得時追加)
                            //查明为HPSPMP回复不能效果
                            ActorPC pc = caster as ActorPC;
                            if (pc.SkillsReserve.ContainsKey(2361) || pc.Skills2_1.ContainsKey(2361) || pc.DualJobSkill.Exists(x => x.ID == 2361))//要害突刺
                            {
                                if (!SkillHandler.Instance.isBossMob(act))
                                {
                                    DefaultBuff skill2 = new DefaultBuff(skill.skill, act, "VenomBlastSeq", 15000);
                                    skill2.OnAdditionStart += this.StartEventHandler;
                                    skill2.OnAdditionEnd += this.EndEventHandler;
                                    SkillHandler.ApplyAddition(act, skill2);
                                }
                            }
                            if (pc.SkillsReserve.ContainsKey(2413) || pc.Skills2_1.ContainsKey(2413) || pc.DualJobSkill.Exists(x => x.ID == 2413))//致命一击
                            {
                                if (SkillHandler.Instance.CanAdditionApply(caster, act, SkillHandler.DefaultAdditions.Stiff, 100))
                                {
                                    Additions.Global.Stiff skill = new SagaMap.Skill.Additions.Global.Stiff(actor.Skill, act, (int)3000);
                                    SkillHandler.ApplyAddition(act, skill);
                                }
                            }

                        }
                    }

                    //SkillHandler.Instance.PhysicalAttack(caster, realAffected, skill, SkillHandler.DefType.Def, caster.WeaponElement, 0, factor, false, 0.0f, false);
                    SkillHandler.Instance.PhysicalAttack(caster, realAffected, skill, caster.WeaponElement, factor);


                    //广播技能效果
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, actor, false);
                    this.Deactivate();
                    //在指定地图删除技能体（技能效果结束）
                    map.DeleteActor(actor);

                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                //解开同步锁
                //ClientManager.LeaveCriticalArea();
            }

            void StartEventHandler(Actor actor, DefaultBuff skill)
            {
                actor.Buff.NoRegen = true;
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            }
            void EndEventHandler(Actor actor, DefaultBuff skill)
            {
                actor.Buff.NoRegen = false;
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            }
            #endregion
        }


    }


}

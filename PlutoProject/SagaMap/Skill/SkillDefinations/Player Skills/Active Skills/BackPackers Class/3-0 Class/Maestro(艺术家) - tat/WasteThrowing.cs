using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.Maestro
{
    /// <summary>
    /// ジリオンブレイド
    /// </summary>
    public class WasteThrowing : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            short range = 200;
            Map map = Manager.MapManager.Instance.GetMap(pc.MapID);
            if (CheckPossible(pc))
            {
                if (map.CheckActorSkillInRange(dActor.X, dActor.Y, range))
                {
                    return -17;
                }
                return 0;
            }
            else
                return -5;

        }

        bool CheckPossible(Actor sActor)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND) || pc.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2).Count > 0)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //创建设置型技能技能体
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //设定技能体位置
            actor.MapID = dActor.MapID;
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
            Activator timer = new Activator(sActor, dActor, actor, args, level);
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
            int countMax = 0, count = 0;
            float factor = 0;
            int lifetime = 0;
            SkillArg thisargs;
            public Activator(Actor caster, Actor theDActor, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.dueTime = 0;
                countMax = new int[] { 0, 6, 8, 10, 12, 16 }[level];
                factor = 0.3f + 0.1f * level;
                this.thisargs = args;
                ActorPC pc = caster as ActorPC;
                int Enhance = pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].Refine;
                if (Enhance >= 1 &&
                    Enhance <= 4)
                {
                    factor += 1.2f;
                }
                if (Enhance >= 5 &&
                    Enhance <= 10)
                {
                    factor += 1.25f;
                }
                else if (Enhance >= 11 &&
                    Enhance <= 24)
                {
                    factor += 1.35f;
                }
                else if (Enhance >= 25 &&
                    Enhance <= 29)
                {
                    factor += 1.55f;
                }
                else if (Enhance == 30)
                {
                    factor += 1.8f;
                }
                //预留强化位结束
                this.lifetime = 800 * level;//攻击总时间不明,暂设定为2000*等级毫秒,5级为10秒
                this.period = lifetime / countMax;
            }
            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                //测试去除技能同步锁ClientManager.EnterCriticalArea();
                try
                {
                    if (count < countMax)
                    {
                        //取得设置型技能，技能体周围7x7范围的怪（范围300，300代表3格，以自己为中心的3格范围就是7x7）
                        List<Actor> actors = map.GetActorsArea(actor, 200, false);
                        List<Actor> affected = new List<Actor>();
                        //取得有效Actor（即怪物）

                        //施加魔法伤害
                        skill.affectedActors.Clear();
                        foreach (Actor i in actors)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, i))
                            {

                                if (i == caster)
                                {
                                    continue;
                                }
                                if (SkillHandler.Instance.CanAdditionApply(caster, i, SkillHandler.DefaultAdditions.Stun, 30) && !SkillHandler.Instance.isBossMob(i))
                                {
                                    Additions.Global.Stun skill1 = new SagaMap.Skill.Additions.Global.Stun(thisargs.skill, i, lifetime);
                                    SkillHandler.ApplyAddition(i, skill1);
                                }
                                if (SkillHandler.Instance.CanAdditionApply(caster, i, SkillHandler.DefaultAdditions.鈍足, 30) && !SkillHandler.Instance.isBossMob(i))
                                {
                                    Additions.Global.MoveSpeedDown skill2 = new SagaMap.Skill.Additions.Global.MoveSpeedDown(thisargs.skill, i, lifetime);
                                    SkillHandler.ApplyAddition(i, skill2);
                                }
                                if (SkillHandler.Instance.CanAdditionApply(caster, i, SkillHandler.DefaultAdditions.Silence, 30) && !SkillHandler.Instance.isBossMob(i))
                                {
                                    Additions.Global.Silence skill3 = new SagaMap.Skill.Additions.Global.Silence(thisargs.skill, i, lifetime);
                                    SkillHandler.ApplyAddition(i, skill3);
                                }
                                if (SkillHandler.Instance.CanAdditionApply(caster, i, SkillHandler.DefaultAdditions.CannotMove, 30) && !SkillHandler.Instance.isBossMob(i))
                                {
                                    Additions.Global.CannotMove skill4 = new SagaMap.Skill.Additions.Global.CannotMove(thisargs.skill, i, lifetime);
                                    SkillHandler.ApplyAddition(i, skill4);
                                }
                                if (SkillHandler.Instance.CanAdditionApply(caster, i, SkillHandler.DefaultAdditions.Confuse, 30) && !SkillHandler.Instance.isBossMob(i))
                                {
                                    Additions.Global.Confuse skill5 = new SagaMap.Skill.Additions.Global.Confuse(thisargs.skill, i, lifetime);
                                    SkillHandler.ApplyAddition(i, skill5);
                                }
                                affected.Add(i);
                            }
                        }
                        SkillHandler.Instance.PhysicalAttack(caster, affected, skill, caster.WeaponElement, factor);

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
                //解开同步锁
                //测试去除技能同步锁ClientManager.LeaveCriticalArea();
            }
        }
        #endregion
    }
}

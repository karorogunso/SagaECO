using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.ForceMaster
{
    class ShockWave : Groove, ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //设定技能体位置            
            actor.MapID = sActor.MapID;
            short vx = (short)SagaLib.Global.Random.Next(-5, 5);
            short vy = (short)SagaLib.Global.Random.Next(-5, 5);
            actor.X = (short)(sActor.X + vx);
            actor.Y = (short)(sActor.Y + vy);
            //actor.Speed = 1000;
            //设定技能体的事件处理器，由于技能体不需要得到消息广播，因此创建个空处理器
            actor.e = new ActorEventHandlers.NullEventHandler();
            //在指定地图注册技能体Actor
            map.RegisterActor(actor);
            //设置Actor隐身属性为非
            actor.invisble = true;
            //广播隐身属性改变事件，以便让玩家看到技能体
            map.OnActorVisibilityChange(actor);
            ActivatorA timer = new ActivatorA(actor, dActor, sActor, args, level);
            timer.Activate();//Call ActivatorA.CallBack 500ms later.
            //创建技能效果处理对象


        }

        #endregion

    }
    class ActivatorA : MultiRunTask
    {

        ActorSkill SkillBody;
        SkillArg Arg;
        Actor AimActor;
        Map map;
        Actor sActor;
        int count = 0;
        int countMax = 3;
        float factor = 1;
        SkillArg SkillFireBolt = new SkillArg();
        bool stop = false;
        public ActivatorA(ActorSkill actor, Actor dActor, Actor sActor, SkillArg args, byte level)
        {
            this.dueTime = 100;
            this.period = 1000;
            this.AimActor = dActor;
            this.Arg = args;
            this.SkillBody = actor;
            this.sActor = sActor;
            map = Manager.MapManager.Instance.GetMap(AimActor.MapID);
            ActorPC Me = (ActorPC)sActor;//Get the total skill level of skill with fire element.
            switch (level)
            {
                case 1:
                    factor = 0.25f;
                    countMax = 4;
                    this.period = 1000;
                    break;
                case 2:
                    factor = 0.5f;
                    countMax = 6;
                    this.period = 800;
                    break;
                case 3:
                    factor = 0.75f;
                    countMax = 10;
                    this.period = 700;
                    break;
                case 4:
                    factor = 1.0f;
                    countMax = 12;
                    this.period = 400;
                    break;
                case 5:
                    factor = 1.25f;
                    countMax = 16;
                    this.period = 100;
                    break;
            }
        }

        public override void CallBack()
        {
            //测试去除技能同步锁
            //ClientManager.EnterCriticalArea();
            short DistanceA = Map.Distance(SkillBody, AimActor);
            short[] Diss = new short[] { 550, 650, 750, 850, 950 };
            if (count <= countMax)
            {

                if (DistanceA <= Diss[Arg.skill.Level - 1])//If mob is out the range that FireBolt can cast, skip out.
                {
                    ActorSkill actor = new ActorSkill(Arg.skill, SkillBody);
                    Map map = Manager.MapManager.Instance.GetMap(SkillBody.MapID);
                    //设定技能体位置            
                    actor.MapID = SkillBody.MapID;
                    short vx = (short)SagaLib.Global.Random.Next(-200, 200);
                    short vy = (short)SagaLib.Global.Random.Next(-200, 200);
                    actor.X = (short)(SkillBody.X + vx);
                    actor.Y = (short)(SkillBody.Y + vy);
                    actor.Speed = 1000;
                    //设定技能体的事件处理器，由于技能体不需要得到消息广播，因此创建个空处理器
                    actor.e = new ActorEventHandlers.NullEventHandler();
                    //在指定地图注册技能体Actor
                    map.RegisterActor(actor);
                    //设置Actor隐身属性为非
                    actor.invisble = false;
                    //广播隐身属性改变事件，以便让玩家看到技能体
                    map.OnActorVisibilityChange(actor);
                    ActivatorC timers = new ActivatorC(AimActor, actor, Arg, Arg.skill.Level);
                    timers.Activate();
                    short[] pos2 = new short[2];
                    pos2[0] = AimActor.X;
                    pos2[1] = AimActor.Y;
                    map.MoveActor(Map.MOVE_TYPE.START, actor, pos2, 0, 1000, true, SagaLib.MoveType.BATTLE_MOTION);
                    if (AimActor.type == ActorType.MOB || AimActor.type == ActorType.PET || AimActor.type == ActorType.SHADOW)
                    {
                        ActorEventHandlers.MobEventHandler mob = (ActorEventHandlers.MobEventHandler)AimActor.e;
                        mob.AI.OnPathInterupt();
                    }

                    Arg.affectedActors.Clear();
                    //SkillHandler.Instance.MagicAttack(sActor, AimActor, Arg, Elements.Neutral, factor);
                    int dmg = SkillHandler.Instance.CalcDamage(false, sActor, AimActor, Arg, SkillHandler.DefType.Def, Elements.Neutral, 0, factor);
                    //SkillHandler.Instance.ApplyDamage(sActor, AimActor, dmg);
                    SkillHandler.Instance.CauseDamage(sActor, AimActor, dmg);
                    SkillHandler.Instance.ShowVessel(AimActor, dmg);
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, Arg, actor, true);
                    EffectArg arg = new EffectArg();
                    arg.effectID = 4353;
                    arg.actorID = AimActor.ActorID;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, AimActor, true);
                    if (SkillFireBolt.flag.Contains(AttackFlag.DIE | AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT))//If mob died,terminate the proccess.
                    {
                        map.DeleteActor(actor);
                        this.Deactivate();
                    }
                }

                count++;
                List<Actor> affected = map.GetActorsArea(AimActor, 50, false);
            }
            else
            {
                map.DeleteActor(SkillBody);
                this.Deactivate();
            }
            //测试去除技能同步锁ClientManager.LeaveCriticalArea();
        }

        private class ActivatorC : MultiRunTask
        {
            ActorSkill actor;
            Actor caster;
            SkillArg skill;
            Map map;
            int countMax = 0;
            int count = 0, lifetime = 0;

            public ActivatorC(Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 0;
                this.dueTime = 800;

            }

            public override void CallBack()
            {

                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问ClientManager.EnterCriticalArea();
                try
                {
                    List<Actor> actors = map.GetActorsArea(actor, 50, false);
                    if (count < countMax)
                    {

                        //广播技能效果
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
    }


}

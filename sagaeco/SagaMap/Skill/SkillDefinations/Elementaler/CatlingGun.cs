using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Mob;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Elementaler
{
    class CatlingGun : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            FireBoll bs = new FireBoll(args.skill, sActor, dActor, 700 + 100*level, 0,args);
            Skill.SkillHandler.ApplyAddition(sActor, bs);
        }
        #endregion
        class FireBoll : DefaultBuff
        {
            public FireBoll(SagaDB.Skill.Skill skill, Actor sActor, Actor dActor, int lifetime, int damage,SkillArg arg)
                : base(skill, sActor, dActor, "FireBoll", lifetime, 100, damage,arg)
            {

                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this.OnUpdate2 += this.TimerUpdate;

            }

            void StartEvent(Actor actor, DefaultBuff skill)
            {
            }

            void EndEvent(Actor actor, DefaultBuff skill)
            {

            }

            void TimerUpdate(Actor sActor, Actor dActor, DefaultBuff skill,SkillArg arg, int damage)
            {
                //测试去除技能同步锁ClientManager.EnterCriticalArea();
                try
                {
                    CreateCat(sActor, dActor, arg, skill.skill.Level);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                //测试去除技能同步锁ClientManager.LeaveCriticalArea();
            }
            void CreateCat(Actor sActor, Actor dActor, SkillArg args, byte level)
            {
                ActorSkill actor = new ActorSkill(SagaDB.Skill.SkillFactory.Instance.GetSkill(7700, 1), sActor);
                byte X = 0;
                byte Y = 0;
                Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                SkillHandler.Instance.GetTRoundPos(map, sActor, out X, out Y,1);
                actor.MapID = sActor.MapID;
                actor.X = SagaLib.Global.PosX8to16(X, map.Width);
                actor.Y = SagaLib.Global.PosY8to16(Y, map.Height);
                actor.Speed = 800;
                actor.e = new ActorEventHandlers.NullEventHandler();
                actor.Name = "NOT_SHOW_DISAPPEAR";
                map.RegisterActor(actor);
                actor.invisble = false;
                map.OnActorVisibilityChange(actor);
                ActivatorA timer = new ActivatorA(actor, sActor, dActor, args, level);
                timer.Activate();
            }
        }
    }
    class ActivatorA : MultiRunTask
    {
        ActorSkill Boll;
        Actor sActor;
        Actor dActor;
        Map map;
        SkillArg arg;

        public ActivatorA(ActorSkill actor, Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            this.Boll = actor;
            this.sActor = sActor;
            this.dActor = dActor;
            this.arg = args;
            map = Manager.MapManager.Instance.GetMap(actor.MapID);
            this.period = 200;
            this.dueTime = 1000;
        }
        public override void CallBack()
        {
            try
            {
                Mob.MobAI ai = new MobAI(Boll, true);
                List<MapNode> path = ai.FindPath(SagaLib.Global.PosX16to8(Boll.X, map.Width), SagaLib.Global.PosY16to8(Boll.Y, map.Height),
                    SagaLib.Global.PosX16to8(dActor.X, map.Width), SagaLib.Global.PosY16to8(dActor.Y, map.Height));
                if (path.Count > 1)
                {
                    int deltaX = path[0].x;
                    int deltaY = path[0].y;
                    if(path.Count == 1)
                    {
                        deltaX = SagaLib.Global.PosX16to8(dActor.X, map.Width);
                        deltaY = SagaLib.Global.PosY16to8(dActor.Y, map.Height);
                    }
                    MapNode node = new MapNode();
                    node.x = (byte)deltaX;
                    node.y = (byte)deltaY;
                    path.Add(node);
                    short[] pos = new short[2];
                    pos[0] = SagaLib.Global.PosX8to16(path[0].x, map.Width);
                    pos[1] = SagaLib.Global.PosY8to16(path[0].y, map.Height);
                    map.MoveActor(Map.MOVE_TYPE.START, Boll, pos, 0, 200);
                    //取得当前格子内的Actor
                }
                else
                { 
                    if(SkillHandler.Instance.isBossMob(dActor))
                    SkillHandler.Instance.PushBack(Boll, dActor, 1);
                    Skill.SkillHandler.Instance.ShowEffect(map, dActor, 5000);
                    int damaga = SkillHandler.Instance.CalcDamage(false, sActor, dActor, arg, SkillHandler.DefType.MDef, Elements.Fire, 50, 1.1f + 0.3f * arg.skill.Level);
                    SkillHandler.Instance.CauseDamage(sActor, dActor, damaga);
                    SkillHandler.Instance.ShowVessel(dActor, damaga);
                    this.Deactivate();
                    map.DeleteActor(Boll);
                    return;
                }
            }
            catch(Exception ex)
            { Logger.ShowError(ex); }
        }
    }
}

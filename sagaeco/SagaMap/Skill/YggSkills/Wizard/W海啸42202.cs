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
namespace SagaMap.Skill.SkillDefinations
{
    class S42202 : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            byte X = 0;
            byte Y = 0;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            SkillHandler.Instance.GetTRoundPos(map, sActor, out X, out Y, 1);
            actor.MapID = sActor.MapID;
            actor.X = SagaLib.Global.PosX8to16(X, map.Width);
            actor.Y = SagaLib.Global.PosY8to16(Y, map.Height);
            actor.Speed = 800;
            actor.Dir = sActor.Dir;
            actor.e = new ActorEventHandlers.NullEventHandler();
            actor.Name = "NOT_SHOW_DISAPPEAR";
            map.RegisterActor(actor);
            actor.invisble = false;
            map.OnActorVisibilityChange(actor);
            TsunamiGO timer = new TsunamiGO(actor, sActor, dActor, args, level);
            timer.Activate();
        }
    }
        #endregion
    class TsunamiGO : MultiRunTask
    {
        ActorSkill Boll;
        Actor sActor;
        Actor dActor;
        Map map;
        SkillArg arg;
        float factor = 2.5f;

        public TsunamiGO(ActorSkill actor, Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            this.Boll = actor;
            this.sActor = sActor;
            this.dActor = dActor;
            this.arg = args;
            map = Manager.MapManager.Instance.GetMap(actor.MapID);
            this.period = 200;
            this.dueTime = 0;

            if (sActor.Status.Additions.ContainsKey("属性契约"))
            {
                if (((OtherAddition)(sActor.Status.Additions["属性契约"])).Variable["属性契约"] == (int)Elements.Water)
                {
                    factor = 4.5f;
                    sActor.EP += 300;
                }
            }
            sActor.EP += 500;
            if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
            if (sActor.Status.Additions.ContainsKey("元素解放"))
            {
                factor = 7.0f;
            }
        }
        public override void CallBack()
        {
            try
            {
                Mob.MobAI ai = new MobAI(Boll, true);
                List<MapNode> path = ai.FindPath(SagaLib.Global.PosX16to8(Boll.X, map.Width), SagaLib.Global.PosY16to8(Boll.Y, map.Height),
                arg.x, arg.y);

                short[] pos = new short[2];
                pos[0] = SagaLib.Global.PosX8to16(path[0].x, map.Width);
                pos[1] = SagaLib.Global.PosY8to16(path[0].y, map.Height);
                map.MoveActor(Map.MOVE_TYPE.START, Boll, pos, Boll.Dir, 1000, true, MoveType.BATTLE_MOTION);

                List<Actor> actors = map.GetRoundAreaActors(pos[0], pos[1], 200);

                foreach (Actor j in actors)
                {
                    if (SkillHandler.Instance.CheckValidAttackTarget(sActor, j))
                    {
                        SkillHandler.AttackResult res = SkillHandler.AttackResult.Hit;
                        int damage = SkillHandler.Instance.CalcDamage(true, sActor, j, arg, SkillHandler.DefType.MDef, SagaLib.Elements.Water, 0, factor,out res);
                        SkillHandler.Instance.CauseDamage(sActor, j, damage);
                        SkillHandler.Instance.ShowVessel(j, damage,0,0,res);
                        SkillHandler.Instance.PushBack(Boll, j, 1);
                    }
                }

                if (path.Count <= 1)
                {
                    this.Deactivate();
                    map.DeleteActor(Boll);
                    return;
                }
            }
            catch (Exception ex)
            { Logger.ShowError(ex);
                this.Deactivate();
                map.DeleteActor(Boll);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31081 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);

            Mob.MobAI ai = new MobAI(sActor, true);
            List<MapNode> path = ai.FindPath(SagaLib.Global.PosX16to8(sActor.X, map.Width), SagaLib.Global.PosY16to8(sActor.Y, map.Height),
                SagaLib.Global.PosX16to8(dActor.X, map.Width), SagaLib.Global.PosY16to8(dActor.Y, map.Height));
            Activator timer = new Activator(sActor, args, level, path,true);
            timer.Activate();

            for (int i = 0; i < 7; i++)
            {
                short[] pos = new short[2];
                pos = map.GetRandomPosAroundPos(sActor.X, sActor.Y, 3500);
                path = ai.FindPath(SagaLib.Global.PosX16to8(sActor.X, map.Width), SagaLib.Global.PosY16to8(sActor.Y, map.Height),
                SagaLib.Global.PosX16to8(pos[0], map.Width), SagaLib.Global.PosY16to8(pos[1], map.Height));
                timer = new Activator(sActor, args, level, path,false);
                timer.Activate();
            }
        }

        #endregion

        #region Timer

        private class Activator : MultiRunTask
        {
            Actor caster;
            SkillArg skill;
            Map map;
            List<MapNode> paths = new List<MapNode>();
            int count = 0;
            bool speak = false;
            public Activator(Actor caster, SkillArg args, byte level,List<MapNode> path,bool speak)
            {
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 50;
                this.dueTime = 0;
                this.speak = speak;
                paths = path;
            }
            public override void CallBack()
            {
                //ClientManager.EnterCriticalArea();
                try
                {
                    if(speak && count == 1)
                    {
                        if (caster.type == ActorType.MOB)
                            SkillHandler.Instance.ActorSpeak(caster, "崩裂吧！大地！！");
                    }
                    if (count < paths.Count)
                    {
                        period += 5;
                        SkillHandler.Instance.ShowEffect(map, caster, paths[count].x, paths[count].y, 5075);
                        short x = SagaLib.Global.PosX8to16(paths[count].x, map.Width);
                        short y = SagaLib.Global.PosY8to16(paths[count].y, map.Height);
                        byte[] pos = new byte[2];
                        pos[0] = paths[count].x;
                        pos[1] = paths[count].y;

                        Activator2 timer = new Activator2(caster, skill, pos);
                        timer.Activate();
                        count++;
                    }
                    else
                    {
                        this.Deactivate();
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    this.Deactivate();
                }
                //解开同步锁
                //ClientManager.LeaveCriticalArea();
            }
        }
        #endregion
        private class Activator2 : MultiRunTask
        {
            Actor caster;
            SkillArg skill;
            Map map;
            byte[] pos = new byte[2];
            public Activator2(Actor caster, SkillArg args,byte[] pos)
            {
                this.caster = caster;
                this.skill = args.Clone();
                this.skill.dActor = 0xffffffff;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 0;
                this.dueTime = 3000;
                this.pos = pos;
 
            }
            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                //ClientManager.EnterCriticalArea();
                try
                {
                    Deactivate();
                    SkillArg s = this.skill.Clone();
                    s.x = pos[0];
                    s.y = pos[1];
                    EffectArg arg = new EffectArg();
                    arg.effectID = 5041;
                    arg.actorID = 0xFFFFFFFF;
                    arg.x = pos[0];
                    arg.y = pos[1];
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, caster, true);
                    short x = SagaLib.Global.PosX8to16(pos[0], map.Width);
                    short y = SagaLib.Global.PosY8to16(pos[1], map.Height);
                    List<Actor> actors = map.GetRoundAreaActors(x, y, 200);
                    List<Actor> affected = new List<Actor>();
                    s.affectedActors.Clear();
                    foreach (Actor j in actors)
                    {
                        if (SkillHandler.Instance.CheckValidAttackTarget(caster, j))
                        {
                            int damage = SkillHandler.Instance.CalcDamage(true, caster, j, skill, SkillHandler.DefType.MDef, SagaLib.Elements.Earth, 1, 3f);
                            SkillHandler.Instance.CauseDamage(caster, j, damage);
                            SkillHandler.Instance.ShowVessel(j, damage);
                            SkillHandler.Instance.ShowEffect(SagaMap.Manager.MapManager.Instance.GetMap(map.ID), j, 4289);
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    this.Deactivate();
                }
                //解开同步锁
                //ClientManager.LeaveCriticalArea();
            }
        }
    }
}

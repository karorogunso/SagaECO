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
    public class S30026 : ISkill
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

            List<Actor> da = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(sActor, 1000);
            List<MapNode> path;
            Activator timer;
            foreach (var item in da)
            {
                path = ai.FindPath(SagaLib.Global.PosX16to8(sActor.X, map.Width), SagaLib.Global.PosY16to8(sActor.Y, map.Height),
    SagaLib.Global.PosX16to8(item.X, map.Width), SagaLib.Global.PosY16to8(item.Y, map.Height));
                timer = new Activator(sActor, args, level, path, true);
                timer.Activate();
            }
            int count = 35;
            if (map.OriID == 20130000)
                count = 20;

            List<short[]> posList = new List<short[]>();

            for (int i = 0; i < count; i++)
            {
                short[] pos = new short[2];
                pos = map.GetRandomPosAroundPos(sActor.X, sActor.Y, 3500);
                posList.Add(pos);
                while(posList.Contains(pos))
                    pos = map.GetRandomPosAroundPos(sActor.X, sActor.Y, 3500);
                path = ai.FindPath(SagaLib.Global.PosX16to8(sActor.X, map.Width), SagaLib.Global.PosY16to8(sActor.Y, map.Height),
                SagaLib.Global.PosX16to8(pos[0], map.Width), SagaLib.Global.PosY16to8(pos[1], map.Height));
                timer = new Activator(sActor, args, level, path, false);
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
            public Activator(Actor caster, SkillArg args, byte level, List<MapNode> path, bool speak)
            {
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 100;
                this.dueTime = 0;
                this.speak = speak;
                paths = path;
            }
            public override void CallBack()
            {
                try
                {
                    if (speak && count == 1)
                    {
                        if (caster.type == ActorType.MOB)
                            SkillHandler.Instance.ActorSpeak(caster, "变成冰棍..然后沐浴在暗雷之下吧！");
                    }
                    if (count < paths.Count)
                    {
                        SkillHandler.Instance.ShowEffect(map, caster, paths[count].x, paths[count].y, 5050);
                        short x = SagaLib.Global.PosX8to16(paths[count].x, map.Width);
                        short y = SagaLib.Global.PosY8to16(paths[count].y, map.Height);

                        frozen f = new frozen(caster, x, y, map);
                        f.Activate();
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
            }
        }
        class frozen : MultiRunTask
        {
            short x;
            short y;
            Map map;
            Actor Caster;
            public frozen(Actor sActor,short x,short y,Map map)
            {
                this.x = x;
                this.y = y;
                dueTime = 2500;
                Caster = sActor;
                this.map = map;
            }

            public override void CallBack()
            {
                Deactivate();
                
                List<Actor> actors = map.GetActorsArea(x, y, 100, false);
                List<Actor> affected = new List<Actor>();
                SkillHandler.Instance.ShowEffect(map, Caster, SagaLib.Global.PosX16to8(x, map.Width), SagaLib.Global.PosY16to8(y, map.Height), 5284);
                foreach (var item in actors)
                {
                    if (SkillHandler.Instance.CheckValidAttackTarget(Caster, item))
                    {
                        if (!item.Status.Additions.ContainsKey("Frosen"))
                        {
                            OtherAddition a = new OtherAddition(null, item, "寒冰之夜伤害", 1000);
                            SkillHandler.ApplyAddition(item, a);
                            int damage = SkillHandler.Instance.CalcDamage(true, Caster, item, null, SkillHandler.DefType.IgnoreAll, Elements.Dark, 1, 5f);
                            SkillHandler.Instance.CauseDamage(Caster, item, damage);
                            SkillHandler.Instance.ShowVessel(item, damage);
                            SkillHandler.Instance.ShowEffect(Manager.MapManager.Instance.GetMap(map.ID), item, 5003);
                            if (!item.Status.Additions.ContainsKey("免疫寒冰之夜"))
                            {
                                Freeze f = new Freeze(null, item, 5000);
                                SkillHandler.ApplyAddition(item, f);
                            }
                            /*SkillHandler.Instance.CauseDamage(Caster, item, 233);
                            SkillHandler.Instance.ShowVessel(item, 233);*/
                        }
                    }
                }
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
                this.dueTime = 4000;
                this.pos = pos;
 
            }
            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                ClientManager.EnterCriticalArea();
                try
                {
                    SkillArg s = this.skill.Clone();
                    s.x = pos[0];
                    s.y = pos[1];
                    /*EffectArg arg = new EffectArg();
                    arg.effectID = 5003;
                    arg.actorID = 0xFFFFFFFF;
                    arg.x = pos[0];
                    arg.y = pos[1];
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, caster, true);*/
                    short x = SagaLib.Global.PosX8to16(pos[0], map.Width);
                    short y = SagaLib.Global.PosY8to16(pos[1], map.Height);
                    List<Actor> actors = map.GetRoundAreaActors(x, y, 150);
                    List<Actor> affected = new List<Actor>();
                    s.affectedActors.Clear();
                    foreach (Actor j in actors)
                    {
                        if (SkillHandler.Instance.CheckValidAttackTarget(caster, j) && !j.Status.Additions.ContainsKey("寒冰之夜伤害"))
                        {

                        }
                    }
                    this.Deactivate();
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    this.Deactivate();
                }
                //解开同步锁
                ClientManager.LeaveCriticalArea();
            }
        }
    }
}

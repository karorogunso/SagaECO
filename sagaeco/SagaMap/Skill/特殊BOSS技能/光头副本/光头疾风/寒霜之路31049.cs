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
    public class S31049 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);

            List<Actor> targets = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(sActor, 3000);

            short[] pos;
            pos = map.GetRandomPosAroundPos(sActor.X, sActor.Y, 2000);
            Mob.MobAI ai = new MobAI(sActor, true);
            List<MapNode> path = ai.FindPath(SagaLib.Global.PosX16to8(pos[0], map.Width), SagaLib.Global.PosY16to8(pos[1], map.Height),
                SagaLib.Global.PosX16to8(dActor.X, map.Width), SagaLib.Global.PosY16to8(dActor.Y, map.Height));
            Activator timer = new Activator(sActor, args, level, path,true);
            timer.Activate();

            for (int i = 0; i < targets.Count; i++)
            {
                pos = new short[2];
                path= ai.FindPath(SagaLib.Global.PosX16to8(pos[0], map.Width), SagaLib.Global.PosY16to8(pos[1], map.Height),
                                SagaLib.Global.PosX16to8(targets[i].X, map.Width), SagaLib.Global.PosY16to8(targets[i].Y, map.Height));
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
                this.period = 240;
                this.dueTime = 0;
                this.speak = speak;
                paths = path;
            }
            public override void CallBack()
            {
                ClientManager.EnterCriticalArea();
                try
                {
                    if (count < paths.Count)
                    {
                        SkillHandler.Instance.ShowEffect(map, caster, paths[count].x, paths[count].y, 5284);
                        short x = SagaLib.Global.PosX8to16(paths[count].x, map.Width);
                        short y = SagaLib.Global.PosY8to16(paths[count].y, map.Height);
                        List<Actor> actors = map.GetActorsArea(x, y, 100, false);
                        List<Actor> affected = new List<Actor>();
                        foreach (var item in actors)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                            {
                                if (!item.Status.Additions.ContainsKey("Frozen"))
                                {
                                    Freeze f = new Freeze(skill.skill, item, 6000);
                                    SkillHandler.ApplyAddition(item, f);
                                }
                                SkillHandler.Instance.DoDamage(false, caster, item, skill, SkillHandler.DefType.MDef, Elements.Water, 50, 8f);
                            }
                            byte[] pos = new byte[2];
                            pos[0] = paths[count].x;
                            pos[1] = paths[count].y;
                        }
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
                ClientManager.LeaveCriticalArea();
            }
        }
        #endregion
    }
}

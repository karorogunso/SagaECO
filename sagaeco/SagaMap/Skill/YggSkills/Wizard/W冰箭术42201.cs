using System;
using System.Collections.Generic;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Mob;


namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 冰箭术：单体水属性魔法多段攻击，附带颤栗
    /// </summary>
    public class S42201 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("冰河术CD")) return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            MobAI ai = new MobAI(sActor, true);
            List<MapNode> path = ai.FindPath(SagaLib.Global.PosX16to8(sActor.X, map.Width), SagaLib.Global.PosY16to8(sActor.Y, map.Height),
    SagaLib.Global.PosX16to8(dActor.X, map.Width), SagaLib.Global.PosY16to8(dActor.Y, map.Height));
            Activator timer = new Activator(sActor, args, level, path);
            timer.Activate();
        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            SkillArg skill;
            Map map;
            List<MapNode> paths = new List<MapNode>();
            int count = 0;
            float factor = 4f;
            public Activator(Actor caster, SkillArg args, byte level, List<MapNode> path)
            {
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 90;
                this.dueTime = 0;
                paths = path;

            }
            public override void CallBack()
            {
                //ClientManager.EnterCriticalArea();
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
                                if (!item.Status.Additions.ContainsKey("连续冰河术伤害"))
                                {
                                    OtherAddition c = new OtherAddition(null, item, "连续冰河术伤害", 1000);
                                    SkillHandler.ApplyBuffAutoRenew(item, c);
                                    SkillHandler.Instance.DoDamage(false, caster, item, skill, SkillHandler.DefType.MDef, Elements.Water, 50, factor);

                                    if (!item.Status.Additions.ContainsKey("冰河术冰冻CD"))
                                    {
                                        Freeze f = new Freeze(skill.skill, item, 4000);
                                        SkillHandler.ApplyAddition(item, f);
                                        SkillHandler.Instance.ShowEffectByActor(item, 5284);
                                        OtherAddition cd = new OtherAddition(null, item, "冰河术冰冻CD", 60000);
                                        SkillHandler.ApplyAddition(item, cd);
                                    }
                                }
                            }
                        }
                        byte[] pos = new byte[2];
                        pos[0] = paths[count].x;
                        pos[1] = paths[count].y;

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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;

namespace SagaMap.Skill.SkillDefinations
{
    public class S30021 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //创建设置型技能技能体
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            Activator timer = new Activator(sActor, args, level);
            timer.Activate();

        }

        #endregion

        #region Timer

        private class Activator : MultiRunTask
        {
            Actor caster;
            SkillArg skill;
            Map map;
            List<short[]> paths = new List<short[]>();
            int countMax = 50, count = 0;
            public Activator(Actor caster, SkillArg args, byte level)
            {
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 50;
                this.dueTime = 0;

            }
            public override void CallBack()
            {
                ClientManager.EnterCriticalArea();
                try
                {
                    if (count == 10)
                    {
                        if (caster.type == ActorType.MOB)
                            SkillHandler.Instance.ActorSpeak(caster, "来尝尝本小姐的暗雷风暴——");
                    }
                    if (count < countMax)
                    {
                        short[] pos;
                        //获取范围内随机坐标
                        pos = map.GetRandomPosAroundPos(caster.X, caster.Y, 1800);
                        paths.Add(pos);
                        SkillHandler.Instance.ShowEffect(map, caster, SagaLib.Global.PosX16to8(pos[0], map.Width), SagaLib.Global.PosY16to8(pos[1], map.Height), 5081);
                        count++;
                    }
                    else
                    {
                        ActorSkill actor2 = new ActorSkill(skill.skill, caster);
                        Map map = Manager.MapManager.Instance.GetMap(caster.MapID);
                        Activator2 timer = new Activator2(caster,  skill, paths);
                        timer.Activate();
                        this.Deactivate();
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                //解开同步锁
                ClientManager.LeaveCriticalArea();
            }
        }
        #endregion
        private class Activator2 : MultiRunTask
        {
            Actor caster;
            SkillArg skill;
            Map map;
            List<short[]> paths;
            public Activator2(Actor caster, SkillArg args, List<short[]> paths)
            {
                this.caster = caster;
                this.skill = args.Clone();
                this.skill.dActor = 0xffffffff;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 0;
                this.dueTime = 2000;
                this.paths = paths;
            }
            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                ClientManager.EnterCriticalArea();
                try
                {
                    for (int i = 0; i < paths.Count; i++)
                    {
                        byte x = SagaLib.Global.PosX16to8(paths[i][0], map.Width);
                        byte y = SagaLib.Global.PosY16to8(paths[i][1], map.Height);
                        SkillArg s = this.skill.Clone();
                        s.x = x;
                        s.y = y;
                        EffectArg arg = new EffectArg();
                        arg.effectID = 5103;
                        arg.actorID = 0xFFFFFFFF;
                        arg.x = s.x;
                        arg.y = s.y;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, caster, true);
                        List<Actor> actors = map.GetRoundAreaActors(paths[i][0], paths[i][1], 250);
                        List<Actor> affected = new List<Actor>();
                        s.affectedActors.Clear();
                        foreach (Actor j in actors)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, j))
                            {
                                int damage = SkillHandler.Instance.CalcDamage(true, caster, j, skill, SkillHandler.DefType.MDef, Elements.Dark, 1, 10f);
                                SkillHandler.Instance.CauseDamage(caster, j, damage);
                                SkillHandler.Instance.ShowVessel(j, damage);
                                SkillHandler.Instance.ShowEffect(SagaMap.Manager.MapManager.Instance.GetMap(map.ID), j, 5282);

                            }
                        }
                    }
                    this.Deactivate();
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                //解开同步锁
                ClientManager.LeaveCriticalArea();
            }
        }
    }
}

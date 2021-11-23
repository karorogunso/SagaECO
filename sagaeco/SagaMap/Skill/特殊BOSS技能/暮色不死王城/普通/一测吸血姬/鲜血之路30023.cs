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
    public class S30023 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            Activator timer = new Activator(sActor, args);
            timer.Activate();

        }

        #endregion

        #region Timer

        private class Activator : MultiRunTask
        {
            Actor caster;
            SkillArg skill;
            Map map;
            int countMax =10, count = 0;
            public Activator(Actor caster, SkillArg args)
            {
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 100;
                this.dueTime = 0;
            }
            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                ClientManager.EnterCriticalArea();
                try
                {
                    if (count < countMax)
                    {
                        short[] pos;
                        //获取范围内随机坐标
                        pos = map.GetRandomPosAroundPos(caster.X, caster.Y, 2500);
                        SkillHandler.Instance.ShowEffect(map, caster, SagaLib.Global.PosX16to8(pos[0], map.Width), SagaLib.Global.PosY16to8(pos[1], map.Height), 5202);
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
                ClientManager.LeaveCriticalArea();
            }
        }
        #endregion
        private class Activator2 : MultiRunTask
        {

            Actor caster;
            SkillArg skill;
            Map map;
            List<MapNode> path = new List<MapNode>();
            int count = 0;
            public Activator2(Actor caster, SkillArg args, short[] pos)
            {
                this.caster = caster;
                this.skill = args.Clone();
                this.skill.dActor = 0xffffffff;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 80;
                this.dueTime = 2000;

                Mob.MobAI ai = new MobAI(caster, true);
                this.path = ai.FindPath(SagaLib.Global.PosX16to8(caster.X, map.Width), SagaLib.Global.PosY16to8(caster.Y, map.Height),
                    SagaLib.Global.PosX16to8(pos[0], map.Width), SagaLib.Global.PosY16to8(pos[1], map.Height));
                int deltaX = this.path[0].x;
                int deltaY = this.path[0].y;
                MapNode node = new MapNode();
                node.x = (byte)deltaX;
                node.y = (byte)deltaY;
                this.path.Add(node);
            }
            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                ClientManager.EnterCriticalArea();
                try
                {
                    if (count < path.Count)
                    {
                        int num = 0;
                        if (count < 6) num = 2;
                        else num = 4;
                        if (count % num == 0)
                        {
                            skill.x = path[count].x;
                            skill.y = path[count].y;
                            short range = 300;
                            uint eid = 5368;
                            if (count >= 6)
                            {
                                eid = 5319;
                                range = 500;
                            }
                            EffectArg arg = new EffectArg();
                            arg.effectID = eid;
                            arg.actorID = 0xFFFFFFFF;
                            arg.x = skill.x;
                            arg.y = skill.y;
                            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, caster, true);
                            List<Actor> actors = map.GetRoundAreaActors(SagaLib.Global.PosX8to16(skill.x, map.Width), SagaLib.Global.PosY8to16(skill.y, map.Height), range);
                            List<Actor> affected = new List<Actor>();
                            skill.affectedActors.Clear();
                            foreach (Actor i in actors)
                            {
                                if (SkillHandler.Instance.CheckValidAttackTarget(caster, i))
                                {
                                    int damage = SkillHandler.Instance.CalcDamage(true, caster, i, skill, SkillHandler.DefType.MDef, SagaLib.Elements.Dark, 0, 8f);
                                    SkillHandler.Instance.CauseDamage(caster, i, damage);
                                    SkillHandler.Instance.ShowVessel(i, damage);
                                    SkillHandler.Instance.ShowEffect(SagaMap.Manager.MapManager.Instance.GetMap(i.MapID), i, 5282);
                                }
                            }
                        } count++;
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
    }
}

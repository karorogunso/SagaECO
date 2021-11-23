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
    public class S31158 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);

            for (int i = 0; i < 12; i++)
            {
                ActorSkill actor = new ActorSkill(args.skill, sActor);
                actor.Name = "海之狂乱曲";
                actor.MapID = sActor.MapID;
                short[] pos = map.GetRandomPosAroundPos(sActor.X, sActor.Y, 2000);
                actor.X = pos[0];
                actor.Y = pos[1];
                actor.Speed = 600;
                actor.e = new ActorEventHandlers.NullEventHandler();
                map.RegisterActor(actor);
                actor.invisble = false;
                map.OnActorVisibilityChange(actor);
                actor.Stackable = false;
                Activator timer = new Activator(sActor, actor);
                timer.Activate();
            }
        }

        private class Activator : MultiRunTask
        {
            Map map;
            Actor caster;
            ActorSkill skill;
            int count = 0;
            int maxcount = 1000;
            int lockcount = 0;
            short[] pos = new short[2];
            public Activator(Actor sactor, ActorSkill skill)
            {
                caster = sactor;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                dueTime = 2000;
                period = 50;
                this.skill = skill;
                pos[0] = skill.X;
                pos[1] = skill.Y;
            }
            public override void CallBack()
            {
                try
                {
                    count++;
                    if ((caster.HP != caster.MaxHP || caster.type == ActorType.PC) && caster.HP > 0 && count < maxcount && skill.MapID == caster.MapID)
                    {
                        if (count % 3 == 0)
                        {
                            List<Actor> actors = map.GetActorsArea(skill, 150, false);
                            List<Actor> affected = new List<Actor>();
                            foreach (var item in actors)
                            {
                                if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                                {
                                    if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                                    {
                                        int damage = SkillHandler.Instance.CalcDamage(true, caster, item, null, SkillHandler.DefType.IgnoreAll, Elements.Dark, 1, 1f);
                                        SkillHandler.Instance.CauseDamage(caster, item, damage);
                                        SkillHandler.Instance.ShowVessel(item, damage);
                                        SkillHandler.Instance.ShowEffect(Manager.MapManager.Instance.GetMap(map.ID), item, 4321);
                                    }
                                }
                            }
                        }
                        if(count % 7 == 0)
                        {
                            lockcount--;
                            MobAI ai = new MobAI(skill, true);
                            List<MapNode> path = ai.FindPath(SagaLib.Global.PosX16to8(skill.X, map.Width), SagaLib.Global.PosY16to8(skill.Y, map.Height),
    SagaLib.Global.PosX16to8(pos[0], map.Width), SagaLib.Global.PosY16to8(pos[1], map.Height));
                            if (path.Count <= 2)
                            {
                                pos = map.GetRandomPosAroundPos(caster.X, caster.Y, 3000);
                                lockcount = 0;
                            }
                            if (lockcount <= -20)
                            {

                                short[] pos2 = new short[2];
                                pos2[0] = SagaLib.Global.PosX8to16(path[0].x, map.Width);
                                pos2[1] = SagaLib.Global.PosY8to16(path[0].y, map.Height);
                                map.MoveActor(Map.MOVE_TYPE.START, skill, pos2, 0, 200);
                            }
                        }
                    }
                    else
                    {
                        map.DeleteActor(skill);
                        Deactivate();
                    }
                }
                catch (Exception ex)
                {
                    map.DeleteActor(skill);
                    Deactivate();
                    Logger.ShowError(ex);
                }
            }
        }
    }
}

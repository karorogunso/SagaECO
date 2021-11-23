using System;
using System.Collections.Generic;
using System.Text;
using SagaMap.Mob;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31161 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            actor.MapID = sActor.MapID;
            actor.Name = "月光奏鸣曲";
            actor.X = sActor.X;
            actor.Y = sActor.Y;
            actor.Speed = 800;
            actor.e = new ActorEventHandlers.NullEventHandler();
            map.RegisterActor(actor);
            actor.invisble = false;
            map.OnActorVisibilityChange(actor);
            actor.Stackable = false;
            Activator timer = new Activator(sActor, dActor, actor, args);
            timer.Activate();
        }
        private class Activator : MultiRunTask
        {
            ActorSkill actor;
            Actor caster;
            Actor dactor;
            SkillArg skill;
            Map map;
            int countMax = 100, count = 0;
            short[] pos;
            public Activator(Actor caster, Actor dActor, ActorSkill skillActor, SkillArg args)
            {
                this.actor = skillActor;
                this.caster = caster;
                this.skill = args.Clone();
                this.skill.dActor = 0xffffffff;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 200;
                this.dueTime = 0;
                this.dactor = dActor;
            }
            public override void CallBack()
            {
                try
                {
                    if (count == countMax || caster.Buff.Stun || caster.HP == caster.MaxHP)//临界消除
                    {
                        Deactivate();
                        map.DeleteActor(actor);
                        count = countMax;
                    }
                    else
                    {
                        count++;

                        MobAI ai = new MobAI(actor, true);
                        List<MapNode> path = ai.FindPath(SagaLib.Global.PosX16to8(actor.X, map.Width), SagaLib.Global.PosY16to8(actor.Y, map.Height),
                            SagaLib.Global.PosX16to8(dactor.X, map.Width), SagaLib.Global.PosY16to8(dactor.Y, map.Height));
                        short[] pos = new short[2];
                        pos[0] = SagaLib.Global.PosX8to16(path[0].x, map.Width);
                        pos[1] = SagaLib.Global.PosY8to16(path[0].y, map.Height);
                        map.MoveActor(Map.MOVE_TYPE.START, actor, pos, 0, 800);

                        List<Actor> actors = map.GetActorsArea(actor, 400, false);
                        foreach (var item in actors)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                            {
                                item.TInt["月光奏鸣曲效果"]++;
                                //连续吃5次，进入睡眠状态
                                SkillHandler.Instance.DoDamage(false, caster, item, null, SkillHandler.DefType.MDef, Elements.Dark, 50, 0.5f);
                                SkillHandler.Instance.ShowEffectOnActor(item, 5275);
                                if (item.TInt["月光奏鸣曲效果"] >= 20)
                                {
                                    if (!item.Status.Additions.ContainsKey("Sleep"))
                                    {
                                        Sleep skill = new Sleep(null, item, 10000);
                                        SkillHandler.ApplyAddition(item, skill);
                                    }
                                }
                                //连续吃10次，被睡杀
                                if (item.TInt["月光奏鸣曲效果"] >= 40)
                                {
                                    SkillHandler.Instance.DoDamage(false, caster, item, null, SkillHandler.DefType.IgnoreAll, Elements.Dark, 50, 50f);
                                    SkillHandler.Instance.ShowEffectOnActor(item, 5154);
                                    SkillHandler.Instance.TitleProccess(item, 81, 1, true);
                                }
                                //连续效果记录器
                                if (!item.Status.Additions.ContainsKey("月光奏鸣曲"))
                                {
                                    OtherAddition skill = new OtherAddition(null, item, "月光奏鸣曲", 25000);
                                    skill.OnAdditionEnd += (s, e) =>
                                    {
                                        item.TInt["月光奏鸣曲效果"] = 0;
                                    };
                                    SkillHandler.ApplyAddition(item, skill);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    Deactivate();
                    map.DeleteActor(actor);
                }
            }
        }
    }
}

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
    public class  S30028 : ISkill
    {

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<short[]> paths = new List<short[]>();
            SkillHandler.Instance.ActorSpeak(sActor, "不要打架不要打架，朋朋这次做美味的冰棍给大家吃。");
            for (int i = 0; i < 10; i++)
            {
                short[] pos;
                pos = map.GetRandomPosAroundPos(sActor.X, sActor.Y, 1500);
                map.SendEffect(sActor, SagaLib.Global.PosX16to8(pos[0], map.Width), SagaLib.Global.PosY16to8(pos[1], map.Height), 5327);

                Activator timer = new Activator(sActor, args, pos);
                timer.Activate();
            }
        }

        private class Activator : MultiRunTask
        {
            Actor caster;
            SkillArg skill;
            Map map;
            short[] pos;
            byte x, y;

            public Activator(Actor caster, SkillArg args, short[] pos)
            {
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.pos = pos;
                this.period = 50;
                this.dueTime = 3000;
                x = SagaLib.Global.PosX16to8(pos[0], map.Width);
                y = SagaLib.Global.PosY16to8(pos[1], map.Height);

            }
            public override void CallBack()
            {
                ClientManager.EnterCriticalArea();
                try
                {
                    List<Actor> actors = map.GetActorsArea(pos[0], pos[1], 300);
                    map.SendEffect(caster, x, y, 5614);
                    foreach (var item in actors)
                    {
                        if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                        {
                            if (item.type == ActorType.PC)
                                Network.Client.MapClient.FromActorPC((ActorPC)item).SendSystemMessage("你被从地下冒出来的寒气打中了，冰棍层数被直接叠满！");
                            SkillHandler.Instance.PushBack(caster, item, 5);
                            SkillHandler.Instance.DoDamage(false, caster, item, skill, SkillHandler.DefType.MDef, Elements.Water, 50, 6f);
                            冰棍的冻结 s = new 冰棍的冻结(skill.skill, item, 30000, 15);
                            SkillHandler.ApplyAddition(item, s);
                        }
                    }
                    ActorSkill actor = new ActorSkill(skill.skill, caster);
                    actor.MapID = caster.MapID;
                    actor.X = pos[0];
                    actor.Y = pos[1];
                    actor.e = new ActorEventHandlers.NullEventHandler();
                    map.RegisterActor(actor);
                    actor.invisble = false;
                    map.OnActorVisibilityChange(actor);
                    actor.Stackable = false;

                    Activator2 timer = new Activator2(caster, actor, skill);
                    timer.Activate();
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
        private class Activator2 : MultiRunTask
        {
            ActorSkill actor;
            Actor caster;
            SkillArg skill;
            Map map;
            int countMax = 12, count = 0;

            public Activator2(Actor caster, ActorSkill actor, SkillArg args)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 1000;
                this.dueTime = 3000;
            }

            public override void CallBack()
            {
                try
                {
                    if (count < countMax)
                    {
                        List<Actor> actors = map.GetActorsArea(actor, 300, true);
                        List<Actor> affected = new List<Actor>();
                        skill.affectedActors.Clear();
                        foreach (Actor i in actors)
                        {
                            if (i.Status.Additions.ContainsKey("冰棍"))
                            {
                                Addition addition = i.Status.Additions["冰棍"];
                                i.Status.Additions.Remove("冰棍");
                                if (addition.Activated)
                                {
                                    addition.AdditionEnd();
                                }
                                addition.Activated = false;
                                string s = "好像变暖和一些了——[冰棍层数0/3]";
                                if (!i.Status.Additions.ContainsKey("AtkUp") || !i.Status.Additions.ContainsKey("MAtkUp"))
                                {
                                    AtkUp au = new AtkUp(null, i, 10000, 500);
                                    SkillHandler.ApplyAddition(i, au);
                                    MAtkUp ak = new MAtkUp(null, i, 10000, 500);
                                    SkillHandler.ApplyAddition(i, ak);
                                    s += "，攻击力得到了上升！";
                                }
                                if (i.type == ActorType.PC)
                                    Network.Client.MapClient.FromActorPC((ActorPC)i).SendSystemMessage(s);
                            }
                            if (i.Status.Additions.ContainsKey("冰棍的冻结"))
                            {
                                Addition addition = i.Status.Additions["冰棍的冻结"];
                                i.Status.Additions.Remove("冰棍的冻结");
                                if (addition.Activated)
                                {
                                    addition.AdditionEnd();
                                }
                                addition.Activated = false;
                                string s = "好像变暖和一些了——[冰棍层数0/3]";
                                if (!i.Status.Additions.ContainsKey("AtkUp") || !i.Status.Additions.ContainsKey("MAtkUp"))
                                {
                                    AtkUp au = new AtkUp(null, i, 10000, 500);
                                    SkillHandler.ApplyAddition(i, au);
                                    MAtkUp ak = new MAtkUp(null, i, 10000, 500);
                                    SkillHandler.ApplyAddition(i, ak);
                                    s += "，攻击力得到了上升！";
                                }
                                if (i.type == ActorType.PC)
                                    Network.Client.MapClient.FromActorPC((ActorPC)i).SendSystemMessage(s);
                            }
                        }
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, actor, false);
                    }
                    else
                    {
                        this.Deactivate();
                        map.DeleteActor(actor);
                    }
                    count++;
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
        }
    }
}

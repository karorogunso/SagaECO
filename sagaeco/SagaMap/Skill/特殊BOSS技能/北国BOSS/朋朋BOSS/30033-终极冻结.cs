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
    public class S30033 : ISkill
    {

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(sActor, 3000);
            Actor a = actors[SagaLib.Global.Random.Next(0, actors.Count - 1)];

            硬直 s = new 硬直(args.skill, sActor, 8000);
            SkillHandler.ApplyAddition(sActor, s);

            Activator skill = new Activator(sActor, a);
            skill.Activate();
        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            Actor dactor;
            Map map;
            public Activator(Actor caster, Actor dActor)
            {
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.dueTime = 10000;
                this.caster = caster;
                this.dactor = dActor;
                SkillHandler.Instance.ActorSpeak(caster, "「" + dActor.Name + "」，你穿得这么少，当心被冻伤哦？");
                map.Announce("「" + dActor.Name + "」要被朋朋抽取冷气了，快来2个以上的人在他周围7x7阻挡冷气，防止冷气蔓延！！");
                SkillHandler.Instance.ShowEffectOnActor(dactor, 4469);
                SkillHandler.Instance.ShowEffectOnActor(caster, 5174);
            }
            public override void CallBack()
            {
                try
                {
                    List<Actor> actors = map.GetActorsArea(dactor, 300, true);
                    List<Actor> targets = new List<Actor>();
                    foreach (var item in actors)
                    {
                        if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                            targets.Add(item);
                    }
                    if (targets.Count < 3)
                    {
                        List<Actor> all = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(caster, 5000);
                        foreach (var item in all)
                        {
                            冰棍的冻结 buff = new 冰棍的冻结(null, item, 30000, 75);
                            SkillHandler.ApplyAddition(item, buff);
                            SkillHandler.Instance.ShowEffectOnActor(item, 5237);
                            if (item.type == ActorType.PC)
                                Network.Client.MapClient.FromActorPC((ActorPC)item).SendSystemMessage("寒气未被成功阻挡，你被爆发的寒气集中，进入了「冰封」状态。");
                        }
                        map.Announce("没有足够的人阻挡被朋朋从「" + dactor.Name + "」身上抽取出来的寒气，寒气爆发了。");
                        SkillHandler.Instance.ActorSpeak(caster, "为什么大家不能在一起友好相处呢？朋朋好伤心啊。");
                    }
                    else
                    {
                        string s = "";
                        foreach (var item in targets)
                        {
                            s += "「" + item.Name + "」 ";
                            if (item.type == ActorType.PC)
                                Network.Client.MapClient.FromActorPC((ActorPC)item).SendSystemMessage("你们成功阻挡了寒气爆发，但也进入了短暂的冻结时间(3秒)。");
                            冰棍的冻结 buff = new 冰棍的冻结(null, item, 3000);
                            SkillHandler.ApplyAddition(item, buff);
                            SkillHandler.Instance.ShowEffectOnActor(item, 5198);
                        }
                        map.Announce(s + "成功阻挡了寒气，但他们也进入了短暂的冻结时间。");
                        SkillHandler.Instance.ActorSpeak(caster, "就算现在展示你们的团结，也已经晚了，朋朋生气了，朋朋要把你们做成冰棍！");
                    }
                    this.Deactivate();
                }
                catch(Exception ex)
                {
                    Logger.ShowError(ex);
                    this.Deactivate();
                }

            }
        }
    }
}

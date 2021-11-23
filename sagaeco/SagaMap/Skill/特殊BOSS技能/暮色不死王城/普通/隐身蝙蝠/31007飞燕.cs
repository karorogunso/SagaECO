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
    public class S31007 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            DefaultBuff skill = new DefaultBuff(args.skill, sActor, "Invisible", 6000);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            SkillHandler.Instance.ShowEffect(map, sActor, 5131);
            SkillHandler.Instance.ShowEffect(map, sActor, 7957);
            Activator timer = new Activator(sActor, args);
            timer.Activate();
            硬直 y = new 硬直(args.skill, sActor, 6000);
            SkillHandler.ApplyAddition(sActor, y);
        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            SkillArg skill;
            Map map;
            List<Actor> dactors = new List<Actor>();
            int countMax = 60, count = 0;
            public Activator(Actor caster, SkillArg args)
            {
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 100;
                this.dueTime = 1000;
            }
            public override void CallBack()
            {
                try
                {
                    count++;
                    if (count == 1)
                    {
                        if (caster.type == ActorType.MOB)
                            SkillHandler.Instance.ActorSpeak(caster, "必杀技！妞噶哇嘎特ki喔哭勒！！");
                    }
                    if (count < countMax)
                    {
                        List<Actor> targets = map.GetActorsArea(caster, 900, true);
                        dactors = new List<Actor>();
                        foreach (var item in targets)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                            {
                                dactors.Add(item);
                            }
                        }
                        if (dactors.Count < 1) return;
                        Actor i = dactors[SagaLib.Global.Random.Next(0, dactors.Count - 1)];
                        if (i == null) return;
                        //map.TeleportActor(caster,i.X, i.Y);
                        SkillHandler.Instance.ShowEffect(map, i, 5194);
                        int damaga = SkillHandler.Instance.CalcDamage(false, caster, i, skill, SkillHandler.DefType.IgnoreAll, Elements.Neutral, 50, 1f);
                        SkillHandler.Instance.CauseDamage(caster, i, damaga);
                        SkillHandler.Instance.ShowVessel(i, damaga);
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
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.Transparent = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.Transparent = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }

}

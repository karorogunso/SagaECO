using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;


namespace SagaMap.Skill.SkillDefinations
{
    public class S31010 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Activator timer = new Activator(sActor, args);
            timer.Activate();
            硬直 y = new 硬直(args.skill, sActor, 2000);
            SkillHandler.ApplyAddition(sActor, y);
        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            SkillArg skill;
            Map map;
            List<Actor> dactors = new List<Actor>();
            int countMax = 3, count = 0;
            public Activator(Actor caster, SkillArg args)
            {
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 1000;
                this.dueTime = 100;

                List<Actor> targets = map.GetActorsArea(caster, 800, true);
                foreach (var item in targets)
                {
                    if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                    {
                        dactors.Add(item);
                    }
                }
            }
            public override void CallBack()
            {
                //ClientManager.EnterCriticalArea();
                try
                {
                    if (count < countMax)
                    {
                        if(count == 0)
                        {
                            if (caster.type == ActorType.MOB)
                                SkillHandler.Instance.ActorSpeak(caster, "在业火中焚烧吧————！");
                            List<Actor> targets = map.GetActorsArea(caster, 250, true);
                            SkillHandler.Instance.ShowEffectByActor(caster, 5101);
                            foreach (var i in targets)
                            {
                                if (SkillHandler.Instance.CheckValidAttackTarget(caster, i))
                                {
                                    int damaga = SkillHandler.Instance.CalcDamage(false, caster, i, skill, SkillHandler.DefType.MDef, Elements.Fire, 50, 3f,3f);
                                    SkillHandler.Instance.CauseDamage(caster, i, damaga);
                                    SkillHandler.Instance.ShowVessel(i, damaga);
                                }
                            }
                        }
                        if (count == 1)
                        {
                            List<Actor> targets = map.GetActorsArea(caster, 350, true);
                            SkillHandler.Instance.ShowEffectByActor(caster, 5304);
                            foreach (var i in targets)
                            {
                                if (SkillHandler.Instance.CheckValidAttackTarget(caster, i))
                                {
                                    int damaga = SkillHandler.Instance.CalcDamage(false, caster, i, skill, SkillHandler.DefType.MDef, Elements.Fire, 50, 5f, 3f);
                                    SkillHandler.Instance.CauseDamage(caster, i, damaga);
                                    SkillHandler.Instance.ShowVessel(i, damaga);
                                }
                            }
                        }
                        if (count == 2)
                        {
                            List<Actor> targets = map.GetActorsArea(caster, 700, true);
                            SkillHandler.Instance.ShowEffectByActor(caster, 5189);
                            foreach (var i in targets)
                            {
                                if (SkillHandler.Instance.CheckValidAttackTarget(caster, i))
                                {
                                    int damaga = SkillHandler.Instance.CalcDamage(false, caster, i, skill, SkillHandler.DefType.MDef, Elements.Dark, 50, 10f, 3f);
                                    SkillHandler.Instance.CauseDamage(caster, i, damaga);
                                    SkillHandler.Instance.ShowVessel(i, damaga);
                                }
                            }
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
                //ClientManager.LeaveCriticalArea();
            }
        }
    }
}

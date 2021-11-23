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
    public class S31045 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Activator timer = new Activator(sActor,dActor,args);
            timer.Activate();
            //SkillHandler.Instance.ShowEffectOnActor(sActor, 5266);
        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            Actor target;
            SkillArg arg;
            Map map;
            float rate;
            public Activator(Actor caster,Actor dActor,SkillArg args)
            {
                this.caster = caster;
                target = dActor;
                arg = args;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                dueTime = 1000;
            }
            public override void CallBack()
            {
                try
                {
                    SkillHandler.Instance.ActorSpeak(caster, "鱼人之力————！");
                    List<Actor> actors = map.GetActorsArea(target, 150, true);
                    SkillHandler.Instance.ShowEffectOnActor(target, 4362);
                    foreach (var item in actors)
                    {
                        if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                        {
                            SkillHandler.Instance.DoDamage(true, caster, item, arg, SkillHandler.DefType.Def, Elements.Water, 50, 5f);
                            Stone skill = new Stone(null, item, 3000);
                            SkillHandler.ApplyAddition(item, skill);
                            Activator2 timer = new Activator2(caster, item,arg);
                            timer.Activate();
                        }
                    }  
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    Deactivate();
                }
                Deactivate();
            }
            #endregion
        }
        private class Activator2 : MultiRunTask
        {
            Actor caster;
            Actor target;
            SkillArg arg;
            Map map;
            public Activator2(Actor caster, Actor dActor, SkillArg args)
            {
                this.caster = caster;
                target = dActor;
                arg = args;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                dueTime = 5000;
            }
            public override void CallBack()
            {
                try
                {
                    List<Actor> actors = map.GetActorsArea(target, 350, false);
                    List<Actor> targets = new List<Actor>();
                    foreach (var item in actors)
                        if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                            targets.Add(item);
                    if (targets.Count < 1)
                    {
                        Deactivate();
                        return;
                    }
                    targets.Add(target);
                    foreach (var item in targets)
                    {
                        SkillHandler.Instance.CauseDamage(caster, item, (int)item.MaxHP);
                        SkillHandler.Instance.ShowVessel(item, (int)item.MaxHP);
                        SkillHandler.Instance.ShowEffectOnActor(item, 5030);
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    Deactivate();
                }
                Deactivate();
            }
        }
    }
}

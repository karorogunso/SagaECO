using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 鬼式·死鎌演舞
    /// </summary>
    public class S43205 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //SkillHandler.Instance.ShowEffect(map, sActor, 4355);
            Activator timer = new Activator(sActor, args);
            timer.Activate();
        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            SkillArg skill;
            Map map;
            List<Actor> dactors = new List<Actor>();
            int countMax = 20, count = 0;
            public Activator(Actor caster, SkillArg args)
            {
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 50;
                this.dueTime = 1300;

                List<Actor> targets = map.GetActorsArea(caster, 300, true);
                foreach (var item in targets)
                {
                    if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                    {
                        dactors.Add(item);
                    }
                }
                countMax = dactors.Count;
            }
            public override void CallBack()
            {
                ClientManager.EnterCriticalArea();
                try
                {
                    if (count < countMax)
                    {
                        Actor d = dactors[count];
                        if (SkillHandler.Instance.CheckValidAttackTarget(caster, d))
                        {
                            if (!d.castaway)
                            {
                                d.castaway = true;
                                SkillHandler.Instance.ShowEffectOnActor(d, 5282, caster);
                            }
                            SkillHandler.Instance.DoDamage(false, caster, d, skill, SkillHandler.DefType.MDef, Elements.Dark, 50, 10f);
                            SkillHandler.Instance.ShowEffectOnActor(d, 5080, caster);
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
                ClientManager.LeaveCriticalArea();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;

namespace SagaMap.Skill.SkillDefinations
{
    public partial class S21117 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //计时器循序攻击
            Activator timer = new Activator(sActor, args, level);
            timer.Activate();
        }

        private class Activator : MultiRunTask
        {
            Actor caster;
            SkillArg skill;
            Map map;
            List<Actor> dactors = new List<Actor>();
            int countMax = 0, count = 0;
            float factor;

            public Activator(Actor caster, SkillArg args, byte level)
            {
                this.caster = caster;
                factor = 2f + 1f * level;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                period = 50;
                dueTime = 1300;

                dactors =  SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(caster, caster, 200);
                countMax = dactors.Count;
            }
            public override void CallBack()
            {
                try
                {
                    if (count < countMax)
                    {
                        period = SagaLib.Global.Random.Next(30, 60);
                        Actor d = dactors[count];
                        if (SkillHandler.Instance.CheckValidAttackTarget(caster, d))
                        {
                            float f = factor;
                            if (d.HP < d.MaxHP * 0.3)
                                f = factor * 2;
                            SkillHandler.Instance.DoDamage(false, caster, d, null, SkillHandler.DefType.MDef, Elements.Dark, 50, factor);
                            SkillHandler.Instance.ShowEffectOnActor(d, 5080);
                        }
                        count++;
                    }
                    else
                        Deactivate();
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    Deactivate();
                }
            }
        }
    }
}

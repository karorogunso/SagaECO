using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S18506 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (!SkillHandler.Instance.CheckSkillCanCastForWeapon(pc, args))
                return -5;
            if (pc.SP < 100)
                return -2;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            dActor = SkillHandler.Instance.GetdActor(sActor,args);
            if (dActor == null) return;

            Activator timer = new Activator(sActor, args,level);
            timer.Activate();
        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            Actor dActor;
            float factor;
            SkillArg skill;
            Map map;
            uint MaxCount = 1, count = 0;
            public Activator(Actor caster, SkillArg args,byte level)
            {
                this.caster = caster;
                skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                period = 100;
                dueTime = 100;
                MaxCount = (caster.SP / 100) + 1;
                factor = level * 0.5f + 1.5f;
                dActor = SkillHandler.Instance.GetdActor(caster,args);
            }
            public override void CallBack()
            {
                try
                {
                    if (count < MaxCount && dActor != null)
                    {
                        SkillHandler.AttackResult res = SkillHandler.AttackResult.Hit;
                        SkillHandler.Instance.ShowEffect(map, dActor, 5194);
                        int damaga = SkillHandler.Instance.CalcDamage(true, caster, dActor, skill, SkillHandler.DefType.Def, Elements.Neutral, 50, factor, out res);
                        SkillHandler.Instance.CauseDamage(caster, dActor, damaga);
                        SkillHandler.Instance.ShowVessel(dActor, damaga, 0, 0, res);
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
            }
        }
        #endregion
    }
}
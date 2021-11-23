using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S40103 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckSkillCanCastForWeapon(pc, args))
                return 0;
            return -5;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Activator timer = new Activator(sActor, dActor, args);
            timer.Activate();
        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            Actor dActor;
            SkillArg skill;
            List<Actor> dactors = new List<Actor>();
            Map map;
            int countMax = 2, count = 0;
            public Activator(Actor caster, Actor dActor, SkillArg args)
            {
                this.period = 250;
                this.dueTime = 500;
                this.caster = caster;
                this.dActor = dActor;
                this.skill = args;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
            }
            public override void CallBack()
            {
                ClientManager.EnterCriticalArea();
                try
                {
                    if (count < countMax)
                    {
                        SkillHandler.Instance.DoDamage(true, caster, dActor, skill, SkillHandler.DefType.Def, Elements.Neutral, 0, 2f);
                        SkillHandler.Instance.ShowEffect(map, dActor, 5194);
                        count++;
                    }
                    else
                    {
                        SkillHandler.Instance.ShowEffectByActor(dActor, 4267);
                        //双防下降
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
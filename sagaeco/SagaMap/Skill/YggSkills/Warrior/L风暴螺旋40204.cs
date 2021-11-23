using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S40204 : ISkill
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
            sActor.EP += 1000;
            if (sActor.EP >= sActor.MaxEP)
                sActor.EP = sActor.MaxEP;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            Actor dActor;
            SkillArg skill;
            List<Actor> dactors = new List<Actor>();
            Map map;
            int countMax = 8, count = 0;
            float factor = 3f;
            public Activator(Actor caster, Actor dActor, SkillArg args)
            {
                this.period = 250;
                this.dueTime = 100;
                this.caster = caster;
                this.dActor = dActor;
                this.skill = args;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                dactors = map.GetActorsArea(caster, 400, false);
            }
            public override void CallBack()
            {
                ClientManager.EnterCriticalArea();
                try
                {
                    if (count < countMax)
                    {
                        foreach (var item in dactors)
                        {
                            if(SkillHandler.Instance.CheckValidAttackTarget(caster,item))
                            {
                                SkillHandler.Instance.DoDamage(true, caster, item, skill, SkillHandler.DefType.Def, Elements.Neutral, 0, factor);
                                SkillHandler.Instance.ShowEffect(map, dActor, 5194);
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
                ClientManager.LeaveCriticalArea();
            }
        }

    }
}

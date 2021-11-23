using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaLib;

namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 白想剑：广域多段无属性魔法攻击
    /// </summary>
    public class S42507 : ISkill
    {

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            sActor.EP += 500;
            if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);

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
            int countMax = 3, count = 0;
            float factor = 3f;
            public Activator(Actor caster, Actor dActor, SkillArg args)
            {
                this.period = 150;
                this.dueTime = 1300;
                this.caster = caster;
                this.dActor = dActor;
                this.skill = args;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                dactors = map.GetActorsArea(dActor, 300, true);
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
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                            {
                                SkillHandler.Instance.DoDamage(false, caster, item, skill, SkillHandler.DefType.MDef, Elements.Neutral, 0, factor);
                                SkillHandler.Instance.ShowEffect(map, dActor, 5275);
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

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
    public class S31060 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            SkillHandler.Instance.ShowEffect(map, sActor, 5131);
            SkillHandler.Instance.ShowEffect(map, sActor, 7957);
            Activator timer = new Activator(sActor, args);
            timer.Activate();
            硬直 y = new 硬直(args.skill, sActor, 4000);
            SkillHandler.ApplyAddition(sActor, y);
        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            SkillArg skill;
            Map map;
            List<Actor> dactors = new List<Actor>();
            int countMax = 40, count = 0;
            public Activator(Actor caster, SkillArg args)
            {
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 80;
                this.dueTime = 1000;

                List<Actor> targets = map.GetActorsArea(caster, 1200, true);
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
                    if (count == 1)
                    {
                        if (caster.type == ActorType.MOB)
                            SkillHandler.Instance.ActorSpeak(caster, "背后的的机械臂其实是核能的哦。");
                    }
                    if (count < countMax)
                    {
                        Actor i = dactors[SagaLib.Global.Random.Next(0, dactors.Count - 1)];
                        if (i == null) return;
                        SkillHandler.Instance.ShowEffect(map, i, 5195);
                        int damaga = 1000;
                        SkillHandler.Instance.CauseDamage(caster, i, damaga);
                        SkillHandler.Instance.ShowVessel(i, damaga);

                        if (caster.type == ActorType.MOB)
                        {
                            ActorMob mob = (ActorMob)caster;
                            mob.TInt["零件数"] += 1;
                            SkillHandler.Instance.ShowVessel(mob, 0, -mob.TInt["零件数"], 0);
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

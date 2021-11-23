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
    /// 鬼式·黄泉之门
    /// </summary>
    public class S43207 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            SkillHandler.Instance.ShowEffect(map,sActor,args.x,args.y, 4385);
            Activator timer = new Activator(sActor, dActor, args);
            timer.Activate();
        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            SkillArg skill;
            Map map;
            List<Actor> dactors = new List<Actor>();
            int countMax = 3, count = 0;
            float factor = 8f;
            public Activator(Actor caster, Actor dactor,SkillArg args)
            {
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 500;
                this.dueTime = 500;
                
                List<Actor> targets = map.GetActorsArea(dactor, 400,true);
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
                ClientManager.EnterCriticalArea();
                try
                {
                    if (count < countMax)
                    {
                        foreach (var item in dactors)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                            {
                                if (item.castaway)
                                {
                                    item.castaway = false;
                                    factor = 18f;
                                    SkillHandler.Instance.ShowEffectOnActor(item, 5202,caster);
                                }
                                SkillHandler.Instance.DoDamage(false, caster, item, skill, SkillHandler.DefType.MDef, Elements.Dark, 50, factor);
                                SkillHandler.Instance.ShowEffectOnActor(item, 5270, caster);
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
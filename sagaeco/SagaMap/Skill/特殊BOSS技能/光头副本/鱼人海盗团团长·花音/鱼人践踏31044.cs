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
    public class S31044 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Activator timer = new Activator(sActor,args);
            timer.Activate();
            //SkillHandler.Instance.ShowEffectOnActor(sActor, 5266);
        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            Map map;
            SkillArg arg;
            public Activator(Actor caster,SkillArg args)
            {
                this.arg = args;
                this.caster = caster;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                dueTime = 1000;
            }
            public override void CallBack()
            {
                try
                {
                    SkillHandler.Instance.ActorSpeak(caster, "呀喝————！");
                    List<Actor> actors = map.GetActorsArea(caster, 300, false);
                    //SkillHandler.Instance.ShowEffectOnActor(caster, 5399);
                    foreach (var item in actors)
                    {
                        if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                        {
                            SkillHandler.Instance.DoDamage(true, caster, item, arg, SkillHandler.DefType.Def, Elements.Neutral, 0, 5f);
                            Stun skill = new Stun(null, item, 3000);
                            SkillHandler.ApplyAddition(item, skill);
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
    }
}

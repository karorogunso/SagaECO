using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31056 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
             return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            sActor.Buff.三转植物寄生 = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, sActor, true);
            Activator timer = new Activator(sActor);
            timer.Activate();
        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            ActorMob mob;
            Map map;
            float rate;
            public Activator(Actor caster)
            {
                this.caster = caster;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                dueTime = 1000;
                mob = (ActorMob)caster;
            }
            public override void CallBack()
            {
                try
                {
                    SkillHandler.Instance.ShowEffectOnActor(caster, 4302);
                    if (mob.TInt["森罗万触-始"] == 1)
                    {
                        SkillHandler.Instance.ActorSpeak(caster, "你不是姐姐大人！！");
                        List<Actor> actors = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(caster, 2000);
                        foreach (var item in actors)
                        {
                            int damage = (int)(item.MaxHP * 0.8f);
                            item.EP = 0;
                            SkillHandler.Instance.CauseDamage(caster, item, damage);
                            SkillHandler.Instance.ShowVessel(item, damage);
                            SkillHandler.Instance.ShowEffectOnActor(item, 8034);
                        }
                        mob.TInt["森罗万触伤害累积"] = 0;
                        mob.TInt["森罗万触-始"] = 0;
                    }
                    else
                    {

                        SkillHandler.Instance.ActorSpeak(caster, "你不是姐姐大人！！");
                        List<Actor> actors = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(caster, 2000);
                        foreach (var item in actors)
                        {
                            int damage = (int)(item.MaxHP * 0.7f);
                            SkillHandler.Instance.CauseDamage(caster, item, damage);
                            SkillHandler.Instance.ShowVessel(item, damage);
                            SkillHandler.Instance.ShowEffectOnActor(item, 8034);

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
        }
    }
}

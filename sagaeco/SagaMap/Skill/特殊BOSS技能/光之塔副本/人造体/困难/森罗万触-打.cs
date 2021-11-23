using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31055 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Activator timer = new Activator(sActor, args);
            timer.Activate();

        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            Map map;
            SkillArg skill;
            float rate;
            int countMax = 7, count = 0;
            public Activator(Actor caster, SkillArg args)
            {
                this.caster = caster;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                period = 1000;
                dueTime = 500;
                this.skill = args;
            }
            public override void CallBack()
            {
                try
                {
                    if (count == 2)
                        SkillHandler.Instance.ActorSpeak(caster, "姐姐大人说坏孩子要打屁股。");
                    int damage = 1000;
                    if (count < countMax)
                    {
                        List<Actor> actors = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(caster, 2000);
                        foreach (var item in actors)
                        {
                            SkillHandler.Instance.CauseDamage(caster, item, damage);
                            SkillHandler.Instance.ShowVessel(item, damage);
                            item.SP -= item.SP / 3;
                            item.MP -= item.MP / 3;
                            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, item, true);
                            SkillHandler.Instance.ShowEffectOnActor(item, 8051);
                        }
                        count++;
                    }
                    else
                    {
                        Deactivate();
                    }
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

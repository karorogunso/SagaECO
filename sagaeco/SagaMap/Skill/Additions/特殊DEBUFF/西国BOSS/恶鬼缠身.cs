using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaLib;

namespace SagaMap.Skill.Additions.Global
{
    public class 恶鬼缠身 : MultiRunTask
    {
        Actor caster;
        Actor dActor;
        Map map;
        int damage;
        int count = 0, maxcount = 25;
        public 恶鬼缠身(Actor caster, Actor dactor,int damage = 550,int period = 900)
        {
            this.damage = damage;
            this.period = period;
            this.dueTime = 3000;
            this.caster = caster;
            this.Name = "恶鬼缠身";
            this.dActor = dactor;
            map = Manager.MapManager.Instance.GetMap(dactor.MapID);
            SkillHandler.Instance.ShowEffectOnActor(dactor, 5318);
        }


        public override void CallBack()
        {
            try
            {
                if(count == 0)
                {
                    dActor.Buff.恶炎 = true;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, dActor, true);
                }
                if (count < maxcount && !dActor.Buff.Dead)
                {
                    List<Actor> actors = map.GetActorsArea(dActor, 250, false);
                    foreach (var item in actors)
                    {
                        if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                        {
                            if (!item.Tasks.ContainsKey("恶鬼缠身"))
                            {
                                恶鬼缠身 skill = new 恶鬼缠身(caster, item);
                                item.Tasks.Add("恶鬼缠身", skill);
                                skill.Activate();
                            }
                        }
                    }
                    SkillHandler.Instance.CauseDamage(caster, dActor, damage);
                    SkillHandler.Instance.ShowVessel(dActor, damage);
                    count++;
                }
                else
                {
                    dActor.Tasks.Remove("恶鬼缠身");
                    this.Deactivate();
                    dActor.Buff.恶炎 = false;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, dActor, true);
                }
            }
            catch (Exception ex)
            {
                dActor.Tasks.Remove("恶鬼缠身");
                Logger.ShowError(ex);
                this.Deactivate();
                dActor.Buff.恶炎 = false;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, dActor, true);
            }
        }

    }
}

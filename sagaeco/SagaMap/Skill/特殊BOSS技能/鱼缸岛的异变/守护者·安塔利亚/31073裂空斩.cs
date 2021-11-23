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
    public class S31073 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float damagerate = 1.1f;
            SkillHandler.Instance.ShowEffectOnActor(dActor, 5376);
            Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
            List<Actor> actors = map.GetActorsArea(dActor, 200, true);
            List<Actor> targets = new List<Actor>();
            foreach (var item in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                    targets.Add(item);
            }
            switch (targets.Count)
            {
                case 1:
                    damagerate = 1.1f;
                    break;
                case 2:
                    damagerate = 0.8f;
                    break;
                case 3:
                    damagerate = 0.6f;
                    break;
                case 4:
                    damagerate = 0.4f;
                    break;
                case 5:
                    damagerate = 0.2f;
                    break;
                default:
                    damagerate = 0.8f;
                    break;
            }

            Activator timer = new Activator(sActor, targets, damagerate);
            timer.Activate();

        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            List<Actor> targets;
            Map map;
            float rate;
            public Activator(Actor caster, List<Actor> targets, float rate)
            {
                this.caster = caster;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                dueTime = 200;
                this.targets = targets;
                this.rate = rate;
            }
            public override void CallBack()
            {
                try
                {
                    if(rate > 1)
                    {
                        SkillHandler.Instance.ActorSpeak(caster, "哼，就凭你一个人也想挡下我的裂空斩？");
                    }
                    else
                       SkillHandler.Instance.ActorSpeak(caster, "我也是...有要守护的东西的。");
                    foreach (var item in targets)
                    {
                        if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                        {
                            int damage = (int)(item.MaxHP * rate);
                            SkillHandler.Instance.CauseDamage(caster, item, damage);
                            SkillHandler.Instance.ShowVessel(item, damage);
                            SkillHandler.Instance.ShowEffectOnActor(item, 5274);
                            if(rate > 1)
                                SkillHandler.Instance.ShowEffectOnActor(item, 5320);
                        }
                    }
                }
                catch(Exception ex)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;


namespace SagaMap.Skill.SkillDefinations
{
    public class S13107 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 2.2f + 0.8f * level;
            factor += factor * (sActor.BeliefDark / 5000f) / 2f;
            float PoisonFactor = 2.4f + level * 0.6f;
            List<Actor> target = new List<Actor>();
            //target.Add(dActor);
            Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
            bool s = false;
            if (dActor.Status.Additions.ContainsKey("暗刻"))
            {
                SkillHandler.RemoveAddition(dActor, "暗刻");
                s = true;
            }

            List<Actor> actors = map.GetActorsArea(dActor, 300, true);
            foreach (var item in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    target.Add(item);
                    if(s)
                    {
                        if(!item.Status.Additions.ContainsKey("Poison1"))
                        {
                            int damage = SkillHandler.Instance.CalcDamage(false, sActor, item, null, SkillHandler.DefType.MDef, Elements.Dark, 50, PoisonFactor);
                            Poison1 skill = new Poison1(null, item, 10000, damage);
                            SkillHandler.ApplyAddition(item, skill);
                        }
                        else
                        {
                            DefaultDeBuff skill = (DefaultDeBuff)item.Status.Additions["Poison1"];
                            skill.endTime = DateTime.Now + new TimeSpan(0, 0, 0, 0, 10000);
                        }
                    }
                }
            }
            if (!sActor.Status.Additions.ContainsKey("意志坚定"))
                sActor.EP -= 300;
            SkillHandler.Instance.MagicAttack(sActor, target, args, Elements.Dark, factor);
        }
    }
}

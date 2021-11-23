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
    public class S11108 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("圣母咏叹CD") && !pc.Status.Additions.ContainsKey("圣母咏叹连续")) return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            dActor = SkillHandler.Instance.GetdActor(sActor, args);
            if (dActor == null) return;

            float factor = 9f + 4f * level;
            if (sActor.TInt["双星刺伤害提升Rate"] > 0)
                factor += factor * sActor.TInt["双星刺伤害提升Rate"] / 100f;

            if (sActor.Status.Additions.ContainsKey("闪光一刺"))
                factor += factor * sActor.TInt["闪光一刺提升%"] / 100f;

            Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
            List<Actor> actors = map.GetActorsArea(dActor, 200, true);
            List<Actor> dest = new List<Actor>();
            foreach (var item in actors)
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    dest.Add(item);
                    SkillHandler.Instance.ShowEffectOnActor(dActor, 4350);
                }

            SkillHandler.Instance.ShowEffectOnActor(sActor, 7894);
            SkillHandler.Instance.PhysicalAttack(sActor, dest, args, Elements.Neutral, factor);

            OtherAddition cd = new OtherAddition(null, sActor, "圣母咏叹CD", 120000);
            cd.OnAdditionEnd += (s, e) =>
            {
                SkillHandler.Instance.ShowEffectOnActor(sActor, 7894);
                SkillHandler.SendSystemMessage(sActor, "『圣母咏叹』已准备就绪！");
            };
            SkillHandler.ApplyAddition(sActor, cd);

            if (sActor.Status.Additions.ContainsKey("圣母咏叹连续"))
                SkillHandler.RemoveAddition(sActor, "圣母咏叹连续");
            else
            {
                OtherAddition skill = new OtherAddition(null, sActor, "圣母咏叹连续", 10000);
                SkillHandler.ApplyAddition(sActor, skill);
                SkillHandler.Instance.SetNextComboSkill(sActor, 11108);
            }
        }
    }
}

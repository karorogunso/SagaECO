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
    public class S11107 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            dActor = SkillHandler.Instance.GetdActor(sActor, args);
            if (dActor == null) return;

            float factor = 1.2f + 0.2f * level;
            int rateup = 10;
            int combo = 2;
            if (level >= 4)
            {
                combo = 3;
                factor = 0.6f + 0.2f * level;
            }
            if (level == 5)
                rateup = 20;

            if (sActor.TInt["双星刺伤害提升Rate"] > 0)
                factor += factor * sActor.TInt["双星刺伤害提升Rate"] / 100f;

            if (sActor.Status.Additions.ContainsKey("闪光一刺"))
                factor += factor * sActor.TInt["闪光一刺提升%"] / 100f;

            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.SLASH;
            args.delayRate = 0.7f;
            List<Actor> dest = new List<Actor>();
            for (int i = 0; i < combo; i++)
                dest.Add(dActor);

            uint epheal = (uint)(140 + 40 * level);
            if (level > 3)
                epheal = 260;
            sActor.EP += epheal;
            if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;

            SkillHandler.Instance.PhysicalAttack(sActor, dest, args, SkillHandler.DefType.Def, Elements.Neutral, 0, factor, false, 0, true);

            if (!sActor.Status.Additions.ContainsKey("双星刺伤害提升"))
            {
                sActor.TInt["双星刺伤害提升Rate"] = rateup;
                OtherAddition skill = new OtherAddition(null, sActor, "双星刺伤害提升", 2000);
                skill.OnAdditionEnd += (s, e) =>
                {
                    SkillHandler.Instance.ShowEffectOnActor(sActor, 4003);
                    SkillHandler.SendSystemMessage(sActor, "『双星刺』的伤害提升消失了！");
                    sActor.TInt["双星刺伤害提升Rate"] = 0;
                };
                SkillHandler.ApplyAddition(sActor, skill);
            }
            else
            {
                Addition 双星刺伤害提升 = sActor.Status.Additions["双星刺伤害提升"];
                TimeSpan span = new TimeSpan(0, 0, 0, 2);
                ((OtherAddition)双星刺伤害提升).endTime = DateTime.Now + span;
                if (sActor.TInt["双星刺伤害提升Rate"] < 100)
                {
                    sActor.TInt["双星刺伤害提升Rate"] += rateup;
                    if (sActor.TInt["双星刺伤害提升Rate"] > 100)
                        sActor.TInt["双星刺伤害提升Rate"] = 100;
                    if (sActor.TInt["双星刺伤害提升Rate"] == 100)
                    {
                        SkillHandler.SendSystemMessage(sActor, "『双星刺』的伤害提升达到了最大值！");
                        SkillHandler.Instance.ShowEffectOnActor(sActor, 4255);
                    }
                }
            }
            SkillHandler.Instance.ShowEffectOnActor(dActor, 4214, sActor);
            SkillHandler.Instance.SetNextComboSkill(sActor, 11107);
            SkillHandler.Instance.SetNextComboSkill(sActor, 11108);
        }
    }
}

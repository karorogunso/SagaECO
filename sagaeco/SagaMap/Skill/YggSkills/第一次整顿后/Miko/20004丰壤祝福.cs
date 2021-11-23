using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;


namespace SagaMap.Skill.SkillDefinations
{
    class S20004 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("丰壤祝福CD"))
                return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //取得目标、造成伤害
            float factor = 2f + 1f * level;
            Map map = SkillHandler.GetActorMap(sActor);
            List<Actor> targets = SkillHandler.Instance.GetAreaActorByPosWhoCanBeAttackedTargets(sActor, args.x, args.y, 200);
            SkillHandler.Instance.MagicAttack(sActor, targets, args, Elements.Neutral, factor);

            //技能CD
            OtherAddition cd = new OtherAddition(null, sActor, "丰壤祝福CD", 5000);
            SkillHandler.ApplyBuffAutoRenew(sActor, cd);

            //保护效果
            OtherAddition skill = new OtherAddition(null, sActor, "丰壤祝福减伤", 1000);
            SkillHandler.ApplyBuffAutoRenew(sActor, skill);
            SkillHandler.OnCheckBuffListReduce.Add("Buff_丰壤祝福减伤", (s, t, d) =>
            {
                if (t.Status.Additions.ContainsKey("丰壤祝福减伤"))
                {
                    d = (int)(d * 0.3f);
                    SkillHandler.Instance.ShowEffectOnActor(t, 7870);
                }
                return d;
            });
        }
        #endregion
    }
}

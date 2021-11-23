using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;

namespace SagaMap.Skill.SkillDefinations
{
    public partial class S21114 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //获取锁定目标
            dActor = SkillHandler.Instance.GetdActor(sActor, args);
            if (dActor == null) return;

            //造成混合伤害
            float factor = 5f + 1.5f * level;
            SkillHandler.Instance.PhysicalAttack(sActor, new List<Actor> { dActor }, args, SkillHandler.DefType.Def, Elements.Dark, 0, factor, true);

            //TODO:祸乱之魂状态

            OtherAddition skill = new OtherAddition(null, dActor, "祸乱之魂", 30000);
            SkillHandler.ApplyBuffAutoRenew(dActor, skill);

            SkillHandler.OnCheckBuffList.Add("DEBUFF_祸乱之魂", (s, t, d) =>
            {
                Map map = SkillHandler.GetActorMap(s);
                if (t.Status.Additions.ContainsKey("祸乱之魂"))
                {
                    List<Actor> das = map.GetActorsArea(dActor, 300, false).Where(tpc => !tpc.Status.Additions.ContainsKey("祸乱之魂")&& SkillHandler.Instance.CheckValidAttackTarget(s, tpc)).ToList();
                    //SkillHandler.Instance.ShowEffectOnActor(t, 1);
                    SkillHandler.Instance.CauseDamage(s, t, d);
                    SkillHandler.Instance.ShowVessel(t, d);
                }
                return d;
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;


namespace SagaMap.Skill.SkillDefinations
{
    public class S31168 : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 8.5f;
            if(SagaLib.Global.Random.Next(0, 1)==1)   //随机造成物理或魔法伤害
                SkillHandler.Instance.MagicAttack(sActor, dActor, args, SkillHandler.DefType.Def, Elements.Neutral, factor);
            else
                SkillHandler.Instance.MagicAttack(sActor, dActor, args, SkillHandler.DefType.MDef,Elements.Neutral, factor);

            //附加DEBUFF，共用死神的易伤效果
            OtherAddition skill = new OtherAddition(null, dActor, "魂之易伤", 15000);
            SkillHandler.ApplyBuffAutoRenew(dActor, skill);

            //增加魂之易伤的处理
            SkillHandler.OnCheckBuffList.Add("DEBUFF_魂之易伤", (s, t, d) =>
            {
                if (t.Status.Additions.ContainsKey("魂之易伤"))
                {
                    SkillHandler.Instance.ShowEffectOnActor(t, 8054);
                    d += (int)(d * 0.3f);
                }
                return d;
            });
        }
        #endregion
    }
}

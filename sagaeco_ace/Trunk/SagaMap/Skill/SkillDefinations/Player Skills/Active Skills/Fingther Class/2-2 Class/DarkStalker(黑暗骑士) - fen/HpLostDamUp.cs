
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.DarkStalker
{
    /// <summary>
    /// 公平交易（ブラッディウエポン）
    /// </summary>
    public class HpLostDamUp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 5000 + 5000 * level;
            int HPLost = 20 + 10 * level;
            int DamUp= 20 + 10 * level;
            HpLostDamUpBuff skill = new HpLostDamUpBuff(args.skill, dActor, lifetime, HPLost, DamUp);
            SkillHandler.ApplyAddition(dActor, skill);
        }
        #endregion
        #region HpLostDamUpBuff
        public class HpLostDamUpBuff : DefaultBuff
        {
            public HpLostDamUpBuff(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int HPLost, int DamUp)
                : base(skill, actor, "HpLostDamUp", lifetime)
            {
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this["DamUp"] = DamUp;
                this["HPLost"] = HPLost;
            }

            void StartEvent(Actor actor, DefaultBuff skill)
            {
            }

            void EndEvent(Actor actor, DefaultBuff skill)
            {
            }
        }
        #endregion
    }
}

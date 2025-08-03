using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.DarkStalker
{
    /// <summary>
    /// 魔王之火（ダークフレア）
    /// </summary>
    public class DegradetionDarkFlare : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float[] factors = { 0f, 0.94f, 1.04f, 1.09f };
            float factor = factors[level];
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, sActor.WeaponElement, factor);
            int rate = 5 + 10 * level;
            if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, "DegradetionDarkFlare", rate) && !SkillHandler.Instance.isBossMob(dActor))
            {
                DegradetionDarkFlareBuff skill = new DegradetionDarkFlareBuff(sActor, args, dActor, 20000, 2000);
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
        public class DegradetionDarkFlareBuff : DefaultBuff
        {
            private SkillArg args;
            private Actor sActor;
            public DegradetionDarkFlareBuff(Actor sActor, SkillArg args, Actor actor, int lifetime, int period)
                : base(args.skill, actor, "DegradetionDarkFlareBuff", lifetime, period)
            {
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this.OnUpdate += this.TimerUpdate;
                this.args = args.Clone();
                this.sActor = sActor;
            }

            void StartEvent(Actor actor, DefaultBuff skill)
            {
            }

            void EndEvent(Actor actor, DefaultBuff skill)
            {
            }
            void TimerUpdate(Actor actor, DefaultBuff skill)
            {
                uint HP_Lost = (uint)(actor.MaxHP * 0.015f);
                SkillHandler.Instance.FixAttack(sActor, actor, args, sActor.WeaponElement, HP_Lost);
                Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, args, actor, false);
            }
        }

        #endregion
    }
}

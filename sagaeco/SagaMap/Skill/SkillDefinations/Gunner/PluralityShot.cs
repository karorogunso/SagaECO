using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Gunner
{
    /// <summary>
    /// 散彈射擊（バラージショット）
    /// </summary>
    public class PluralityShot : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, dActor) && SkillHandler.Instance.CheckSkillCanCastForWeapon(sActor, args))
            {
                return 0;
            }
            else
            {
                return -5;
            }
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 1.2f + 0.3f * level;
            args.argType = SkillArg.ArgType.Attack;
            int times = 1;
            List<Actor> target = new List<Actor>();
            for (int i = 0; i < times; i++)
            {
                target.Add(dActor);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, target, args, SagaLib.Elements.Neutral, factor);

            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "ASPDDOWNFORMPS", 10000);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            if (skill.Variable.ContainsKey("PluralityShot"))
                skill.Variable.Remove("PluralityShot");
            skill.Variable["PluralityShot"] = skill.skill.Level;
            actor.Status.aspd_rate_skill -= (short)(10f + 5 * skill.Variable["PluralityShot"]);

            actor.Buff.DelayCancel = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            short raspd_skill_perc_restore = (short)(10f + 5 * skill.Variable["PluralityShot"]);
            actor.Status.aspd_rate_skill += raspd_skill_perc_restore;

            actor.Buff.DelayCancel = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}

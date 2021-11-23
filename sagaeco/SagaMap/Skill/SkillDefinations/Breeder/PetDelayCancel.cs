
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Breeder
{
    /// <summary>
    ///  ペットディレイキャンセル（ペットディレイキャンセル）
    /// </summary>
    public class PetDelayCancel : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 20000;
            ActorPet p = SkillHandler.Instance.GetPet(sActor);
            if (p != null)
            {
                DefaultBuff skill = new DefaultBuff(args.skill, p, "PetDelayCancel", lifetime);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.aspd_rate_skill += 150;

            actor.Buff.DelayCancel = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            short raspd_skill_perc_restore = 150;
            if (actor.Status.aspd_rate_skill > 100 + raspd_skill_perc_restore)
            {
                actor.Status.aspd_rate_skill -= raspd_skill_perc_restore;
            }
            else
            {
                actor.Status.aspd_rate_skill = 100;
            }

            actor.Buff.DelayCancel = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        #endregion
    }
}

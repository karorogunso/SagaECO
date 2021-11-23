using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaDB.Item;

namespace SagaMap.Skill.SkillDefinations.Harvest
{
    /// <summary>
    /// ハーヴェストマスター
    /// </summary>
    public class HarvestMaster : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.isEquipmentRight(pc, ItemType.CARD))
            {
                return 0;
            }
            return -14;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "HarvestMaster", true);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.HarvestMaster_Lv = skill.skill.Level;

        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.HarvestMaster_Lv = 0;
        }

        #endregion
    }
}

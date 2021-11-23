
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.TreasureHunter
{
    /// <summary>
    /// 纏繞捆綁（バックラッシュ）
    /// </summary>
    public class BackRush : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (Skill.SkillHandler.Instance.isEquipmentRight(sActor, SagaDB.Item.ItemType.ROPE) || sActor.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2).Count > 0)
            {
                if (dActor.type == ActorType.MOB)
                {
                    ActorMob actorMob = (ActorMob)dActor;
                    if (SkillHandler.Instance.isBossMob(actorMob))
                    {
                        return -14;
                    }
                }
                return 0;
            }
            return -5;
           
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 2000 + 1000 * level;
            硬直 dskill = new 硬直(args.skill, dActor, lifetime);
            SkillHandler.ApplyAddition(dActor, dskill);
            硬直 sskill = new 硬直(args.skill, sActor, lifetime);
            SkillHandler.ApplyAddition(sActor, sskill);
        }
        #endregion
    }
}

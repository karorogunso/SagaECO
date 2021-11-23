
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Bard
{
    /// <summary>
    /// 雄壯音樂演奏（ミュージカルブロウ）
    /// </summary>
    public class MusicalBlow : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (Skill.SkillHandler.Instance.isEquipmentRight(sActor, SagaDB.Item.ItemType.STRINGS) || sActor.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2).Count > 0)
            {
                return 0;
            }
            return -5;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 2.2f + 0.2f * level;
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Holy , factor);
        }
        #endregion
    }
}
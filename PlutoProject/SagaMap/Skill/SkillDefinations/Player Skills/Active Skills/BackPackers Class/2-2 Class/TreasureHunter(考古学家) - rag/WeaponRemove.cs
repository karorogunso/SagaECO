
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.TreasureHunter
{
    /// <summary>
    /// 武裝解除（ウエポンキャプチャー）
    /// </summary>
    public class WeaponRemove : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (Skill.SkillHandler.Instance.isEquipmentRight(sActor, SagaDB.Item.ItemType.ROPE) || sActor.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2).Count > 0)
            {
                return 0;
            }
            return -5;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0.01f;
            if (dActor.type == ActorType.PC)
            {
                int dePossessionRate = 10 + 10 * level;
                if (SagaLib.Global.Random.Next(0, 99) < dePossessionRate)
                {
                    ActorPC actor = (ActorPC)dActor;
                    SkillHandler.Instance.PossessionCancel(actor, SagaLib.PossessionPosition.RIGHT_HAND);
                }
            }
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, factor);
        }
        #endregion
    }
}
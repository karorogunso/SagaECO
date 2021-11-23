using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Striker
{
    /// <summary>
    /// 多連箭（バラージアロー）
    /// </summary>
    public class PluralityArrow : ISkill
    {
        #region ISkill Members
        int numdownmin = 0;
        int numdownmax = 0;
        int numdown = 0;
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            int[] numdownmin = new int[] { 0, 3, 4, 4, 5, 5 };
            int[] numdownmax = new int[] { 0, 2, 2, 3, 3, 4 };
            if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
            {
                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BOW)
                {
                    if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                    {
                        if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.ARROW)
                        {
                            numdown = SagaLib.Global.Random.Next(numdownmin[args.skill.Level], numdownmin[args.skill.Level]);
                            if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Stack >= numdown)
                            {
                                return 0;
                            }
                            else
                            {
                                return -55;
                            }
                        }
                        else
                            return -34;
                    }
                    return -34;
                }
                else
                    return -5;
            }
            else
                return -5;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BOW)
                    {
                        if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                        {
                            if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.ARROW)
                            {
                                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Stack >= numdown)
                                {
                                    for (int i = 0; i < numdown; i++)
                                        Network.Client.MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Slot, 1, false);
                                }

                            }
                        }
                    }
                }
            }
            int[] times = { 0, 3, 4, 5, 5, 5 };
            float factor = 1.2f;
            List<Actor> target = new List<Actor>();
            for (int i = 0; i < times[level]; i++)
            {
                target.Add(dActor);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, target, args, sActor.WeaponElement, factor);
        }
        #endregion
    }
}

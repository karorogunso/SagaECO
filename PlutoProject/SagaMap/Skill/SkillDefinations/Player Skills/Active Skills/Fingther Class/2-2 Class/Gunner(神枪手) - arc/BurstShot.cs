using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Gunner
{
    /// <summary>
    /// 3連發精密射擊（バーストショット）
    /// </summary>
    public class BurstShot : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            int numdown = 3;
            if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
            {
                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.GUN ||
                    pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.DUALGUN ||
                    pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.RIFLE)
                {
                    if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                    {
                        if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BULLET)
                        {
                            if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Stack >= numdown)
                            {

                                return 0;
                            }
                            else
                            {
                                return -56;
                            }
                        }

                        return -35;
                    }
                    return -35;
                }
                else
                    return -5;
            }
            else
                return -5;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int numdown = 3;
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.GUN ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.DUALGUN ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.RIFLE)
                    {
                        if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                        {
                            if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BULLET)
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
            float factor = 1.0f + 0.1f * level;
            int rate = 5 * level;
            args.argType = SkillArg.ArgType.Attack;
            List<Actor> affected = new List<Actor>();
            for (int i = 0; i < 3; i++)
            {
                affected.Add(dActor);
            }
            args.delayRate = 4.5f;
            SkillHandler.Instance.PhysicalAttack(sActor, affected, args, sActor.WeaponElement, factor);
            if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, "BurstShot", rate))
            {
                DefaultBuff skill = new DefaultBuff(args.skill, dActor, "BurstShot", 15000);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {

            //近命中
            actor.Status.hit_melee_skill = (short)(-20);

            //遠命中
            actor.Status.hit_ranged_skill = (short)(-20);

        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.hit_melee_skill -= (short)(-20);

            //遠命中
            actor.Status.hit_ranged_skill -= (short)(-20);

        }
        #endregion
    }
}

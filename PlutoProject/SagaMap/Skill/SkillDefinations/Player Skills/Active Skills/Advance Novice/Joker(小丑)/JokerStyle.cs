using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    ///  ジョーカースタイル
    /// </summary>
    public class JokerStyle : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            switch (args.skill.Level)
            {
                case 1:
                    return 0;
                    break;
                case 2:
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BOW ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.GUN ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.DUALGUN ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.RIFLE)
                    {
                        return 0;
                    }
                    break;
                case 3:
                    if ((pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SWORD ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.AXE ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.HAMMER ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SPEAR) &&
                        pc.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2).Count > 0)
                    {
                        return 0;
                    }
                    break;
            }
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            args.argType = SkillArg.ArgType.Attack;
            switch (level)
            {
                case 1:
                    DefaultBuff skill = new DefaultBuff(args.skill, dActor, "JokerStyle1", 5000);
                    skill.OnAdditionStart += this.StartJokerStyle1;
                    skill.OnAdditionEnd += this.EndJokerStyle1;
                    SkillHandler.ApplyAddition(dActor, skill);
                    SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, 10.5f);
                    break;
                case 2:
                    if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.鈍足, 50))
                    {
                        Additions.Global.鈍足 skill2 = new SagaMap.Skill.Additions.Global.鈍足(args.skill, dActor, (int)(750 + 250 * level));
                        SkillHandler.ApplyAddition(dActor, skill2);
                    }
                    List<Actor> dest = new List<Actor>();
                    for (int i = 0; i < 3; i++)
                        dest.Add(dActor);
                    args.delayRate = 4.5f;
                    SkillHandler.Instance.PhysicalAttack(sActor, dest, args, sActor.WeaponElement, 3.0f);
                    break;
                case 3:
                    DefaultBuff skill3 = new DefaultBuff(args.skill, dActor, "JokerStyle3", 5000);
                    skill3.OnAdditionStart += this.StartJokerStyle3;
                    skill3.OnAdditionEnd += this.EndJokerStyle3;
                    SkillHandler.ApplyAddition(dActor, skill3);
                    SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, 10.5f);
                    break;
            }
        }
        #endregion

        void StartJokerStyle1(Actor actor, DefaultBuff skill)
        {
            actor.Status.str_skill -= 10;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndJokerStyle1(Actor actor, DefaultBuff skill)
        {
            actor.Status.str_skill += 10;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }



        void StartJokerStyle3(Actor actor, DefaultBuff skill)
        {
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndJokerStyle3(Actor actor, DefaultBuff skill)
        {

            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}

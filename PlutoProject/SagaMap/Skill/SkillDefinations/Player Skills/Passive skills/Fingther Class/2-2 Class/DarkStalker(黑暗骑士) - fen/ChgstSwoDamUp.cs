
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.DarkStalker
{
    /// <summary>
    /// 追魂刃（追い討ちの刃）
    /// </summary>
    public class ChgstSwoDamUp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {

            ushort[] Values = { 0, 12, 14, 16, 18, 20 };
            bool active = false;
            ushort value = Values[level];

            if (((ActorPC)sActor).Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
            {
                if (((ActorPC)sActor).Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHORT_SWORD ||
                    ((ActorPC)sActor).Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SWORD || 
                    ((ActorPC)sActor).Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.RAPIER)
                {
                    active = true;
                }
            }

            //判斷對方異常狀態，傷害增加12% 14% 16% 18% 20% 
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "ChgstSwoDamUp", active);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
        }
        #endregion
    }
}


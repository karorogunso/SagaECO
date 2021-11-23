using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Archer
{
    /// <summary>
    /// 沖擊之箭
    /// </summary>
    public class ChargeArrow: ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return SkillHandler.Instance.CheckPcBowAndArrow(pc);
        }

        //bool CheckPossible(Actor sActor)
        //{
        //    if (sActor.type == ActorType.PC)
        //    {
        //        ActorPC pc = (ActorPC)sActor;
        //        if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
        //        {
        //            if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BOW ||  SkillHandler.Instance.CheckDEMRightEquip(sActor, SagaDB.Item.ItemType.PARTS_BLOW))
        //                return true;
        //            else
        //                return false;
        //        }
        //        else
        //            return false;
        //    }
        //    else
        //        return true;
        //}


        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.Instance.PcArrowDown(sActor);
            float factor = 0;
            factor = 1.00f + 0.1f * level;
            if (level == 1)
            {
                factor -= 0.1f;
            }
            if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.Stiff, 100))
            {
                Additions.Global.Stiff skill = new SagaMap.Skill.Additions.Global.Stiff(args.skill, dActor, 3000);//实际上是2秒，但因为甩手动作（估计是本服务器问题）本身就要1秒，所以变成3秒来达成“攻击后2秒”的效果
                SkillHandler.ApplyAddition(dActor, skill);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, factor);
        }
        #endregion
    }
}

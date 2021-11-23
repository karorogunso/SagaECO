using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Gunner
{
    /// <summary>
    /// 煙幕彈射擊（スロウグレネード）
    /// </summary>
    public class GrenadeSlow:ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, dActor) && CheckPossible(sActor))
            {
                return 0;
            }
            else
            {
                return -5;
            }
        }
        bool CheckPossible(Actor sActor)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.GUN ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.DUALGUN ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.RIFLE ||
                        pc.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2).Count > 0)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return true;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 1.2f + 0.1f * level;
            int rate = 20 + 10 * level;
            args.argType = SkillArg.ArgType.Attack;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(SagaLib.Global.PosX8to16(args.x , map.Width), SagaLib.Global.PosY8to16(args.y , map.Height), 100, null);
            List<Actor> affected = new List<Actor>();
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                {
                    if (SkillHandler.Instance.CanAdditionApply(sActor,i, SkillHandler.DefaultAdditions.鈍足, rate))
                    {
                        Additions.Global.鈍足 skill1 = new SagaMap.Skill.Additions.Global.鈍足(args.skill, i, 6000);
                        SkillHandler.ApplyAddition(i, skill1);
                    }
                }
            }
           
        }
        #endregion
    }
}

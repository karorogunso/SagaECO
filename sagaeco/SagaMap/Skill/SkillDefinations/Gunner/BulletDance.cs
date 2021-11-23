using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Gunner
{
    /// <summary>
    /// 亂槍掃射（バレットダンス）
    /// </summary>
    public class BulletDance :ISkill 
    {
         #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (CheckPossible(sActor))
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
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.GUN || pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.DUALGUN || pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.RIFLE || pc.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2).Count > 0)
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
            float factor = 0.75f + 0.25f * level;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 200, false);
            List<Actor> realAffected=new List<Actor>();
            foreach(Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    for (int i = 0; i < 8; i++)
                    {
                        realAffected.Add(act);
                    }
                }
            }
            if (realAffected.Count > 0)
            {
                List<Actor> finalAffected = new List<Actor>();
                for (int i = 0; i < realAffected.Count; i++)
                {
                    finalAffected.Add(realAffected[SagaLib.Global.Random.Next(0,realAffected.Count-1)]);
                }
                SkillHandler.Instance.PhysicalAttack(sActor, finalAffected, args, SagaLib.Elements.Neutral, factor);
            }
        }
        #endregion
    }
}


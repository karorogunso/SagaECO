using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Archer
{
    /// <summary>
    /// 大地箭
    /// </summary>
    public class EarthArrow : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (CheckPossible(pc))
                return 0;
            else
                return -5;
        }

        bool CheckPossible(Actor sActor)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BOW || SkillHandler.Instance.CheckDEMRightEquip(sActor, SagaDB.Item.ItemType.PARTS_BLOW))
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
            float factor = 0;
            factor = 0.8f + 0.1f * level;
            List<Actor> target = new List<Actor>();
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(dActor, 200, true);
            List<Actor> affacted = new List<Actor>();
            byte MaxCount = 3;
            byte Count = 0;
            args.argType = SkillArg.ArgType.Attack;
            if (actors.Count > 0)
            {
                while (Count < MaxCount)
                {
                    foreach (Actor i in actors)
                    {
                        if (Count < MaxCount)
                        {
                            Count += 1;
                            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                            {
                                affacted.Add(i);
                                map.SendEffect(i, 8034);
                            }
                        }
                    }
                }

                SkillHandler.Instance.PhysicalAttack(sActor, affacted, args, SagaLib.Elements.Earth, factor);
            }
        }
        #endregion
    }
}

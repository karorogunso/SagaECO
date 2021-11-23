using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Scout
{
    public class ConThrust:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (!pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND) || pc.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2).Count > 0)
                return -5;
            else
                return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int combo = 0;
            float factor = 0f;
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.STAB;
            switch (level)
            {
                case 1:
                    combo = 4;
                    factor = 1f;
                    break;
                case 2:
                    combo = 4;
                    factor = 1.1f;
                    break;
                case 3:
                    combo = 5;
                    factor = 1.1f;
                    break;
                case 4:
                    combo = 5;
                    factor = 1.2f;
                    break;
                case 5:
                    combo = 6;
                    factor = 1.2f;
                    break;
            }
            List<Actor> target = new List<Actor>();
            for (int i = 0; i < combo; i++)
            {
                target.Add(dActor);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, target, args, sActor.WeaponElement, factor);
        }
        #endregion
    }
}

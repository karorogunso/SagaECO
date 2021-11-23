using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Scout
{
    public class Brandish:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (!(pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND) || pc.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2).Count > 0))
                return -5;
            else
                return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int combo;
            int min = 1, max = 1;
            float factor = 1f;
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.SLASH;
            switch (level)
            {
                case 1:
                case 2:
                    min = 2;
                    max = 4;
                    break;
                case 3:
                case 4:
                case 5:
                    min = 4;
                    max = 6;
                    break;
            }
            combo = SagaLib.Global.Random.Next(min, max);
            List<Actor> target = new List<Actor>();
            for (int i = 0; i < combo; i++)
            {
                target.Add(dActor);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, target, args, SagaLib.Elements.Neutral, factor);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaDB.Item;
namespace SagaMap.Skill.SkillDefinations
{
    public class S26000 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = false;
            int amount = 0;
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                List<EnumEquipSlot> enums = new List<EnumEquipSlot>();
                enums.Add(EnumEquipSlot.RIGHT_HAND);
                enums.Add(EnumEquipSlot.UPPER_BODY);
                enums.Add(EnumEquipSlot.CHEST_ACCE);
                for (int i = 0; i < enums.Count; i++)
                {
                    if (pc.Inventory.Equipments.ContainsKey(enums[i]))
                    {
                        SagaDB.Item.Item item = pc.Inventory.Equipments[enums[i]];
                        if (item.BaseData.passiveSkill == 26000)
                        {
                            active = true;
                            amount += item.Refine;
                        }
                    }
                }

                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "Enchant26000", active, amount);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(sActor, skill);
            }
        }

        void StartEventHandler(Actor actor, DefaultPassiveSkill buff)
        {
            if (actor.type == ActorType.PC)
            {
                int Elementvalue = buff.amount;
                int Customvalue = buff.amount * 5;

                if (buff.Variable.ContainsKey("Enchant26000ElementUP"))
                    buff.Variable.Remove("Enchant26000ElementUP");
                buff.Variable.Add("Enchant26000ElementUP", Elementvalue);

                if (buff.Variable.ContainsKey("Enchant26000CustomvalueUP"))
                    buff.Variable.Remove("Enchant26000CustomvalueUP");
                buff.Variable.Add("Enchant26000CustomvalueUP", Customvalue);

                actor.Status.attackelements_skill[SagaLib.Elements.Fire] += (short)Elementvalue;
                actor.Status.min_atk1_skill += (short)Customvalue;
                actor.Status.min_atk2_skill += (short)Customvalue;
                actor.Status.min_atk3_skill += (short)Customvalue;
                actor.Status.min_matk_skill += (short)Customvalue;
            }
        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill buff)
        {
            if (actor.type == ActorType.PC)
            {
                int Elementvalue = buff.Variable["Enchant26000ElementUP"];
                int Customvalue = buff.Variable["Enchant26000CustomvalueUP"];

                actor.Status.attackelements_skill[SagaLib.Elements.Fire] -= (short)Elementvalue;
                actor.Status.min_atk1_skill -= (short)Customvalue;
                actor.Status.min_atk2_skill -= (short)Customvalue;
                actor.Status.min_atk3_skill -= (short)Customvalue;
                actor.Status.min_matk_skill -= (short)Customvalue;

            }
        }

        #endregion
    }
}

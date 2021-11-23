using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S11000 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = false;
            if(sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if(pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    if (!pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.doubleHand)
                        active = true;
                }
            }
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "南辰流", active);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int HPUPvalue;
            byte level = skill.skill.Level;  
            float HPfactor = 0.05f + 0.1f * level;
            HPUPvalue = (int)(actor.MaxHP * HPfactor);
            if (skill.Variable.ContainsKey("HPValue"))
                skill.Variable.Remove("HPValue");
            skill.Variable.Add("HPValue", HPUPvalue);
            actor.Status.hp_skill += (short)HPUPvalue;

            int DamageReduceValue = 6 + 4 * level;
            if (skill.Variable.ContainsKey("DamageReduceValue"))
                skill.Variable.Remove("DamageReduceValue");
            skill.Variable.Add("DamageReduceValue", DamageReduceValue);
            actor.Status.ReduceDamage += DamageReduceValue / 100f;

            int HateUpValue = 20 + 80 * level;
            if (skill.Variable.ContainsKey("HateUpValue"))
                skill.Variable.Remove("HateUpValue");
            skill.Variable.Add("HateUpValue", HateUpValue);
            actor.Status.HateRate += HateUpValue / 100f;
        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int HPUPvalue = skill.Variable["HPValue"];
            actor.Status.hp_skill -= (short)HPUPvalue;

            int DamageReduceValue = skill.Variable["DamageReduceValue"];
            actor.Status.ReduceDamage -= DamageReduceValue / 100f;

            int HateUpValue = skill.Variable["HateUpValue"];
            actor.Status.HateRate -= HateUpValue / 100f;
        }

        #endregion
    }
}

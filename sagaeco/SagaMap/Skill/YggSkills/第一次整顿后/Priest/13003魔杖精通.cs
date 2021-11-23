using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S13003 : ISkill
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
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.STAFF)
                        active = true;
                }
            }
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "魔杖精通", active);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int STAFFATKUPvalue, STAFFMAGUPs;
            byte level = skill.skill.Level;
            STAFFMAGUPs = 2 + 5 * level;


            if (skill.Variable.ContainsKey("STAFFMAGUPs"))
                skill.Variable.Remove("STAFFMAGUPs");
            skill.Variable.Add("STAFFMAGUPs", STAFFMAGUPs);
            actor.Status.mag_skill += (short)STAFFMAGUPs;

            STAFFATKUPvalue = 4 + 8 * level;
            if (skill.Variable.ContainsKey("STAFFATKUPvalue"))
                skill.Variable.Remove("STAFFATKUPvalue");
            skill.Variable.Add("STAFFATKUPvalue", STAFFATKUPvalue);

            actor.Status.min_matk_rate_skill += (short)STAFFATKUPvalue;
            actor.Status.max_matk_rate_skill += (short)STAFFATKUPvalue;
        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int STAFFMAGUPs = skill.Variable["STAFFMAGUPs"];
            actor.Status.mag_skill -= (short)STAFFMAGUPs;

            int STAFFATKUPvalue = skill.Variable["STAFFATKUPvalue"];
            actor.Status.min_matk_rate_skill -= (short)STAFFATKUPvalue;
            actor.Status.max_matk_rate_skill -= (short)STAFFATKUPvalue;
        }

        #endregion
    }
}

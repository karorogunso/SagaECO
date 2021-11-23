using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S14006 : ISkill
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
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BOOK)
                        active = true;
                }
            }
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "书本精通", active);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int BOOKATKUPvalue, BOOKINTUP ;
            byte level = skill.skill.Level;

            BOOKINTUP = 10 + 5 * level;
            if (skill.Variable.ContainsKey("BOOKINTUP"))
                skill.Variable.Remove("BOOKINTUP");
            skill.Variable.Add("BOOKINTUP", BOOKINTUP);
            actor.Status.int_skill += (short)BOOKINTUP;

            BOOKATKUPvalue = 4 * 3 * level; ;
            if (skill.Variable.ContainsKey("BOOKATKUPvalue"))
                skill.Variable.Remove("BOOKATKUPvalue");
            skill.Variable.Add("BOOKATKUPvalue", BOOKATKUPvalue);
            actor.Status.min_matk_rate_skill += (short)BOOKATKUPvalue;
            actor.Status.max_matk_rate_skill += (short)BOOKATKUPvalue;


        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int BOOKATKUPvalue = skill.Variable["BOOKATKUPvalue"];
            actor.Status.min_matk_rate_skill -= (short)BOOKATKUPvalue;
            actor.Status.max_matk_rate_skill -= (short)BOOKATKUPvalue;

            int BOOKINTUP = skill.Variable["BOOKINTUP"];
            actor.Status.int_skill -= (short)BOOKINTUP;
        }

        #endregion
    }
}

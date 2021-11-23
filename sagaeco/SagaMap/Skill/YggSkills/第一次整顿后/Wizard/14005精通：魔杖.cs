using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S14005 : ISkill
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
            int STAFFMAGUP,STAFFCRIDAGAMEUP;
            byte level = skill.skill.Level;

            STAFFMAGUP = 10 + 5 * level;
            STAFFCRIDAGAMEUP = 10 + 5 * level;

            if (skill.Variable.ContainsKey("STAFFMAGUP"))
                skill.Variable.Remove("STAFFMAGUP");
            skill.Variable.Add("STAFFMAGUP", STAFFMAGUP);
            actor.Status.mag_skill += (short)STAFFMAGUP;
            if (actor.TInt["魔杖精通暴击提升"] == 0)
            {
                actor.TInt["魔杖精通暴击提升"] = 50;
                actor.Status.hit_critical_skill += 50;
            }

            actor.TInt["魔杖暴击伤害提升"] = STAFFCRIDAGAMEUP;

            //((ActorPC)actor).TInt["14005魔杖精通"] = skill.skill.Level;
        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int STAFFMAGUP = skill.Variable["STAFFMAGUP"];
            actor.Status.mag_skill -= (short)STAFFMAGUP;
            actor.Status.hit_critical_skill -= 50;
            actor.TInt["魔杖精通暴击提升"] = 0;
            actor.TInt["魔杖暴击伤害提升"] = 0;

            //((ActorPC)actor).TInt["14005魔杖精通"] = 0;

        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S13004 : ISkill
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
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.STRINGS)
                        active = true;
                }
            }
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "韵律精通", active);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int MAXSPUP = 50 + 150 * skill.skill.Level;
            ActorPC pc = (ActorPC)actor;
            pc.TInt["韵律精通MAXSPUP"] = MAXSPUP;
            pc.TInt["韵律精通Recover"] = 10 * skill.skill.Level;
        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            ActorPC pc = (ActorPC)actor;
            pc.TInt["韵律精通MAXSPUP"] = 0;
            pc.TInt["韵律精通Recover"] = 0;
        }

        #endregion
    }
}

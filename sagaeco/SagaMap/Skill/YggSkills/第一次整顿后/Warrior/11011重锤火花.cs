using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S11011 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = false;
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    SagaDB.Item.ItemType tp = pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType;
                    if (tp == SagaDB.Item.ItemType.AXE || tp == SagaDB.Item.ItemType.HAMMER)
                        active = true;
                }
            }
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "重锤火花", active);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            actor.TInt["重锤火花提升%"] = 10 + skill.skill.Level * 5;
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            actor.TInt["重锤火花提升%"] = 0;
        }
    }
}

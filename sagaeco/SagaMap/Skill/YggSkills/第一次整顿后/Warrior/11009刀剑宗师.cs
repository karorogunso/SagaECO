using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S11009 : ISkill
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
                    if (tp == SagaDB.Item.ItemType.SWORD)
                        active = true;
                }
            }
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "刀剑宗师", active);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            actor.TInt["刀剑宗师提升%"] = 10 + skill.skill.Level * 5;
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            actor.TInt["刀剑宗师提升%"] = 0;
        }
    }
}

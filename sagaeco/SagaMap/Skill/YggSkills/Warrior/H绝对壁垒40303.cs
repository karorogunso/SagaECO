using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S40303 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.EP < 8000)
                return -5;
            pc.EP -= 8000;
            Map map = Manager.MapManager.Instance.GetMap(pc.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, pc, true);
            if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
            {
                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType != SagaDB.Item.ItemType.SHIELD)
                    return -5;
            }
            if (args.result != -5)
            {
                /*Freeze fz = new Freeze(args.skill, pc, 10000, 0);
                SkillHandler.ApplyAddition(pc, fz);*/
                DefaultBuff skill = new DefaultBuff(args.skill, pc, "Invincible", 10000);
                SkillHandler.ApplyAddition(dActor, skill);
                if (pc.Status.Additions.ContainsKey("Invincible"))
                    return 0;
                else return -1;
            }
            else
                return -5;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (sActor.Status.Additions.ContainsKey("Invincible"))
                sActor.Status.Additions["Invincible"].AdditionEnd();
        }
        #endregion
    }
}
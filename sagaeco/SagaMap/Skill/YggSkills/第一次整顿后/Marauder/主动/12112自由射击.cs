using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12112 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("自由射击CD"))
                return -30;
            if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
            {
                SagaDB.Item.Item item = pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND];
                if (item.BaseData.itemType == SagaDB.Item.ItemType.BOW || item.BaseData.itemType == SagaDB.Item.ItemType.RIFLE
                    || item.BaseData.itemType == SagaDB.Item.ItemType.GUN || item.BaseData.itemType == SagaDB.Item.ItemType.DUALGUN)
                    return 0;
            }
            return -2;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.Instance.ChangdeWeapons(sActor, 1);

            OtherAddition skill = new OtherAddition(null, sActor, "自由射击", 900000000);
            skill.OnAdditionStart += (s, e) =>
            {
                //sActor.Status.aspd_skill += (short)(50 * level);
                sActor.TInt["自由射击Value"] = level;
                sActor.Buff.BowDelayCancel = true;
                Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, sActor, true);
            };
            skill.OnAdditionEnd += (s, e) =>
            {
                //sActor.Status.aspd_skill = 0;
                sActor.TInt["自由射击Value"] = 0;
                SkillHandler.Instance.ChangdeWeapons(sActor, 0);
                sActor.Buff.BowDelayCancel = false;
                Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, sActor, true);
            };
            SkillHandler.ApplyAddition(sActor, skill);

            OtherAddition skillCD = new OtherAddition(null, sActor, "自由射击CD", 10000);
            SkillHandler.ApplyAddition(sActor, skillCD);

        }
    }
}

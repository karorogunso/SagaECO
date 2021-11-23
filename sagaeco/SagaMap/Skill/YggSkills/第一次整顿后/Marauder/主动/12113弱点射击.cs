using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12113 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
            {
                SagaDB.Item.Item item = pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND];
                if ((item.BaseData.itemType == SagaDB.Item.ItemType.BOW || item.BaseData.itemType == SagaDB.Item.ItemType.RIFLE
                    || item.BaseData.itemType == SagaDB.Item.ItemType.GUN || item.BaseData.itemType == SagaDB.Item.ItemType.DUALGUN) && pc.TInt["斥候远程模式"] == 1
                    && pc.Status.Additions.ContainsKey("自由射击"))
                    return 0;
                else return -2;
            }
            return -2;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            dActor = SkillHandler.Instance.GetdActor(sActor, args);
            if (dActor == null) return;

            SkillHandler.Instance.ShowEffectByActor(dActor, 8048);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            float factor = 1f + 0.5f * level;
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.STAB;
            args.delayRate = 15f;

            ActorPC pc = (ActorPC)sActor;
            SagaDB.Item.Item item2 = pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND];
            if (item2.BaseData.itemType == SagaDB.Item.ItemType.DUALGUN)
                args.delayRate = 0.8f;

            if (dActor.Status.Additions.ContainsKey("Stun") || dActor.Status.Additions.ContainsKey("Frozen"))
            {
                factor *= 3;
                uint epheal = 30;
                sActor.EP += epheal;
                if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
                SkillHandler.Instance.ShowEffectByActor(dActor, 8053);
            }

            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, factor);

        }
    }
}

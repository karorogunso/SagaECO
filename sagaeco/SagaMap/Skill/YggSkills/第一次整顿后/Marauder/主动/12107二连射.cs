using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12107 : ISkill
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

            //SkillHandler.Instance.ShowEffectOnActor(dActor, 8050);
            SkillHandler.Instance.ShowEffectOnActor(dActor, 5294, sActor);
            float factor = 1.4f + 0.2f * level;
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.STAB;
            args.delayRate = 5f;

            ActorPC pc = (ActorPC)sActor;
            SagaDB.Item.Item item = pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND];
            if(item.BaseData.itemType == SagaDB.Item.ItemType.DUALGUN)
                args.delayRate = 1f;
            List<Actor> dest = new List<Actor>();


            int count = 2;
            if (level > 3)
            {
                count = 3;
                factor = 0.8f + 0.2f * level;
            }

            if(sActor.Status.Additions.ContainsKey("审判"))
            {
                count *= 2;
                SkillHandler.Instance.ShowEffectOnActor(sActor, 5294);
            }
            for (int i = 0; i < count; i++)
            {
                dest.Add(dActor);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, dest, args, SagaLib.Elements.Neutral, factor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            uint epheal = 35;
            sActor.EP += epheal;
            if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
            sActor.SP += 1;
            if (sActor.SP > sActor.MaxSP) sActor.SP = sActor.MaxSP; 
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }
    }
}

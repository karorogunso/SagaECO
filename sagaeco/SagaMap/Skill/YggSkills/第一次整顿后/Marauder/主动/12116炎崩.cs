using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12116 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("炎崩CD"))
                return -30;
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
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);

            float factor = 4f + 3f * level;
            int stunTime = 3000;
            OtherAddition cd = new OtherAddition(null, sActor, "炎崩CD", 30000);
            cd.OnAdditionEnd += (s, e) =>
            {
                SkillHandler.Instance.ShowEffectOnActor(sActor, 7713, sActor);
                SkillHandler.SendSystemMessage(sActor, "『炎崩』可以再次使用了。");
            };
            SkillHandler.ApplyAddition(sActor, cd);

            List<Actor> targets = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(sActor, dActor, 300, true);
            foreach (var item in targets)
            {
                if(!item.Status.Additions.ContainsKey("炎崩晕眩CD"))
                {
                    Stun stun = new Stun(null, item, stunTime);
                    SkillHandler.ApplyBuffAutoRenew(item, stun);
                    OtherAddition cd2 = new OtherAddition(null, item, "炎崩晕眩CD", 30000);
                    SkillHandler.ApplyBuffAutoRenew(item, cd2);
                }
                SkillHandler.Instance.DoDamage(true, sActor, dActor, null, SkillHandler.DefType.Def, Elements.Fire, 0, factor);
            }
            uint epheal = 100;
            sActor.EP += epheal;
            if (sActor.SP > sActor.MaxSP) sActor.SP = sActor.MaxSP;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }
    }
}

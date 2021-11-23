using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12115 : ISkill
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

            //爆裂次数
            ActorPC pc = (ActorPC)sActor;
            pc.TInt["爆裂次数"]++;
            if (pc.TInt["爆裂次数"] >= 5)
                SkillHandler.Instance.ShowEffectOnActor(sActor, 7918);

            SkillHandler.Instance.ShowEffectOnActor(dActor, 7713, sActor);
            SkillHandler.Instance.ShowEffectOnActor(dActor, 5274, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            float factor = 1f + 1f * level;

            if (sActor.Status.Additions.ContainsKey("审判"))
            {
                factor *= 1.75f;
                SkillHandler.Instance.ShowEffectOnActor(sActor, 5294);
            }

            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.STAB;
            args.delayRate = 20f;

            SagaDB.Item.Item item2 = pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND];
            if (item2.BaseData.itemType == SagaDB.Item.ItemType.DUALGUN)
                args.delayRate = 1f;

            List<Actor> das = map.GetActorsArea(dActor, 200, true);
            List<Actor> targets = new List<Actor>();
            foreach (var item in das)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item) && item != dActor)
                {
                    SkillHandler.Instance.DoDamage(true, sActor, item, null, SkillHandler.DefType.Def, Elements.Fire, 0, factor);
                    SkillHandler.Instance.ShowEffectByActor(item, 7713);
                    //SkillHandler.Instance.ShowEffectOnActor(item, 5274);
                    SkillHandler.Instance.ShowEffectOnActor(item, 8201, sActor);
                }
            }
            uint epheal = 40;
            sActor.EP += epheal;
            if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;

            List<Actor> dest = new List<Actor>() { dActor };
            if (level == 4 && SagaLib.Global.Random.Next(0, 100) < 10)
                dest.Add(dActor);

            float healRate = 0f;
            if (!sActor.Status.Additions.ContainsKey("爆裂回血CD"))
            {
                healRate = 0.1f;
                OtherAddition hpcd = new OtherAddition(null, sActor, "爆裂回血CD", 1000);
                SkillHandler.ApplyAddition(sActor, hpcd);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, dest, args, SkillHandler.DefType.Def, Elements.Neutral, 0, factor, false, healRate, false);

            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }
    }
}

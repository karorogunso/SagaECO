using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12119 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.TInt["爆裂次数"] < 5)
                return -2;
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
            ActorPC pc = (ActorPC)sActor;
            pc.TInt["爆裂次数"] = 0;

            SkillHandler.Instance.ShowEffectOnActor(sActor, 5154);
            //SkillHandler.Instance.ShowEffectOnActor(dActor, 5252);
            float factor = 1.5f + 0.5f * level;

            List<Actor> targets = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(sActor, 1000);
            List<Actor> dest = new List<Actor>();
            List<Actor> K = new List<Actor>();

            if (targets.Count > 0)
            {
                //SkillHandler.Instance.ShowEffectByActor(sActor, 8017);
                //SkillHandler.Instance.ShowEffectByActor(sActor, 8008);
                for (int i = 0; i < 5; i++)
                {
                    Actor a = targets[SagaLib.Global.Random.Next(0, targets.Count - 1)];
                    dest.Add(a);
                    if (!K.Contains(a))
                    {
                        K.Add(a);
                        SkillHandler.Instance.ShowEffectOnActor(a, 5257, sActor);
                    }
                }
                args.argType = SkillArg.ArgType.Attack;
                args.type = ATTACK_TYPE.STAB;
                args.delayRate =5f;
                SagaDB.Item.Item item = pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND];
                if (item.BaseData.itemType == SagaDB.Item.ItemType.DUALGUN)
                    args.delayRate = 5f;

                SkillHandler.Instance.PhysicalAttack(sActor, dest, args, SkillHandler.DefType.Def, Elements.Neutral, 0, factor, false, 0, false, 0, 100);
            }

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            uint epheal = 200;
            sActor.EP += epheal;
            if (sActor.SP > sActor.MaxSP) sActor.SP = sActor.MaxSP; 
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }
    }
}

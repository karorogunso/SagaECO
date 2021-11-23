using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12117 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("枪决CD"))
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
            StableAddition cd = new StableAddition(null, sActor, "枪决CD", 120000);
            cd.OnAdditionEnd += (s, e) =>
            {
                SkillHandler.Instance.ShowEffectOnActor(sActor, 7014);
                SkillHandler.SendSystemMessage(sActor, "『枪决』可以再次使用了。");
            };
            SkillHandler.ApplyAddition(sActor, cd);

            SkillHandler.Instance.ShowEffectOnActor(sActor, 5115);
            SkillHandler.Instance.ShowEffectOnActor(dActor, 5266, sActor);
            //SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)sActor).SendNPCShowEffect(sActor.ActorID, 0, 0, 0, 7958, true);
            SkillHandler.Instance.ShowEffectOnActor(dActor, 8046, sActor);
            float factor = 40f + 10f * level;
            /*args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.STAB;
            args.delayRate = 15f;*/
            List<Actor> dest = new List<Actor>() { dActor };
            SkillHandler.Instance.PhysicalAttack(sActor, dest, args, SagaLib.Elements.Neutral, factor);
        }
    }
}

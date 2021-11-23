using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12100 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            /*if (pc.Status.Additions.ContainsKey("弹药装填伤害提升"))
            {
                return -33;
            }*/
            if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
            {
                SagaDB.Item.Item item = pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND];
                if (item.BaseData.itemType == SagaDB.Item.ItemType.BOW || item.BaseData.itemType == SagaDB.Item.ItemType.RIFLE
                    || item.BaseData.itemType == SagaDB.Item.ItemType.GUN || item.BaseData.itemType == SagaDB.Item.ItemType.DUALGUN)
                    return 0;
            }
            if(!pc.Status.Additions.ContainsKey("自由射击"))
            {
                Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("非自由射击状态下无法装填。");
                return -100;
            }

            return -2;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            
                
            int lifetime = 3000 + 3000 * level;
            sActor.TInt["弹药装填伤害提升Rate"] = 15;
            if (level == 4)
            {
                lifetime = 3000 + 3000 * 3;
                sActor.TInt["弹药装填伤害提升Rate"] = 20;
            }

            OtherAddition up = new OtherAddition(null, sActor, "弹药装填伤害提升", 3000 + 3000 * level);
            up.OnAdditionStart += (s, e) =>
            {
                sActor.Buff.三转枪连弹 = true;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, sActor, true);
            };
            up.OnAdditionEnd += (s,e) =>
            {
                sActor.Buff.三转枪连弹 = false;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, sActor, true);
            };
            SkillHandler.ApplyBuffAutoRenew(sActor, up);



            SkillHandler.Instance.ShowVessel(sActor, 0, (int)-sActor.MaxMP);
            sActor.MP = sActor.MaxMP;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }
    }
}

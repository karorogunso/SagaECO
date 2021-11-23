using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;
using SagaMap.Manager;
using SagaMap.Network.Client;

namespace SagaMap.Skill.SkillDefinations
{
    class S32101 : ISkill
    {
        #region ISkill Members

        public List<string> Names = new List<string> { "岩石", "矿石" };

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.PET].BaseData.itemType == SagaDB.Item.ItemType.RIDE_PARTNER)
            {
                MapClient.FromActorPC(pc).SendSystemMessage("不要再伤害海豹啦！不要再站在海豹身上挖矿啦！海豹辣么可怜！！！");
                return -99;
            }
            if (pc.TranceID != 0)
            {
                MapClient.FromActorPC(pc).SendSystemMessage("变身状态无法采矿。");
                return -99;
            }
            if (dActor == null) return -99;
            if (dActor.type != ActorType.MOB) return -99;
            if(!Names.Contains(dActor.Name)) return -99;

            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            ActorPC pc = (ActorPC)sActor;
            MapClient client = MapClient.FromActorPC(pc);
            Map map = MapManager.Instance.GetMap(sActor.MapID);


        }
        #endregion
    }
}

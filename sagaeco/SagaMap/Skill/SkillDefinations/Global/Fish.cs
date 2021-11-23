
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// 钓鱼
    /// </summary>
    public class Fish : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            if (sActor.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.PET))
            {
                if (sActor.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.PET].BaseData.itemType == SagaDB.Item.ItemType.RIDE_PARTNER)
                {
                    Network.Client.MapClient.FromActorPC(sActor).SendSystemMessage("不要再伤害海豹啦！不要再站在海豹身上钓鱼啦！海豹辣么可怜！！！");
                    return -99;
                }
            }
            if (sActor.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
            {
                if (sActor.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType != SagaDB.Item.ItemType.ETC_WEAPON
                    || sActor.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].Durability < 1)
                    return -29;
                if ((map.Info.canfish[args.x, args.y] == 41000003 || map.Info.canfish[args.x, args.y] == 41200000 || map.Info.canfish[args.x, args.y] == 41100002) && (map.ID == 10005000 || map.ID == 10056000))
                    return 0;
                Network.Client.MapClient.FromActorPC(sActor).SendSystemMessage("指定坐标x:" + args.x + "y:" + args.y + map.Info.canfish[args.x, args.y].ToString());
                return -29;
            }
            else
            {
                SagaMap.Network.Client.MapClient.FromActorPC(sActor).SendSystemMessage("指定坐标x:" + args.x + "y:" + args.y + map.Info.canfish[args.x, args.y].ToString());
                return -13;
            }
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (!sActor.Status.Additions.ContainsKey("fish"))
            {
                int lifetime = SagaLib.Global.Random.Next(5000, 20000);
                Additions.Global.Fish skill1 = new Additions.Global.Fish(args.skill, sActor, 5000000, lifetime);
                SkillHandler.ApplyAddition(sActor, skill1);
            }
        }
        #endregion
    }
}
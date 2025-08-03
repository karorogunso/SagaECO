using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaDB.FFarden;

namespace SagaDB.Server
{
    public class Server
    {
        Dictionary<FFardenSlot, uint> equips = new Dictionary<FFardenSlot, uint>();
        Dictionary<FurniturePlace, List<SagaDB.Actor.ActorFurniture>> furnitures = new Dictionary<FurniturePlace, List<ActorFurniture>>();
        Dictionary<FurniturePlace, List<SagaDB.Actor.ActorFurniture>> furnituresoffg = new Dictionary<FurniturePlace, List<ActorFurniture>>();

        /// <summary>
        /// 飞空城的装备
        /// </summary>
        public Dictionary<FFardenSlot, uint> FFardenEquipments { get { return this.equips; } }

        /// <summary>
        /// 飞空城的家具
        /// </summary>
        public Dictionary<FurniturePlace, List<SagaDB.Actor.ActorFurniture>> Furnitures { get { return this.furnitures; } }

        /// <summary>
        /// 飞空庭的家具
        /// </summary>
        public Dictionary<FurniturePlace, List<SagaDB.Actor.ActorFurniture>> FurnituresofFG { get { return this.furnituresoffg; } }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;

namespace SagaDB.FFarden
{
    public enum FFardenSlot
    {
        FLYING_BASE,
        FLYING_SAIL,
        GARDEN_FLOOR,
        GARDEN_MODELHOUSE,
        HouseOutSideWall,
        HouseRoof,
        ROOM_FLOOR,
        ROOM_WALL,
    }

    public enum FurniturePlace
    {
        GARDEN,
        ROOM,
        FARM,
        FISHERY,
        HOUSE,
    }

    public class FFarden
    {
        ActorPC owner;
        uint mapID;
        uint roomMapID;
        uint id;
        uint ringID;
        bool islock;
        string name,content;
        byte obmode,healthmode;

        uint level,ffexp;
        uint flevel, fffexp, sulevel, ffsuexp, bplevel, ffbpexp, demlevel, ffdemexp;
        uint materialpoint, materialconsume;

        Dictionary<FFardenSlot, uint> equips = new Dictionary<FFardenSlot, uint>();
        Dictionary<FurniturePlace, List<Actor.ActorFurniture>> furnitures = new Dictionary<FurniturePlace, List<ActorFurniture>>();


        /// <summary>
        /// 飞空城的名字
        /// </summary>
        public string Name { get { return this.name; } set { this.name = value; } }

        /// <summary>
        /// 飞空城的简介
        /// </summary>
        public string Content { get { return this.content; } set { this.content = value; } }

        /// <summary>
        /// 飞空城是否需要密码
        /// </summary>
        public bool IsLock { get { return this.islock; } set { this.islock = value; } }

        /// <summary>
        /// 飞空城所属工会
        /// </summary>
        public uint RingID { get { return this.ringID; } set { this.ringID = value; } }

        /// <summary>
        /// 飞空城的ID
        /// </summary>
        public uint ID { get { return this.id; } set { this.id = value; } }

        /// <summary>
        /// 飞空城的主人
        /// </summary>
        public ActorPC Owner { get { return this.owner; } set { this.owner = value; } }

        /// <summary>
        /// 飞空城地图ID
        /// </summary>
        public uint MapID { get { return this.mapID; } set { this.mapID = value; } }

        /// <summary>
        /// 飞空城状态(00 = 无入手   01 = 作出可能  03 = 已入手)
        /// </summary>
        public byte ObMode { get { return this.obmode; } set { this.obmode = value; } }

        /// <summary>
        /// 飞空城健康(00 = 正常 01 = 停滞状态 02 = 扣押状态 03 = 维持不能)
        /// </summary>
        public byte HealthMode { get { return this.healthmode; } set { this.healthmode = value; } }

        /// <summary>
        /// 飞空城的所持材料数(所持マテリアルポイント)
        /// </summary>
        public uint MaterialPoint { get { return this.materialpoint; } set { this.materialpoint = value; } }

        /// <summary>
        /// 飞空城等级
        /// </summary>
        public uint Level { get { return this.level; } set { this.level = value; } }

        /// <summary>
        /// 飞空城等级的经验值
        /// </summary>
        public uint FFexp { get { return this.ffexp; } set { this.ffexp = value; } }

        /// <summary>
        /// 飞空城F系等级
        /// </summary>
        public uint FLevel { get { return this.flevel; } set { this.flevel = value; } }
        /// <summary>
        /// 飞空城F系经验值
        /// </summary>
        public uint FFFexp { get { return this.fffexp; } set { this.fffexp = value; } }
        /// <summary>
        /// 飞空城SU系等级
        /// </summary>
        public uint SULevel { get { return this.sulevel; } set { this.sulevel = value; } }
        /// <summary>
        /// 飞空城SU系经验值
        /// </summary>
        public uint FFSUexp { get { return this.ffsuexp; } set { this.ffsuexp = value; } }
        /// <summary>
        /// 飞空城BP系等级
        /// </summary>
        public uint BPLevel { get { return this.bplevel; } set { this.bplevel = value; } }
        /// <summary>
        /// 飞空城BP系经验值
        /// </summary>
        public uint FFBPexp { get { return this.ffbpexp; } set { this.ffbpexp = value; } }
        /// <summary>
        /// 飞空城DEM系等级
        /// </summary>
        public uint DEMLevel { get { return this.demlevel; } set { this.demlevel = value; } }
        /// <summary>
        /// 飞空城DEM系经验值
        /// </summary>
        public uint FFDEMexp { get { return this.ffdemexp; } set { this.ffdemexp = value; } }
        /// <summary>
        /// 飞空城的材料点数消耗(マテリアルコスト)
        /// </summary>
        public uint MaterialConsume { get { return this.materialconsume; } set { this.materialconsume = value; } }

        /// <summary>
        /// 飞空城房间地图ID
        /// </summary>
        public uint RoomMapID { get { return this.roomMapID; } set { this.roomMapID = value; } }

        /// <summary>
        /// 飞空城的装备
        /// </summary>
        public Dictionary<FFardenSlot, uint> FFardenEquipments { get { return this.equips; } }

        /// <summary>
        /// 飞空城的家具
        /// </summary>
        public Dictionary<FurniturePlace, List<Actor.ActorFurniture>> Furnitures { get { return this.furnitures; } }
    }
}

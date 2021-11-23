using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.Actor
{
    /// <summary>
    /// 家具Actor
    /// </summary>
    public class ActorFurniture : Actor
    {
        uint itemID;
        uint pictID;
        short z, xaxis, yaxis, zaxis;
        ushort motion = 111;

        public ActorFurniture()
        {
            this.type = ActorType.FURNITURE;
        }

        /// <summary>
        /// 道具ID
        /// </summary>
        public uint ItemID { get { return this.itemID; } set { this.itemID = value; } }

        /// <summary>
        /// 图像ID，用于确定怪物雕像的怪物ID
        /// </summary>
        public uint PictID { get { return this.pictID; } set { this.pictID = value; } }

        /// <summary>
        /// Z坐标
        /// </summary>
        public short Z { get { return this.z; } set { this.z = value; } }
        /// <summary>
        /// Z轴旋转
        /// </summary>
        public short Xaxis { get { return this.xaxis; } set { this.xaxis = value; } }

        /// <summary>
        /// Z轴旋转
        /// </summary>
        public short Yaxis { get { return this.yaxis; } set { this.yaxis = value; } }

        /// <summary>
        /// Z轴旋转
        /// </summary>
        public short Zaxis { get { return this.zaxis; } set { this.zaxis = value; } }


        /// <summary>
        /// 动作
        /// </summary>
        public ushort Motion { get { return this.motion; } set { this.motion = value; } }
    }
}

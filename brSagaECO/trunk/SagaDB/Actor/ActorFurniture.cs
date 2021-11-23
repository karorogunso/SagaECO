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
        public ActorFurniture()
        {
            this.type = ActorType.FURNITURE;
        }

        uint itemID;
        /// <summary>
        /// 道具ID
        /// </summary>
        public uint ItemID { get { return this.itemID; } set { this.itemID = value; } }

        uint pictID;
        /// <summary>
        /// 图像ID，用于确定怪物雕像的怪物ID
        /// </summary>
        public uint PictID { get { return this.pictID; } set { this.pictID = value; } }

        short z, xaxis, yaxis, zaxis;
        /// <summary>
        /// Z坐标
        /// </summary>
        public short Z { get { return this.z; } set { this.z = value; } }
        /// <summary>
        /// X轴旋转
        /// </summary>
        public short Xaxis { get { return this.xaxis; } set { this.xaxis = value; } }

        /// <summary>
        /// Y轴旋转
        /// </summary>
        public short Yaxis { get { return this.yaxis; } set { this.yaxis = value; } }

        /// <summary>
        /// Z轴旋转
        /// </summary>
        public short Zaxis { get { return this.zaxis; } set { this.zaxis = value; } }

        ushort motion = 111;
        /// <summary>
        /// 动作
        /// </summary>
        public ushort Motion { get { return this.motion; } set { this.motion = value; } }
    }
}

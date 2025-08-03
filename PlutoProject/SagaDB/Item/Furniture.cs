using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.Furniture
{
    public class Furniture
    {
        uint itemid , pictid, eventid;
        string name;
        //ushort motion1,motion2, motion3, motion4, motion5, motion6, motion7;
        ushort default_motion;
        List<ushort> motion = new List<ushort> ();
        byte type;
        ushort capacity;

        public override string ToString()
        {
            return this.name;
        }
        public Furniture()
        {

        }
        public Furniture(Furniture baseData)
        {
            itemid = baseData.itemid;

        }


        /// <summary>
        /// Furniture的ID
        /// </summary>
        public uint ItemID { get { return this.itemid; } set { this.itemid = value; } }

        /// <summary>
        /// Furniture的名字
        /// </summary>
        public string Name { get { return this.name; } set { this.name = value; } }

        /// <summary>
        /// Furniture的PictID
        /// </summary>
        public uint PictID { get { return this.pictid; } set { this.pictid = value; } }

        /// <summary>
        /// Furniture的EventID
        /// </summary>
        public uint EventID { get { return this.eventid; } set { this.eventid = value; } }


        /// <summary>
        /// 預設動作ID
        /// </summary>
        public ushort DefaultMotion { get { return this.default_motion; } set { this.default_motion = value; } }


        /// <summary>
        /// 動作ID
        /// </summary>
        public List<ushort> Motion {  get { return this.motion; }set { this.motion = value; } }

        /// <summary>
        /// 類別
        /// </summary>
        public byte Type { get { return this.type; } set { this.type = value; } }

        /// <summary>
        /// 飛空倉庫容量
        /// </summary>
        public ushort Capacity { get { return this.capacity; } set { this.capacity = value; } }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.Npc
{
    public class NPC
    {
        uint id;
        string name;
        uint mapid;
        byte x, y;

        public override string ToString()
        {
            return this.name;
        }

        /// <summary>
        /// NPC的ID
        /// </summary>
        public uint ID { get { return this.id; } set { this.id = value; } }

        /// <summary>
        /// NPC的名字
        /// </summary>
        public string Name { get { return this.name; } set { this.name = value; } }

        /// <summary>
        /// NPC所在地图
        /// </summary>
        public uint MapID { get { return this.mapid; } set { this.mapid = value; } }

        /// <summary>
        /// NPC的X坐标
        /// </summary>
        public byte X { get { return this.x; } set { this.x = value; } }

        /// <summary>
        /// NPC的Y坐标
        /// </summary>
        public byte Y { get { return this.y; } set { this.y = value; } }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using ICSharpCode.SharpZipLib.Zip;

namespace SagaDB.Map
{
    public enum MapFlags
    {
        Healing = 0x1,
        Cold = 0x2,
        Hot = 0x4,
        Wet = 0x8,
        Wrp = 0x10,
        Dominion = 0x20,
        FGarden = 0x40,
    }

    public class MapInfo
    {
        public uint id;
        public string name;
        public ushort width;
        public ushort height;

        public uint[,] canfish;
        public byte[,] walkable;
        public byte[,] holy;
        public byte[,] dark;
        public byte[,] neutral;
        public byte[,] fire;
        public byte[,] wind;
        public byte[,] water;
        public byte[,] earth;

        public Dictionary<Marionette.GatherType, int> gatherInterval = new Dictionary<SagaDB.Marionette.GatherType, int>();
        public Dictionary<uint, byte[]> events = new Dictionary<uint, byte[]>();

        public BitMask<MapFlags> Flag = new BitMask<MapFlags>(new BitMask());

        public override string ToString()
        {
            return this.name;
        }

        public bool Healing { get { return Flag.Test(MapFlags.Healing); } }
        public bool Cold { get { return Flag.Test(MapFlags.Cold); } }
        public bool Hot { get { return Flag.Test(MapFlags.Hot); } }
        public bool Wet { get { return Flag.Test(MapFlags.Wet); } }
    }    
}

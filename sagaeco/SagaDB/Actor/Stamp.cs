using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;

namespace SagaDB.Actor
{
    public enum StampGenre
    {
        Special,
        Pururu,
        Field,
        Coast,
        Wild,
        Cave,
        Snow,
        Colliery,
        Northan,
        IronSouth,
        SouthDungeon,
    }

    public enum StampSlot
    {
        One = 0x1,
        Two = 0x2,
        Three = 0x4,
        Four = 0x8,
        Five = 0x10,
        Six = 0x20,
        Seven = 0x40,
        Eight = 0x80,
        Nine = 0x100,
        Ten = 0x200,
    }

    public class Stamp
    {
        Dictionary<StampGenre, BitMask<StampSlot>> stamps = new Dictionary<StampGenre, BitMask<StampSlot>>();
        public Stamp()
        {
            stamps.Add(StampGenre.Special, new BitMask<StampSlot>());
            stamps.Add(StampGenre.Pururu, new BitMask<StampSlot>());
            stamps.Add(StampGenre.Field, new BitMask<StampSlot>());
            stamps.Add(StampGenre.Coast, new BitMask<StampSlot>());
            stamps.Add(StampGenre.Wild, new BitMask<StampSlot>());
            stamps.Add(StampGenre.Cave, new BitMask<StampSlot>());
            stamps.Add(StampGenre.Snow, new BitMask<StampSlot>());
            stamps.Add(StampGenre.Colliery, new BitMask<StampSlot>());
            stamps.Add(StampGenre.Northan, new BitMask<StampSlot>());
            stamps.Add(StampGenre.IronSouth, new BitMask<StampSlot>());
            stamps.Add(StampGenre.SouthDungeon, new BitMask<StampSlot>());
        }

        public void Dispose()
        {
            stamps = null;
        }

        public BitMask<StampSlot> this[StampGenre genre]
        {
            get
            {
                return stamps[genre];
            }
        }
    }
}

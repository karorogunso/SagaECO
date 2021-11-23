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

        NorthanUnderground,
        EastDungeon,
        BreadTreeForest,
        GodWildCave,
        SeaCave,
        LightTower,
        UndeadCity,
        MaimaiDungeon,
        TitaniaField,
        DominianField,
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

            stamps.Add(StampGenre.NorthanUnderground, new BitMask<StampSlot>());
            stamps.Add(StampGenre.EastDungeon, new BitMask<StampSlot>());
            stamps.Add(StampGenre.BreadTreeForest, new BitMask<StampSlot>());
            stamps.Add(StampGenre.GodWildCave, new BitMask<StampSlot>());
            stamps.Add(StampGenre.SeaCave, new BitMask<StampSlot>());
            stamps.Add(StampGenre.LightTower, new BitMask<StampSlot>());
            stamps.Add(StampGenre.UndeadCity, new BitMask<StampSlot>());
            stamps.Add(StampGenre.MaimaiDungeon, new BitMask<StampSlot>());
            stamps.Add(StampGenre.TitaniaField, new BitMask<StampSlot>());
            stamps.Add(StampGenre.DominianField, new BitMask<StampSlot>());
        }

        public void Dispose()
        {
            stamps = null;
        }

        public BitMask<StampSlot> this[StampGenre genre]
        {
            get
            {
                if (stamps == null)
                    stamps = new Dictionary<StampGenre, BitMask<StampSlot>>();
                return stamps[genre];
            }
        }
    }
}

using SagaDB.Actor;
using SagaLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SagaDB.Tamaire
{
    public class TamaireRental
    {
        byte levelDiff;
        uint renter, currentlender, lastlender;
        DateTime rentdue;

        public uint Renter { get { return this.renter; } set { this.renter = value; } }
        public uint CurrentLender { get { return this.currentlender; } set { this.currentlender = value; } }
        public uint LastLender { get { return this.lastlender; } set { this.lastlender = value; } }
        public byte LevelDiff { get { return this.levelDiff; } set { this.levelDiff = value; } }
        public DateTime RentDue { get { return this.rentdue; } set { this.rentdue = value; } }

        public short hp, sp, mp, atk_min, atk_max, matk_min, matk_max;
        public short def, mdef, hit_melee, hit_range, avoid_melee, avoid_range, aspd, cspd, payload, capacity;
    }
}
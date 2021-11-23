using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.DEMIC
{
    public class Chip
    {
        public class BaseData
        {
            public short chipID;
            public string name;
            public uint itemID;
            public byte type;
            public Model model;
            public byte possibleLv;
            public short engageTaskChip;
            public short hp, mp, sp;
            public short str, mag, vit, dex, agi, intel;
            public uint skill1, skill2, skill3;

            public override string ToString()
            {
                return name;
            }
        }

        BaseData data;
        byte x, y;

        public Chip(BaseData baseData)
        {
            this.data = baseData;
        }

        public short ChipID
        {
            get { return data.chipID; }
        }

        public uint ItemID
        {
            get { return data.itemID; }
        }

        public BaseData Data
        {
            get
            {
                return data;
            }
        }

        public Model Model { get { return data.model; } }

        public byte X { get { return this.x; } set { this.x = value; } }

        public byte Y { get { return this.y; } set { this.y = value; } }

        public bool IsNear(byte x, byte y)
        {
            foreach (byte[] i in this.Model.Cells)
            {
                int X = this.x + i[0] - this.Model.CenterX;
                int Y = this.y + i[1] - this.Model.CenterY;

                if ((X == x + 1) && (Y == y))
                    return true;
                if ((X == x - 1) && (Y == y))
                    return true;
                if ((X == x) && (Y == y + 1))
                    return true;
                if ((X == x) && (Y == y - 1))
                    return true;
            }
            return false;
        }

        public override string ToString()
        {
            return data.name;
        }
    }
}

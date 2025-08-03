using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.DEMIC
{
    public class DEMICPanel
    {
        List<Chip> chips = new List<Chip>();

        public List<Chip> Chips { get { return chips; } set { this.chips = value; } }

        byte engageTask1 = 255;
        byte engageTask2 = 255;

        public byte EngageTask1 { get { return this.engageTask1; } set { this.engageTask1 = value; } }

        public byte EngageTask2 { get { return this.engageTask2; } set { this.engageTask2 = value; } }
    }
}

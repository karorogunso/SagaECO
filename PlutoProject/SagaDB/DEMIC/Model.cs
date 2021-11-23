using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.DEMIC
{
    public class Model
    {
        List<byte[]> cells = new List<byte[]>();
        uint id;

        public uint ID { get { return this.id; } set { this.id = value; } }

        byte centerX, centerY;

        public byte CenterX { get { return this.centerX; } set { this.centerX = value; } }
        public byte CenterY { get { return this.centerY; } set { this.centerY = value; } }

        public List<byte[]> Cells { get { return cells; } }
    }
}

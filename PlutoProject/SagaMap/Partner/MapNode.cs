using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaMap.Partner
{
    public class MapNode
    {
        public int G, H, F;
        public MapNode Previous;
        public byte x, y;
    }
}

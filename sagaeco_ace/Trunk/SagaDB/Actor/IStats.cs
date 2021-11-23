using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.Actor
{
    public interface IStats
    {
        ushort Str { get; set; }
        ushort Dex { get; set; }
        ushort Int { get; set; }
        ushort Vit { get; set; }
        ushort Agi { get; set; }
        ushort Mag { get; set; }
    }
}

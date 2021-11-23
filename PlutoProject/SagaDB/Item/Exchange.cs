using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaDB.Item
{
    public class Exchange
    {
        public short SelectID { get; set; }
        public short Returnable { get; set; }
        public uint OriItemID { get; set; }
        public uint[] ItemsID { get; set; }
        public short Type { get; set; }
        public string OriItemName { get; set; }
        public string[] ItemsName { get; set; }
    }
}

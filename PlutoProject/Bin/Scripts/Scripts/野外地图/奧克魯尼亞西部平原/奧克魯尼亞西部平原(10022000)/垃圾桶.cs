using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞西部平原(10022000) NPC基本信息:垃圾桶
namespace SagaScript.M10022000
{
    public class S12000003 : 垃圾桶
    {
        public S12000003()
        {
            this.EventID = 12000003;
        }
    }

    public class S12000004 : 垃圾桶
    {
        public S12000004()
        {
            this.EventID = 12000004;
        }
    }
}

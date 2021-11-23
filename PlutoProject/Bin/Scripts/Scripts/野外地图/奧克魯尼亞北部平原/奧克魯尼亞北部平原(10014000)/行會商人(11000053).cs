using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaDB.Item;
//所在地圖:奧克魯尼亞北部平原(10014000) NPC基本信息:行會商人(11000053) X:40 Y:135
namespace SagaScript.M10014000
{
    public class S11000053 : 行會商人
    {
        public S11000053()
        {
            Init(11000053, 1, 0, WarehousePlace.Acropolis);
        }
    }
}

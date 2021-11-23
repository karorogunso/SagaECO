using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaDB.Item;
//所在地圖:奧克魯尼亞西部平原(10022000) NPC基本信息:行會商人(11000051) X:40 Y:135
namespace SagaScript.M10022000
{
    public class S11000051 : 行會商人
    {
        public S11000051()
        {
            Init(11000051, 1, 0, WarehousePlace.Acropolis);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaDB.Item;
//所在地圖:奧克魯尼亞南部平原(10031000) NPC基本信息:行會商人(11000052) X:120 Y:40
namespace SagaScript.M10031000
{
    public class S11000052 : 行會商人
    {
        public S11000052()
        {
            Init(11000052, 1, 0, WarehousePlace.Acropolis);
        }
    }
}

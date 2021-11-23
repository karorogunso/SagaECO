using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaDB.Item;
//所在地圖:奧克魯尼亞東部平原(10025000) NPC基本信息:行會商人(11000050) X:40 Y:135
namespace SagaScript.M10025000
{
    public class S11000050 : 行會商人
    {
        public S11000050()
        {
            Init(11000050, 1, 0, WarehousePlace.Acropolis);
        }
    }
}

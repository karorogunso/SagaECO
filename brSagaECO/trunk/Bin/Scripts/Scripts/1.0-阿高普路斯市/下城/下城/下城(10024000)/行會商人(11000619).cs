using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaDB.Item;
//所在地圖:下城(10024000) NPC基本信息:行會商人(11000619) X:141 Y:168
namespace SagaScript.M10024000
{
    public class S11000619 : 行會商人
    {
        public S11000619()
        {
            Init(11000619, 1, 0, WarehousePlace.Acropolis);
        }
    }
}

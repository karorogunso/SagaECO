using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:行會商人(11000618) X:110 Y:65
namespace SagaScript.M10023000
{
    public class S11000618 : 行會商人
    {
        public S11000618()
        {
            Init(11000618, 1, 0, WarehousePlace.Acropolis);
        }
    }
}

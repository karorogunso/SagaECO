using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaDB.Item;
//所在地圖:上城南邊吊橋(10023300) NPC基本信息:行會商人(11000626) X:122 Y:240
namespace SagaScript.M10023300
{
    public class S11000626 : 行會商人
    {
        public S11000626()
        {
            Init(11000626, 1, 0, WarehousePlace.Acropolis);
        }
    }
}

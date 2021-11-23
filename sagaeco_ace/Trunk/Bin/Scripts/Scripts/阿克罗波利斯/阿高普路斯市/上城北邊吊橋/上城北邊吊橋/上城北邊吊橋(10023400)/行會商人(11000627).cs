using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaDB.Item;
//所在地圖:上城北邊吊橋(10023400) NPC基本信息:行會商人(11000627) X:133 Y:16
namespace SagaScript.M10023400
{
    public class S11000627 : 行會商人
    {
        public S11000627()
        {
            Init(11000627, 1, 0, WarehousePlace.Acropolis);
        }
    }
}

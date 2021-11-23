using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城南邊吊橋(10023300) NPC基本信息:道具票交換機
namespace SagaScript.M10023300
{
    public class S12001018 : 道具票交換機
    {
        public S12001018()
        {
            this.EventID = 12001018;
        }
    }

    public class S12001019 : 道具票交換機
    {
        public S12001019()
        {
            this.EventID = 12001019;
        }
    }
}

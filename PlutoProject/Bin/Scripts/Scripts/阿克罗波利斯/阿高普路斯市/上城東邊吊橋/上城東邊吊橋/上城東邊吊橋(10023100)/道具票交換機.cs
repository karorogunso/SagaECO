using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城東邊吊橋(10023100) NPC基本信息:道具票交換機
namespace SagaScript.M10023100
{
    public class S12001016 : 道具票交換機
    {
        public S12001016()
        {
            this.EventID = 12001016;
        }
    }

    public class S12001017 : 道具票交換機
    {
        public S12001017()
        {
            this.EventID = 12001017;
        }
    }
}

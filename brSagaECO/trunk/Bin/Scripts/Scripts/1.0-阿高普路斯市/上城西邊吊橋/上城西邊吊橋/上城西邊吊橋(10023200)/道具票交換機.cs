using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城西邊吊橋(10023200) NPC基本信息:道具票交換機
namespace SagaScript.M10023200
{
    public class S12001012 : 道具票交換機
    {
        public S12001012()
        {
            this.EventID = 12001012;
        }
    }

    public class S12001013 : 道具票交換機
    {
        public S12001013()
        {
            this.EventID = 12001013;
        }
    }
}

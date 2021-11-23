using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城北邊吊橋(10023400) NPC基本信息:道具票交換機
namespace SagaScript.M10023400
{
    public class S12001014 : 道具票交換機
    {
        public S12001014()
        {
            this.EventID = 12001014;
        }
    }

    public class S12001015 : 道具票交換機
    {
        public S12001015()
        {
            this.EventID = 12001015;
        }
    }
}

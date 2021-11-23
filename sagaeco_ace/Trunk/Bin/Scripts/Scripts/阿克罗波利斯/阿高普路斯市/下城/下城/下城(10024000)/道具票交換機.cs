using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:道具票交換機
namespace SagaScript.M10024000
{
    public class S12001020 : 道具票交換機
    {
        public S12001020()
        {
            this.EventID = 12001020;
        }
    }

    public class S12001021 : 道具票交換機
    {
        public S12001021()
        {
            this.EventID = 12001021;
        }
    }

    public class S12001022 : 道具票交換機
    {
        public S12001022()
        {
            this.EventID = 12001022;
        }
    }

    public class S12001023 : 道具票交換機
    {
        public S12001023()
        {
            this.EventID = 12001023;
        }
    }
}

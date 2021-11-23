using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:東方海角(10018101) NPC基本信息:道具票交換機(12002071) X:197 Y:70
namespace SagaScript.M10018101
{
    public class S12002071 : 道具票交換機
    {
        public S12002071()
        {
            this.EventID = 12002071;
        }
    }
}

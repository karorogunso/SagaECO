using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城東邊吊橋(10023100) NPC基本信息:垃圾桶
namespace SagaScript.M10023100
{
    public class S12000014 : 垃圾桶 
    {
        public S12000014() 
        { 
            this.EventID = 12000014; 
        } 
    }

    public class S12000016 : 垃圾桶 
    {
        public S12000016() 
        { 
            this.EventID = 12000016; 
        } 
    }
}

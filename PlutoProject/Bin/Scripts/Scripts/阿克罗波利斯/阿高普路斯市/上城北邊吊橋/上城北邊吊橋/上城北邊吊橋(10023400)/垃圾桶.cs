using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城北邊吊橋(10023400) NPC基本信息:垃圾桶
namespace SagaScript.M10023400
{
    public class S12000013 : 垃圾桶 
    {
        public S12000013() 
        { 
            this.EventID = 12000013; 
        } 
    }

    public class S12000015 : 垃圾桶 
    {
        public S12000015() 
        { 
            this.EventID = 12000015; 
        } 
    }
}
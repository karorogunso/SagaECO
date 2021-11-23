using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城南邊吊橋(10023300) NPC基本信息:垃圾桶
namespace SagaScript.M10023300
{
    public class S12000010 : 垃圾桶 
    {
        public S12000010() 
        { 
            this.EventID = 12000010; 
        } 
    }

    public class S12000012 : 垃圾桶 
    {
        public S12000012() 
        { 
            this.EventID = 12000012; 
        } 
    }
}

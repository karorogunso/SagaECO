using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞北部平原(10014000) NPC基本信息:道具精製師(11000121) X:120 Y:280
namespace SagaScript.M10014000
{
    public class S11000121 : 道具精製師 
    {
        public S11000121() 
        { 
            this.EventID = 11000121; 
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞西部平原(10022000) NPC基本信息:道具精製師(11000119) X:208 Y:135
namespace SagaScript.M10022000
{
    public class S11000119 : 道具精製師 
    {
        public S11000119() 
        { 
            this.EventID = 11000119; 
        } 
    }
}
using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞東部平原(10025000) NPC基本信息:道具精製師(11000118) X:47 Y:120
namespace SagaScript.M10025000
{
    public class S11000118 : 道具精製師 
    {
        public S11000118() 
        { 
            this.EventID = 11000118; 
        } 
    }
}
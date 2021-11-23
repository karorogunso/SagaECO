using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞南部平原(10031000) NPC基本信息:道具精製師(11000120) X:135 Y:47
namespace SagaScript.M10031000
{
    public class S11000120 : 道具精製師 
    {
        public S11000120() 
        { 
            this.EventID = 11000120; 
        } 
    }
}
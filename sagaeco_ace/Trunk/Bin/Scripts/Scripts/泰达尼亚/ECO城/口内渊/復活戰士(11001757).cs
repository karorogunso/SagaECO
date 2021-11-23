using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
//所在地圖:口内渊(21190000) NPC基本信息:復活戰士(11001757) X:133 Y:5
namespace SagaScript.M10023400
{
    public class S11001757 : 復活戰士 
    {
        public S11001757() 
        {
            byte x, y;

            x = (byte)Global.Random.Next(31, 33);
            y = (byte)Global.Random.Next(164, 165);

            Init(11001757, 21190000, x, y); 
        } 
    }
}

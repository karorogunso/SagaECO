using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
//所在地圖:上城西邊吊橋(10023200) NPC基本信息:復活戰士(11000348) X:5 Y:122
namespace SagaScript.M10023200
{
    public class S11000348 : 復活戰士
    {
        public S11000348()
        {
            byte x, y;

            x = (byte)Global.Random.Next(2, 8);
            y = (byte)Global.Random.Next(123, 131);

            Init(11000348, 10023200, x, y);
        }
    }
}

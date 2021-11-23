using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
//所在地圖:上城南邊吊橋(10023300) NPC基本信息:復活戰士(11000349) X:122 Y:251
namespace SagaScript.M10023300
{
    public class S11000349 : 復活戰士
    {
        public S11000349()
        {
            byte x, y;

            x = (byte)Global.Random.Next(123, 129);
            y = (byte)Global.Random.Next(245, 253);

            Init(11000349, 10023300, x, y);
        }
    }
}

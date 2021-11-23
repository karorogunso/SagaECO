using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
//所在地圖:上城東邊吊橋(10023100) NPC基本信息:復活戰士(11000347) X:250 Y:133
namespace SagaScript.M10023100
{
    public class S11000347 : 復活戰士 
    {
        public S11000347() 
        {
            byte x, y;

            x = (byte)Global.Random.Next(245, 253);
            y = (byte)Global.Random.Next(122, 133);

            Init(11000347, 10023100, x, y); 
        } 
    }
}

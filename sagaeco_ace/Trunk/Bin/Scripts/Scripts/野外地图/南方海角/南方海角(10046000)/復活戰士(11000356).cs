using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M10046000
{
    public class S11000356 : 復活戰士
    {
        public S11000356()
        {
            byte x, y;
            //153,231
            x = (byte)Global.Random.Next(151, 155);
            y = (byte)Global.Random.Next(225, 230);

            Init(11000356, 10046000, x, y);
        }
    }
}

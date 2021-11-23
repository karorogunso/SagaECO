using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M10001000
{
    public class S11000351 : 復活戰士
    {
        public S11000351()
        {
            byte x, y;
            //102,24
            x = (byte)Global.Random.Next(98, 101);
            y = (byte)Global.Random.Next(22, 26);

            Init(11000351, 10001000, x, y);
        }
    }
}

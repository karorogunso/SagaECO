using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M10032000
{
    public class S11000354 : 復活戰士
    {
        public S11000354()
        {
            byte x, y;
            //139,110
            x = (byte)Global.Random.Next(137, 141);
            y = (byte)Global.Random.Next(112, 114);

            Init(11000354, 10032000, x, y);
        }
    }
}

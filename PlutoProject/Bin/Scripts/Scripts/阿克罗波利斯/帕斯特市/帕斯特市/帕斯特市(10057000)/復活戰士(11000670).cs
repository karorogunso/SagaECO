using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M10057000
{
    public class S11000670 : 復活戰士
    {
        public S11000670()
        {
            byte x, y;
            //5,123
            x = (byte)Global.Random.Next(6, 9);
            y = (byte)Global.Random.Next(120, 125);

            Init(11000670, 10057000, x, y);
        }
    }
}

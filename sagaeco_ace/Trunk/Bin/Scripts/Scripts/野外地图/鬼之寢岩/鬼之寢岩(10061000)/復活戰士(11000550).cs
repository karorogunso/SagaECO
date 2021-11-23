using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M10061000
{
    public class S11000550 : 復活戰士
    {
        public S11000550()
        {
            byte x, y;
            //138,49
            x = (byte)Global.Random.Next(143, 147);
            y = (byte)Global.Random.Next(45, 48);

            Init(11000550, 10061000, x, y);
        }
    }
}

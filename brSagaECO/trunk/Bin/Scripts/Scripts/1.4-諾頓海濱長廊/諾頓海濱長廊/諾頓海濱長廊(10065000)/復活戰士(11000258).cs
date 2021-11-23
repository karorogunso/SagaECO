using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M10065000
{
    public class S11000258 : 復活戰士
    {
        public S11000258()
        {
            byte x, y;
            //43,121
            x = (byte)Global.Random.Next(44, 46);
            y = (byte)Global.Random.Next(120, 122);

            Init(11000258, 10065000, x, y);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M10063100
{
    public class S11000554 : 復活戰士
    {
        public S11000554()
        {
            byte x, y;
            //132,153
            x = (byte)Global.Random.Next(130, 135);
            y = (byte)Global.Random.Next(155, 157);

            Init(11000554, 10063100, x, y);
        }
    }
}

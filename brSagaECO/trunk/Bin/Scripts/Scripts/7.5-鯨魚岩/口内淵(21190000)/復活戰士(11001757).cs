using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M10063100
{
    public class S11001757 : 復活戰士
    {
        public S11001757()
        {
            byte x, y;
            //37,125
            x = (byte)Global.Random.Next(36, 38);
            y = (byte)Global.Random.Next(125, 127);

            Init(11000554, 10063100, x, y);
        }
    }
}
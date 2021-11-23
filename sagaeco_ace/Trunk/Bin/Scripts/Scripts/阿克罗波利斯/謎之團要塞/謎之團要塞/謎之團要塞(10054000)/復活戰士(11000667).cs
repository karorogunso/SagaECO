using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M10063100
{
    public class S11000667 : 復活戰士
    {
        public S11000667()
        {
            byte x, y;
            //184,140
            x = (byte)Global.Random.Next(186, 187);
            y = (byte)Global.Random.Next(135, 139);

            Init(11000667, 10054000, x, y);
        }
    }
}

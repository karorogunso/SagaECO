using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M10018100
{
    public class S11000352 : 復活戰士
    {
        public S11000352()
        {
            byte x, y;
            //232,89
            x = (byte)Global.Random.Next(227, 230);
            y = (byte)Global.Random.Next(88, 92);

            Init(11000352, 10018100, x, y);
        }
    }
}

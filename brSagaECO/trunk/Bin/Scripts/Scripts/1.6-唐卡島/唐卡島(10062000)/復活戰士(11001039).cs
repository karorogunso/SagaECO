using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M10062000
{
    public class S11001039 : 復活戰士
    {
        public S11001039()
        {
            byte x, y;
            //85,207
            x = (byte)Global.Random.Next(84, 87);
            y = (byte)Global.Random.Next(203, 206);

            Init(11001039, 10062000, x, y);
        }
    }
}

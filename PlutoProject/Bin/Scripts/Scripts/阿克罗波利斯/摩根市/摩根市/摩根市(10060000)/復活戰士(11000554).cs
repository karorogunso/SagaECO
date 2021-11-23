using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M10060000
{
    public class S11000845 : 復活戰士
    {
        public S11000845()
        {
            byte x, y;

            x = (byte)Global.Random.Next(161, 165);
            y = (byte)Global.Random.Next(145, 149);

            Init(11000845, 10060000, x, y);
        }
    }
}

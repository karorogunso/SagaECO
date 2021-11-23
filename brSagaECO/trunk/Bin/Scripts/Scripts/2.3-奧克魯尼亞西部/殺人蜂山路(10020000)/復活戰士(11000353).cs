using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M10020000
{
    public class S11000353 : 復活戰士 
    {
        public S11000353() 
        {
            byte x, y;

            x = (byte)Global.Random.Next(105, 108);
            y = (byte)Global.Random.Next(74, 76);

            Init(11000353, 10020000, x, y); 
        } 
    }
}

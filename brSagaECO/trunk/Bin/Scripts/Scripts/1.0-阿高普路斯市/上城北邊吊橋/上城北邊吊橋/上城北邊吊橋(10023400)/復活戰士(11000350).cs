using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
//所在地圖:上城北邊吊橋(10023400) NPC基本信息:復活戰士(11000350) X:133 Y:5
namespace SagaScript.M10023400
{
    public class S11000350 : 復活戰士 
    {
        public S11000350() 
        {
            byte x, y;

            x = (byte)Global.Random.Next(128, 132);
            y = (byte)Global.Random.Next(7, 8);
 
            Init(11000350, 10023400, x, y); 
        } 
    }
}

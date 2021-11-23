using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript
{
    public class P10000310 : RandomPortal
    {
        public P10000310()
        {
            Init(10000310, 10058000, 2, 3, 2, 3);
        }
    }
    //原始地圖:升降機室(20170000)
    //目標地圖:通天之塔(10058000)
    //目標坐標:(2,3) ~ (2,3)

    public class P10000311 : RandomPortal
    {
        public P10000311()
        {
            Init(10000311, 20170000, 31, 17, 31, 17);
        }
    }
    //原始地圖:通天之塔(10058000)
    //目標地圖:升降機室(20170000)
    //目標坐標:(31,17) ~ (31,17)
}
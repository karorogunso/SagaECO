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
    public class P10000812 : RandomPortal
    {
        public P10000812()
        {
            Init(10000812, 10057002, 2, 138, 4, 141);
        }
    }
    //原始地圖:牛牛草原(10056000)
    //目標地圖:帕斯特市(10057000)
    //目標坐標:(2,138) ~ (4,141)

    public class P10000816 : RandomPortal
    {
        public P10000816()
        {
            Init(10000816, 10057002, 166, 2, 169, 4);
        }
    }
    //原始地圖:謎之團要塞(10054001)
    //目標地圖:帕斯特市(10057000)
    //目標坐標:(166,2) ~ (169,4)

}
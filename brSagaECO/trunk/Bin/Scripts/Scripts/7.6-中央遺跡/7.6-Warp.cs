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
    public class P10001111 : RandomPortal
    {
        public P10001111()
        {
            Init(10001111, 20080014, 22, 28, 28, 41);
        }
    }
    //原始地圖:阿伊恩市上層(10063100)
    //目標地圖:遺跡入口前廣場(20080014)
    //目標坐標:(22,28) ~ (28,41)

    public class P10001112 : RandomPortal
    {
        public P10001112()
        {
            Init(10001112, 10024000, 131, 55, 137, 59);
        }
    }
    //原始地圖:遺跡入口前廣場(20080012)
    //目標地圖:下城(10024000)
    //目標坐標:(131,55) ~ (137,59)

    public class P10001113 : RandomPortal
    {
        public P10001113()
        {
            Init(10001113, 10063100, 47, 148, 50, 152);
        }
    }
    //原始地圖:遺跡入口前廣場(20080014)
    //目標地圖:阿伊恩市上層(10063100)
    //目標坐標:(47,148) ~ (50,152)

    public class P10001114 : RandomPortal
    {
        public P10001114()
        {
            Init(10001114, 10065000, 74, 40, 78, 48);
        }
    }
    //原始地圖:遺跡入口前廣場(20080013)
    //目標地圖:諾頓海濱長廊(10065000)
    //目標坐標:(74,40) ~ (78,48)

}
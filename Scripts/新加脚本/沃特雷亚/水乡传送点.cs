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
    public class P10000308 : RandomPortal
    {
        public P10000308()
        {
            Init(10000308, 11053000, 217, 19, 218, 20);
        }
    }
    //原始地圖:海底洞窟(21180000)
    //目標地圖:飄流水鄉(11053000)
    //目標坐標:(217,19) ~ (218,20)

    public class P10001553 : RandomPortal
    {
        public P10001553()
        {
            Init(10001553, 11053000, 100, 130, 105, 135);
        }
    }
    //原始地圖:
    //目標地圖:飄流水鄉(11053000)
    //目標坐標:(100,130) ~ (105,135)

    public class P10001554 : RandomPortal
    {
        public P10001554()
        {
            Init(10001554, 21003000, 31, 49, 32, 50);
        }
    }
    //原始地圖:
    //目標地圖:美人魚之家(21003000)
    //目標坐標:(31,49) ~ (32,50)

    public class P10001555 : RandomPortal
    {
        public P10001555()
        {
            Init(10001555, 11053000, 71, 108, 72, 109);
        }
    }
    //原始地圖:
    //目標地圖:飄流水鄉(11053000)
    //目標坐標:(71,108) ~ (72,109)

    public class P10001556 : RandomPortal
    {
        public P10001556()
        {
            Init(10001556, 21003000, 107, 49, 108, 50);
        }
    }
    //原始地圖:
    //目標地圖:美人魚之家(21003000)
    //目標坐標:(107,49) ~ (108,50)

    public class P10001557 : RandomPortal
    {
        public P10001557()
        {
            Init(10001557, 11053000, 176, 112, 177, 113);
        }
    }
    //原始地圖:
    //目標地圖:飄流水鄉(11053000)
    //目標坐標:(176,112) ~ (177,113)

    public class P10001558 : RandomPortal
    {
        public P10001558()
        {
            Init(10001558, 21003000, 63, 103, 64, 104);
        }
    }
    //原始地圖:
    //目標地圖:美人魚之家(21003000)
    //目標坐標:(63,103) ~ (64,104)

    public class P10001559 : RandomPortal
    {
        public P10001559()
        {
            Init(10001559, 11053000, 112, 170, 113, 171);
        }
    }
    //原始地圖:
    //目標地圖:飄流水鄉(11053000)
    //目標坐標:(112,170) ~ (113,171)


    public class P10001568 : Event
    {
        public P10001568()
        {
            this.EventID = 10001568;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 0, "入口太小了，似乎进不去。$R;", "");
            //Init(10001568, 21003000, 81, 48, 83, 50);
        }
    }

    public class P10001550 : RandomPortal
    {
        public P10001550()
        {
            Init(10001550, 11053000, 215, 19, 221, 20);
        }
    }
    //原始地圖:
    //目標地圖:飄流水鄉(11053000)
    //目標坐標:(215,19) ~ (221,20)

}
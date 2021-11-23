
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using System.Text;
using System.Diagnostics;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace WeeklyExploration
{
    public class P10000954 : RandomPortal
    {
        public P10000954()
        {
            Init(10000954, 30090000, 4, 7, 4, 7);
        }
    }
    //原始地圖:牛牛草原(10056000)
    //目標地圖:寵物小屋(30090000)
    //目標坐標:(4,7) ~ (4,7)

    public class P10000955 : RandomPortal
    {
        public P10000955()
        {
            Init(10000955, 10056000, 180, 56, 180, 56);
        }
    }
    //原始地圖:寵物小屋(30090000)
    //目標地圖:牛牛草原(10056000)
    //目標坐標:(180,56) ~ (180,56)
    public class P10000814 : RandomPortal
    {
        public P10000814()
        {
            Init(10000814, 10054001, 2, 202, 4, 205);
        }
    }
    //原始地圖:牛牛草原(10056000)
    //目標地圖:謎之團要塞(10054001)
    //目標坐標:(2,202) ~ (4,205)

    public class P10000815 : RandomPortal
    {
        public P10000815()
        {
            Init(10000815, 10056000, 207, 2, 210, 4);
        }
    }
    //原始地圖:謎之團要塞(10054001)
    //目標地圖:牛牛草原(10056000)
    //目標坐標:(207,2) ~ (210,4)
    public class S10001817 : Event
    {
        public S10001817()
        {
            this.EventID = 10001817;
        }


        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 11073000, 99, 9);
        }
    }

    public class S10001816 : Event
    {
        public S10001816()
        {
            this.EventID = 10001816;
        }


        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 11073000, 236, 9);
        }
    }
    public class S10001818 : Event
    {
        public S10001818()
        {
            this.EventID = 10001818;
        }


        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 11073000, 2, 107);
        }
    }
    public class S10001819 : Event
    {
        public S10001819()
        {
            this.EventID = 10001819;
        }


        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 11025000, 253, 121);
        }
    }
}


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
    public class P10000665 : Event
    {
        public P10000665()
        {
            this.EventID = 10000665;
        }

        public override void OnEvent(ActorPC pc)
        {
                Warp(pc, 10061000, 145, 3);
                return;

        }
    }
    /*public class P10000667 : RandomPortal
    {
        public P10000667()
        {
            Init(10000667, 10064000, 185, 2, 188, 4);
        }
    }*/
    //原始地圖:鬼之寢岩(10061000)
    //目標地圖:鐵火山(10064000)
    //目標坐標:(185,2) ~ (188,4)

    public class P10000668 : RandomPortal
    {
        public P10000668()
        {
            Init(10000668, 10061000, 185, 251, 188, 253);
        }
    }
    //原始地圖:鐵火山(10064000)
    //目標地圖:鬼之寢岩(10061000)
    //目標坐標:(185,251) ~ (188,253)

    /*public class P10000669 : RandomPortal
    {
        public P10000669()
        {
            Init(10000669, 10064000, 26, 2, 29, 4);
        }
    }*/
    //原始地圖:鬼之寢岩(10061000)
    //目標地圖:鐵火山(10064000)
    //目標坐標:(26,2) ~ (29,4)

    public class P10000670 : RandomPortal
    {
        public P10000670()
        {
            Init(10000670, 10061000, 26, 251, 29, 253);
        }
    }
    //原始地圖:鐵火山(10064000)
    //目標地圖:鬼之寢岩(10061000)
    //目標坐標:(26,251) ~ (29,253)

    public class P10000671 : RandomPortal
    {
        public P10000671()
        {
            Init(10000671, 10063000, 251, 38, 253, 41);
        }
    }
    //原始地圖:鐵火山(10064000)
    //目標地圖:阿伊恩薩烏斯路(10063000)
    //目標坐標:(251,38) ~ (253,41)

    public class P10000672 : RandomPortal
    {
        public P10000672()
        {
            Init(10000672, 10064000, 2, 38, 4, 41);
        }
    }
    //原始地圖:阿伊恩薩烏斯路(10063000)
    //目標地圖:鐵火山(10064000)
    //目標坐標:(2,38) ~ (4,41)

    public class P10000673 : RandomPortal
    {
        public P10000673()
        {
            Init(10000673, 10063000, 251, 206, 253, 209);
        }
    }
    //原始地圖:鐵火山(10064000)
    //目標地圖:阿伊恩薩烏斯路(10063000)
    //目標坐標:(251,206) ~ (253,209)

    public class P10000674 : RandomPortal
    {
        public P10000674()
        {
            Init(10000674, 10064000, 2, 207, 4, 210);
        }
    }
    //原始地圖:阿伊恩薩烏斯路(10063000)
    //目標地圖:鐵火山(10064000)
    //目標坐標:(2,207) ~ (4,210)

    public class P10000675 : RandomPortal
    {
        public P10000675()
        {
            Init(10000675, 10063100, 140, 158, 141, 159);
        }
    }
    //原始地圖:阿伊恩薩烏斯路(10063000)
    //目標地圖:南牢
    public class P10000692 : RandomPortal
    {
        public P10000692()
        {
            Init(10000692, 10063000, 160, 157, 160, 157);
        }
    }
    //原始地圖:南牢
    //目標地圖:阿伊恩薩烏斯路(10063000)
    public class P10000694 : RandomPortal
    {
        public P10000694()
        {
            Init(10000694, 10063000, 160, 157, 160, 157);
        }
    }

    public class P10000695 : RandomPortal
    {
        public P10000695()
        {
            Init(10000695, 20022000, 126, 241, 129, 243);
        }
    }
    //原始地圖:南部地牢B2F(20021000)
    //目標地圖:南部地牢B3F(20022000)
    //目標坐標:(126,241) ~ (129,243)

    public class P10000696 : RandomPortal
    {
        public P10000696()
        {
            Init(10000696, 20021000, 179, 73, 182, 75);
        }
    }
    //原始地圖:南部地牢B1F(20020000)
    //目標地圖:南部地牢B2F(20021000)
    //目標坐標:(179,73) ~ (182,75)

    public class P10000697 : RandomPortal
    {
        public P10000697()
        {
            Init(10000697, 20020000, 180, 70, 182, 73);
        }
    }
    //原始地圖:南部地牢B2F(20021000)
    //目標地圖:南部地牢B1F(20020000)
    //目標坐標:(180,70) ~ (182,73)
    public class P10000701 : RandomPortal
    {
        public P10000701()
        {
            Init(10000701, 20021000, 126, 241, 129, 243);
        }
    }
    //原始地圖:南部地牢B3F(20022000)
    //目標地圖:南部地牢B2F(20021000)
    //目標坐標:(126,241) ~ (129,243)
}
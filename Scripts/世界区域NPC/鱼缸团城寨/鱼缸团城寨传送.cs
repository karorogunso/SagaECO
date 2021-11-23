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
    public class P10000817 : Event
    {
        public P10000817()
        {
            this.EventID = 10000817;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "有通往下城的階梯唷$R;");
            switch (Select(pc, "怎麼辦呢？", "", "去海盜島(暂未开放)", "去中立島(暂未开放)", "去聖女島", "不去"))
            {
                case 1:
                    return;
                    Warp(pc, 10054100, 123, 77);
                    break;
                case 2:
                    return;
                    Warp(pc, 10054100, 224, 86);
                    break;

                case 3:
                    Warp(pc, 10054000, 72, 140);
                    break;
            }
        }
    }
    public class P10000979 : RandomPortal
    {
        public P10000979()
        {
            Init(10000979, 10054001, 153, 165, 154, 166);
        }
    }
    //原始地圖:謎之團要塞(10054000)
    //目標地圖:謎之團要塞(10054001)
    //目標坐標:(153,165) ~ (154,166)

    public class P10000807 : RandomPortal
    {
        public P10000807()
        {
            Init(10000807, 10054000, 41, 139, 41, 140);
        }
    }
    //原始地圖:約克之家(30090001)
    //目標地圖:謎之團要塞(10054000)
    //目標坐標:(41,139) ~ (41,140)

    public class P10000808 : RandomPortal
    {
        public P10000808()
        {
            Init(10000808, 30090001, 4, 7, 4, 7);
        }
    }
    //原始地圖:謎之團要塞(10054000)
    //目標地圖:約克之家(30090001)
    //目標坐標:(4,7) ~ (4,7)

    /* public class P12001118 : Event
    {
        public P12001118()
        {
            this.EventID = 12001118;
        }
        public override void OnEvent(ActorPC pc)
        {
            //Warp(pc, 30131001, 6, 1);
        }
    }
        public class P10000968 : RandomPortal
    {
        public P10000968()
        {
            Init(10000968, 30013002, 3, 5, 3, 5);
        }
    }
    */
    public class P10000968 : RandomPortal
    {
        public P10000968()
        {
            Init(10000968, 30013002, 3, 5, 3, 5);
        }
    }
    //原始地圖:謎之團要塞(10054000)
    //目標地圖:要塞的武器製造所(30013002)
    //目標坐標:(3,5) ~ (3,5)

    public class P10000969 : RandomPortal
    {
        public P10000969()
        {
            Init(10000969, 10054000, 171, 137, 171, 137);
        }
    }
    //原始地圖:要塞的武器製造所(30013002)
    //目標地圖:謎之團要塞(10054000)
    //目標坐標:(171,137) ~ (171,137)

    public class P10000970 : RandomPortal
    {
        public P10000970()
        {
            Init(10000970, 30020005, 3, 5, 3, 5);
        }
    }

    //原始地圖:
    //目標地圖:謎之團要塞(10054000)
    //目標坐標:(156,138) ~ (157,138)
    public class P10001030 : RandomPortal
    {
        public P10001030()
        {
            Init(10001030, 10054000, 156, 138, 157, 138);
        }
    }
    //原始地圖:
    //目標地圖:謎之團要塞(10054000)
    //目標坐標:(156,138) ~ (157,138)
    public class S10001031 : Event
    {
        public S10001031()
        {
            EventID = 10001031;
        }
        public override void OnEvent(ActorPC pc)
        {
            pc.TInt["TempBGM"] = 1024;
            Warp(pc, 30131001, 6, 10);
        }
    }
    //原始地圖:謎之團要塞(10054000)
    //目標地圖:謎之團本部(30131001)
    //目標坐標:(5,9) ~ (7,10)

    public class P10000962 : RandomPortal
    {
        public P10000962()
        {
            Init(10000962, 30010007, 3, 5, 3, 5);
        }
    }
    //原始地圖:謎之團要塞(10054000)
    //目標地圖:要塞的咖啡館(30010007)
    //目標坐標:(3,5) ~ (3,5)

    public class P10000963 : Event
    {
        public P10000963()
        {
            this.EventID = 10000963;
        }

        public override void OnEvent(ActorPC pc)
        {
            if(pc.CInt["东牢进入任务"] == 1)
            {
                SetNextMoveEvent(pc, 100000100);//东牢进入任务-沙月
                Warp(pc, 10054000, 143, 138);
                return;
            }
            Warp(pc, 10054000, 143, 138);
        }
    }
}
    //原始地圖:要塞的咖啡館(30010007)
    //目標地圖:謎之團要塞(10054000)
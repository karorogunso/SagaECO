using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public class P10000315 : Event
    {
        public P10000315()
        {
            this.EventID = 10000315;
        }

        public override void OnEvent(ActorPC pc)
        {
            EnterFGRoom(pc);
        }
    }
    //原始地圖:飛空庭(70000000)
    //目標地圖:家之中(75000000)
    //目標坐標:(4,10) ~ (4,10)

    public class P10000316 : RandomPortal
    {
        public P10000316()
        {
            this.EventID = 10000316;
        }

        public override void OnEvent(ActorPC pc)
        {
            ExitFGRoom(pc);
        }
    }
    //原始地圖:家之中(75000000)
    //目標地圖:飛空庭(70000000)
    //目標坐標:(5,8) ~ (5,8)
}

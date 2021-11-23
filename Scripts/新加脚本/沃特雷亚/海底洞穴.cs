
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using WeeklyExploration;
using System.Globalization;
namespace 海底副本
{
    public partial class 海底副本 : Event
    {
        public 海底副本()
        {
            this.EventID = 10001549;
        }

        public override void OnEvent(ActorPC pc)
        {
                switch(Select(pc,"才不是GM特权","","进入暖流","进入寒流","离开"))
                {
                    case 1:
                        暖流(pc);
                        return;
                    case 2:
                        寒流(pc);
                        return;
                    case 3:
                        break;
                }
                if (DateTime.Now.Hour % 2 == 0)
                    暖流(pc);
                else
                    寒流(pc);
        }
        void 寒流(ActorPC pc)
        {
            Say(pc, 131, "现在是『$CC寒流$CD』$R;");
            switch (Select(pc, "要卷入寒流吗？(要求达到60级）", "", "单人前往（单人模式）", "结团前往（多人模式）", "不进去了"))
            {
                case 1:
                    寒流单人(pc);
                    break;
                case 2:
                    寒流多人(pc);
                    return;
            }
        }
        void 暖流(ActorPC pc)
        {
            Say(pc, 131, "现在是『$CO暖流$CD』$R;");
            Warp(pc, 21180001, 68, 253);
        }
    }
}
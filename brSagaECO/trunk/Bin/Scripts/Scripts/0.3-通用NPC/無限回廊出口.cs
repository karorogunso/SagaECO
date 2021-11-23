using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public abstract class 無限回廊出口 : Event
    {
        public 無限回廊出口()
        {

        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "回到入口嗎？", "", "不回去", "回去"))
            {
                case 1:
                    break;
                case 2:
                    Warp(pc, 10042000, 80, 134);
                    break;
            }
        }
    }
}
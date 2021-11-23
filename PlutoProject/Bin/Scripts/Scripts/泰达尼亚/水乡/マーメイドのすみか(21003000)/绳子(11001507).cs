using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21003000
{
    public class S11001507 : Event
    {
        public S11001507()
        {
            this.EventID = 11001507;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "要爬上去吗?", "", "爬上去", "不使用"))
            {
                case 1:
                    Warp(pc, 11053000, 147, 137);
                    break;
                case 2:
                    break;
            }
        }


    }
}



using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S13000427 : Event
    {
        public S13000427()
        {
            this.EventID = 13000427;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 90001999, 25, 82);
        }
    }
    public class S13000426 : Event
    {
        public S13000426()
        {
            this.EventID = 13000426;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 90001999, 29, 82);
        }
    }
    public class S13000428 : Event
    {
        public S13000428()
        {
            this.EventID = 13000428;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 90001999, 45, 82);
        }
    }
    public class S13000429 : Event
    {
        public S13000429()
        {
            this.EventID = 13000429;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 90001999, 41, 82);
        }
    }
    public class S13000423 : Event
    {
        public S13000423()
        {
            this.EventID = 13000423;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 90001999, 9, 62);
        }
    }
    public class S13000422 : Event
    {
        public S13000422()
        {
            this.EventID = 13000422;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 90001999, 14, 62);
        }
    }
    public class S13000424 : Event
    {
        public S13000424()
        {
            this.EventID = 13000424;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 90001999, 62, 62);
        }
    }
    public class S13000425 : Event
    {
        public S13000425()
        {
            this.EventID = 13000425;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 90001999, 57, 62);
        }
    }

    public class S10002029 : Event
    {
        public S10002029()
        {
            this.EventID = 10002029;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 90001999, 11, 48);
        }
    }
}


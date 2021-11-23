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
    //圓柱
    public class S31060000:Event
    {
        public S31060000()
        {
            this.EventID = 31060000;
        }
        public override void OnEvent(ActorPC pc)
        {
        }
    }
    //圓柱(木)
    public class S31060001 : Event
    {
        public S31060001()
        {
            this.EventID = 31060001;
        }
        public override void OnEvent(ActorPC pc)
        {
        }
    }
    //粗四角柱
    public class S31060100 : Event
    {
        public S31060100()
        {
            this.EventID = 31060100;
        }
        public override void OnEvent(ActorPC pc)
        {
        }
    }
    //粗四角柱(鐵)
    public class S31060101 : Event
    {
        public S31060101()
        {
            this.EventID = 31060101;
        }
        public override void OnEvent(ActorPC pc)
        {
        }
    }
    //活動桅杆
    public class S31060200 : Event
    {
        public S31060200()
        {
            this.EventID = 31060200;
        }
        public override void OnEvent(ActorPC pc)
        {
        }
    }
    //活動桅杆(太陽)
    public class S31060201 : Event
    {
        public S31060201()
        {
            this.EventID = 31060201;
        }
        public override void OnEvent(ActorPC pc)
        {
        }
    }
    //固定桅杆
    public class S31060300 : Event
    {
        public S31060300()
        {
            this.EventID = 31060300;
        }
        public override void OnEvent(ActorPC pc)
        {
        }
    }
    //固定桅杆(微風)
    public class S31060301 : Event
    {
        public S31060301()
        {
            this.EventID = 31060301;
        }
        public override void OnEvent(ActorPC pc)
        {
        }
    }
    //植物柱
    public class S31060400 : Event
    {
        public S31060400()
        {
            this.EventID = 31060400;
        }
        public override void OnEvent(ActorPC pc)
        {
        }
    }
}
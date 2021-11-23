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
    //圓木椅
    public class S31090000 : ActorFurniture
    {
        public S31090000()
        {
            this.ItemID = 31090000;
            this.Motion = 135;
            this.PictID =0;
            this.Xaxis = 0;
            this.Yaxis = 0;
            this.Zaxis = 0;
            this.Z = 0;
        }
    }
    //圓木椅(紅色)
    public class S31090001 : Event
    {
        public S31090001()
        {
            this.EventID = 31090001;
        }
        public override void OnEvent(ActorPC pc)
        {
        }
    }
    //木椅
    public class S31090100 : Event
    {
        public S31090100()
        {
            this.EventID = 31090100;
        }
        public override void OnEvent(ActorPC pc)
        {
        }
    }
    //木椅(鐵版)
    public class S31090101 : Event
    {
        public S31090101()
        {
            this.EventID = 31090101;
        }
        public override void OnEvent(ActorPC pc)
        {
        }
    }
    //野餐椅
    public class S31090200 : Event
    {
        public S31090200()
        {
            this.EventID = 31090200;
        }
        public override void OnEvent(ActorPC pc)
        {

        }
    }
    //野餐椅(黑白)
    public class S31090201 : Event
    {
        public S31090201()
        {
            this.EventID = 31090201;
        }
        public override void OnEvent(ActorPC pc)
        {

        }
    }
    //野餐椅(泰迪)
    public class S31090202 : Event
    {
        public S31090202()
        {
            this.EventID = 31090202;
        }
        public override void OnEvent(ActorPC pc)
        {

        }
    }
    //安樂椅
    public class S31090300 : Event
    {
        public S31090300()
        {
            this.EventID = 31090300;
        }
        public override void OnEvent(ActorPC pc)
        {

        }
    }
    //安樂椅(黑色)
    public class S31090301 : Event
    {
        public S31090301()
        {
            this.EventID = 31090301;
        }
        public override void OnEvent(ActorPC pc)
        {

        }
    }
    //俏皮的椅子
    public class S31090400 : Event
    {
        public S31090400()
        {
            this.EventID = 31090400;
        }
        public override void OnEvent(ActorPC pc)
        {

        }
    }
    //俏皮的椅子(黃色)
    public class S31090401 : Event
    {
        public S31090401()
        {
            this.EventID = 31090401;
        }
        public override void OnEvent(ActorPC pc)
        {

        }
    }
    //蘑菇椅
    public class S31090500 : Event
    {
        public S31090500()
        {
            this.EventID = 31090500;
        }
        public override void OnEvent(ActorPC pc)
        {

        }
    }
    //山茶屋的椅子
    public class S31090600 : Event
    {
        public S31090600()
        {
            this.EventID = 31090600;
        }
        public override void OnEvent(ActorPC pc)
        {

        }
    }
    //熊猫椅
    public class S31090700 : Event
    {
        public S31090700()
        {
            this.EventID = 31090700;
        }
        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
   
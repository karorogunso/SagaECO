using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M20004002
{
    public class S12002041 : Event
    {
        public S12002041()
        {
            this.EventID = 12002041;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "曼陀蘿胡蘿蔔的日記$R;" +
                "$P今天是疲倦的一天$R;" +
                "主人給我做了營養劑$R;" +
                "$R把蜂蜜和辛香草攪勻後$R;" +
                "用未知的道具灌溉士地$R;" +
                "讓我都吸收了$R;" +
                "$P非常……非常好吃$R;");
        }
    }
}

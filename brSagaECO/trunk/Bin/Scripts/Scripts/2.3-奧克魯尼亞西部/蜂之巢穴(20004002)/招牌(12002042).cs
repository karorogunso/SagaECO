using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M20004002
{
    public class S12002042 : Event
    {
        public S12002042()
        {
            this.EventID = 12002042;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "著手開發寵物糧食好幾天了……$R;" +
                "整天忙個不停的工作…累透了$R;" +
                "$P可以做出所有寵物都喜歡的糧食嗎？$R;" +
                "$R調查了有什麼寵物喜歡$R;" +
                "有蟹粉的味道糧食$R;" +
                "$P爬爬蟲好像…$R;");
        }
    }
}

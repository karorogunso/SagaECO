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
            Say(pc, 131, "着手开发宠物粮食好几天了……$R;" +
                "整天忙个不停的工作…累透了$R;" +
                "$P可以做出所有宠物都喜欢的粮食吗？$R;" +
                "$R调查了有什么宠物喜欢$R;" +
                "有蟹粉的味道粮食$R;" +
                "$P爬爬虫好像…$R;");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10060000
{
    public class S11000833 : Event
    {
        public S11000833()
        {
            this.EventID = 11000833;

        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000064, 361, "这是法伊斯特的宠物神仙给的，$R;" +
                "发育状态很好啊。$R;" +
                "$P摩戈的气候不好、$R;" +
                "食物也不算丰富$R;" +
                "$R如果在更好的环境下成长$R;" +
                "那该多好…$R;" +
                "$R对不起了，劳佩$R;");
            Say(pc, 11000833, 361, "哞哞~哞哞~$R;");
        }
    }
}
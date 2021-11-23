using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30091000
{
    public class S11000699 : Event
    {
        public S11000699()
        {
            this.EventID = 11000699;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 160, "不可能的，我没有才能！$R;" +
                "也不像贤皮克前辈一样聪明!$R;");
            Say(pc, 11000698, 131, "你这家伙！在说什么呀！$R;" +
                "你不是想回去诺森总部吗?$R;");
            Say(pc, 160, "但是！！真的一个都不知道啊！$R;");
        }
    }
}
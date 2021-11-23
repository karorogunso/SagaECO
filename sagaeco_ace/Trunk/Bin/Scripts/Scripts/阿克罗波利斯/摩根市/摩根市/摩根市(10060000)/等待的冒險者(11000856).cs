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
    public class S11000856 : Event
    {
        public S11000856()
        {
            this.EventID = 11000856;

        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 131, "不好意思$R;" +
                "问您一件事情行吗？$R;" +
                "$R到「光之塔」的飞空庭，$R;" +
                "是在这儿乘坐吗？$R;");
            switch (Select(pc, "到「光之塔」的飞空庭，$R是在这儿乘坐吗？", "", "是的", "不是"))
            {
                case 1:
                    Say(pc, 131, "原来如此！$R;" +
                        "那些家伙们到底在干什么呢？$R;" +
                        "$R让人等这么久！$R;" +
                        "$P哎呀…谢谢您告诉我呀$R;");
                    break;
                case 2:
                    Say(pc, 131, "是吗？真的吗？$R;" +
                        "哎呀…我好像走错地方了…$R;" +
                        "$R哎呀…怎么办才好呢$R;");
                    break;
            }
        }
    }
}
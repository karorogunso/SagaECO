using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001142 : Event
    {
        public S11001142()
        {
            this.EventID = 11001142;
        }

        public override void OnEvent(ActorPC pc)
        {
            int a = Global.Random.Next(1, 2);
            if (a == 1)
            {
                Say(pc, 131, "奶奶~！！！$R;");
                Say(pc, 11001141, 131, "哎，怎么了？$R;");
                Say(pc, 131, "堤亚拉也想成为皮诺喔$R;");
                Say(pc, 11001141, 131, "呵呵呵……$R;" +
                    "看样子堤亚拉喜欢皮诺$R;");
                return;
            }
            if (pc.Gender == PC_GENDER.FEMALE)
            {
                Say(pc, 131, "姐姐是皮诺吗？$R;" +
                    "$R皮诺呀，会成为人类哦！$R;" +
                    "等长大了会变成人类的$R;" +
                    "$P所以皮诺喜欢冒险！$R;" +
                    "淘气鬼，虽然啰嗦。$R;" +
                    "$R还有?！$R;");
                Say(pc, 0, 131, "(???)$R;");
                Say(pc, 11001141, 131, "小子，姐姐不好意思了。$R;" +
                    "$R对不起…$R;" +
                    "这小子总说关于皮诺的故事…$R;");
                return;
            }
            Say(pc, 131, "哥哥是皮诺吗?$R;" +
                "$R是皮诺吧，会成为人类哦！$R;" +
                "等长大了，就会变成人类的$R;" +
                "$P所以皮诺喜欢冒险！$R;" +
                "淘气鬼，虽然啰嗦。$R;" +
                "$R还有?！$R;");
            Say(pc, 0, 131, "(???)$R;");
            Say(pc, 11001141, 131, "小子，哥哥不好意思了。$R;" +
                "$R对不起…$R;" +
                "这小子总说关于皮诺的故事…$R;");
        }
    }
}
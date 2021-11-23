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
                Say(pc, 11001141, 131, "哎，怎麼了？$R;");
                Say(pc, 131, "堤亞拉也想成為皮諾喔$R;");
                Say(pc, 11001141, 131, "呵呵呵……$R;" +
                    "看樣子堤亞拉喜歡皮諾$R;");
                return;
            }
            if (pc.Gender == PC_GENDER.FEMALE)
            {
                Say(pc, 131, "姐姐是皮諾嗎？$R;" +
                    "$R皮諾呀，會成為人類唷！$R;" +
                    "等長大了會變成人類的$R;" +
                    "$P所以皮諾喜歡冒險！$R;" +
                    "淘氣鬼，雖然囉嗦。$R;" +
                    "$R還有?！$R;");
                Say(pc, 0, 131, "(???)$R;");
                Say(pc, 11001141, 131, "小子，姐姐不好意思了。$R;" +
                    "$R對不起…$R;" +
                    "這小子總說關於皮諾的故事…$R;");
                return;
            }
            Say(pc, 131, "哥哥是皮諾嗎?$R;" +
                "$R是皮諾吧，會成為人類唷！$R;" +
                "等長大了，就會變成人類的$R;" +
                "$P所以皮諾喜歡冒險！$R;" +
                "淘氣鬼，雖然囉嗦。$R;" +
                "$R還有?！$R;");
            Say(pc, 0, 131, "(???)$R;");
            Say(pc, 11001141, 131, "小子，哥哥不好意思了。$R;" +
                "$R對不起…$R;" +
                "這小子總說關於皮諾的故事…$R;");
        }
    }
}
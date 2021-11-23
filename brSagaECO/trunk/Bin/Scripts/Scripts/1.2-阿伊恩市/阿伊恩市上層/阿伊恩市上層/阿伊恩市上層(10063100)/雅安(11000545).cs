using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10063100
{
    public class S11000545 : Event
    {
        public S11000545()
        {
            this.EventID = 11000545;
        }

        public override void OnEvent(ActorPC pc)
        {
            NavigateCancel(pc);
            Say(pc, 131, "歡迎來到礦產都市$R;" +
                "『阿伊恩市』。$R;" +
                "$R這個都市分成$R;" +
                "上層部和下層部兩部份$R;");
            switch (Select(pc, "希望我給您介紹哪個部分呢？", "", "下層", "商業區", "合同大廈", "不用了"))
            {
                case 1:
                    Say(pc, 131, "往前走一點，$R;" +
                        "從階梯下去，就是下層部了。$R;");
                    break;
                case 2:
                    Say(pc, 131, "商業區就在眼前所見，$R;" +
                        "高樓林立的地方$R;" +
                        "有很多裁縫和寶石商開的店呢。$R;" +
                        "下層部也有商店，$R;" +
                        "也得去看看啊。$R;");
                    break;
                case 3:
                    Say(pc, 131, "合同大廈就是前面$R;" +
                        "很大的那座建築。$R;" +
                        "$R跟著箭頭走吧。$R;");
                    Navigate(pc, 127, 103);
                    break;
                case 4:
                    Say(pc, 131, "這個都市很大，$R;" +
                        "小心不要迷路啊。$R;");
                    break;
            }
        }
    }
}
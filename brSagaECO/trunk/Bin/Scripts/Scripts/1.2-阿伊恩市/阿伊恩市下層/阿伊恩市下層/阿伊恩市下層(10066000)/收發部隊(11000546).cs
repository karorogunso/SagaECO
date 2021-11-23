using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10066000
{
    public class S11000546 : Event
    {
        public S11000546()
        {
            this.EventID = 11000546;
        }

        public override void OnEvent(ActorPC pc)
        {
            NavigateCancel(pc);
            int selection;
            Say(pc, 131, "我們是收發部隊。$R;" +
                "知道關於這個城市的所有消息唷。$R;");
            selection = Select(pc, "要介紹嗎？", "", "商店", "咖啡館", "鑑定所", "武器商店", "武器製造所", "古董商店", "下一頁→", "算了");
            while (selection != 8)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 131, "旁邊那個大入口的商店$R;" +
                            "就是武器商店喔$R;");
                        return;
                    case 2:
                        Say(pc, 131, "現在介紹咖啡館$R;" +
                            "$R箭頭方向就是咖啡館唷$R;");
                        Navigate(pc, 135, 145);
                        return;
                    case 3:
                        Say(pc, 131, "現在介紹鑑定所吧$R;" +
                            "$R箭頭方向就是鑑定所阿$R;");
                        Navigate(pc, 139, 145);
                        return;
                    case 4:
                        Say(pc, 131, "現在介紹武器商店$R;" +
                            "$R箭頭方向就是武器商店喔$R;");
                        Navigate(pc, 122, 51);
                        return;
                    case 5:
                        Say(pc, 131, "現在介紹武器製造所吧$R;" +
                            "$R箭頭方向就是武器製造所阿$R;");
                        Navigate(pc, 135, 62);
                        return;
                    case 6:
                        Say(pc, 131, "現在介紹古董商店吧$R;" +
                            "$R箭頭方向就是古董商店唷$R;");
                        Navigate(pc, 139, 62);
                        return;
                    case 7:
                        switch (Select(pc, "想去哪裡？", "", "鋼鐵工廠", "大工廠", "南方地牢", "動力控制中心", "返回"))
                        {
                            case 1:
                                Say(pc, 131, "現在介紹鋼鐵工廠$R;" +
                                    "有兩個入口，兩邊都可以進去$R;" +
                                    "$R箭頭方向走就是鋼鐵工廠阿$R;");
                                Navigate(pc, 155, 59);
                                return;
                            case 2:
                                Say(pc, 131, "現在介紹大工廠$R;" +
                                    "有兩個入口，兩邊都可以進去$R;" +
                                    "$R箭頭方向就是大工廠阿$R;");
                                Navigate(pc, 156, 148);
                                return;
                            case 3:
                                Say(pc, 131, "現在介紹南部地牢$R;" +
                                    "有兩個入口，兩邊都可以進去$R;" +
                                    "$R箭頭方向就是南部地牢$R;");
                                Navigate(pc, 200, 81);
                                return;
                            case 4:
                                Say(pc, 131, "對不起$R;" +
                                    "動力控制中心$R;" +
                                    "一般人不可以進去唷$R;");
                                return;
                        }
                        selection = Select(pc, "要介紹嗎？", "", "商店", "咖啡館", "鑑定所", "武器商店", "武器製造所", "古董商店", "下一頁→", "算了");
                        break;
                }
            }
        }
    }
}
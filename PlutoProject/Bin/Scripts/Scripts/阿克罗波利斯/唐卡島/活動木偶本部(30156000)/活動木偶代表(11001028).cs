using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30156000
{
    public class S11001028 : Event
    {
        public S11001028()
        {
            this.EventID = 11001028;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*
            if (_7a78)
            {
                Say(pc, 131, "現在調查剩下的資料和機器，$R;" +
                    "馬上就能找到犯人的。$R;" +
                    "$R現在拜託我們吧。$R;");
                return;
            }
            */
            /*
            if (_7a77)
            {
                if (_7a78)
                {
                    switch (Select(pc, "怎麼辦呢？", "", "買東西", "製做石像", "發石像產品目錄", "製作魔物模型", "什麼也不做"))
                    {
                        case 1:
                            GOTO EVT1100002409;
                            break;
                        case 2:
                            GOTO EVT1100002401;
                            break;
                        case 3:
                            GiveItem(pc, 10029600, 1);
                            Say(pc, 131, "想註冊產品目錄需要500金幣唷$R;" +
                                "$R如果有多餘的錢，$R可以一試，怎麼樣呢？$R;");
                            Say(pc, 131, "得到了石像產品目錄。$R;");
                            break;
                        case 4:
                            GOTO EVT1100002417;
                            break;
                        case 5:
                            break;
                    }
                    return;
                }
                if (_Xb27)
                {
                    if (CheckInventory(pc, 10000603, 1))
                    {
                        _7a78 = true;
                        GiveItem(pc, 10000603, 1);
                        Say(pc, 131, "一直在等著您呢。$R;" +
                            "上次太謝謝了。$R;" +
                            "為了向您表示謝意，請收下禮物吧。$R;");
                        Say(pc, 131, "收到了『活動木偶皮諾』$R;");
                        Say(pc, 131, "換做埃米爾早就旅遊去了。$R;" +
                            "找到了要找的東西..$R;" +
                            "$P對了，把下面的話轉達給他。$R;" +
                            "$R謝謝您這麼幫我！$R;" +
                            "總有一天會見面的…$R;");
                        return;
                    }
                    Say(pc, 131, "一直在等著您呢。$R;" +
                        "上次太謝謝了。$R;" +
                        "$R為了表示謝意準備了禮物，$R能不能把行李減輕一些呢？$R;");
                    return;
                }
                if (CheckInventory(pc, 10027000, 1))
                {
                    _Xb27 = true;
                    _7a78 = true;
                    GiveItem(pc, 10027000, 1);
                    Say(pc, 131, "一直在等著您呢。$R;" +
                        "上次太謝謝了。$R;" +
                        "為了向您表示謝意，請收下禮物吧。$R;");
                    Say(pc, 131, "收到了『活動木偶皮諾』$R;");
                    Say(pc, 131, "換做埃米爾早就旅遊去了。$R;" +
                        "找到了要找的東西..$R;" +
                        "$P對了，把下面的話轉告他。$R;" +
                        "$R謝謝您這麼幫我！$R;" +
                        "總有一天會見面的…$R;");
                    return;
                }
                Say(pc, 131, "一直在等著您呢。$R;" +
                    "上次太謝謝了。$R;" +
                    "$R為了表示謝意準備了禮物，$R能不能把行李減輕一些呢？$R;");
                return;
            }
            */
            Say(pc, 131, "啊，头好痛呀。$R;");
        }
    }
}
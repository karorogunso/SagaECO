using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21193000
{
    public class S11001788 : Event
    {
        public S11001788()
        {
            this.EventID = 11001788;
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;
            if (CountItem(pc, 10066400) >= 1 && CountItem(pc, 10067000) >= 1 && CountItem(pc, 10066600) >= 1)
            {
                if (pc.ECoin >= 20000)
                {
                    if (CountItem(pc, 10066700) >= 1 && CountItem(pc, 10066800) >= 1)//依代の鉄布 天霊珠 
                    {
                        if (Select(pc, "换么？", "", "换", "不换") == 1)
                        {
                            selection = Global.Random.Next(1, 3);
                            switch (selection)
                            {
                                case 1:
                                    TakeItem(pc, 10066400, 1);
                                    TakeItem(pc, 10067000, 1);
                                    TakeItem(pc, 10066600, 1);
                                    TakeItem(pc, 10066700, 1);
                                    TakeItem(pc, 10066800, 1);
                                    pc.ECoin -= 20000;
                                    GiveItem(pc, 10067900, 1);//片手剣
                                    return;

                                case 2:
                                    TakeItem(pc, 10066400, 1);
                                    TakeItem(pc, 10067000, 1);
                                    TakeItem(pc, 10066600, 1);
                                    TakeItem(pc, 10066700, 1);
                                    TakeItem(pc, 10066800, 1);
                                    pc.ECoin -= 20000;
                                    GiveItem(pc, 10068100, 1);//短剣
                                    return;
                                case 3:
                                    TakeItem(pc, 10066400, 1);
                                    TakeItem(pc, 10067000, 1);
                                    TakeItem(pc, 10066600, 1);
                                    TakeItem(pc, 10066700, 1);
                                    TakeItem(pc, 10066800, 1);
                                    pc.ECoin -= 20000;
                                    GiveItem(pc, 10069500, 1);//投擲
                                    return;
                            }
                        }
                    }
                    if (CountItem(pc, 10066700) >= 1 && CountItem(pc, 10066900) >= 1)//依代の鉄布 夢幻の焔 
                    {
                        if (Select(pc, "换么？", "", "换", "不换") == 1)
                        {
                            selection = Global.Random.Next(1, 3);
                            switch (selection)
                            {
                                case 1:
                                    TakeItem(pc, 10066400, 1);
                                    TakeItem(pc, 10067000, 1);
                                    TakeItem(pc, 10066600, 1);
                                    TakeItem(pc, 10066700, 1);
                                    TakeItem(pc, 10066900, 1);
                                    pc.ECoin -= 20000;
                                    GiveItem(pc, 10068300, 1);//片手斧
                                    return;
                                case 2:
                                    TakeItem(pc, 10066400, 1);
                                    TakeItem(pc, 10067000, 1);
                                    TakeItem(pc, 10066600, 1);
                                    TakeItem(pc, 10066700, 1);
                                    TakeItem(pc, 10066900, 1);
                                    pc.ECoin -= 20000;
                                    GiveItem(pc, 10068000, 1);//細剣
                                    return;
                                case 3:
                                    TakeItem(pc, 10066400, 1);
                                    TakeItem(pc, 10067000, 1);
                                    TakeItem(pc, 10066600, 1);
                                    TakeItem(pc, 10066700, 1);
                                    TakeItem(pc, 10066900, 1);
                                    pc.ECoin -= 20000;
                                    GiveItem(pc, 10069100, 1);//銃
                                    return;

                            }
                        }
                    }

                    if (CountItem(pc, 10066800) >= 1 && CountItem(pc, 10066900) >= 1)//天霊珠 夢幻の焔 
                    {
                        if (Select(pc, "换么？", "", "换", "不换") == 1)
                        {
                            selection = Global.Random.Next(1, 3);
                            switch (selection)
                            {
                                case 1:
                                    TakeItem(pc, 10066400, 1);
                                    TakeItem(pc, 10067000, 1);
                                    TakeItem(pc, 10066600, 1);
                                    TakeItem(pc, 10066800, 1);
                                    TakeItem(pc, 10066900, 1);
                                    pc.ECoin -= 20000;
                                    GiveItem(pc, 10068500, 1);//片手棒
                                    return;
                                case 2:
                                    TakeItem(pc, 10066400, 1);
                                    TakeItem(pc, 10067000, 1);
                                    TakeItem(pc, 10066600, 1);
                                    TakeItem(pc, 10066800, 1);
                                    TakeItem(pc, 10066900, 1);
                                    pc.ECoin -= 20000;
                                    GiveItem(pc, 10068700, 1);//杖
                                    return;
                                case 3:
                                    TakeItem(pc, 10066400, 1);
                                    TakeItem(pc, 10067000, 1);
                                    TakeItem(pc, 10066600, 1);
                                    TakeItem(pc, 10066800, 1);
                                    TakeItem(pc, 10066900, 1);
                                    pc.ECoin -= 20000;
                                    GiveItem(pc, 10069000, 1);//弩
                                    return;

                            }
                        }
                    }

                    if (CountItem(pc, 10066700) >= 2)//依代の鉄布 依代の鉄布 
                    {
                        if (Select(pc, "换么？", "", "换", "不换") == 1)
                        {
                            selection = Global.Random.Next(1, 3);
                            switch (selection)
                            {
                                case 1:
                                    TakeItem(pc, 10066400, 1);
                                    TakeItem(pc, 10067000, 1);
                                    TakeItem(pc, 10066600, 1);
                                    TakeItem(pc, 10066700, 2);
                                    pc.ECoin -= 20000;
                                    GiveItem(pc, 10068600, 1);//両手棒
                                    return;
                                case 2:
                                    TakeItem(pc, 10066400, 1);
                                    TakeItem(pc, 10067000, 1);
                                    TakeItem(pc, 10066600, 1);
                                    TakeItem(pc, 10066700, 2);
                                    pc.ECoin -= 20000;
                                    GiveItem(pc, 10068200, 1);//爪
                                    return;
                                case 3:
                                    TakeItem(pc, 10066400, 1);
                                    TakeItem(pc, 10067000, 1);
                                    TakeItem(pc, 10066600, 1);
                                    TakeItem(pc, 10066700, 2);
                                    pc.ECoin -= 20000;
                                    GiveItem(pc, 10069200, 1);//ライフル
                                    return;

                            }
                        }
                    }

                    if (CountItem(pc, 10066800) >= 2)//天霊珠 天霊珠 
                    {
                        if (Select(pc, "换么？", "", "换", "不换") == 1)
                        {
                            selection = Global.Random.Next(1, 3);
                            switch (selection)
                            {
                                case 1:
                                    TakeItem(pc, 10066400, 1);
                                    TakeItem(pc, 10067000, 1);
                                    TakeItem(pc, 10066600, 1);
                                    TakeItem(pc, 10066800, 2);
                                    pc.ECoin -= 20000;
                                    GiveItem(pc, 10068400, 1);//両手斧
                                    return;

                                case 2:
                                    TakeItem(pc, 10066400, 1);
                                    TakeItem(pc, 10067000, 1);
                                    TakeItem(pc, 10066600, 1);
                                    TakeItem(pc, 10066800, 2);
                                    pc.ECoin -= 20000;
                                    GiveItem(pc, 10069300, 1);//鞭
                                    return;
                                case 3:
                                    TakeItem(pc, 10066400, 1);
                                    TakeItem(pc, 10067000, 1);
                                    TakeItem(pc, 10066600, 1);
                                    TakeItem(pc, 10066800, 2);
                                    pc.ECoin -= 20000;
                                    GiveItem(pc, 10068900, 1);//弓
                                    return;

                            }
                        }
                    }
                    if (CountItem(pc, 10066900) >= 2)//夢幻の焔 夢幻の焔 
                    {
                        if (Select(pc, "换么？", "", "换", "不换") == 1)
                        {
                            selection = Global.Random.Next(1, 3);
                            switch (selection)
                            {
                                case 1:
                                    TakeItem(pc, 10066400, 1);
                                    TakeItem(pc, 10067000, 1);
                                    TakeItem(pc, 10066600, 1);
                                    TakeItem(pc, 10066900, 2);
                                    pc.ECoin -= 20000;
                                    GiveItem(pc, 10069400, 1);//楽器
                                    return;

                                case 2:
                                    TakeItem(pc, 10066400, 1);
                                    TakeItem(pc, 10067000, 1);
                                    TakeItem(pc, 10066600, 1);
                                    TakeItem(pc, 10066900, 2);
                                    pc.ECoin -= 20000;
                                    GiveItem(pc, 10068800, 1);//本
                                    return;
                                case 3:
                                    TakeItem(pc, 10066400, 1);
                                    TakeItem(pc, 10067000, 1);
                                    TakeItem(pc, 10066600, 1);
                                    TakeItem(pc, 10066900, 2);
                                    pc.ECoin -= 20000;
                                    GiveItem(pc, 10069600, 1);//カード
                                    return;
                            }
                        }
                    }
                    Say(pc, 0, "材料不足呢!$R;", "ローウェン");
                    return;
                }
                Say(pc, 0, "ecoin好像不够呢!$R;", "ローウェン");
                return;
            }
            Say(pc, 0, "唔…找我有甚麼事？$R;" +
            "$P老子現在、因為正尋找優質的素材$R;" +
            "而十分忙。$R;" +
            "$R有話要說的話、晚點再來找我吧。$R;", "哈根");

            //
            /*
             Say(pc, 0, "ん…何か用か？$R;" +
            "$P俺は今、良質の素材を探すので$R;" +
            "忙しいんだ。$R;" +
            "$R話があるなら、後にしてくれ。$R;", "ハガン");
            */
        }
    }
}
 
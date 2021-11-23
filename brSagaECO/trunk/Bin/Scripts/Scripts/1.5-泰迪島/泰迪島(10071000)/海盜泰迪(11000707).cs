using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:海盜泰迪(11000707) X:19 Y:104
namespace SagaScript.M10071000
{
    public class S11000707 : Event
    {
        public S11000707()
        {
            this.EventID = 11000707;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "哇，我的朋友！$R;", "海盜泰迪");
            switch (Select(pc, "歡迎光臨海盗寶物交換所", "", "用泰迪徽章交換寶物", "用泰迪彩虹徽章交換寶物", "什麼也不做"))
            {
                case 1:
                    Say(pc, 131, "用泰迪徽章交換是嗎！$R;" +
                    "可以交換的是以下幾個！$R;", "海盜泰迪");
                    switch (Select(pc, "用泰迪徽章交換寶物嗎？", "", "１枚　星型棒子", "１枚　泰迪精緻的桌子", "１枚　泰迪的野餐椅子", "１枚　迷你日曆", "３枚　藍色泰迪手提箱（右）", "３枚　桃紅色泰迪手提箱（右）", "１０枚　泰迪彩虹徽章", "什麼也不做"))
                    {
                        case 1:
                            if (CountItem(pc, 10050300) >= 1)
                            {
                                if (Select(pc, "要交换吗？", "", "不要", "要") == 2)
                                {
                                    TakeItem(pc, 10050300, 1);
                                    GiveItem(pc, 60072200, 1);
                                    
                                }
                            }
                            break;
                        case 2:
                            if (CountItem(pc, 10050300) >= 1)
                            {
                                if (Select(pc, "要交换吗？", "", "不要", "要") == 2)
                                {
                                    TakeItem(pc, 10050300, 1);
                                    GiveItem(pc, 31080402, 1);

                                }
                            }
                            break;
                        case 3:
                            if (CountItem(pc, 10050300) >= 1)
                            {
                                if (Select(pc, "要交换吗？", "", "不要", "要") == 2)
                                {
                                    TakeItem(pc, 10050300, 1);
                                    GiveItem(pc, 31090202, 1);

                                }
                            }
                            break;
                        case 4:
                            if (CountItem(pc, 10050300) >= 1)
                            {
                                if (Select(pc, "要交换吗？", "", "不要", "要") == 2)
                                {
                                    TakeItem(pc, 10050300, 1);
                                    GiveItem(pc, 31131402, 1);

                                }
                            }
                            break;
                        case 5:
                            if (CountItem(pc, 10050300) >= 3)
                            {
                                if (Select(pc, "要交换吗？", "", "不要", "要") == 2)
                                {
                                    TakeItem(pc, 10050300, 3);
                                    GiveItem(pc, 50071051, 1);

                                }
                            }
                            break;
                        case 6:
                            if (CountItem(pc, 10050300) >= 3)
                            {
                                if (Select(pc, "要交换吗？", "", "不要", "要") == 2)
                                {
                                    TakeItem(pc, 10050300, 3);
                                    GiveItem(pc, 50071052, 1);

                                }
                            }
                            break;
                        case 7:
                            if (CountItem(pc, 10050300) >= 10)
                            {
                                if (Select(pc, "要交换吗？", "", "不要", "要") == 2)
                                {
                                    TakeItem(pc, 10050300, 10);
                                    GiveItem(pc, 10050350, 1);

                                }
                            }
                            break;
                    }
                    break;
                case 2:
                    Say(pc, 131, "用泰迪彩虹徽章交換是嗎？！$R;" +
                    "可以交換的是這些！$R;", "海盜泰迪");
                    switch (Select(pc, "用泰迪彩虹徽章交換寶物", "", "１枚　ウェイトレスの服♀（紫）", "３枚　藍色泰迪手提箱（左）", "３枚　桃紅色泰迪手提箱（左）", "什麼也不做"))
                   {
                       case 1:
                           if (CountItem(pc, 10050350) >= 1)
                           {
                               if (Select(pc, "要交换吗？", "", "不要", "要") == 2)
                               {
                                   TakeItem(pc, 10050350, 1);
                                   GiveItem(pc, 50001761, 1);

                               }
                           }
                           break;
                       case 2:
                           if (CountItem(pc, 10050350) >= 3)
                           {
                               if (Select(pc, "要交换吗？", "", "不要", "要") == 2)
                               {
                                   TakeItem(pc, 10050350, 3);
                                   GiveItem(pc, 50071750, 1);

                               }
                           }
                           break;
                       case 3:
                           if (CountItem(pc, 10050350) >= 3)
                           {
                               if (Select(pc, "要交换吗？", "", "不要", "要") == 2)
                               {
                                   TakeItem(pc, 10050350, 3);
                                   GiveItem(pc, 50071751, 1);

                               }
                           }
                           break;
                   }
                    break;
            }

            Say(pc, 131, "不要把這艘船是假船的事情說出去唷！$R;", "海盜泰迪");

        }
    }
}





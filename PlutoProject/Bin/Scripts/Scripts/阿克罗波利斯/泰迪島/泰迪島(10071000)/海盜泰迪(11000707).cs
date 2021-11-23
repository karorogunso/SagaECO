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
            Say(pc, 131, "哇，我的朋友！$R;", "海盗泰迪");
            switch (Select(pc, "欢迎光临海盗宝物交换所", "", "用泰迪徽章交换宝物", "用泰迪彩虹徽章交换宝物", "什么也不做"))
            {
                case 1:
                    Say(pc, 131, "用泰迪徽章交换是吗！$R;" +
                    "可以交换的是以下几个！$R;", "海盗泰迪");
                    switch (Select(pc, "用泰迪徽章交换宝物吗？", "", "１枚　泰迪魔棒", "１枚　精致的桌子（泰迪）", "１枚　野餐椅（泰迪）", "１枚　挂历（泰迪）", "３枚　泰迪手提箱（藏青）", "３枚　泰迪手提箱（粉色）", "１０枚　泰迪彩虹徽章", "什么也不做"))
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
                    Say(pc, 131, "用泰迪彩虹徽章交换是吗？！$R;" +
                    "可以交换的是这些！$R;", "海盗泰迪");
                    switch (Select(pc, "用泰迪彩虹徽章交换宝物", "", "１枚　服务生制服♀（紫色）", "３枚　泰迪手提箱（藏青·左手）", "３枚　泰迪手提箱（粉色·左手）", "什么也不做"))
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

            Say(pc, 131, "不要把这艘船是假船的事情说出去唷！$R;", "海盗泰迪");

        }
    }
}





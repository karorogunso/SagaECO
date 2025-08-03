using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20023000
{
    public class S11000388 : Event
    {
        public S11000388()
        {
            this.EventID = 11000388;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<MarkPier> MarkPier_mask = pc.CMask["MarkPier"];
            if (pc.Level > 44 && 
                pc.Job == PC_JOB.TATARABE &&
                pc.Job == PC_JOB.BLACKSMITH &&
                pc.Job == PC_JOB.MACHINERY) 
            
            {
                if (MarkPier_mask.Test(MarkPier.任務完成))
                {
                    Say(pc, 131, "您好，我是矿石博士$R;" +
                        "有挺多好用的矿石呢。$R;" +
                        "真的很高兴。$R;" +
                        "$P进研究室之前$R;" +
                        "先下一会儿棋吧$R;" +
                        "$R呵呵，真的很高兴呢。$R;");
                    return;
                }
                if (MarkPier_mask.Test(MarkPier.給予礦石))
                {
                    if (pc.Gender == PC_GENDER.FEMALE)
                    {
                        if (CheckInventory(pc, 50062650, 1))
                        {
                            MarkPier_mask.SetValue(MarkPier.任務完成, true);
                            //_2a73 = true;
                            GiveItem(pc, 50062650, 1);
                            Say(pc, 131, "这是给您的谢礼$R;" +
                                "要好好珍惜啊$R;");
                            return;
                        }
                        Say(pc, 131, "要给您谢礼，$R;" +
                            "去整理一下行李，再来吧$R;");
                        return;
                    }
                    if (CheckInventory(pc, 50012950, 1))
                    {
                        MarkPier_mask.SetValue(MarkPier.任務完成, true);
                        //_2a73 = true;
                        GiveItem(pc, 50012950, 1);
                        Say(pc, 131, "这是给您的谢礼$R;" +
                            "要好好珍惜啊$R;");
                        return;
                    }
                    Say(pc, 131, "要给您谢礼，$R;" +
                        "去整理一下行李，再来吧$R;");
                    return;
                }
                if (MarkPier_mask.Test(MarkPier.收集任務開始))
                {
                    if (CountItem(pc, 10014600) >= 1 && 
                        CountItem(pc, 10014650) >= 1 && 
                        CountItem(pc, 10008400) >= 1 && 
                        CountItem(pc, 10015400) >= 1 && 
                        CountItem(pc, 10014750) >= 1 &&
                        CountItem(pc, 10015701) >= 1 &&
                        CountItem(pc, 10015703) >= 1 && 
                        CountItem(pc, 10015600) >= 1 &&
                        CountItem(pc, 10015700) >= 1 &&
                        CountItem(pc, 10015710) >= 1 &&
                        CountItem(pc, 10015300) >= 1 &&
                        CountItem(pc, 10019304) >= 1 &&
                        CountItem(pc, 10011201) >= 1 &&
                        CountItem(pc, 10011203) >= 1 && 
                        CountItem(pc, 10011205) >= 1 && 
                        CountItem(pc, 10011207) >= 1 && 
                        CountItem(pc, 10011209) >= 1 && 
                        CountItem(pc, 10011210) >= 1)
                    {
                        MarkPier_mask.SetValue(MarkPier.給予礦石, true);
                        //_2a74 = true;
                        TakeItem(pc, 10014600, 1);
                        TakeItem(pc, 10014650, 1);
                        TakeItem(pc, 10008400, 1);
                        TakeItem(pc, 10015400, 1);
                        TakeItem(pc, 10014750, 1);
                        TakeItem(pc, 10015701, 1);
                        TakeItem(pc, 10015703, 1);
                        TakeItem(pc, 10015600, 1);
                        TakeItem(pc, 10015700, 1);
                        TakeItem(pc, 10015710, 1);
                        TakeItem(pc, 10015300, 1);
                        TakeItem(pc, 10019304, 1);
                        TakeItem(pc, 10011201, 1);
                        TakeItem(pc, 10011203, 1);
                        TakeItem(pc, 10011205, 1);
                        TakeItem(pc, 10011207, 1);
                        TakeItem(pc, 10011209, 1);
                        TakeItem(pc, 10011210, 1);
                        TakeItem(pc, 10020100, 1);
                        Say(pc, 131, "您好，我是矿石博士$R;" +
                            "$R啊?都搜集到了吗？$R;" +
                            "太感谢了$R;" +
                            "$P不辛苦吗?$R;" +
                            "找的时候不难吗?$R;" +
                            "$R真是太感谢您啰$R;" +
                            "那我收下了$R;");
                        Say(pc, 0, 131, "给他矿石。$R;");
                        if (pc.Gender == PC_GENDER.FEMALE)
                        {
                            if (CheckInventory(pc, 50062650, 1))
                            {
                                MarkPier_mask.SetValue(MarkPier.任務完成, true);
                                //_2a73 = true;
                                GiveItem(pc, 50062650, 1);
                                Say(pc, 131, "这是给您的谢礼$R;" +
                                    "要好好珍惜啊$R;");
                                return;
                            }
                            Say(pc, 131, "要给您谢礼，$R;" +
                                "去整理一下行李，再来吧$R;");
                            return;
                        }
                        if (CheckInventory(pc, 50012950, 1))
                        {
                            MarkPier_mask.SetValue(MarkPier.任務完成, true);
                            //_2a73 = true;
                            GiveItem(pc, 50012950, 1);
                            Say(pc, 131, "这是给您的谢礼$R;" +
                                "要好好珍惜啊$R;");
                            return;
                        }
                        Say(pc, 131, "要给您谢礼，$R;" +
                            "去整理一下行李，再来吧$R;");
                        return;
                    }
                    Say(pc, 131, "您好，我是矿石博士$R;" +
                        "要找的矿石清单$R;" +
                        "已经拟好了$R;" +
                        "拜托了$R;");
                    return;
                }
                Say(pc, 131, "我是矿石博士呢。$R;" +
                    "来采集矿石的。$R;" +
                    "可是太热了，受不了$R;" +
                    "$R你们的体力都很好啊$R;" +
                    "$P…$R;" +
                    "$P真是不好意思$R;" +
                    "我……$R;" +
                    "希望您可以帮帮忙$R;" +
                    "我是研究矿石的需要矿石标本。$R;" +
                    "可不可以帮我搜集几块矿石呢？$R;" +
                    "$R当然会有谢礼的。$R;" +
                    "嗯…让我看有什么好的…$R;" +
                    "$P送您鞋子好不好？$R;" +
                    "我们家是造鞋子的$R;" +
                    "有…很多存货$R;" +
                    "$R说是存货，但品质不错的$R;" +
                    "$R挺坚硬，也挺方便的呢$R;");
                switch (Select(pc, "怎么办呢？", "", "拒绝", "好啊"))
                {
                    case 1:
                        break;
                    case 2:
                        if (CheckInventory(pc, 10020100, 1))
                        {
                            MarkPier_mask.SetValue(MarkPier.收集任務開始, true);
                            //_2a72 = true;
                            GiveItem(pc, 10020100, 1);
                            Say(pc, 131, "那就拜托了，$R;" +
                                "给您这张要找的矿石清单$R;" +
                                "没有时间限制的，$R;" +
                                "慢慢来也没关系。$R;");
                            return;
                        }
                        Say(pc, 131, "那就拜托了，$R;" +
                            "给您这张要找的矿石清单$R;" +
                            "请把行李整理一下，再来吧。$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "我是矿石博士呢。$R;" +
                "来采集矿石的。$R;" +
                "可是太热了，受不了$R;" +
                "$R你们的体力都很好啊$R;");
        }
    }
}
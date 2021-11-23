using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10042000
{
    public class S11000539 : Event
    {
        public S11000539()
        {
            this.EventID = 11000539;

            this.leastQuestPoint = 1;
            this.questFailed = "任务失败$R;" +
    "下次再挑战吧$R;";
            this.notEnoughQuestPoint = "现在没有什么要您帮忙的$R;" +
    "$R下次再来吧$R;";
            this.gotNormalQuest = "在这里接到的任务$R;" +
    "是「队伍」用的击退任务喔$R;" +
    "$R队伍中如果有人接受一样任务的话$R;" +
    "可以共享击退魔物数量$R;" +
    "$P一起合作的话$R;" +
    "执行任务会比较容易哦$R;";
            this.questCompleted = "辛苦了$R;" +
    "$R任务成功$R;" +
    "请领取报酬吧。$R;" +
    "$P将您手上的戒指$R;" +
    "转交给上城的老人$R;" +
    "肯定会有好事情的哦$R;";
            this.questCanceled = "会等待您的再次挑战的。$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;
            BitMask<JLFlags> mask = new BitMask<JLFlags>(pc.CMask["JL"]);
            if (!mask.Test(JLFlags.D大師第一次對話))
            {
                Say(pc, 131, "在这里接到的任务$R;" +
                    "是『队伍』用的击退任务$R;" +
                    "$R队五中如果有人接受一样的任务$R;" +
                    "可以共享击退的魔物数量$R;" +
                    "$P一起合作的话$R;" +
                    "执行任务会比较容易哦$R;");
                mask.SetValue(JLFlags.D大師第一次對話, true);
            }
            else
            {
                Say(pc, 255, "那么！要去回廊宠物养殖场?$R;");
            }
            selection = Select(pc, "有什么事啊？", "", "进入无限回廊", "任务服务台", "这建筑是什么？", "返回");
            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 131, "如果不接受任务的话，$R;" +
                        "进入这个建筑物就没意义了$R;" +
                        "$R那您还要进去吗？$R;");
                        進入回廊(pc);
                        return;
                    case 2:
                        HandleQuest(pc, 18);
                        return;
                    case 3:
                        Say(pc, 131, "这建筑物是模仿某建筑建造的。$R;" +
                            "现在作为修炼场所$R也会对一般人开放喔。$R;" +
                            "$P若想使用的话，就接受我的任务吧。$R;" +
                            "$R任务内容是为了$R获取各阶段进级必需的重点道具$R;" +
                            "$R为了任务成功，要努力啊！$R;" +
                            "$P当然，成功的话$R;" +
                            "会得到相应的报酬哦$R;");
                        break;
                    case 4:
                        return;
                }
                selection = Select(pc, "有什么事啊？", "", "进入无限回廊", "任务服务台", "这建筑是什么？", "返回");
            }
        }

        void 進入回廊(ActorPC pc)
        {
            if (pc.Skills2.Count == 706 ||
                pc.SkillsReserve.Count == 706)
            {
                Say(pc, 131, "哦哦！$R;" +
                    pc.Name + "，是您吗？$R;" +
                    "想进入无限回廊吗？$R;" +
                    "您的到来，真是我的荣幸啊$R;" +
                    "不论如何，都要好好招待您吧！$R;" +
                    "$P您可能已经知道吧$R;" +
                    "不接受任务的话，进去也没有意义阿$R;" +
                    "$R还要进去吗？$R;");
                switch (Select(pc, "想去几楼呢？", "", "无限回廊B1F", "无限回廊B11F（手续费4000金币）", "无限回廊B21F（手续费8000金币）", "无限回廊B31F（手续费12000金币）", "还是算了吧"))
                {
                    case 1:
                        Warp(pc, 20070000, 23, 84);
                        break;
                    case 2:
                        Say(pc, 131, "可以略过中层，从11楼开始$R;" +
                            "但是需要手续费4000金币啊$R;" +
                            "$R没关系吗？$R;");
                        switch (Select(pc, "支付4000金币吗？", "", "算了吧", "支付后进入11楼"))
                        {
                            case 1:
                                break;
                            case 2:
                                if (pc.Gold > 3999)
                                {
                                    pc.Gold -= 4000;
                                    Warp(pc, 20070010, 23, 84);
                                    return;
                                }
                                Say(pc, 131, "钱好像不够啊$R;" +
                                    "$P这是为了建筑的维修费$R;" +
                                    "请您帮帮忙吧$R;");
                                break;
                        }
                        break;
                    case 3:
                        Say(pc, 131, "可以略过中层，从21楼开始$R;" +
                            "但是需要手续费8000金币啊$R;" +
                            "$R没关系吗？$R;");
                        switch (Select(pc, "支付8000金币吗？", "", "算了吧", "支付后进入21楼"))
                        {
                            case 1:
                                break;
                            case 2:
                                if (pc.Gold > 7999)
                                {
                                    pc.Gold -= 8000;
                                    Warp(pc, 20070020, 23, 84);
                                    return;
                                }
                                Say(pc, 131, "钱好像不够啊$R;" +
                                    "$P这是为了建筑的维修费$R;" +
                                    "请您帮帮忙吧$R;");
                                break;
                        }
                        break;
                    case 4:
                        Say(pc, 131, "可以略过中层，从31楼开始$R;" +
                            "但是需要手续费12000金币啊$R;" +
                            "$R没关系吗？$R;");
                        switch (Select(pc, "支付12000金币吗？", "", "算了吧", "支付后进入31楼"))
                        {
                            case 1:
                                break;
                            case 2:
                                if (pc.Gold > 11999)
                                {
                                    pc.Gold -= 12000;
                                    Warp(pc, 20070030, 23, 84);
                                    return;
                                }
                                Say(pc, 131, "钱好像不够啊$R;" +
                                    "$P这是为了建筑的维修费$R;" +
                                    "请您帮帮忙吧$R;");
                                break;
                        }
                        break;
                    case 5:
                        break;
                }
                return;
            }

            Say(pc, 131, "如果不接受任务的话，$R;" +
                "进入这个建筑物就没意义了$R;" +
                "$R那您还要进去吗？$R;");
            switch (Select(pc, "想去几楼呢？", "", "无限回廊B1F", "无限回廊B11F（手续费5000金币）", "无限回廊B21F（手续费10000金币）", "无限回廊31F（手续费15000金币）", "还是算了吧"))
            {
                case 1:
                    Warp(pc, 20070000, 23, 84);
                    break;
                case 2:
                    Say(pc, 131, "可以略过中层，从11楼开始$R;" +
                        "但是需要手续费5000金币啊$R;" +
                        "$R没关系吗？$R;");
                    switch (Select(pc, "支付5000金币吗？", "", "算了吧", "支付后进入11楼"))
                    {
                        case 1:
                            break;
                        case 2:
                            if (pc.Gold > 4999
                            )
                            {
                                pc.Gold -= 5000;
                                Warp(pc, 20070010, 23, 84);
                                return;
                            }
                            Say(pc, 131, "钱好像不够啊$R;" +
                                "$P这是为了建筑的维修费$R;" +
                                "请您帮帮忙吧$R;");
                            break;
                    }
                    break;
                case 3:
                    Say(pc, 131, "可以略过中层，从21楼开始$R;" +
                        "但是需要手续费10000金币啊$R;" +
                        "$R没关系吗？$R;");
                    switch (Select(pc, "支付10000金币吗？", "", "算了吧", "支付后进入21楼"))
                    {
                        case 1:
                            break;
                        case 2:
                            if (pc.Gold > 9999)
                            {
                                pc.Gold -= 10000;
                                Warp(pc, 20070020, 23, 84);
                                return;
                            }
                            Say(pc, 131, "钱好像不够啊$R;" +
                                "$P这是为了建筑的维修费$R;" +
                                "请您帮帮忙吧$R;");
                            break;
                    }
                    break;
                case 4:
                    Say(pc, 131, "可以略过中层，从31楼开始$R;" +
                        "但是需要手续费15000金币啊$R;" +
                        "$R没关系吗？$R;");
                    switch (Select(pc, "支付15000金币吗？", "", "算了吧", "支付后进入31楼"))
                    {
                        case 1:
                            break;
                        case 2:
                            if (pc.Gold > 14999)
                            {
                                pc.Gold -= 15000;
                                Warp(pc, 20070030, 23, 84);
                                return;
                            }
                            Say(pc, 131, "钱好像不够啊$R;" +
                                "$P这是为了建筑的维修费$R;" +
                                "请您帮帮忙吧$R;");
                            break;
                    }
                    break;
                case 5:
                    break;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30079000
{
    public class S11000294 : Event
    {
        public S11000294()
        {
            this.EventID = 11000294;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Knights> Knights_mask = pc.CMask["Knights"];
            switch (Select(pc, "什么事呢？", "", "加特林机关炮的销售许可", "我不喜欢政府！"))
            {
                case 1:
                    Say(pc, 131, "您说是加特林机关炮的销售许可吗？$R;" +
                        "稍等$R;" +
                        "$P…$R;" +
                        "$P按照艾恩萨乌斯联邦法第36条$R;" +
                        "加特林机关炮只可贩卖给南军$R;" +
                        "$R…是这样写着的$R;" +
                        "如果是南军的话，应该拥有$R;" +
                        "『艾恩萨乌斯骑士团证』吧？$R;" +
                        "$R请出示证件阿$R;");
                    switch (Select(pc, "出示证件吗？", "", "是的", "不了"))
                    {
                        case 1:
                            if (CountItem(pc, 10041500) >= 1 && !Knights_mask.Test(Knights.加入南軍騎士團))
                            {
                                Say(pc, 131, "那么让我查证一下$R;" +
                                    "确认期间，请稍等$R;" +
                                    "$P…$R;" +
                                    "$P…$R;" +
                                    "南军登记簿上，没有您的名字呢。$R;" +
                                    "$P这个是谁的许可证啊？$R;" +
                                    "这样我们不能办理通行喔，$R;" +
                                    "$R反正就是不行$R;" +
                                    "不能准许其他所属军队通过$R;" +
                                    "回去吧$R;");
                                return;
                            }
                            if (CountItem(pc, 10041500) >= 1 && Knights_mask.Test(Knights.加入南軍騎士團))
                            {
                                Say(pc, 131, "那么让我查证一下$R;" +
                                    "确认期间，请稍等$R;" +
                                    "$P…$R;" +
                                    "$P…$R;" +
                                    "$R确认完毕$R;" +
                                    "加特林机关炮购入税5000金币$R;" +
                                    "加特林机关炮登记税3000金币$R;" +
                                    "加特林机关炮持有税1200金币$R;" +
                                    "$R加特林机关炮税金一共9200金币。$R;" +
                                    "没关系吗？$R;");
                                switch (Select(pc, "支付加特林机关炮税金9200金币吗？", "", "支付", "不支付"))
                                {
                                    case 1:
                                        if (pc.Gold > 9199)
                                        {
                                            if (CheckInventory(pc, 10048000, 1))
                                            {
                                                GiveItem(pc, 10048000, 1);
                                                pc.Gold -= 9200;
                                                Say(pc, 131, "给您许可证$R;");
                                                return;
                                            }
                                            Say(pc, 131, "行李太多了，整理后再来吧$R;");
                                            return;
                                        }
                                        Say(pc, 131, "钱好像不够啊$R;");
                                        break;
                                    case 2:
                                        Say(pc, 131, "下一位$R;");
                                        break;
                                }
                                return;
                            }
                            Say(pc, 131, "没有证件的话，是不能通行的。$R;" +
                                "$R拿到艾恩萨乌斯骑士团证$R;" +
                                "再来吧。$R;" +
                                "『艾恩萨乌斯骑士团证』哦？$R;");
                            break;
                        case 2:
                            Say(pc, 131, "下一位$R;");
                            break;
                    }
                    break;
                case 2:
                    Say(pc, 131, "下一位$R;");
                    break;
            }
        }
    }
}

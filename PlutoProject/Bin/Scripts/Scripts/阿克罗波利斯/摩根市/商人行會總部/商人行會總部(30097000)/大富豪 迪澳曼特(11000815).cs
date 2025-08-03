using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30097000
{
    public class S11000815 : Event
    {
        public S11000815()
        {
            this.EventID = 11000815;
        }

        public override void OnEvent(ActorPC pc)
        {

            BitMask<Sinker> mask = pc.AMask["Sinker"];
            BitMask<TravelFGarden> TravelFGarden_mask = pc.CMask["TravelFGarden"];

            if (mask.Test(Sinker.未獲得給社長的信))//_7a97)
            {
                if (CheckInventory(pc, 10043083, 1) && CheckInventory(pc, 10019302, 1))
                {
                    mask.SetValue(Sinker.未獲得給社長的信, false);
                    //_7a97 = false;
                    GiveItem(pc, 10043083, 1);
                    GiveItem(pc, 10019302, 1);
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 131, "得到了『给社长的信』$R;" +
                        "得到了『不明合金的碎片』$R;");
                    Say(pc, 131, "拜托了$R;");
                    return;
                }
                Say(pc, 131, "呀？行李太多了，不能给您啊$R;");
                mask.SetValue(Sinker.未獲得給社長的信, true);
                //_7a97 = true;
                return;
            }
            Say(pc, 131, "您好！客人！$R;" +
                "欢迎来到商人行会总部$R;" +
                "$R今天有何贵干呢？$R;");
            if (CountItem(pc, 10011202) >= 1 && CountItem(pc, 10011900) >= 1)
            {
                Say(pc, 131, "要转交的物品？$R;" +
                    "啊…这太感谢您了$R;");
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "给了他『未知的合金』$R;" +
                    "给了他『紫水晶』$R;");
                TakeItem(pc, 10011202, 1);
                TakeItem(pc, 10011900, 1);
                Say(pc, 131, "嗯…钢铁工厂老板送来的东西？$R;" +
                    "你问贵不贵是吗？$R;" +
                    "不可能很贵吧…$R;" +
                    "$R这种东西也算贵呀，$R;" +
                    "钢铁工厂的老板也真厉害啊$R;" +
                    "$P不过这个不行，$R;" +
                    "这是不能卖的仿造品$R;" +
                    "$R得重新委托钢铁工厂的老板了$R;" +
                    "$P接受了『紫水晶』$R;" +
                    "$R啊…$R;" +
                    "如果可以的话把这封信交给老板吧？$R;" +
                    "$P不能说是报酬，$R但会给您一些合金呢。$R;");
                //EVT1100081537
                if (CheckInventory(pc, 10043083, 1) && CheckInventory(pc, 10019302, 1))
                {
                    mask.SetValue(Sinker.未獲得給社長的信, false);
                    //_7a97 = false;
                    GiveItem(pc, 10043083, 1);
                    GiveItem(pc, 10019302, 1);
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 131, "得到了『给社长的信』$R;" +
                        "得到了『不明合金的碎片』$R;");
                    Say(pc, 131, "拜托了$R;");
                    return;
                }
                Say(pc, 131, "呀？行李太多了，不能给您啊$R;");
                mask.SetValue(Sinker.未獲得給社長的信, true);
                //_7a97 = true;
                return;
            }
            switch (Select(pc, "今天有什么事情呢?", "", "购买到光之塔的飞空庭票", "到银行存钱", "到银行取钱", "使用仓库（收费20金币）", "什么也不做"))
            {
                case 1:
                    /*
                    if (_6a69)
                    {
                        if (_6a54)
                        {
                            Say(pc, 131, "哦？客人已經…？$R;" +
                                "$R啊！沒什麽！$R;" +
                                "沒什麽問題了$R;");
                            //EVT1100081508
                            //PARAM ME.WORK1 = ME.LV
                            //PARAM ME.WORK1 * 50
                            //PARAM STR1 = INT2STR ME.WORK1
                            //PARAM STR1 + 金幣
                            Say(pc, 131, "知道了$R;" +
                                "是到光之塔的飛空庭票吧？$R;" +
                                "$P最近銷量不好$R;" +
                                "所以不能提高優惠幅度$R;" +
                                "$R讓我看一下…$R量一量客人的身高和體重…$R;" +
                                "STR1$R;");
                            switch (Select(pc, "買不買飛空庭票呢?", "", "買", "不買"))
                            {
                                case 1:
                                    //PARAM ME.WORK1 = ME.LV
                                    //PARAM ME.WORK1 * 50
                                    if (a//ME.MONEY < ME.WORK1
                                    )
                                    {
                                        Say(pc, 255, "唰…$R;");
                                        return;
                                    }
                                    //PARAM ME.MONEY - ME.WORK1
                                    //PARAM STR3 = INT2STR ME.WORK1
                                    //PARAM STR3 + 金幣
                                    Say(pc, 131, "沒錯的是$R;" +
                                        "STR3$R;" +
                                        "$P我們行會爲了節省$R;" +
                                        "正在做代替的印章$R;" +
                                        "$R給負責人看一下印有印章的手吧$R;");
                                    ShowEffect(pc, 8013);
                                    PlaySound(pc, 3277, false, 100, 50);
                                    _6a54 = true;
                                    break;
                            }
                            return;
                        }
                        return;
                    }
                     //*/
                    if (TravelFGarden_mask.Test(TravelFGarden.已经买票))
                    {
                        Say(pc, 131, "哦？客人已经…？$R;" +
                            "$R啊！没什么！$R;" +
                            "没什么问题了$R;");
                    }
                    //*/
                    int a = pc.Level * 200;
                    int b = pc.Level * 100;
                    Say(pc, 131, "知道了$R;" +
                        "是到光之塔的飞空庭票吧？$R;" +
                        "$R让我看一下…$R;" +
                        "量一量客人的身高和体重…$R;" +
                        a + "金币$R;" +
                        "$P但是今天很特别！$R;" +
                        "$R您知道吗？$R;" +
                        "$P只给客人您一个人$R;" +
                        "提供特别服务！$R;" +
                        b + "金币$R;" +
                        "提供给您！$R;" +
                        "$R现在是最好的机会！$R;");
                    switch (Select(pc, "买不买飞空庭票呢?", "", "买", "不买"))
                    {
                        case 1:
                            if (b > pc.Gold)
                            {
                                Say(pc, 255, "唰…$R;");
                                return;
                            }
                            pc.Gold -= b;
                            Say(pc, 131, "没错$R;" +
                                b + "金币$R;" +
                                "$P我们行会为了节省$R;" +
                                "正在做代替的印章$R;" +
                                "$R给负责人看一下印有印章的手吧$R;");
                            ShowEffect(pc, 8013);
                            PlaySound(pc, 3277, false, 100, 50);
                            TravelFGarden_mask.SetValue(TravelFGarden.已经买票, true);
                            //_6a54 = true;
                            break;
                    }
                    break;
                case 2:
                    BankDeposit(pc);
                    break;
                case 3:
                    BankWithdraw(pc);
                    break;
                case 4:
                    if (pc.Gold >= 20)
                    {
                        pc.Gold -= 20;

                        OpenWareHouse(pc, WarehousePlace.Morg);
                    }
                    else
                    {
                        Say(pc, 131, "资金不足喔!$R;", "行会商人");
                    }
                    break;
            }
        }
    }
}
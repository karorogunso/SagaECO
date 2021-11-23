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
                    Say(pc, 131, "得到了『給社長的信』$R;" +
                        "得到了『不知名的合金破片』$R;");
                    Say(pc, 131, "拜託了$R;");
                    return;
                }
                Say(pc, 131, "呀？行李太多了，不能給您阿$R;");
                mask.SetValue(Sinker.未獲得給社長的信, true);
                //_7a97 = true;
                return;
            }
            Say(pc, 131, "您好！客人！$R;" +
                "歡迎來到商人行會總部$R;" +
                "$R今天有何貴幹呢？$R;");
            if (CountItem(pc, 10011202) >= 1 && CountItem(pc, 10011900) >= 1)
            {
                Say(pc, 131, "要轉交的物品？$R;" +
                    "啊…這太感謝您了$R;");
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "給了他『不明的合金』$R;" +
                    "給了他『紫水晶』$R;");
                TakeItem(pc, 10011202, 1);
                TakeItem(pc, 10011900, 1);
                Say(pc, 131, "嗯…鋼鐵工廠老闆送來的東西？$R;" +
                    "你問貴不貴是嗎？$R;" +
                    "不可能很貴吧…$R;" +
                    "$R這種東西也算貴呀，$R;" +
                    "鋼鐵工廠的老闆也真厲害啊$R;" +
                    "$P不過這個不行，$R;" +
                    "這是不能賣的仿造品$R;" +
                    "$R得重新委託鋼鐵工廠的老闆了$R;" +
                    "$P接受了『紫水晶』$R;" +
                    "$R啊…$R;" +
                    "如果可以的話把這封信交給老闆吧？$R;" +
                    "$P不能說是報酬，$R但會給您一些合金唷。$R;");
                //EVT1100081537
                if (CheckInventory(pc, 10043083, 1) && CheckInventory(pc, 10019302, 1))
                {
                    mask.SetValue(Sinker.未獲得給社長的信, false);
                    //_7a97 = false;
                    GiveItem(pc, 10043083, 1);
                    GiveItem(pc, 10019302, 1);
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 131, "得到了『給社長的信』$R;" +
                        "得到了『不知名的合金破片』$R;");
                    Say(pc, 131, "拜託了$R;");
                    return;
                }
                Say(pc, 131, "呀？行李太多了，不能給您阿$R;");
                mask.SetValue(Sinker.未獲得給社長的信, true);
                //_7a97 = true;
                return;
            }
            switch (Select(pc, "今天有什麽事情呢?", "", "購買到光之塔的飛空庭票", "委託銀行", "到銀行提錢", "使用倉庫（收費20金幣）", "什麽也不做"))
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
                        Say(pc, 131, "哦？客人已經…？$R;" +
                            "$R啊！沒什麽！$R;" +
                            "沒什麽問題了$R;");
                    }
                    //*/
                    int a = pc.Level * 200;
                    int b = pc.Level * 100;
                    Say(pc, 131, "知道了$R;" +
                        "是到光之塔的飛空庭票吧？$R;" +
                        "$R讓我看一下…$R;" +
                        "量一量客人的身高和體重…$R;" +
                        a + "金幣$R;" +
                        "$P但是今天很特別！$R;" +
                        "$R您知道嗎？$R;" +
                        "$P只給客人您一個人$R;" +
                        "提供特別服務！$R;" +
                        b + "金幣$R;" +
                        "提供給您！$R;" +
                        "$R現在是好的機會唷！$R;");
                    switch (Select(pc, "買不買飛空庭票呢?", "", "買", "不買"))
                    {
                        case 1:
                            if (b > pc.Gold)
                            {
                                Say(pc, 255, "唰…$R;");
                                return;
                            }
                            pc.Gold -= b;
                            Say(pc, 131, "沒錯$R;" +
                                b + "金幣$R;" +
                                "$P我們行會爲了節省$R;" +
                                "正在做代替的印章$R;" +
                                "$R給負責人看一下印有印章的手吧$R;");
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
                        Say(pc, 131, "資金不足喔!$R;", "行會商人");
                    }
                    break;
            }
        }
    }
}
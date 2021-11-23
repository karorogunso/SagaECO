using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaDB.Item;

namespace SagaScript
{
    public abstract class 行會商人 : Event
    {
        uint shopID;
        int wareFee;
        WarehousePlace warePlace;

        public 行會商人()
        {
        
        }

        /// <summary>
        /// 初始化行會商人
        /// </summary>
        /// <param name="eventID">事件ID</param>
        /// <param name="shop">商店ID</param>
        /// <param name="wareFee">倉庫費用</param>
        /// <param name="place">倉庫地點</param>

        protected void Init(uint eventID, uint shop, int wareFee, WarehousePlace place)
        {
            this.EventID = eventID;
            this.shopID = shop;
            this.wareFee = wareFee;
            this.warePlace = place;
        }

        public override void OnEvent(ActorPC pc)
        {
            string fee;

            if (wareFee > 0)
            {
                fee = wareFee.ToString() + "金幣";
            }
            else
            {
                fee = "免費";
            }

            switch (Select(pc, "想要做什麼事情呢?", "", "買東西", "賣東西", "到銀行存款", "到銀行提錢", "使用倉庫（" + fee + "）", "『寶物金幣』辦理", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, shopID);
                    break;

                case 2:
                    OpenShopSell(pc, shopID);
                    break;

                case 3:
                    BankDeposit(pc);
                    break;

                case 4:
                    BankWithdraw(pc);
                    break;

                case 5:
                    if (pc.Gold >= wareFee)
                    {
                        pc.Gold -= wareFee;

                        OpenWareHouse(pc, warePlace);
                    }
                    else
                    {
                        Say(pc, 131, "資金不足喔!$R;", "行會商人");
                    }
                    break;
                case 6:
                    switch (Select(pc, "『寶物金幣』的辦理么？", "", "『寶物金幣』購買", "『寶物金幣』販賣", "什麽是『寶物金幣』？", "沒什麼"))
                    {
                        case 1:
                            Say(pc, 11000050, 131, "啊啊！這位尊貴的客人……$R;" +
                            "因為準備了很多$R;" +
                            "請來購入點吧。$R;" +
                            "$P『寶物金幣』$R;" +
                            "購買的時候$R;" +
                            "會收取你１％的手續費、$R;" +
                            "這樣可以麼？$R;", "行會商人");
                            if (Select(pc, "１％的手續費如何？", "", "好", "不好") == 1)
                            {
                                string temp = InputBox(pc, "購買幾個呢？", InputType.Bank);
                                if (temp != "")
                                {
                                    ushort count = ushort.Parse(temp);
                                    if (count <= 99 && pc.Gold >= (count * 1010000))
                                    {
                                        Say(pc, 11000050, 131, "『寶物金幣』$R;" +
                                            count + "個購入。$R;" +
                                            "請您笑納……。$R;", "行會商人");
                                        PlaySound(pc, 2040, false, 100, 50);
                                        pc.Gold -= (count * 1010000);
                                        GiveItem(pc, 10037600, count);

                                        Say(pc, 0, 131, "『寶物金幣』$R;" +
                                        count + "個$R;" +
                                        "獲得！$R;", " ");
                                    }
                                    else
                                    {
                                        Say(pc, 0, 131, "钱好像不够呢！$R;", " ");
                                    }
                                }
                            }
                            break;
                        case 2:
                            if (CountItem(pc, 10037600) >= 1)
                            {
                                if (Select(pc, "要兌換嗎？", "", "是", "不") == 1)
                                {
                                    string temp = InputBox(pc, "要兌換幾個？", InputType.Bank);
                                    if (temp != "temp")
                                    {
                                        int count = int.Parse(temp);
                                        if (CountItem(pc, 10037600) >= count)
                                        {
                                            if (pc.Gold + (uint)(count * 1000000) <= 99999999)
                                            {
                                                Say(pc, 0, 131, "失去" + count + "個『寶物金幣』。$R;", "行會商人");
                                                TakeItem(pc, 10037600, (ushort)count);
                                                pc.Gold += (count * 1000000);
                                                Say(pc, 0, 131, "得到" + (count * 1000000) + "G。$R;", "行會商人");
                                            }
                                            else
                                            {
                                                Say(pc, 0, 131, "身上攜帶的錢太多了呢。$R;", "行會商人");
                                            }
                                        }
                                        else
                                        {
                                            Say(pc, 0, 131, "『寶物金幣』好像不够呢。$R;", "行會商人");
                                        }
                                    }
                                }
                            }
                            else
                                Say(pc, 0, 131, "你手上好像没有『寶物金幣』呢！$R;", "行會商人");
                            break;
                        case 3:
                            Say(pc, 11000050, 131, "我們正在受理一種$R;" +
                            "特別的寶石。$R;" +
                            "$R正是如此，皮卡皮卡！$R;" +
                            "$可以通過100萬金幣購買、$R;" +
                            "也可以再在我們行會商人這裡，$R;" +
                            "兌換等價的100萬金幣。$R;" +
                            "$P一個『寶物金幣』相當於$R;" +
                            "１００萬金幣的價值……$R;" +
                            "$R２個的話，相當於200萬金幣！$R;" +
                            "３個的話，相當於300萬金幣！$R;" +
                            "$P１０的話呢？一，一千萬金幣哦……。$R;" +
                            "$R啊，啊啊啊啊……有那麼多錢？$R;" +
                            "$P要問為什麼會准備這種$R;" +
                            "東西的話……。$R;" +
                            "$P例如，您的錢包沒辦法$R;" +
                            "放入９９９９萬９９９９$R;" +
                            "以上的金幣吧？$R;" +
                            "$P需要和朋友們和伙伴們，$R;" +
                            "安全地交易９９９９萬９９９９$R;" +
                            "以上的時候就請考慮使用吧。$R;" +
                            "$P用『寶物金幣』交易的話$R;" +
                            "交易一次就可以了！$R;" +
                            "$R之後再安心的交換成金幣了。$R;" +
                            "$P另外，因為是特別的寶石，$R;" +
                            "所以只能在我們行會商人這裡使用$R;" +
                            "$P還有在買入的時候會$R;" +
                            "收取１％的手續費。$R;" +
                            "因此請注意一下。$R;" +
                            "也就是說1個『寶物金幣』$R;" +
                            "的話要收取1萬金幣的手續費。$R;" +
                            "$P如果有需要的話，請隨時$R;" +
                            "都來光顧我們行會商人。$R;", "行會商人");
                            break;
                    }
                    break;
            }

            Say(pc, 131, "歡迎下次光臨唷!$R;", "行會商人");
        }
    }
}

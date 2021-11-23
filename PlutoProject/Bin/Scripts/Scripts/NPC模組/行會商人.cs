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
                fee = wareFee.ToString() + "金币";
            }
            else
            {
                fee = "免费";
            }

            switch (Select(pc, "想要做什么事情呢?", "", "买东西", "卖东西", "到银行存款", "到银行取款", "使用仓库（" + fee + "）", "『代金珠宝』办理", "什么也不做"))
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
                        Say(pc, 131, "资金不足哦!$R;", "行会商人");
                    }
                    break;
                case 6:
                    switch (Select(pc, "『代金珠宝』的办理么？", "", "『代金珠宝』购买", "『代金珠宝』出售", "什么是『代金珠宝』？", "没什么"))
                    {
                        case 1:
                            Say(pc, 11000050, 131, "啊啊！这位尊贵的客人……$R;" +
                            "因为准备了很多$R;" +
                            "请来购入点吧。$R;" +
                            "$P『代金珠宝』$R;" +
                            "购买的时候$R;" +
                            "会收取你１％的手续费、$R;" +
                            "这样可以么？$R;", "行会商人");
                            if (Select(pc, "１％的手续费如何？", "", "好", "不好") == 1)
                            {
                                string temp = InputBox(pc, "购买几个呢？", InputType.Bank);
                                if (temp != "")
                                {
                                    ushort count = ushort.Parse(temp);
                                    if (count <= 99 && pc.Gold >= (count * 1010000))
                                    {
                                        Say(pc, 11000050, 131, "『代金珠宝』$R;" +
                                            count + "个购入。$R;" +
                                            "请您笑纳……。$R;", "行会商人");
                                        PlaySound(pc, 2040, false, 100, 50);
                                        pc.Gold -= (count * 1010000);
                                        GiveItem(pc, 10037600, count);

                                        Say(pc, 0, 131, "『代金珠宝』$R;" +
                                        count + "个$R;" +
                                        "获得！$R;", " ");
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
                                if (Select(pc, "要兑换吗？", "", "是", "不") == 1)
                                {
                                    string temp = InputBox(pc, "要兑换几个？", InputType.Bank);
                                    if (temp != "temp")
                                    {
                                        int count = int.Parse(temp);
                                        if (CountItem(pc, 10037600) >= count)
                                        {
                                            if (pc.Gold + (uint)(count * 1000000) <= 99999999)
                                            {
                                                Say(pc, 0, 131, "失去" + count + "个『代金珠宝』。$R;", "行会商人");
                                                TakeItem(pc, 10037600, (ushort)count);
                                                pc.Gold += (count * 1000000);
                                                Say(pc, 0, 131, "得到" + (count * 1000000) + "G。$R;", "行会商人");
                                            }
                                            else
                                            {
                                                Say(pc, 0, 131, "身上携带的钱太多了呢。$R;", "行会商人");
                                            }
                                        }
                                        else
                                        {
                                            Say(pc, 0, 131, "『代金珠宝』好像不够呢。$R;", "行会商人");
                                        }
                                    }
                                }
                            }
                            else
                                Say(pc, 0, 131, "你手上好像没有『代金珠宝』呢！$R;", "行会商人");
                            break;
                        case 3:
                            Say(pc, 11000050, 131, "我们正在受理一种$R;" +
                            "特別的宝石。$R;" +
                            "$R;" +
                            "可以通过100万金币购买、$R;" +
                            "也可以再在我们行会商人这里，$R;" +
                            "兑换等价的100万金币。$R;" +
                            "$P一个『宝物金币』相当于$R;" +
                            "１００万金币的价值……$R;" +
                            "$R２个的话，相当于200万金币！$R;" +
                            "３个的话，相当于300万金币！$R;" +
                            "$P１０的话呢？一，一千万金币哦……。$R;" +
                            "$R啊，啊啊啊啊……有那么多钱？$R;" +
                            "$P要问为什么会准备这种$R;" +
                            "东西的话……。$R;" +
                            "$P例如，您的钱包没办法$R;" +
                            "放入９９９９万９９９９$R;" +
                            "以上的金币吧？$R;" +
                            "$P需要和朋友们和伙伴们，$R;" +
                            "安全地交易９９９９万９９９９$R;" +
                            "以上的时候就请考虑使用吧。$R;" +
                            "$P用『代金珠宝』交易的话$R;" +
                            "交易一次就可以了！$R;" +
                            "$R之后再安心的交换成金币了。$R;" +
                            "$P另外，因为是特别的宝石，$R;" +
                            "所以只能在我们行会商人这里使用$R;" +
                            "$P还有在买入的时候会$R;" +
                            "收取１％的手续费。$R;" +
                            "因此请注意一下。$R;" +
                            "也就是说1个『代金珠宝』$R;" +
                            "的话要收取1万金币的手续费。$R;" +
                            "$P如果有需要的话，请随时$R;" +
                            "都来光顾我们行会商人。$R;", "行会商人");
                            break;
                    }
                    break;
            }

            Say(pc, 131, "欢迎下次光临!$R;", "行会商人");
        }
    }
}

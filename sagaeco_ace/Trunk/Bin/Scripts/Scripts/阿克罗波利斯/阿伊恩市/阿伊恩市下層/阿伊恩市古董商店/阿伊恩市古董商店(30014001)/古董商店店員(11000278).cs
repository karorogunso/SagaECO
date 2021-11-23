using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30021002
{
    public class S11000278 : Event
    {
        public S11000278()
        {
            this.EventID = 11000278;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "这里是古董商店。", "", "买东西", "卖东西", "装配机械", "打开『集装箱』", "交换『合成失败物』", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 151);
                    Say(pc, 131, "再看看吧。$R;");
                    break;
                case 2:
                    OpenShopSell(pc, 151);
                    break;
                case 3:
                    Synthese(pc, 2039, 3);
                    break;
                case 4:
                    OpenTreasureBox(pc);
                    break;
                case 5:
                    交換合成失敗物(pc);
                    break;
            }
        }

        void 交換合成失敗物(ActorPC pc)
        {
            string quantity;
            ushort number01, number02;
            switch (Select(pc, "交换『合成失败物』！", "", "杰利科", "霉菌", "红色杰利科", "不交换"))
            {
                case 1:
                    quantity = InputBox(pc, "要换几个？", InputType.Bank);

                    if (quantity != "")
                    {
                        number01 = ushort.Parse(quantity);

                        number02 = (ushort)(number01 * 1);

                        if (CountItem(pc, 10001250) >= number02)
                        {
                            if (CheckInventory(pc, 10032800, number01))
                            {
                                TakeItem(pc, 10001250, number02);
                                Say(pc, 0, 0, "『合成失败物』" + quantity + "个。$R;", " ");

                                GiveItem(pc, 10032800, number01);
                                Say(pc, 0, 0, "交换了『杰利科』" + quantity + "个。$R;", " ");
                            }
                            else
                            {
                                Say(pc, 131, "行李太多了，整理一下吧$R;");
                            }
                        }
                        else
                        {
                            Say(pc, 131, "合成失败物不足吗?$R;");
                        }
                    }
                    break;

                case 2:
                    quantity = InputBox(pc, "要换几个？", InputType.Bank);

                    if (quantity != "")
                    {
                        number01 = ushort.Parse(quantity);

                        number02 = (ushort)(number01 * 10);

                        if (CountItem(pc, 10001250) >= number02)
                        {
                            if (CheckInventory(pc, 10034502, number01))
                            {
                                TakeItem(pc, 10001250, number02);
                                Say(pc, 0, 0, "『合成失败物』" + quantity + "个。$R;", " ");

                                GiveItem(pc, 10034502, number01);
                                Say(pc, 0, 0, "交换了『霉菌』" + quantity + "个。$R;", " ");
                            }
                            else
                            {
                                Say(pc, 131, "行李太多了，整理一下吧$R;");
                            }
                        }
                        else
                        {
                            Say(pc, 131, "合成失败物不足吗?$R;");
                        }
                    }
                    break;

                case 3:
                    quantity = InputBox(pc, "要换几个？", InputType.Bank);

                    if (quantity != "")
                    {
                        number01 = ushort.Parse(quantity);

                        number02 = (ushort)(number01 * 100);

                        if (CountItem(pc, 10001250) >= number02)
                        {
                            if (CheckInventory(pc, 10032801, number01))
                            {
                                TakeItem(pc, 10001250, number02);
                                Say(pc, 0, 0, "『合成失败物』" + quantity + "个。$R;", " ");

                                GiveItem(pc, 10032801, number01);
                                Say(pc, 0, 0, "交换了『红色杰利科』" + quantity + "个。$R;", " ");
                            }
                            else
                            {
                                Say(pc, 131, "行李太多了，整理一下吧$R;");
                            }
                        }
                        else
                        {
                            Say(pc, 131, "合成失败物不足吗?$R;");
                        }
                    }
                    break;
            }
        }
    }
}
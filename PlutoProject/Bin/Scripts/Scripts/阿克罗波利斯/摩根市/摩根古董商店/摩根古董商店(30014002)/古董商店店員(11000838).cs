using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30014002
{
    public class S11000838 : Event
    {
        public S11000838()
        {
            this.EventID = 11000838;

        }

        public override void OnEvent(ActorPC pc)
        {
            string quantity;
            ushort number01, number02;

            switch (Select(pc, "什么事呢？", "", "买东西", "卖东西", "交换『合成失败物』", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 174);
                    break;
                case 2:
                    OpenShopSell(pc, 174);
                    break;
                case 3:
                    switch (Select(pc, "交换『合成失败物』", "", "杰利科", "霉菌1个=需要10个合成失败物", "黄色杰利科", "不交换"))
                    {
                        case 1:
                            quantity = InputBox(pc, "交换几个？", InputType.Bank);

                            if (quantity != "")
                            {
                                number01 = ushort.Parse(quantity);

                                number02 = (ushort)(number01 * 1);

                                if (CountItem(pc, 10001250) >= number02)
                                {
                                    if (CheckInventory(pc, 10032800, number01))
                                    {
                                        TakeItem(pc, 10001250, number02);
                                        Say(pc, 0, 65535, "『合成失败物』" + quantity + "个和$R;", " ");

                                        GiveItem(pc, 10032800, number01);
                                        Say(pc, 0, 65535, "交换了『杰利科』" + quantity + "个$R;", " ");
                                    }
                                    else
                                    {
                                        Say(pc, 131, "行李太多了$R;");
                                    }
                                }
                                else
                                {
                                    Say(pc, 131, "合成失败物不够吗？$R;");
                                }
                            }
                            break;
                        case 2:
                            quantity = InputBox(pc, "交换几个？", InputType.Bank);

                            if (quantity != "")
                            {
                                number01 = ushort.Parse(quantity);

                                number02 = (ushort)(number01 * 1);

                                if (CountItem(pc, 10001250) >= number02)
                                {
                                    if (CheckInventory(pc, 10032800, number01))
                                    {
                                        TakeItem(pc, 10001250, number02);
                                        Say(pc, 0, 65535, "『合成失败物』" + quantity + "个和$R;", " ");

                                        GiveItem(pc, 10034502, number01);
                                        Say(pc, 0, 65535, "交换了『霉菌』" + quantity + "个$R;", " ");
                                    }
                                    else
                                    {
                                        Say(pc, 131, "行李太多了$R;");
                                    }
                                }
                                else
                                {
                                    Say(pc, 131, "合成失败物不够吗？$R;");
                                }
                            }
                            break;
                        case 3:
                            quantity = InputBox(pc, "交换几个？", InputType.Bank);

                            if (quantity != "")
                            {
                                number01 = ushort.Parse(quantity);

                                number02 = (ushort)(number01 * 10);

                                if (CountItem(pc, 10001250) >= number02)
                                {
                                    if (CheckInventory(pc, 10032800, number01))
                                    {
                                        TakeItem(pc, 10001250, number02);
                                        Say(pc, 0, 65535, "『合成失败物』" + quantity + "个和$R;", " ");

                                        GiveItem(pc, 10032807, number01);
                                        Say(pc, 0, 65535, "交换了『黄色杰利科』" + quantity + "个$R;", " ");
                                    }
                                    else
                                    {
                                        Say(pc, 131, "行李太多了$R;");
                                    }
                                }
                                else
                                {
                                    Say(pc, 131, "合成失败物不够吗？$R;");
                                }
                            }
                            break;

                        case 4:
                            break;
                    }
                    break;

                case 4:
                    break;
            }

        }
    }
}
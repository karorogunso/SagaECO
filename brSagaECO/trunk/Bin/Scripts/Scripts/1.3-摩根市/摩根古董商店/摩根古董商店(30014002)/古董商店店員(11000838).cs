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

            switch (Select(pc, "什麼事呢？", "", "買東西", "賣東西", "交換『合成失敗物』", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 174);
                    break;
                case 2:
                    OpenShopSell(pc, 174);
                    break;
                case 3:
                    switch (Select(pc, "交換『合成失敗物』", "", "杰利科", "微菌1個=需要10個合成失敗物", "黃色杰利科", "不交換"))
                    {
                        case 1:
                            quantity = InputBox(pc, "交換幾個？", InputType.Bank);

                            if (quantity != "")
                            {
                                number01 = ushort.Parse(quantity);

                                number02 = (ushort)(number01 * 1);

                                if (CountItem(pc, 10001250) >= number02)
                                {
                                    if (CheckInventory(pc, 10032800, number01))
                                    {
                                        TakeItem(pc, 10001250, number02);
                                        Say(pc, 0, 65535, "『合成失敗物』" + quantity + "個和$R;", " ");

                                        GiveItem(pc, 10032800, number01);
                                        Say(pc, 0, 65535, "交換了『杰利科』" + quantity + "個$R;", " ");
                                    }
                                    else
                                    {
                                        Say(pc, 131, "行李太多了$R;");
                                    }
                                }
                                else
                                {
                                    Say(pc, 131, "合成失敗物不夠嗎？$R;");
                                }
                            }
                            break;
                        case 2:
                            quantity = InputBox(pc, "交換幾個？", InputType.Bank);

                            if (quantity != "")
                            {
                                number01 = ushort.Parse(quantity);

                                number02 = (ushort)(number01 * 1);

                                if (CountItem(pc, 10001250) >= number02)
                                {
                                    if (CheckInventory(pc, 10032800, number01))
                                    {
                                        TakeItem(pc, 10001250, number02);
                                        Say(pc, 0, 65535, "『合成失敗物』" + quantity + "個和$R;", " ");

                                        GiveItem(pc, 10034502, number01);
                                        Say(pc, 0, 65535, "交換了『微菌』" + quantity + "個$R;", " ");
                                    }
                                    else
                                    {
                                        Say(pc, 131, "行李太多了$R;");
                                    }
                                }
                                else
                                {
                                    Say(pc, 131, "合成失敗物不夠嗎？$R;");
                                }
                            }
                            break;
                        case 3:
                            quantity = InputBox(pc, "交換幾個？", InputType.Bank);

                            if (quantity != "")
                            {
                                number01 = ushort.Parse(quantity);

                                number02 = (ushort)(number01 * 10);

                                if (CountItem(pc, 10001250) >= number02)
                                {
                                    if (CheckInventory(pc, 10032800, number01))
                                    {
                                        TakeItem(pc, 10001250, number02);
                                        Say(pc, 0, 65535, "『合成失敗物』" + quantity + "個和$R;", " ");

                                        GiveItem(pc, 10032807, number01);
                                        Say(pc, 0, 65535, "交換了『黃色杰利科』" + quantity + "個$R;", " ");
                                    }
                                    else
                                    {
                                        Say(pc, 131, "行李太多了$R;");
                                    }
                                }
                                else
                                {
                                    Say(pc, 131, "合成失敗物不夠嗎？$R;");
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
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30014003
{
    public class S11001049 : Event
    {
        public S11001049()
        {
            this.EventID = 11001049;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "來的好~！", "", "買東西", "賣東西", "交換『合成失敗物』", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 210);
                    break;
                case 2:
                    OpenShopSell(pc, 210);
                    break;
                case 3:
                    交換合成失敗物(pc);
                    break;
                case 4:
                    break;
            }
        }


        void 交換合成失敗物(ActorPC pc)
        {
            string quantity;
            ushort number01, number02;

            switch (Select(pc, "想要交換什麼?", "", "1個=杰利科", "10個=微菌", "100個=紫色杰利科", "不想交換"))
            {
                case 1:
                    quantity = InputBox(pc, "想要交換幾個呢?", InputType.Bank);

                    if (quantity != "")
                    {
                        number01 = ushort.Parse(quantity);

                        number02 = (ushort)(number01 * 1);

                        if (CountItem(pc, 10001250) >= number02)
                        {
                            if (CheckInventory(pc, 10032800, number01))
                            {
                                TakeItem(pc, 10001250, number02);
                                Say(pc, 0, 65535, "交換『合成失敗物』" + quantity + "個。$R;", " ");

                                GiveItem(pc, 10032800, number01);
                                Say(pc, 0, 65535, "得到『杰利科』" + quantity + "個。$R;", " ");
                            }
                            else
                            {
                                Say(pc, 11000003, 131, "行李太多了?$R;", "古董商店 店員");
                            }
                        }
                        else
                        {
                            Say(pc, 11000003, 131, "『合成失敗物』數目不夠喔!$R;", "古董商店 店員");
                        }
                    }
                    break;

                case 2:
                    quantity = InputBox(pc, "想要交換幾個呢?", InputType.Bank);

                    if (quantity != "")
                    {
                        number01 = ushort.Parse(quantity);

                        number02 = (ushort)(number01 * 10);

                        if (CountItem(pc, 10001250) >= number02)
                        {
                            if (CheckInventory(pc, 10034502, number01))
                            {
                                TakeItem(pc, 10001250, number02);
                                Say(pc, 0, 65535, "交換『合成失敗物』" + quantity + "個。$R;", " ");

                                GiveItem(pc, 10034502, number01);
                                Say(pc, 0, 65535, "得到『微菌』" + quantity + "個。$R;", " ");
                            }
                            else
                            {
                                Say(pc, 11000003, 131, "行李太多了?$R;", "古董商店 店員");
                            }
                        }
                        else
                        {
                            Say(pc, 11000003, 131, "『合成失敗物』數目不夠喔!$R;", "古董商店 店員");
                        }
                    }
                    break;

                case 3:
                    quantity = InputBox(pc, "想要交換幾個呢?", InputType.Bank);

                    if (quantity != "")
                    {
                        number01 = ushort.Parse(quantity);

                        number02 = (ushort)(number01 * 100);

                        if (CountItem(pc, 10032802) >= number02)
                        {
                            if (CheckInventory(pc, 10032802, number01))
                            {
                                TakeItem(pc, 10001250, number02);
                                Say(pc, 0, 65535, "交換『合成失敗物』" + quantity + "個。$R;", " ");

                                GiveItem(pc, 10032803, number01);
                                Say(pc, 0, 65535, "得到『紫色杰利科』" + quantity + "個。$R;", " ");
                            }
                            else
                            {
                                Say(pc, 11000003, 131, "行李太多了?$R;", "古董商店 店員");
                            }
                        }
                        else
                        {
                            Say(pc, 11000003, 131, "『合成失敗物』數目不夠喔!$R;", "古董商店 店員");
                        }
                    }
                    break;

                case 4:
                    break;
            }
        }
    }
}
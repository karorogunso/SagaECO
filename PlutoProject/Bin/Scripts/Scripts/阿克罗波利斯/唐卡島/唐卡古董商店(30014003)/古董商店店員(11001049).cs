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
            switch (Select(pc, "来的好~！", "", "买东西", "卖东西", "交换『合成失败物』", "什么也不做"))
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

            switch (Select(pc, "想要交换什么?", "", "1个=杰利科", "10个=霉菌", "100个=紫色杰利科", "不想交换"))
            {
                case 1:
                    quantity = InputBox(pc, "想要交换几个呢?", InputType.Bank);

                    if (quantity != "")
                    {
                        number01 = ushort.Parse(quantity);

                        number02 = (ushort)(number01 * 1);

                        if (CountItem(pc, 10001250) >= number02)
                        {
                            if (CheckInventory(pc, 10032800, number01))
                            {
                                TakeItem(pc, 10001250, number02);
                                Say(pc, 0, 65535, "交换『合成失败物』" + quantity + "个。$R;", " ");

                                GiveItem(pc, 10032800, number01);
                                Say(pc, 0, 65535, "得到『杰利科』" + quantity + "个。$R;", " ");
                            }
                            else
                            {
                                Say(pc, 11000003, 131, "行李太多了?$R;", "古董商店 店员");
                            }
                        }
                        else
                        {
                            Say(pc, 11000003, 131, "『合成失败物』数目不够喔!$R;", "古董商店 店员");
                        }
                    }
                    break;

                case 2:
                    quantity = InputBox(pc, "想要交换几个呢?", InputType.Bank);

                    if (quantity != "")
                    {
                        number01 = ushort.Parse(quantity);

                        number02 = (ushort)(number01 * 10);

                        if (CountItem(pc, 10001250) >= number02)
                        {
                            if (CheckInventory(pc, 10034502, number01))
                            {
                                TakeItem(pc, 10001250, number02);
                                Say(pc, 0, 65535, "交换『合成失败物』" + quantity + "个。$R;", " ");

                                GiveItem(pc, 10034502, number01);
                                Say(pc, 0, 65535, "得到『霉菌』" + quantity + "个。$R;", " ");
                            }
                            else
                            {
                                Say(pc, 11000003, 131, "行李太多了?$R;", "古董商店 店员");
                            }
                        }
                        else
                        {
                            Say(pc, 11000003, 131, "『合成失败物』数目不够喔!$R;", "古董商店 店员");
                        }
                    }
                    break;

                case 3:
                    quantity = InputBox(pc, "想要交换几个呢?", InputType.Bank);

                    if (quantity != "")
                    {
                        number01 = ushort.Parse(quantity);

                        number02 = (ushort)(number01 * 100);

                        if (CountItem(pc, 10032802) >= number02)
                        {
                            if (CheckInventory(pc, 10032802, number01))
                            {
                                TakeItem(pc, 10001250, number02);
                                Say(pc, 0, 65535, "交换『合成失败物』" + quantity + "个。$R;", " ");

                                GiveItem(pc, 10032803, number01);
                                Say(pc, 0, 65535, "得到『紫色杰利科』" + quantity + "个。$R;", " ");
                            }
                            else
                            {
                                Say(pc, 11000003, 131, "行李太多了?$R;", "古董商店 店员");
                            }
                        }
                        else
                        {
                            Say(pc, 11000003, 131, "『合成失败物』数目不够喔!$R;", "古董商店 店员");
                        }
                    }
                    break;

                case 4:
                    break;
            }
        }
    }
}
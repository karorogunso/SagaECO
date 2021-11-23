using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

using SagaDB.Item;
//所在地圖:古董商店(30014000) NPC基本信息:古董商店 店員(11000003) X:5 Y:1
namespace SagaScript.M30014000
{
    public class S11000003 : Event
    {
        public S11000003()
        {
            this.EventID = 11000003;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Halloween_00> Halloween_00_mask = pc.CMask["Halloween_00"];                                                                                         //活動：萬聖節
            BitMask<Neko_04> Neko_04_mask = pc.CMask["Neko_04"];                                                                                                        //任務：凱堤(山吹)
            BitMask<Puppet_01> Puppet_01_mask = pc.CMask["Puppet_01"];
            //任務：製作『活動木偶 電路機械』
            BitMask<Neko_04> Neko_04_amask = pc.AMask["Neko_04"];
            BitMask<Neko_04> Neko_04_cmask = pc.CMask["Neko_04"];

            Halloween_00_mask.SetValue(Halloween_00.萬聖節活動期間, false);                                                                                             //萬聖節活動期間                開/關

            if (Neko_04_amask.Test(Neko_04.任務開始) &&
                !Neko_04_amask.Test(Neko_04.任務結束) &&
                Neko_04_cmask.Test(Neko_04.告知關卡的事情) && 
                !Neko_04_cmask.Test(Neko_04.與小孩對話) &&
                !Neko_04_cmask.Test(Neko_04.製作共振體))
            {
                if (CountItem(pc, 10014500) >= 5)
                {
                    Say(pc, 11000003, 131, "用『水晶破片』5個$R製作『水晶共振體』嗎？$R;");
                    switch (Select(pc, "怎麼做呢？", "", "做", "放棄"))
                    {
                        case 1:
                            if (CheckInventory(pc, 10014350, 1))
                            {
                                TakeItem(pc, 10014500, 5);
                                GiveItem(pc, 10014350, 1);
                                Neko_04_cmask.SetValue(Neko_04.製作共振體, true);
                                PlaySound(pc, 2040, false, 100, 50);
                                Say(pc, 0, 131, "收到了『水晶共振體』！$R;");
                                Say(pc, 11000003, 131, "拿著那個的話$R應該可以通過「關卡」$R;" +
                                    "$P如果有人在憑依$R會因為波長會震動而導致失敗的$R所以小心啊！$R;");
                                return;
                            }
                            Say(pc, 11000003, 131, "行李太多了？把行李減少以後再來！$R;");
                            //GOTO EVT1100000338
                            return;
                        case 2:
                            break;
                    }
                }
                if (CountItem(pc, 10014300) >= 1)
                {
                    Say(pc, 11000003, 131, "用『水晶』1個$R製作『水晶共振體』嗎？$R;");
                    switch (Select(pc, "怎麼做呢？", "", "做", "放棄"))
                    {
                        case 1:
                            if (CheckInventory(pc, 10014350, 1))
                            {
                                TakeItem(pc, 10014300, 1);
                                GiveItem(pc, 10014350, 1);
                                Neko_04_cmask.SetValue(Neko_04.製作共振體, true);
                                PlaySound(pc, 2040, false, 100, 50);
                                Say(pc, 0, 131, "收到了『水晶共振體』！$R;");
                                Say(pc, 11000003, 131, "拿著那個的話$R應該可以通過「關卡」$R;" +
                                    "$P如果有人在憑依$R會因為波長會震動而導致失敗的$R所以小心啊！$R;");
                                return;
                            }
                            Say(pc, 11000003, 131, "行李太多了？把行李減少以後再來！$R;");
                            break;
                        case 2:
                            break;
                    }
                }
            }
            if (Neko_04_amask.Test(Neko_04.任務開始) &&
                !Neko_04_amask.Test(Neko_04.任務結束) &&
                Neko_04_cmask.Test(Neko_04.碰到看不見的墻) &&
                !Neko_04_cmask.Test(Neko_04.告知關卡的事情))
            {
                Neko_04_cmask.SetValue(Neko_04.告知關卡的事情, true);
                Say(pc, 11000003, 131, "什麼？有看不見的牆！$R所以不能前進？$R;" +
                    "$R啊！那應該是「關卡」$R;" +
                    "$R你現在在瑪衣瑪衣遺跡的光之塔$R探索嗎？$R;" +
                    "$P嗯！中和關卡的物件可以自己做$R;" +
                    "$R原理是把「水晶」特別調整後$R把跟關卡的重力波頻率一樣的波長$R送到關卡的發動機$R;" +
                    "$P那樣做的話可以造成$R能夠通過的空間縫$R;" +
                    "$R古魯杜先生也經常委託的$R;" +
                    "$P需要1個『水晶』或5個『水晶破片』$R拿過來的話就幫你做$R;");
                return;
            }

            if (Neko_04_mask.Test(Neko_04.山吹任務開始) &&
                !Neko_04_mask.Test(Neko_04.水晶共振體製作完成))
            {
                山吹任務(pc);
            }

            if (Halloween_00_mask.Test(Halloween_00.萬聖節活動期間))
            {
                萬聖節(pc);
            }

            製作活動木偶電路機械(pc);

            switch (Select(pc, "歡迎光臨!!", "", "買東西", "賣東西", "委託「組裝機械」", "委託打開『集裝箱』", "交換『合成失敗物』", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 6);

                    Say(pc, 11000003, 131, "下次再見吧!$R;", "古董商店 店員");
                    break;

                case 2:
                    OpenShopSell(pc, 6);

                    Say(pc, 11000003, 131, "下次再見吧!$R;", "古董商店 店員");
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

                case 6:
                    Say(pc, 11000003, 131, "下次再見吧!$R;", "古董商店 店員");
                    break;
            }
        }

        void 萬聖節(ActorPC pc)
        {
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.HEAD))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50025800 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024350 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024351 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024352 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024353 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024354 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024355 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024356 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024357 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024358 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50022500 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50022600 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50022700 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50022800)
                {
                    Say(pc, 11000003, 131, "啊! 嚇我一跳!!$R;", "古董商店 店員");
                }
            }
        }

        void 交換合成失敗物(ActorPC pc)
        {
            string quantity;
            ushort number01, number02;

            switch (Select(pc, "想要交換什麼?", "", "杰利科 (需要1個『合成失敗物』)", "麵包樹果實 (需要10個『合成失敗物』)", "藍色杰利科 (需要100個『合成失敗物』)", "不想交換"))
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
                            if (CheckInventory(pc, 10006000, number01))
                            {
                                TakeItem(pc, 10001250, number02);
                                Say(pc, 0, 65535, "交換『合成失敗物』" + quantity + "個。$R;", " ");

                                GiveItem(pc, 10006000, number01);
                                Say(pc, 0, 65535, "得到『麵包樹果實』" + quantity + "個。$R;", " ");
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

                        if (CountItem(pc, 10001250) >= number02)
                        {
                            if (CheckInventory(pc, 10032803, number01))
                            {
                                TakeItem(pc, 10001250, number02);
                                Say(pc, 0, 65535, "交換『合成失敗物』" + quantity + "個。$R;", " ");

                                GiveItem(pc, 10032803, number01);
                                Say(pc, 0, 65535, "得到『藍色杰利科』" + quantity + "個。$R;", " ");
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

        void 製作活動木偶電路機械(ActorPC pc)
        {
            BitMask<Puppet_01> Puppet_01_mask = new BitMask<Puppet_01>(pc.CMask["Puppet_01"]);                                                                          //任務：製作『活動木偶 電路機械』

            if (!Puppet_01_mask.Test(Puppet_01.古董商店店員給予洋鐵鋸齒輪) &&
                (CountItem(pc, 10015500) > 0 ||
                 CountItem(pc, 10018000) > 0 ||
                 CountItem(pc, 10029900) > 0 ||
                 CountItem(pc, 10016900) > 0 ||
                 CountItem(pc, 10039000) > 0 ||
                 CountItem(pc, 10023100) > 0 ||
                 CountItem(pc, 10023200) > 0 ||
                 CountItem(pc, 10030201) > 0))
            {
                詢問要不要製作活動木偶電路機械(pc);
                return;
            }

            if (Puppet_01_mask.Test(Puppet_01.古董商店店員給予洋鐵鋸齒輪) &&
                !Puppet_01_mask.Test(Puppet_01.委託製作洋鐵的心))
            {
                製作電路機械(pc);
                return;
            }

            if (Puppet_01_mask.Test(Puppet_01.委託製作洋鐵的心) &&
                !Puppet_01_mask.Test(Puppet_01.古董商店店員給予空空的心))
            {
                製作洋鐵的心(pc);
                return;
            }

            if (Puppet_01_mask.Test(Puppet_01.古董商店店員給予空空的心) &&
                !Puppet_01_mask.Test(Puppet_01.聖堂祭司給予洋鐵的心))
            {
                Say(pc, 11000003, 131, "拿去聖堂試試看吧。$R;", "古董商店 店員");
            }

            if (Puppet_01_mask.Test(Puppet_01.聖堂祭司給予洋鐵的心))
            {
                活動木偶電路機械製作材料收集完成(pc);
            }
        }

        void 詢問要不要製作活動木偶電路機械(ActorPC pc)
        {
            Say(pc, 11000003, 131, "你喜歡機器人嗎?$R;", "古董商店 店員");

            switch (Select(pc, "你喜歡機器人嗎?", "", "喜歡", "不喜歡"))
            {
                case 1:
                    古董商店店員給予洋鐵鋸齒輪(pc);
                    break;

                case 2:
                    Say(pc, 11000003, 131, "是嗎?$R;" +
                                           "$R我知道了!$R;" +
                                           "沒關係的，請你不要在意。$R;", "古董商店 店員");
                    break;
            }
        }

        void 古董商店店員給予洋鐵鋸齒輪(ActorPC pc)
        {
            BitMask<Puppet_01> Puppet_01_mask = new BitMask<Puppet_01>(pc.CMask["Puppet_01"]);                                                                          //任務：製作『活動木偶 電路機械』

            Say(pc, 11000003, 131, "喜歡?!$R;" +
                                   "$R那樣的話，$R;" +
                                   "想不想親自製作$R;" +
                                   "『活動木偶電路機械』看看?$R;", "古董商店 店員");

            switch (Select(pc, "想要製作看看嗎?", "", "製作", "不製作"))
            {
                case 1:
                    Say(pc, 11000003, 131, "那我告訴你製作方法吧!$R;" +
                                           "$P先收下這個，$R;" +
                                           "這是製作所需的材料之一。$R;", "古董商店 店員");

                    if (CheckInventory(pc, 10023400, 1))
                    {
                        Puppet_01_mask.SetValue(Puppet_01.古董商店店員給予洋鐵鋸齒輪, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        GiveItem(pc, 10023400, 1);
                        Say(pc, 0, 65535, "得到『洋鐵鋸齒輪』$R;", " ");

                        活動木偶電路機械的製作方法(pc);
                    }
                    else
                    {
                        Say(pc, 11000003, 131, "…$R;" +
                                               "$P行李太多了，沒辦法給你啊!$R;", "古董商店 店員");
                    }
                    break;

                case 2:
                    Say(pc, 11000003, 131, "是嗎?$R;" +
                                           "$R我知道了!$R;" +
                                           "沒關係的，請你不要在意。$R;", "古董商店 店員");
                    break;
            }
        }

        void 製作電路機械(ActorPC pc)
        {
            Say(pc, 11000003, 131, pc.Name + "!$R;" +
                                   "$R謝謝你來找我。$R;", "古董商店 店員");

            if (CountItem(pc, 10030003) > 0)
            {
                洋鐵的身軀製作完成(pc);
                return;
            }

            switch (Select(pc, "歡迎光臨!!", "", "買東西", "賣東西", "委託「組裝機械」", "詢問『活動木偶 電路機械』的製作方法", "委託打開『集裝箱』", "交換『合成失敗物』", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 6);

                    Say(pc, 11000003, 131, "下次再見吧!$R;", "古董商店 店員");
                    break;

                case 2:
                    OpenShopSell(pc, 6);

                    Say(pc, 11000003, 131, "下次再見吧!$R;", "古董商店 店員");
                    break;

                case 3:
                    Synthese(pc, 2039, 3);
                    break;

                case 4:
                    活動木偶電路機械的製作方法(pc);
                    break;

                case 5:
                    OpenTreasureBox(pc);
                    break;

                case 6:
                    交換合成失敗物(pc);
                    break;

                case 7:
                    break;
            }
        }

        void 活動木偶電路機械的製作方法(ActorPC pc)
        {
            int selection;

            selection = Select(pc, "想要問什麼呢?", "", "製作『發動機』的方法", "製作『電路機械模型』的方法", "『超微型電腦』的方法", "製作『洋鐵的身軀』的方法", "什麼也不問");

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000003, 131, "『發動機』可以說是機器人動力的來源。$R;" +
                                               "$R用『乾電池』、『磁石』$R;" +
                                               "以及『線圈』就可以製作了。$R;", "古董商店 店員");
                        break;

                    case 2:
                        Say(pc, 11000003, 131, "只要把之前給你的『洋鐵鋸齒輪』和$R;" +
                                               "『洋鐵部件』、『鐵板』三個組裝起來，$R;" +
                                               "就可以製作『電路機械模型』了。$R;", "古董商店 店員");
                        break;

                    case 3:
                        Say(pc, 11000003, 131, "『超微型電腦』是夢幻的力量啊。$R;" +
                                               "$R形態雖然跟『計算機』相似，$R;" +
                                               "但是功能完全不能相比的。$R;", "古董商店 店員");
                        break;

                    case 4:
                        Say(pc, 11000003, 131, "把『發動機』和『超微型電腦』合成的話，$R;" +
                                               "可以製作『洋鐵的身軀』。$R;" +
                                               "$P如果完成『洋鐵的身軀』的話，$R;" +
                                               "可以來找我一下嗎?$R;" +
                                               "$R因為有非常要緊的事要跟你說喔!$R;", "古董商店 店員");
                        break;
                }

                selection = Select(pc, "想要問什麼呢?", "", "製作『發動機』的方法", "製作『電路機械模型』的方法", "『超微型電腦』的方法", "製作『洋鐵的身軀』的方法", "什麼也不問");
            }
        }

        void 洋鐵的身軀製作完成(ActorPC pc)
        {
            BitMask<Puppet_01> Puppet_01_mask = new BitMask<Puppet_01>(pc.CMask["Puppet_01"]);                                                                          //任務：製作『活動木偶 電路機械』

            Puppet_01_mask.SetValue(Puppet_01.委託製作洋鐵的心, true);

            Say(pc, 11000003, 131, "完成『洋鐵的身軀』了?$R;" +
                                   "$R嗯…製作得非常好啊。$R;" +
                                   "$R但是這個只是一個外殼，$R;" +
                                   "必須還要給它一顆心。$R;" +
                                   "$P如果沒有心的話，$R;" +
                                   "它會陷入痛苦之中，$R;" +
                                   "最後漸漸變成魔物。$R;" +
                                   "$R聽說在城市的南邊，$R;" +
                                   "經常會出現這種魔物，沒見過嗎?$R;" +
                                   "$P製作『洋鐵的心』需要『心型紅寶石』。$R;" +
                                   "$R得到以後，拿到我這裡來吧!$R;", "古董商店 店員");
        }

        void 製作洋鐵的心(ActorPC pc)
        {
            BitMask<Puppet_01> Puppet_01_mask = new BitMask<Puppet_01>(pc.CMask["Puppet_01"]);                                                                          //任務：製作『活動木偶 電路機械』

            if (CountItem(pc, 10012100) > 0)
            {
                Puppet_01_mask.SetValue(Puppet_01.古董商店店員給予空空的心, true);

                Say(pc, 11000003, 131, "好美麗的『心型紅寶石』啊!$R;" +
                                       "$R肯定能產生非常美麗的心。$R;", "古董商店 店員");

                PlaySound(pc, 2210, false, 100, 50);
                Wait(pc, 2000);

                PlaySound(pc, 2210, false, 100, 50);
                Wait(pc, 2000);

                PlaySound(pc, 2210, false, 100, 50);
                Wait(pc, 2000);

                TakeItem(pc, 10012100, 1);
                GiveItem(pc, 10047201, 1);
                Say(pc, 0, 65535, "『心型紅寶石』變成了『空空的心』。$R;", " ");

                Say(pc, 11000003, 131, "來! 完成了!$R;" +
                                       "$R我只能幫到這裡了。$R;" +
                                       "$R你問我這樣就完成了?$R;" +
                                       "不是…還剩了一些，$R;" +
                                       "要在這裡注入愛心才可以算是完成。$R;" +
                                       "$P拿去聖堂試試看吧。$R;", "古董商店 店員");
            }
            else
            {
                Say(pc, 11000003, 131, "$P製作『洋鐵的心』需要『心型紅寶石』。$R;" +
                                       "$R得到以後，拿到我這裡來吧!$R;", "古董商店 店員");
            }
        }

        void 活動木偶電路機械製作材料收集完成(ActorPC pc)
        {
            BitMask<Puppet_01> Puppet_01_mask = new BitMask<Puppet_01>(pc.CMask["Puppet_01"]);                                                                          //任務：製作『活動木偶 電路機械』

            Puppet_01_mask.SetValue(Puppet_01.古董商店店員給予洋鐵鋸齒輪, false);
            Puppet_01_mask.SetValue(Puppet_01.委託製作洋鐵的心, false);
            Puppet_01_mask.SetValue(Puppet_01.古董商店店員給予空空的心, false);
            Puppet_01_mask.SetValue(Puppet_01.聖堂祭司給予洋鐵的心, false);

            Say(pc, 11000003, 131, "得到『洋鐵的心』了?$R;" +
                                   "$R把『洋鐵的心』和$R;" +
                                   "『洋鐵的身軀』組合以後，$R;" +
                                   "就可以完成『活動木偶 電路機械』了!$R;", "古董商店 店員");
        }

        void 山吹任務(ActorPC pc)
        {
            BitMask<Neko_04> Neko_04_mask = pc.CMask["Neko_04"];                                                                                                        //任務：凱堤(山吹)

            if (!Neko_04_mask.Test(Neko_04.向古董商店店員詢問有關結界的事情))
            {
                詢問有關結界的事情(pc);
                return;
            }
            else
            {
                委託製作水晶共振體(pc);
            }
        }

        void 詢問有關結界的事情(ActorPC pc)
        {
            BitMask<Neko_04> Neko_04_mask = pc.CMask["Neko_04"];                                                                                                        //任務：凱堤(山吹)

            Neko_04_mask.SetValue(Neko_04.向古董商店店員詢問有關結界的事情, true);

            Say(pc, 11000003, 131, "什麼? 有一道看不見的牆!!$R;" +
                                   "所以沒有辦法向前進?$R;" +
                                   "$R啊! 那應該是「結界」。$R;" +
                                   "$P中和「結界」的物品是可以自己製作的。$R;" +
                                   "$R原理是把『水晶』經過特別調整後，$R;" +
                                   "使的『水晶』跟「結界」的波長能產生共鳴。$R;" +
                                   "$P藉此就可以破壞節界，$R;" +
                                   "製造出能暫時通過的空間隙縫。$R;" +
                                   "$P需要『水晶』1個或『水晶破片』5個，$R;" +
                                   "拿過來的話我就可以幫你製作喔!$R;", "古董商店 店員");
        }

        void 委託製作水晶共振體(ActorPC pc)
        {
            BitMask<Neko_04> Neko_04_mask = pc.CMask["Neko_04"];                                                                                                        //任務：凱堤(山吹)

            if (CountItem(pc, 10014500) >= 5)
            {
                Say(pc, 11000003, 131, "想用『水晶破片』5個，$R;" +
                                       "製作『水晶共振體』嗎?$R;", "古董商店 店員");

                switch (Select(pc, "想要製作『水晶共振體』嗎?", "", "做", "放棄"))
                {
                    case 1:
                        if (CheckInventory(pc, 10014350, 1))
                        {
                            Neko_04_mask.SetValue(Neko_04.水晶共振體製作完成, true);

                            TakeItem(pc, 10014500, 5);
                            PlaySound(pc, 2040, false, 100, 50);
                            GiveItem(pc, 10014350, 1);

                            Say(pc, 0, 65535, "得到『水晶共振體』!$R;", " ");

                            Say(pc, 11000003, 131, "拿著這個的話，$R;" +
                                                   "應該就可以通過「結界」了。$R;" +
                                                   "$P但是要特別注意，$R;" +
                                                   "如果身上有人憑依的話，$R;" +
                                                   "會因為波長不同，$R;" +
                                                   "而導致失敗，$R;" +
                                                   "所以要特別小心啊!!$R;", "古董商店 店員");
                        }
                        else
                        {
                            Say(pc, 11000003, 131, "行李太多了? 把行李減少以後再來吧!$R;", "古董商店 店員");
                        }
                        return;

                    case 2:
                        break;
                }
            }

            else if (CountItem(pc, 10014300) >= 1)
            {
                Say(pc, 11000003, 131, "想用『水晶』1個，$R;" +
                                       "製作『水晶共振體』嗎?$R;", "古董商店 店員");

                switch (Select(pc, "想要製作『水晶共振體』嗎?", "", "做", "放棄"))
                {
                    case 1:
                        if (CheckInventory(pc, 10014350, 1))
                        {
                            Neko_04_mask.SetValue(Neko_04.水晶共振體製作完成, true);

                            TakeItem(pc, 10014300, 1);
                            PlaySound(pc, 2040, false, 100, 50);
                            GiveItem(pc, 10014350, 1);

                            Say(pc, 0, 65535, "得到『水晶共振體』!$R;", " ");

                            Say(pc, 11000003, 131, "拿著這個的話，$R;" +
                                                   "應該就可以通過「結界」了。$R;" +
                                                   "$P但是要特別注意，$R;" +
                                                   "如果身上有人憑依的話，$R;" +
                                                   "會因為波長不同，$R;" +
                                                   "而導致失敗，$R;" +
                                                   "所以要特別小心啊!!$R;", "古董商店 店員");
                        }
                        else
                        {
                            Say(pc, 11000003, 131, "行李太多了? 把行李減少以後再來吧!$R;", "古董商店 店員");
                        }
                        return;

                    case 2:
                        break;
                }
            }
        }
    }
}
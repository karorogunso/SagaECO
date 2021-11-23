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
                    Say(pc, 11000003, 131, "用『水晶的碎片』5个$R制作『水晶共振体』吗？$R;");
                    switch (Select(pc, "怎么做呢？", "", "做", "放弃"))
                    {
                        case 1:
                            if (CheckInventory(pc, 10014350, 1))
                            {
                                TakeItem(pc, 10014500, 5);
                                GiveItem(pc, 10014350, 1);
                                Neko_04_cmask.SetValue(Neko_04.製作共振體, true);
                                PlaySound(pc, 2040, false, 100, 50);
                                Say(pc, 0, 131, "得到了『水晶共振体』！$R;");
                                Say(pc, 11000003, 131, "拿着那个的话$R应该可以通过「关卡」$R;" +
                                    "$P如果有人在凭依$R会因为波长会震动而导致失败的$R所以小心啊！$R;");
                                return;
                            }
                            Say(pc, 11000003, 131, "行李太多了？把行李减少以后再来！$R;");
                            //GOTO EVT1100000338
                            return;
                        case 2:
                            break;
                    }
                }
                if (CountItem(pc, 10014300) >= 1)
                {
                    Say(pc, 11000003, 131, "用『水晶』1个$R制作『水晶共振体』吗？$R;");
                    switch (Select(pc, "怎么做呢？", "", "做", "放弃"))
                    {
                        case 1:
                            if (CheckInventory(pc, 10014350, 1))
                            {
                                TakeItem(pc, 10014300, 1);
                                GiveItem(pc, 10014350, 1);
                                Neko_04_cmask.SetValue(Neko_04.製作共振體, true);
                                PlaySound(pc, 2040, false, 100, 50);
                                Say(pc, 0, 131, "得到了『水晶共振体』！$R;");
                                Say(pc, 11000003, 131, "拿着那个的话$R应该可以通过「关卡」$R;" +
                                    "$P如果有人在凭依$R会因为波长会震动而导致失败的$R所以小心啊！$R;");
                                return;
                            }
                            Say(pc, 11000003, 131, "行李太多了？把行李减少以后再来！$R;");
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
                Say(pc, 11000003, 131, "什么？有看不见的墙！$R所以不能前进？$R;" +
                    "$R啊！那应该是「关卡」$R;" +
                    "$R你现在在玛衣玛衣遗跡的光之塔$R探索吗？$R;" +
                    "$P嗯！中和关卡的物件可以自己做$R;" +
                    "$R原理是把「水晶」特别调整后$R把跟关卡的重力波频率一样的波长$R送到关卡的发动机$R;" +
                    "$P那样做的话可以造成$R能够通过的空间缝$R;" +
                    "$R古鲁杜先生也经常委托的$R;" +
                    "$P需要1个『水晶』或5个『水晶破片』$R拿过来的话就帮你做$R;");
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

            switch (Select(pc, "欢迎光临!!", "", "买东西", "卖东西", "委托「组装机械」", "委托打开『集装箱』", "交换『合成失败物』", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 6);

                    Say(pc, 11000003, 131, "下次再见吧!$R;", "古董商店 店员");
                    break;

                case 2:
                    OpenShopSell(pc, 6);

                    Say(pc, 11000003, 131, "下次再见吧!$R;", "古董商店 店员");
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
                    Say(pc, 11000003, 131, "下次再见吧!$R;", "古董商店 店員");
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
                    Say(pc, 11000003, 131, "啊! 吓我一跳!!$R;", "古董商店 店员");
                }
            }
        }

        void 交換合成失敗物(ActorPC pc)
        {
            string quantity;
            ushort number01, number02;

            switch (Select(pc, "想要交换什么?", "", "杰利科 (需要1个『合成失败物』)", "面包树果实 (需要10个『合成失败物』)", "蓝色杰利科 (需要100个『合成失败物』)", "不想交换"))
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
                            if (CheckInventory(pc, 10006000, number01))
                            {
                                TakeItem(pc, 10001250, number02);
                                Say(pc, 0, 65535, "交换『合成失败物』" + quantity + "个。$R;", " ");

                                GiveItem(pc, 10006000, number01);
                                Say(pc, 0, 65535, "得到『面包树果实』" + quantity + "个。$R;", " ");
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

                        if (CountItem(pc, 10001250) >= number02)
                        {
                            if (CheckInventory(pc, 10032803, number01))
                            {
                                TakeItem(pc, 10001250, number02);
                                Say(pc, 0, 65535, "交换『合成失败物』" + quantity + "个。$R;", " ");

                                GiveItem(pc, 10032803, number01);
                                Say(pc, 0, 65535, "得到『蓝色杰利科』" + quantity + "个。$R;", " ");
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
                Say(pc, 11000003, 131, "拿去圣堂试试看吧。$R;", "古董商店 店员");
            }

            if (Puppet_01_mask.Test(Puppet_01.聖堂祭司給予洋鐵的心))
            {
                活動木偶電路機械製作材料收集完成(pc);
            }
        }

        void 詢問要不要製作活動木偶電路機械(ActorPC pc)
        {
            Say(pc, 11000003, 131, "你喜欢机器人吗?$R;", "古董商店 店员");

            switch (Select(pc, "你喜欢机器人吗?", "", "喜欢", "不喜欢"))
            {
                case 1:
                    古董商店店員給予洋鐵鋸齒輪(pc);
                    break;

                case 2:
                    Say(pc, 11000003, 131, "是吗?$R;" +
                                           "$R我知道了!$R;" +
                                           "没关系的，请你不要在意。$R;", "古董商店 店员");
                    break;
            }
        }

        void 古董商店店員給予洋鐵鋸齒輪(ActorPC pc)
        {
            BitMask<Puppet_01> Puppet_01_mask = new BitMask<Puppet_01>(pc.CMask["Puppet_01"]);                                                                          //任務：製作『活動木偶 電路機械』

            Say(pc, 11000003, 131, "喜欢?!$R;" +
                                   "$R那样的话，$R;" +
                                   "想不想亲自制作$R;" +
                                   "『活动木偶电路机械』看看?$R;", "古董商店 店员");

            switch (Select(pc, "想要制作看看吗?", "", "制作", "不制作"))
            {
                case 1:
                    Say(pc, 11000003, 131, "那我告诉你制作方法吧!$R;" +
                                           "$P先收下这个，$R;" +
                                           "这是制作所需的材料之一。$R;", "古董商店 店员");

                    if (CheckInventory(pc, 10023400, 1))
                    {
                        Puppet_01_mask.SetValue(Puppet_01.古董商店店員給予洋鐵鋸齒輪, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        GiveItem(pc, 10023400, 1);
                        Say(pc, 0, 65535, "得到『洋铁齿轮』$R;", " ");

                        活動木偶電路機械的製作方法(pc);
                    }
                    else
                    {
                        Say(pc, 11000003, 131, "…$R;" +
                                               "$P行李太多了，没办法给你啊!$R;", "古董商店 店员");
                    }
                    break;

                case 2:
                    Say(pc, 11000003, 131, "是吗?$R;" +
                                           "$R我知道了!$R;" +
                                           "没关系的，请你不要在意。$R;", "古董商店 店员");
                    break;
            }
        }

        void 製作電路機械(ActorPC pc)
        {
            Say(pc, 11000003, 131, pc.Name + "!$R;" +
                                   "$R谢谢你来找我。$R;", "古董商店 店员");

            if (CountItem(pc, 10030003) > 0)
            {
                洋鐵的身軀製作完成(pc);
                return;
            }

            switch (Select(pc, "欢迎光临!!", "", "买东西", "卖东西", "委托「组装机械」", "询问『活动木偶 电路机械』的制作方法", "委托打开『集装箱』", "交换『合成失败物』", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 6);

                    Say(pc, 11000003, 131, "下次再见吧!$R;", "古董商店 店员");
                    break;

                case 2:
                    OpenShopSell(pc, 6);

                    Say(pc, 11000003, 131, "下次再见吧!$R;", "古董商店 店员");
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

            selection = Select(pc, "想要问什么呢?", "", "制作『发动机』的方法", "制作『电路机械的模型』的方法", "『掌上电脑』是什么","制作『洋铁的身体』的方法", "什么也不问");

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000003, 131, "『发动机』可以说是机器人动力的来源。$R;" +
                                               "$R用『干电池』、『磁石』$R;" +
                                               "以及『线圈』就可以制作了。$R;", "古董商店 店员");
                        break;

                    case 2:
                        Say(pc, 11000003, 131, "只要把之前给你的『洋铁齿轮』和$R;" +
                                               "『洋铁部件』、『铁板』三个组装起来，$R;" +
                                               "就可以制作『电路机械的模型』了。$R;", "古董商店 店员");
                        break;

                    case 3:
                        Say(pc, 11000003, 131, "『掌上电脑』是梦幻的力量啊。$R;" +
                                               "$R形态虽然跟『计算机』相似，$R;" +
                                               "但是功能完全不能相比的。$R;", "古董商店 店员");
                        break;

                    case 4:
                        Say(pc, 11000003, 131, "把『发动机』和『掌上电脑』合成的话，$R;" +
                                               "可以制作『洋铁的身体』。$R;" +
                                               "$P如果完成『洋铁的身躯』的话，$R;" +
                                               "可以来找我一下吗?$R;" +
                                               "$R因为有非常要紧的事要跟你说喔!$R;", "古董商店 店员");
                        break;
                }

                selection = Select(pc, "想要问什么呢?", "", "制作『发动机』的方法", "制作『电路机械的模型』的方法", "『掌上电脑』是什么", "制作『洋铁的身体』的方法", "什么也不问");
            }
        }

        void 洋鐵的身軀製作完成(ActorPC pc)
        {
            BitMask<Puppet_01> Puppet_01_mask = new BitMask<Puppet_01>(pc.CMask["Puppet_01"]);                                                                          //任務：製作『活動木偶 電路機械』

            Puppet_01_mask.SetValue(Puppet_01.委託製作洋鐵的心, true);

            Say(pc, 11000003, 131, "完成『洋铁的身体』了?$R;" +
                                   "$R嗯…制作得非常好啊。$R;" +
                                   "$R但是这个只是一个外壳，$R;" +
                                   "必须还要给它一颗心。$R;" +
                                   "$P如果没有心的话，$R;" +
                                   "它会陷入痛苦之中，$R;" +
                                   "最后渐渐变成魔物。$R;" +
                                   "$R听说在城市的南边，$R;" +
                                   "经常会出现这种魔物，没见过吗?$R;" +
                                   "$P制作『洋铁的心』需要『心型红宝石』。$R;" +
                                   "$R得到以后，拿到我这里来吧!$R;", "古董商店 店员");
        }

        void 製作洋鐵的心(ActorPC pc)
        {
            BitMask<Puppet_01> Puppet_01_mask = new BitMask<Puppet_01>(pc.CMask["Puppet_01"]);                                                                          //任務：製作『活動木偶 電路機械』

            if (CountItem(pc, 10012100) > 0)
            {
                Puppet_01_mask.SetValue(Puppet_01.古董商店店員給予空空的心, true);

                Say(pc, 11000003, 131, "好美丽的『心型红宝石』啊!$R;" +
                                       "$R肯定能产生非常美丽的心。$R;", "古董商店 店员");

                PlaySound(pc, 2210, false, 100, 50);
                Wait(pc, 2000);

                PlaySound(pc, 2210, false, 100, 50);
                Wait(pc, 2000);

                PlaySound(pc, 2210, false, 100, 50);
                Wait(pc, 2000);

                TakeItem(pc, 10012100, 1);
                GiveItem(pc, 10047201, 1);
                Say(pc, 0, 65535, "『心型红宝石』变成了『空空的心』。$R;", " ");

                Say(pc, 11000003, 131, "来! 完成了!$R;" +
                                       "$R我只能帮到这里了。$R;" +
                                       "$R你问我这样就完成了?$R;" +
                                       "不是…还剩了一些，$R;" +
                                       "要在这里注入爱心才可以算是完成。$R;" +
                                       "$P拿去圣堂试试看吧。$R;", "古董商店 店员");
            }
            else
            {
                Say(pc, 11000003, 131, "$P制作『洋铁的心』需要『心型红宝石』。$R;" +
                                       "$R得到以后，拿到我这里来吧!$R;", "古董商店 店员");
            }
        }

        void 活動木偶電路機械製作材料收集完成(ActorPC pc)
        {
            BitMask<Puppet_01> Puppet_01_mask = new BitMask<Puppet_01>(pc.CMask["Puppet_01"]);                                                                          //任務：製作『活動木偶 電路機械』

            Puppet_01_mask.SetValue(Puppet_01.古董商店店員給予洋鐵鋸齒輪, false);
            Puppet_01_mask.SetValue(Puppet_01.委託製作洋鐵的心, false);
            Puppet_01_mask.SetValue(Puppet_01.古董商店店員給予空空的心, false);
            Puppet_01_mask.SetValue(Puppet_01.聖堂祭司給予洋鐵的心, false);

            Say(pc, 11000003, 131, "得到『洋铁的心』了?$R;" +
                                   "$R把『洋铁的心』和$R;" +
                                   "『洋铁的身体』组合以后，$R;" +
                                   "就可以完成『活动木偶 电路机械』了!$R;", "古董商店 店员");
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

            Say(pc, 11000003, 131, "什么? 有一道看不见的墙!!$R;" +
                                   "所以没有办法向前进?$R;" +
                                   "$R啊! 那应该是「结界」。$R;" +
                                   "$P中和「结界」的物品是可以自己制作的。$R;" +
                                   "$R原理是把『水晶』经过特别调整后，$R;" +
                                   "使的『水晶』跟「结界」的波长能产生共鸣。$R;" +
                                   "$P藉此就可以破坏结界，$R;" +
                                   "制造出能暂时通过的空间隙缝。$R;" +
                                   "$P需要『水晶』1个或『水晶破片』5个，$R;" +
                                   "拿过来的话我就可以帮你制作喔!$R;", "古董商店 店员");
        }

        void 委託製作水晶共振體(ActorPC pc)
        {
            BitMask<Neko_04> Neko_04_mask = pc.CMask["Neko_04"];                                                                                                        //任務：凱堤(山吹)

            if (CountItem(pc, 10014500) >= 5)
            {
                Say(pc, 11000003, 131, "想用『水晶破片』5个，$R;" +
                                       "制作『水晶共振体』吗?$R;", "古董商店 店员");

                switch (Select(pc, "想要制作『水晶共振体』吗?", "", "做", "放弃"))
                {
                    case 1:
                        if (CheckInventory(pc, 10014350, 1))
                        {
                            Neko_04_mask.SetValue(Neko_04.水晶共振體製作完成, true);

                            TakeItem(pc, 10014500, 5);
                            PlaySound(pc, 2040, false, 100, 50);
                            GiveItem(pc, 10014350, 1);

                            Say(pc, 0, 65535, "得到『水晶共振体』!$R;", " ");

                            Say(pc, 11000003, 131, "拿着这个的话，$R;" +
                                                   "应该就可以通过「结界」了。$R;" +
                                                   "$P但是要特别注意，$R;" +
                                                   "如果身上有人凭依的话，$R;" +
                                                   "会因为波长不同，$R;" +
                                                   "而导致失败，$R;" +
                                                   "所以要特别小心啊!!$R;", "古董商店 店员");
                        }
                        else
                        {
                            Say(pc, 11000003, 131, "行李太多了? 把行李减少以后再来吧!$R;", "古董商店 店员");
                        }
                        return;

                    case 2:
                        break;
                }
            }

            else if (CountItem(pc, 10014300) >= 1)
            {
                Say(pc, 11000003, 131, "想用『水晶』1个，$R;" +
                                       "制作『水晶共振体』吗?$R;", "古董商店 店员");

                switch (Select(pc, "想要制作『水晶共振体』吗?", "", "做", "放弃"))
                {
                    case 1:
                        if (CheckInventory(pc, 10014350, 1))
                        {
                            Neko_04_mask.SetValue(Neko_04.水晶共振體製作完成, true);

                            TakeItem(pc, 10014300, 1);
                            PlaySound(pc, 2040, false, 100, 50);
                            GiveItem(pc, 10014350, 1);

                            Say(pc, 0, 65535, "得到『水晶共振体』!$R;", " ");

                            Say(pc, 11000003, 131, "拿着这个的话，$R;" +
                                                   "应该就可以通过「结界」了。$R;" +
                                                   "$P但是要特别注意，$R;" +
                                                   "如果身上有人凭依的话，$R;" +
                                                   "会因为波长不同，$R;" +
                                                   "而导致失败，$R;" +
                                                   "所以要特别小心啊!!$R;", "古董商店 店员");
                        }
                        else
                        {
                            Say(pc, 11000003, 131, "行李太多了? 把行李减少以后再来吧!$R;", "古董商店 店员");
                        }
                        return;

                    case 2:
                        break;
                }
            }
        }
    }
}
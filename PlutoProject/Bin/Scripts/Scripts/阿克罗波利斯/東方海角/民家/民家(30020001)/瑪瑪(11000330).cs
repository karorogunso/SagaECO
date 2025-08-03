using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M30020001
{
    public class S11000330 : Event
    {
        public S11000330()
        {
            this.EventID = 11000330;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<DFHJFlags> mask = new BitMask<DFHJFlags>(pc.CMask["DFHJ"]);
            int count = 0;
            if (pc.Account.GMLevel >= 100)
            {
                if (Select(pc, "要怎么做呢？", "", "进入管理模式", "算了") == 1)
                {
                    管理用(pc);
                    return;
                }
            }
            if (SInt["Mamasan_Carapace"] < 100000)
            {
                if (!mask.Test(DFHJFlags.甲壳收集开始))
                {
                    mask.SetValue(DFHJFlags.甲壳收集开始, true);
                    Say(pc, 131, "我的儿子竟然$R;" +
                        "偷养了小狗呢。$R;");
                    Say(pc, 131, "妈妈对不起。$R;");
                    Say(pc, 131, "本来想扔掉的。$R;" +
                        "后来有了感情……$R;" +
                        "$R汪汪的叫著，不是很可爱吗?$R;" +
                        "$P现在，没有小狗的话，感觉不自在了$R;" +
                        "所以干脆想开个宠物商店呢。$R;" +
                        "$P汪汪很喜欢『甲壳』$R;" +
                        "甲壳多的话就可以开店了。$R;" +
                        "$R一个人搜集起来很困难。$R;" +
                        "$P如果见到的话$R;" +
                        "别扔掉，给我就好了$R;" +
                        "$P目标是10万个。$R;" +
                        "找到10万个就开店了。$R;");
                    return;
                }
                switch (Select(pc, "欢迎！", "", "搜集甲壳", "现在搜集了多少甲壳？", "什么也不做。"))
                {
                    case 1:
                        foreach (SagaDB.Item.Item i in NPCTrade(pc))
                        {
                            if (i.ItemID == 10007500)
                                count += i.Stack;
                        }
                        if (count > 0)
                        {
                            Say(pc, 131, string.Format("交给了{0}个甲壳", count));
                            SInt["Mamasan_Carapace"] += count;
                        }
                        SaveServerSvar();
                        break;
                    case 2:
                        查看收集数(pc);
                        break;
                    case 3:
                        break;
                }
                return;
            }
            if (SInt["Mamasan_Carapace"] < 200000)
            {
                if (!mask.Test(DFHJFlags.甲壳收集十万))
                {
                    mask.SetValue(DFHJFlags.甲壳收集十万, true);
                    Say(pc, 131, "有很多可爱小狗的宠物店$R;" +
                        "快要开幕了$R;" +
                        "$P只要集齐10万个『甲壳』，$R还会贩卖宠物食粮呢$R;");
                    OpenShopBuy(pc, 87);
                    Say(pc, 131, "谢谢$R;");
                    return;
                }
                switch (Select(pc, "欢迎！", "", "看东西", "搜集甲壳", "现在搜集了多少甲壳？", "什么也不做。"))
                {
                    case 1:
                        OpenShopBuy(pc, 87);
                        Say(pc, 131, "谢谢$R;");
                        break;
                    case 2:
                        foreach (SagaDB.Item.Item i in NPCTrade(pc))
                        {
                            if (i.ItemID == 10007500)
                                count += i.Stack;
                        }
                        if (count > 0)
                        {
                            Say(pc, 131, string.Format("交给了{0}个甲壳", count));
                            SInt["Mamasan_Carapace"] += count;
                            SaveServerSvar();
                        }
                        break;
                    case 3:
                        查看收集数(pc);
                        break;
                    case 4:
                        break;
                }
                return;
            }
            if (SInt["Mamasan_Carapace"] < 500000)
            {
                if (!mask.Test(DFHJFlags.甲壳收集二十万))
                {
                    mask.SetValue(DFHJFlags.甲壳收集二十万, true);
                    Say(pc, 131, "已经有20万个甲壳了！$R;" +
                        "$R要出售美味可口的『宠物食粮』啊！$R;" +
                        "$R还有植物也算是宠物呢$R听说，客人会根据需要，$R购买不同的『植物营养剂』。$R;" +
                        "集齐30万个『甲壳』的话$R就卖新一批小狗吧$R;");
                    OpenShopBuy(pc, 100);
                    Say(pc, 131, "再来啊！$R;");
                    return;
                }
                switch (Select(pc, "欢迎！", "", "看东西", "搜集甲壳", "现在搜集了多少甲壳？", "什么也不做。"))
                {
                    case 1:
                        OpenShopBuy(pc, 100);
                        Say(pc, 131, "再来啊！$R;");
                        break;
                    case 2:
                        foreach (SagaDB.Item.Item i in NPCTrade(pc))
                        {
                            if (i.ItemID == 10007500)
                                count += i.Stack;
                        }
                        if (count > 0)
                        {
                            Say(pc, 131, string.Format("交给了{0}个甲壳", count));
                            SInt["Mamasan_Carapace"] += count;
                            SaveServerSvar();
                        }
                        break;
                    case 3:
                        查看收集数(pc);
                        break;
                    case 4:
                        break;
                }
                return;
            }
            if (SInt["Mamasan_Carapace"] < 550000)
            {
                if (!mask.Test(DFHJFlags.甲壳收集五十万))
                {
                    mask.SetValue(DFHJFlags.甲壳收集五十万, true);
                    Say(pc, 131, "已经有50万个甲壳了！$R;" +
                        "$R新的小狗也到了$R;");
                    OpenShopBuy(pc, 101);
                    Say(pc, 131, "再来啊$R;");
                    return;
                }
                switch (Select(pc, "欢迎！", "", "看东西", "搜集甲壳", "现在搜集了多少甲壳？", "什么也不做。"))
                {
                    case 1:
                        OpenShopBuy(pc, 101);
                        Say(pc, 131, "再来啊$R;");
                        break;
                    case 2:
                        foreach (SagaDB.Item.Item i in NPCTrade(pc))
                        {
                            if (i.ItemID == 10007500)
                                count += i.Stack;
                        }
                        if (count > 0)
                        {
                            Say(pc, 131, string.Format("交给了{0}个甲壳", count));
                            SInt["Mamasan_Carapace"] += count;
                            SaveServerSvar();
                        }
                        break;
                    case 3:
                        查看收集数(pc);
                        break;
                    case 4:
                        break;
                }
                return;
            }
            if (SInt["Mamasan_Carapace"] < 600000)
            {
                if (!mask.Test(DFHJFlags.甲壳收集五十五万))
                {
                    mask.SetValue(DFHJFlags.甲壳收集五十五万, true);
                    Say(pc, 131, "已经有55万个甲壳了！$R;" +
                        "$R新商品也到货了$R;");
                    OpenShopBuy(pc, 160);
                    Say(pc, 131, "再来啊！$R;");
                    return;
                }
                switch (Select(pc, "欢迎！", "", "看东西", "搜集甲壳", "现在搜集了多少甲壳？", "什么也不做。"))
                {
                    case 1:
                        OpenShopBuy(pc, 160);
                        Say(pc, 131, "再来啊！$R;");
                        break;
                    case 2:
                        foreach (SagaDB.Item.Item i in NPCTrade(pc))
                        {
                            if (i.ItemID == 10007500)
                                count += i.Stack;
                        }
                        if (count > 0)
                        {
                            Say(pc, 131, string.Format("交给了{0}个甲壳", count));
                            SInt["Mamasan_Carapace"] += count;
                            SaveServerSvar();
                        }
                        break;
                    case 3:
                        查看收集数(pc);
                        break;
                    case 4:
                        break;
                }
                return;
            }
            if (SInt["Mamasan_Carapace"] < 650000)
            {
                if (!mask.Test(DFHJFlags.甲壳收集六十万))
                {
                    mask.SetValue(DFHJFlags.甲壳收集六十万, true);
                    Say(pc, 131, "已经有60万个甲壳了！$R;" +
                        "$R新商品也到货了$R;");
                    OpenShopBuy(pc, 161);
                    Say(pc, 131, "再来啊$R;");
                    return;
                }
                switch (Select(pc, "欢迎！", "", "看东西", "搜集甲壳", "现在搜集了多少甲壳？", "什么也不做。"))
                {
                    case 1:
                        OpenShopBuy(pc, 161);
                        Say(pc, 131, "再来啊$R;");
                        break;
                    case 2:
                        foreach (SagaDB.Item.Item i in NPCTrade(pc))
                        {
                            if (i.ItemID == 10007500)
                                count += i.Stack;
                        }
                        if (count > 0)
                        {
                            Say(pc, 131, string.Format("交给了{0}个甲壳", count));
                            SInt["Mamasan_Carapace"] += count;
                        }
                        break;
                    case 3:
                        查看收集数(pc);
                        break;
                    case 4:
                        break;
                }
                return;
            }
            if (SInt["Mamasan_Carapace"] < 700000)
            {
                if (!mask.Test(DFHJFlags.甲壳收集六十五万))
                {
                    mask.SetValue(DFHJFlags.甲壳收集六十五万, true);
                    Say(pc, 131, "已经有65万个甲壳了！$R;" +
                        "$R新商品也到货了$R;");
                    OpenShopBuy(pc, 162);
                    Say(pc, 131, "再来啊$R;");
                    return;
                }
                switch (Select(pc, "欢迎！", "", "看东西", "搜集甲壳", "现在搜集了多少甲壳？", "什么也不做。"))
                {
                    case 1:
                        OpenShopBuy(pc, 162);
                        Say(pc, 131, "再来啊$R;");
                        break;
                    case 2:
                        foreach (SagaDB.Item.Item i in NPCTrade(pc))
                        {
                            if (i.ItemID == 10007500)
                                count += i.Stack;
                        }
                        if (count > 0)
                        {
                            Say(pc, 131, string.Format("交给了{0}个甲壳", count));
                            SInt["Mamasan_Carapace"] += count;
                        }
                        break;
                    case 3:
                        查看收集数(pc);
                        break;
                    case 4:
                        break;
                }
                return;
            }
            if (SInt["Mamasan_Carapace"] < 750000)
            {
                if (!mask.Test(DFHJFlags.甲壳收集七十万))
                {
                    mask.SetValue(DFHJFlags.甲壳收集七十万, true);
                    Say(pc, 131, "已经有70万个甲壳了！$R;" +
                        "$R新商品也到货了$R;");
                    OpenShopBuy(pc, 163);
                    Say(pc, 131, "再来啊！$R;");
                    return;
                }
                switch (Select(pc, "欢迎！", "", "看东西", "搜集甲壳", "现在搜集了多少甲壳？", "什么也不做。"))
                {
                    case 1:
                        OpenShopBuy(pc, 163);
                        Say(pc, 131, "再来啊！$R;");
                        break;
                    case 2:
                        foreach (SagaDB.Item.Item i in NPCTrade(pc))
                        {
                            if (i.ItemID == 10007500)
                                count += i.Stack;
                        }
                        if (count > 0)
                        {
                            Say(pc, 131, string.Format("交给了{0}个甲壳", count));
                            SInt["Mamasan_Carapace"] += count;
                        }
                        break;
                    case 3:
                        查看收集数(pc);
                        break;
                    case 4:
                        break;
                }
                return;
            }
            if (SInt["Mamasan_Carapace"] < 800000
            )
            {
                if (!mask.Test(DFHJFlags.甲壳收集七十五万))
                {
                    mask.SetValue(DFHJFlags.甲壳收集七十五万, true);
                    Say(pc, 131, "已经有75万个甲壳了！$R;" +
                        "$R新商品也到货了$R;");
                    OpenShopBuy(pc, 164);
                    Say(pc, 131, "再来啊$R;");
                    return;
                }
                switch (Select(pc, "欢迎！", "", "看东西", "搜集甲壳", "现在搜集了多少甲壳？", "什么也不做。"))
                {
                    case 1:
                        OpenShopBuy(pc, 164);
                        Say(pc, 131, "再来啊$R;");
                        break;
                    case 2:
                        foreach (SagaDB.Item.Item i in NPCTrade(pc))
                        {
                            if (i.ItemID == 10007500)
                                count += i.Stack;
                        }
                        if (count > 0)
                        {
                            Say(pc, 131, string.Format("交给了{0}个甲壳", count));
                            SInt["Mamasan_Carapace"] += count;
                        }
                        break;
                    case 3:
                        查看收集数(pc);
                        break;
                    case 4:
                        break;
                }
                return;
            }
            if (SInt["Mamasan_Carapace"] < 900000)
            {
                if (!mask.Test(DFHJFlags.甲壳收集八十万))
                {
                    mask.SetValue(DFHJFlags.甲壳收集八十万, true);
                    Say(pc, 131, "已经有80万个甲壳了！$R;" +
                        "$R新商品也到货了$R;");
                    OpenShopBuy(pc, 165);
                    Say(pc, 131, "再来啊！$R;");
                    return;
                }
                switch (Select(pc, "欢迎！", "", "看东西", "搜集甲壳", "现在搜集了多少甲壳？", "什么也不做。"))
                {
                    case 1:
                        OpenShopBuy(pc, 165);
                        Say(pc, 131, "再来啊！$R;");
                        break;
                    case 2:
                        foreach (SagaDB.Item.Item i in NPCTrade(pc))
                        {
                            if (i.ItemID == 10007500)
                                count += i.Stack;
                        }
                        if (count > 0)
                        {
                            Say(pc, 131, string.Format("交给了{0}个甲壳", count));
                            SInt["Mamasan_Carapace"] += count;
                        }
                        break;
                    case 3:
                        查看收集数(pc);
                        break;
                    case 4:
                        break;
                }
                return;
            }
            if (SInt["Mamasan_Carapace"] < 1000000)
            {
                if (!mask.Test(DFHJFlags.甲壳收集九十万))
                {
                    mask.SetValue(DFHJFlags.甲壳收集九十万, true);
                    Say(pc, 131, "已经有90万个甲壳了！$R;" +
                        "$R新商品也到货了$R;");
                    OpenShopBuy(pc, 194);
                    Say(pc, 131, "再来啊$R;");
                    return;
                }
                switch (Select(pc, "欢迎！", "", "看东西", "搜集甲壳", "现在搜集了多少甲壳？", "什么也不做。"))
                {
                    case 1:
                        OpenShopBuy(pc, 194);
                        Say(pc, 131, "再来啊$R;");
                        break;
                    case 2:
                        foreach (SagaDB.Item.Item i in NPCTrade(pc))
                        {
                            if (i.ItemID == 10007500)
                                count += i.Stack;
                        }
                        if (count > 0)
                        {
                            Say(pc, 131, string.Format("交给了{0}个甲壳", count));
                            SInt["Mamasan_Carapace"] += count;
                        }
                        break;
                    case 3:
                        查看收集数(pc);
                        break;
                    case 4:
                        break;
                }
                return;
            }

            if (!mask.Test(DFHJFlags.甲壳收集一百万))
            {
                mask.SetValue(DFHJFlags.甲壳收集一百万, true);
                Say(pc, 131, "真是感谢大家啊，$R;" +
                    "终於有100万个甲壳了！$R;" +
                    "$R新商品也到货了$R;");
                OpenShopBuy(pc, 195);
                Say(pc, 131, "再来啊！$R;");
                return;
            }
            switch (Select(pc, "欢迎！", "", "看东西(1号店)", "看东西(2号店)", "搜集甲壳", "现在搜集了多少甲壳？", "什么也不做。"))
            {
                case 1:
                    OpenShopBuy(pc, 194);
                    Say(pc, 131, "再来啊$R;");
                    break;
                case 2:
                    OpenShopBuy(pc, 195);
                    Say(pc, 131, "再来啊！$R;");
                    break;
                case 3:
                    foreach (SagaDB.Item.Item i in NPCTrade(pc))
                    {
                        if (i.ItemID == 10007500)
                            count += i.Stack;
                    }
                    if (count > 0)
                    {
                        Say(pc, 131, string.Format("交给了{0}个甲壳", count));
                        SInt["Mamasan_Carapace"] += count;
                    }
                    break;
                case 4:
                    查看收集数(pc);
                    break;
                case 5:
                    break;
            }
        }
        void 查看收集数(ActorPC pc)
        {
            BitMask<DFHJFlags> mask = new BitMask<DFHJFlags>(pc.CMask["DFHJ"]);
            Say(pc, 131, "现在……$R;" +
                "" + SInt["Mamasan_Carapace"] + "个甲壳！$R;");
            if (mask.Test(DFHJFlags.甲壳收集一百万))
            {
                Say(pc, 131, "真的谢谢您。$R幸好有您，$R我们家有很多『甲壳』了$R;" +
                    "$R下次还有事的话$R还要拜託您呢$R;");
                return;
            }
            if (mask.Test(DFHJFlags.甲壳收集九十万))
            {
                Say(pc, 131, "集齐100万个『甲壳』的话$R;" +
                    "就卖宠物喜欢的道具吧$R;");
                return;
            }
            if (mask.Test(DFHJFlags.甲壳收集八十万))
            {
                Say(pc, 131, "集齐90万个『甲壳』的话$R;" +
                    "就卖宠物喜欢的道具吧$R;");
                return;
            }
            if (mask.Test(DFHJFlags.甲壳收集七十五万))
            {

                Say(pc, 131, "集齐80万个『甲壳』的话$R;" +
                    "就卖宠物喜欢的道具吧$R;");

                return;
            }
            if (mask.Test(DFHJFlags.甲壳收集七十万))
            {
                Say(pc, 131, "集齐75万个『甲壳』的话$R;" +
                    "就卖宠物喜欢的道具吧$R;");
                return;
            }
            if (mask.Test(DFHJFlags.甲壳收集六十五万))
            {
                Say(pc, 131, "集齐70万个『甲壳』的话$R;" +
                    "就卖宠物喜欢的道具吧$R;");
                return;
            }
            if (mask.Test(DFHJFlags.甲壳收集六十万))
            {
                Say(pc, 131, "集齐65万个『甲壳』的话$R;" +
                    "就卖宠物喜欢的道具吧$R;");
                return;
            }
            if (mask.Test(DFHJFlags.甲壳收集五十五万))
            {
                Say(pc, 131, "集齐60万个『甲壳』的话$R;" +
                    "就卖宠物喜欢的道具吧$R;");
                return;
            }
            if (mask.Test(DFHJFlags.甲壳收集五十万))
            {
                Say(pc, 131, "集齐55万个『甲壳』的话$R;" +
                    "就卖宠物喜欢的道具吧$R;");
                return;
            }
            if (mask.Test(DFHJFlags.甲壳收集二十万))
            {
                Say(pc, 131, "集齐50万个『甲壳』的话$R;" +
                    "就卖新一批小狗吧$R;");
                return;
            }
            if (mask.Test(DFHJFlags.甲壳收集十万))
            {
                Say(pc, 131, "集齐50万个『甲壳』的话$R;" +
                    "就卖宠物食粮吧$R;" +
                    "$P还有，新一批小狗$R;" +
                    "好像快要出生了！$R;" +
                    "$R要是开始贩卖宠物食粮$R;" +
                    "希望也可以增加小狗的品种阿$R;");
                return;
            }
            Say(pc, 131, "找到10万个『甲壳』$R;" +
                "就可以开店了！$R;");
        }
        void 管理用(ActorPC pc)
        {

            switch (Select(pc, "怎么办呢？", "", "增加", "减少", "什么也不做。"))
            {
                case 1:
                    SInt["Mamasan_Carapace"] += int.Parse(InputBox(pc, "输入要增加的数量", InputType.ItemCode));
                    break;
                case 2:
                    int count = int.Parse(InputBox(pc, "输入要减少的数量", InputType.ItemCode));
                    if (SInt["Mamasan_Carapace"] > count)
                    {
                        SInt["Mamasan_Carapace"] -= count;
                    }
                    else
                    {
                        SInt["Mamasan_Carapace"] = 0;
                    }
                    break;
                case 3:
                    break;
            }
        }
    }
}
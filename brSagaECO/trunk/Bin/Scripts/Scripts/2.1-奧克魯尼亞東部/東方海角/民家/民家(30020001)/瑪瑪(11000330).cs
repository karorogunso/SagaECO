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
                if (Select(pc, "要怎麼做呢？", "", "進入管理模式", "算了") == 1)
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
                    Say(pc, 131, "我的兒子竟然帶來了$R;" +
                        "偷養的小狗呢。$R;");
                    Say(pc, 131, "媽媽對不起。$R;");
                    Say(pc, 131, "本來想扔掉的。$R;" +
                        "後來有了感情……$R;" +
                        "$R汪汪的叫著，不是很可愛嗎?$R;" +
                        "$P現在，沒有小狗的話，不想活了$R;" +
                        "所以乾脆想開個寵物商店呢。$R;" +
                        "$P汪汪很喜歡『甲殼』$R;" +
                        "甲殼多的話就可以開店了。$R;" +
                        "$R一個人搜集起來很困難。$R;" +
                        "$P如果見到的話$R;" +
                        "別扔掉，給我就好了$R;" +
                        "$P目標是10萬個。$R;" +
                        "找到10萬個就開店了。$R;");
                    return;
                }
                switch (Select(pc, "歡迎！", "", "搜集甲殼", "現在搜集了多少甲殼？", "什麼也不做。"))
                {
                    case 1:
                        foreach (SagaDB.Item.Item i in NPCTrade(pc))
                        {
                            if (i.ItemID == 10007500)
                                count += i.Stack;
                        }
                        if (count > 0)
                        {
                            Say(pc, 131, string.Format("交給了{0}個甲殼", count));
                            SInt["Mamasan_Carapace"] += count;
                        }
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
                    Say(pc, 131, "有很多可愛小狗的寵物店$R;" +
                        "快要開幕了$R;" +
                        "$P只要集齊10萬個『甲殼』，$R還會販賣寵物食糧呢$R;");
                    OpenShopBuy(pc, 87);
                    Say(pc, 131, "謝謝$R;");
                    return;
                }
                switch (Select(pc, "歡迎！", "", "看東西", "搜集甲殼", "現在搜集了多少甲殼？", "什麼也不做。"))
                {
                    case 1:
                        OpenShopBuy(pc, 87);
                        Say(pc, 131, "謝謝$R;");
                        break;
                    case 2:
                        foreach (SagaDB.Item.Item i in NPCTrade(pc))
                        {
                            if (i.ItemID == 10007500)
                                count += i.Stack;
                        }
                        if (count > 0)
                        {
                            Say(pc, 131, string.Format("交給了{0}個甲殼", count));
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
            if (SInt["Mamasan_Carapace"] < 500000)
            {
                if (!mask.Test(DFHJFlags.甲壳收集二十万))
                {
                    mask.SetValue(DFHJFlags.甲壳收集二十万, true);
                    Say(pc, 131, "已經有20萬個甲殼了！$R;" +
                        "$R要出售美味可口的『寵物食糧』啊！$R;" +
                        "$R還有植物也算是寵物呢$R聽說，客人會根據需要，$R購買不同的『植物營養劑』。$R;" +
                        "集齊30萬個『甲殼』的話$R就賣新一批小狗吧$R;");
                    OpenShopBuy(pc, 100);
                    Say(pc, 131, "再來啊！$R;");
                    return;
                }
                switch (Select(pc, "歡迎！", "", "看東西", "搜集甲殼", "現在搜集了多少甲殼？", "什麼也不做。"))
                {
                    case 1:
                        OpenShopBuy(pc, 100);
                        Say(pc, 131, "再來啊！$R;");
                        break;
                    case 2:
                        foreach (SagaDB.Item.Item i in NPCTrade(pc))
                        {
                            if (i.ItemID == 10007500)
                                count += i.Stack;
                        }
                        if (count > 0)
                        {
                            Say(pc, 131, string.Format("交給了{0}個甲殼", count));
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
            if (SInt["Mamasan_Carapace"] < 550000)
            {
                if (!mask.Test(DFHJFlags.甲壳收集五十万))
                {
                    mask.SetValue(DFHJFlags.甲壳收集五十万, true);
                    Say(pc, 131, "已經有50萬個甲殼了！$R;" +
                        "$R新的小狗也到了$R;");
                    OpenShopBuy(pc, 101);
                    Say(pc, 131, "再來啊$R;");
                    return;
                }
                switch (Select(pc, "歡迎！", "", "看東西", "搜集甲殼", "現在搜集了多少甲殼？", "什麼也不做。"))
                {
                    case 1:
                        OpenShopBuy(pc, 101);
                        Say(pc, 131, "再來啊$R;");
                        break;
                    case 2:
                        foreach (SagaDB.Item.Item i in NPCTrade(pc))
                        {
                            if (i.ItemID == 10007500)
                                count += i.Stack;
                        }
                        if (count > 0)
                        {
                            Say(pc, 131, string.Format("交給了{0}個甲殼", count));
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
            if (SInt["Mamasan_Carapace"] < 600000)
            {
                if (!mask.Test(DFHJFlags.甲壳收集五十五万))
                {
                    mask.SetValue(DFHJFlags.甲壳收集五十五万, true);
                    Say(pc, 131, "已經有55萬個甲殼了！$R;" +
                        "$R新商品也到貨了$R;");
                    OpenShopBuy(pc, 160);
                    Say(pc, 131, "再來啊！$R;");
                    return;
                }
                switch (Select(pc, "歡迎！", "", "看東西", "搜集甲殼", "現在搜集了多少甲殼？", "什麼也不做。"))
                {
                    case 1:
                        OpenShopBuy(pc, 160);
                        Say(pc, 131, "再來啊！$R;");
                        break;
                    case 2:
                        foreach (SagaDB.Item.Item i in NPCTrade(pc))
                        {
                            if (i.ItemID == 10007500)
                                count += i.Stack;
                        }
                        if (count > 0)
                        {
                            Say(pc, 131, string.Format("交給了{0}個甲殼", count));
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
            if (SInt["Mamasan_Carapace"] < 650000)
            {
                if (!mask.Test(DFHJFlags.甲壳收集六十万))
                {
                    mask.SetValue(DFHJFlags.甲壳收集六十万, true);
                    Say(pc, 131, "已經有60萬個甲殼了！$R;" +
                        "$R新商品也到貨了$R;");
                    OpenShopBuy(pc, 161);
                    Say(pc, 131, "再來啊$R;");
                    return;
                }
                switch (Select(pc, "歡迎！", "", "看東西", "搜集甲殼", "現在搜集了多少甲殼？", "什麼也不做。"))
                {
                    case 1:
                        OpenShopBuy(pc, 161);
                        Say(pc, 131, "再來啊$R;");
                        break;
                    case 2:
                        foreach (SagaDB.Item.Item i in NPCTrade(pc))
                        {
                            if (i.ItemID == 10007500)
                                count += i.Stack;
                        }
                        if (count > 0)
                        {
                            Say(pc, 131, string.Format("交給了{0}個甲殼", count));
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
                    Say(pc, 131, "已經有65萬個甲殼了！$R;" +
                        "$R新商品也到貨了$R;");
                    OpenShopBuy(pc, 162);
                    Say(pc, 131, "再來啊$R;");
                    return;
                }
                switch (Select(pc, "歡迎！", "", "看東西", "搜集甲殼", "現在搜集了多少甲殼？", "什麼也不做。"))
                {
                    case 1:
                        OpenShopBuy(pc, 162);
                        Say(pc, 131, "再來啊$R;");
                        break;
                    case 2:
                        foreach (SagaDB.Item.Item i in NPCTrade(pc))
                        {
                            if (i.ItemID == 10007500)
                                count += i.Stack;
                        }
                        if (count > 0)
                        {
                            Say(pc, 131, string.Format("交給了{0}個甲殼", count));
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
                    Say(pc, 131, "已經有70萬個甲殼了！$R;" +
                        "$R新商品也到貨了$R;");
                    OpenShopBuy(pc, 163);
                    Say(pc, 131, "再來啊！$R;");
                    return;
                }
                switch (Select(pc, "歡迎！", "", "看東西", "搜集甲殼", "現在搜集了多少甲殼？", "什麼也不做。"))
                {
                    case 1:
                        OpenShopBuy(pc, 163);
                        Say(pc, 131, "再來啊！$R;");
                        break;
                    case 2:
                        foreach (SagaDB.Item.Item i in NPCTrade(pc))
                        {
                            if (i.ItemID == 10007500)
                                count += i.Stack;
                        }
                        if (count > 0)
                        {
                            Say(pc, 131, string.Format("交給了{0}個甲殼", count));
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
                    Say(pc, 131, "已經有75萬個甲殼了！$R;" +
                        "$R新商品也到貨了$R;");
                    OpenShopBuy(pc, 164);
                    Say(pc, 131, "再來啊$R;");
                    return;
                }
                switch (Select(pc, "歡迎！", "", "看東西", "搜集甲殼", "現在搜集了多少甲殼？", "什麼也不做。"))
                {
                    case 1:
                        OpenShopBuy(pc, 164);
                        Say(pc, 131, "再來啊$R;");
                        break;
                    case 2:
                        foreach (SagaDB.Item.Item i in NPCTrade(pc))
                        {
                            if (i.ItemID == 10007500)
                                count += i.Stack;
                        }
                        if (count > 0)
                        {
                            Say(pc, 131, string.Format("交給了{0}個甲殼", count));
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
                    Say(pc, 131, "已經有80萬個甲殼了！$R;" +
                        "$R新商品也到貨了$R;");
                    OpenShopBuy(pc, 165);
                    Say(pc, 131, "再來啊！$R;");
                    return;
                }
                switch (Select(pc, "歡迎！", "", "看東西", "搜集甲殼", "現在搜集了多少甲殼？", "什麼也不做。"))
                {
                    case 1:
                        OpenShopBuy(pc, 165);
                        Say(pc, 131, "再來啊！$R;");
                        break;
                    case 2:
                        foreach (SagaDB.Item.Item i in NPCTrade(pc))
                        {
                            if (i.ItemID == 10007500)
                                count += i.Stack;
                        }
                        if (count > 0)
                        {
                            Say(pc, 131, string.Format("交給了{0}個甲殼", count));
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
                    Say(pc, 131, "已經有90萬個甲殼了！$R;" +
                        "$R新商品也到貨了$R;");
                    OpenShopBuy(pc, 194);
                    Say(pc, 131, "再來啊$R;");
                    return;
                }
                switch (Select(pc, "歡迎！", "", "看東西", "搜集甲殼", "現在搜集了多少甲殼？", "什麼也不做。"))
                {
                    case 1:
                        OpenShopBuy(pc, 194);
                        Say(pc, 131, "再來啊$R;");
                        break;
                    case 2:
                        foreach (SagaDB.Item.Item i in NPCTrade(pc))
                        {
                            if (i.ItemID == 10007500)
                                count += i.Stack;
                        }
                        if (count > 0)
                        {
                            Say(pc, 131, string.Format("交給了{0}個甲殼", count));
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
                Say(pc, 131, "真是感謝大家啊，$R;" +
                    "終於有100萬個甲殼了！$R;" +
                    "$R新商品也到貨了$R;");
                OpenShopBuy(pc, 195);
                Say(pc, 131, "再來啊！$R;");
                return;
            }
            switch (Select(pc, "歡迎！", "", "看東西(1號店)", "看東西(2號店)", "搜集甲殼", "現在搜集了多少甲殼？", "什麼也不做。"))
            {
                case 1:
                    OpenShopBuy(pc, 194);
                    Say(pc, 131, "再來啊$R;");
                    break;
                case 2:
                    OpenShopBuy(pc, 195);
                    Say(pc, 131, "再來啊！$R;");
                    break;
                case 3:
                    foreach (SagaDB.Item.Item i in NPCTrade(pc))
                    {
                        if (i.ItemID == 10007500)
                            count += i.Stack;
                    }
                    if (count > 0)
                    {
                        Say(pc, 131, string.Format("交給了{0}個甲殼", count));
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
            Say(pc, 131, "現在……$R;" +
                "" + SInt["Mamasan_Carapace"] + "个甲殼！$R;");
            if (mask.Test(DFHJFlags.甲壳收集一百万))
            {
                Say(pc, 131, "真的謝謝您。$R幸好有您，$R我們家有很多『甲殼』了$R;" +
                    "$R下次還有事的話$R還要拜託您呢$R;");
                return;
            }
            if (mask.Test(DFHJFlags.甲壳收集九十万))
            {
                Say(pc, 131, "集齊100萬個『甲殼』的話$R;" +
                    "就賣寵物喜歡的道具吧$R;");
                return;
            }
            if (mask.Test(DFHJFlags.甲壳收集八十万))
            {
                Say(pc, 131, "集齊90萬個『甲殼』的話$R;" +
                    "就賣寵物喜歡的道具吧$R;");
                return;
            }
            if (mask.Test(DFHJFlags.甲壳收集七十五万))
            {

                Say(pc, 131, "集齊80萬個『甲殼』的話$R;" +
                    "就賣寵物喜歡的道具吧$R;");

                return;
            }
            if (mask.Test(DFHJFlags.甲壳收集七十万))
            {
                Say(pc, 131, "集齊75萬個『甲殼』的話$R;" +
                    "就賣寵物喜歡的道具吧$R;");
                return;
            }
            if (mask.Test(DFHJFlags.甲壳收集六十五万))
            {
                Say(pc, 131, "集齊70萬個『甲殼』的話$R;" +
                    "就賣寵物喜歡的道具吧$R;");
                return;
            }
            if (mask.Test(DFHJFlags.甲壳收集六十万))
            {
                Say(pc, 131, "集齊65萬個『甲殼』的話$R;" +
                    "就賣寵物喜歡的道具吧$R;");
                return;
            }
            if (mask.Test(DFHJFlags.甲壳收集五十五万))
            {
                Say(pc, 131, "集齊60萬個『甲殼』的話$R;" +
                    "就賣寵物喜歡的道具吧$R;");
                return;
            }
            if (mask.Test(DFHJFlags.甲壳收集五十万))
            {
                Say(pc, 131, "集齊55萬個『甲殼』的話$R;" +
                    "就賣寵物喜歡的道具吧$R;");
                return;
            }
            if (mask.Test(DFHJFlags.甲壳收集二十万))
            {
                Say(pc, 131, "集齊50萬個『甲殼』的話$R;" +
                    "就賣新一批小狗吧$R;");
                return;
            }
            if (mask.Test(DFHJFlags.甲壳收集十万))
            {
                Say(pc, 131, "集齊50萬個『甲殼』的話$R;" +
                    "就賣寵物食糧吧$R;" +
                    "$P還有，新一批小狗$R;" +
                    "好像快要出生了！$R;" +
                    "$R要是開始販賣寵物食糧$R;" +
                    "希望也可以增加小狗的品種阿$R;");
                return;
            }
            Say(pc, 131, "找到10萬個『甲殼』$R;" +
                "就可以開店了！$R;");
        }
        void 管理用(ActorPC pc)
        {

            switch (Select(pc, "怎麼辦呢？", "", "增加", "減少", "什麼也不做。"))
            {
                case 1:
                    SInt["Mamasan_Carapace"] += int.Parse(InputBox(pc, "輸入要增加的數量", InputType.ItemCode));
                    break;
                case 2:
                    int count = int.Parse(InputBox(pc, "輸入要減少的數量", InputType.ItemCode));
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
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30131002
{
    public class S11000769 : Event
    {
        public S11000769()
        {
            this.EventID = 11000769;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<MZTYSFlags> mask = pc.CMask["MZTYS"];

            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.HEAD))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50025000 ||
                    pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50026800)
                {
                    Say(pc, 131, "你這傢伙，死對頭謎語團$R;" +
                        "走開！$R;");
                    return;
                }
            }
            /*
            if (_2b35 && !_Xb41)
            {
                Say(pc, 342, "哇哈哈！$R;" +
                    "得到『飛空庭甲板』了。$R;");
                switch (Select(pc, "怎麼辦呢？", "", "什麼都不做", "交還『飛空庭甲板』！"))
                {
                    case 2:
                        Say(pc, 342, "啊？$R;" +
                            "您怎麼知道的？$R;" +
                            "$R嗯…那就沒辦法了，還給您吧。$R;");
                        if (CheckInventory(pc, 10029100, 1))
                        {
                            Say(pc, 342, "很重的，能拿嗎？$R;");
                            switch (Select(pc, "能拿嗎？", "", "不能", "能"))
                            {
                                case 2:
                                    _Xb41 = true;
                                    GiveItem(pc, 10029100, 1);
                                    PlaySound(pc, 2040, false, 100, 50);
                                    Say(pc, 131, "得到『飛空庭甲板』$R;");
                                    break;
                            }
                            return;
                        }
                        PlaySound(pc, 2041, false, 100, 50);
                        Say(pc, 131, "行李太多，搬不了…$R;");
                        break;
                }
                return;
            }
            */
            if (CountItem(pc, 10042701) >= 1 && !mask.Test(MZTYSFlags.提示泰迪皇冠) && !mask.Test(MZTYSFlags.獲得泰迪皇冠))//_Xb18)
            {
                ShowEffect(pc, 11000769, 4520);
                Say(pc, 361, "哎！血板！！$R;" +
                    "$P鳴鳴$R;" +
                    "竟然弄壞我的玩偶！！$R;" +
                    "$R鳴鳴……鳴鳴……$R;");
                if (CountItem(pc, 10042701) >= 5 && !mask.Test(MZTYSFlags.獲得鵝卵石))//_6a32)
                {
                    Say(pc, 342, "我給你這個，$R;" +
                        "走吧！$R;");
                    if (CheckInventory(pc, 10014600, 1))
                    {
                        TakeItem(pc, 10042701, 5);
                        GiveItem(pc, 10014600, 1);
                        mask.SetValue(MZTYSFlags.獲得鵝卵石, true);
                        //_6a32 = true;
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 131, "給『鵝卵石』擲中了！$R;");
                        return;
                    }
                    PlaySound(pc, 2041, false, 100, 50);
                    Say(pc, 131, "行李太多了，不能給您……$R;");
                    return;
                }
                if (CountItem(pc, 10042701) >= 10 && !mask.Test(MZTYSFlags.獲得鐵塊))//_6a33)
                {
                    Say(pc, 342, "這個真是給您的$R;" +
                        "請走吧！$R;");
                    if (CheckInventory(pc, 10015600, 1))
                    {
                        TakeItem(pc, 10042701, 10);
                        GiveItem(pc, 10015600, 1);
                        mask.SetValue(MZTYSFlags.獲得鐵塊, true);
                        //_6a33 = true;
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 131, "給『鐵塊』擲中了！$R;");
                        return;
                    }
                    PlaySound(pc, 2041, false, 100, 50);
                    Say(pc, 131, "行李太多了，不能給您……$R;");
                    return;
                }
                if (CountItem(pc, 10042701) >= 25 && !mask.Test(MZTYSFlags.獲得銀塊))//_6a34)
                {
                    Say(pc, 342, "這個真是給您的$R;" +
                        "請走吧！$R;");
                    if (CheckInventory(pc, 10015710, 1))
                    {
                        TakeItem(pc, 10042701, 25);
                        GiveItem(pc, 10015710, 1);
                        mask.SetValue(MZTYSFlags.獲得銀塊, true);
                       // _6a34 = true;
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 131, "給『銀塊』擲中了！$R;");
                        return;
                    }
                    PlaySound(pc, 2041, false, 100, 50);
                    Say(pc, 131, "行李太多了，不能給您……$R;");
                    return;
                }
                if (CountItem(pc, 10042701) >= 50 && !mask.Test(MZTYSFlags.獲得金塊))//_6a35)
                {
                    Say(pc, 342, "這個真是給您的$R;" +
                        "請走吧！$R;");
                    if (CheckInventory(pc, 10015300, 1))
                    {
                        TakeItem(pc, 10042701, 50);
                        GiveItem(pc, 10015300, 1);
                        mask.SetValue(MZTYSFlags.獲得金塊, true);
                        //_6a35 = true;
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 131, "給『金塊』擲中了！$R;");
                        return;
                    }
                    PlaySound(pc, 2041, false, 100, 50);
                    Say(pc, 131, "行李太多了，不能給您……$R;");
                    return;
                }
                if (CountItem(pc, 10042701) >= 100 && !mask.Test(MZTYSFlags.提示泰迪皇冠))//_6a25)
                {
                    TakeItem(pc, 10042701, 100);
                    mask.SetValue(MZTYSFlags.提示泰迪皇冠, true);
                    //_6a25 = true;
                    Say(pc, 131, "已經沒有可以扔掉的東西了。$R;" +
                        "知道了，我知道了$R;" +
                        "我認輪了…$R;" +
                        "$R為了證明我降服$R;" +
                        "你把我最珍惜的拿走吧！$R;" +
                        "$P我最珍惜的東西$R;" +
                        "現在由我的夥伴他們守護著$R;" +
                        "$P提示是……$R;" +
                        "$R『巨大的戰艦』……鳴鳴…$R;");
                    return;
                }
                Say(pc, 131, "如果輸了的話$R;" +
                    "就要給對方，自己最珍惜的東西。$R;" +
                    "這雖然是海盜的規則，$R;" +
                    "$R但我還沒輸掉呢！$R;" +
                    "$P聽好了$R;" +
                    "如果不能收集到更多的話$R;" +
                    "我是不會認輸的！$R;" +
                    "$R知道就離開吧！$R;");
                return;
            }
            if (!mask.Test(MZTYSFlags.獲得泰迪皇冠) && mask.Test(MZTYSFlags.提示泰迪皇冠))//_6a25)
            {
                Say(pc, 131, "我最珍惜的東西$R;" +
                    "現在由我的夥伴他們守護著$R;" +
                    "$P提示是……$R;" +
                    "$R『巨大的戰艦』……鳴鳴…$R;");
                return;
            }
            if (mask.Test(MZTYSFlags.獲得泰迪皇冠) && mask.Test(MZTYSFlags.提示泰迪皇冠))//_6a25)
            {
                Say(pc, 131, "你又來了$R;" +
                    "夠了，別再這樣！$R;");
                return;
            }
            Say(pc, 131, "哈哈哈$R;" +
                "我是海盜老大，$R;" +
                "什麼，這裡有什麼東西？$R;" +
                "這是秘密$R;" +
                "這裡沒有到泰迪島的秘密入口$R;" +
                "$R知道了就離開吧$R;");
        }
    }
}
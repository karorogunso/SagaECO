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
                    Say(pc, 131, "你这傢伙，死对头谜之团$R;" +
                        "走开！$R;");
                    return;
                }
            }
            /*
            if (_2b35 && !_Xb41)
            {
                Say(pc, 342, "哇哈哈！$R;" +
                    "得到『飛空庭甲板』了。$R;");
                switch (Select(pc, "怎么办呢？", "", "什么都不做", "交还『飞空庭甲板』！"))
                {
                    case 2:
                        Say(pc, 342, "啊？$R;" +
                            "你怎么知道的？$R;" +
                            "$R嗯…那就没办法了，还給你吧。$R;");
                        if (CheckInventory(pc, 10029100, 1))
                        {
                            Say(pc, 342, "很重的，能拿吗？$R;");
                            switch (Select(pc, "能拿吗？", "", "不能", "能"))
                            {
                                case 2:
                                    _Xb41 = true;
                                    GiveItem(pc, 10029100, 1);
                                    PlaySound(pc, 2040, false, 100, 50);
                                    Say(pc, 131, "得到『飞空庭甲板』$R;");
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
                    "$P鸣鸣$R;" +
                    "竟然弄坏我的玩偶！！$R;" +
                    "$R鸣鸣……鸣鸣……$R;");
                if (CountItem(pc, 10042701) >= 5 && !mask.Test(MZTYSFlags.獲得鵝卵石))//_6a32)
                {
                    Say(pc, 342, "我给你这个，$R;" +
                        "走吧！$R;");
                    if (CheckInventory(pc, 10014600, 1))
                    {
                        TakeItem(pc, 10042701, 5);
                        GiveItem(pc, 10014600, 1);
                        mask.SetValue(MZTYSFlags.獲得鵝卵石, true);
                        //_6a32 = true;
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 131, "被『石块』打中了！$R;");
                        return;
                    }
                    PlaySound(pc, 2041, false, 100, 50);
                    Say(pc, 131, "行李太多了，不能给你……$R;");
                    return;
                }
                if (CountItem(pc, 10042701) >= 10 && !mask.Test(MZTYSFlags.獲得鐵塊))//_6a33)
                {
                    Say(pc, 342, "我给你这个$R;" +
                        "走吧！$R;");
                    if (CheckInventory(pc, 10015600, 1))
                    {
                        TakeItem(pc, 10042701, 10);
                        GiveItem(pc, 10015600, 1);
                        mask.SetValue(MZTYSFlags.獲得鐵塊, true);
                        //_6a33 = true;
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 131, "被『铁块』打中了！$R;");
                        return;
                    }
                    PlaySound(pc, 2041, false, 100, 50);
                    Say(pc, 131, "行李太多了，不能给你……$R;");
                    return;
                }
                if (CountItem(pc, 10042701) >= 25 && !mask.Test(MZTYSFlags.獲得銀塊))//_6a34)
                {
                    Say(pc, 342, "我给你这个$R;" +
                        "走吧！$R;");
                    if (CheckInventory(pc, 10015710, 1))
                    {
                        TakeItem(pc, 10042701, 25);
                        GiveItem(pc, 10015710, 1);
                        mask.SetValue(MZTYSFlags.獲得銀塊, true);
                       // _6a34 = true;
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 131, "被『银块』打中了！$R;");
                        return;
                    }
                    PlaySound(pc, 2041, false, 100, 50);
                    Say(pc, 131, "行李太多了，不能给你……$R;");
                    return;
                }
                if (CountItem(pc, 10042701) >= 50 && !mask.Test(MZTYSFlags.獲得金塊))//_6a35)
                {
                    Say(pc, 342, "我给你这个$R;" +
                        "走吧！$R;");
                    if (CheckInventory(pc, 10015300, 1))
                    {
                        TakeItem(pc, 10042701, 50);
                        GiveItem(pc, 10015300, 1);
                        mask.SetValue(MZTYSFlags.獲得金塊, true);
                        //_6a35 = true;
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 131, "被『金块』打中了！$R;");
                        return;
                    }
                    PlaySound(pc, 2041, false, 100, 50);
                    Say(pc, 131, "行李太多了，不能给你……$R;");
                    return;
                }
                if (CountItem(pc, 10042701) >= 100 && !mask.Test(MZTYSFlags.提示泰迪皇冠))//_6a25)
                {
                    TakeItem(pc, 10042701, 100);
                    mask.SetValue(MZTYSFlags.提示泰迪皇冠, true);
                    //_6a25 = true;
                    Say(pc, 131, "已经没有可以扔掉的东西了。$R;" +
                        "知道了，我知道了$R;" +
                        "我认输了…$R;" +
                        "$R要证明我输了的话$R;" +
                        "你把我最珍贵的东西拿走吧！$R;" +
                        "$P我最珍贵的东西$R;" +
                        "现在由我的伙伴他们守护着$R;" +
                        "$P提示是……$R;" +
                        "$R『巨大的战舰』……鸣鸣…$R;");
                    return;
                }
                Say(pc, 131, "如果输了的话$R;" +
                    "就要给对方，自己最珍贵的东西。$R;" +
                    "这虽然是海盗的规则，$R;" +
                    "$R但我还没输掉呢！$R;" +
                    "$P听好了$R;" +
                    "如果不能收集到更多的话$R;" +
                    "我是不会认输的！$R;" +
                    "$R知道就离开吧！$R;");
                return;
            }
            if (!mask.Test(MZTYSFlags.獲得泰迪皇冠) && mask.Test(MZTYSFlags.提示泰迪皇冠))//_6a25)
            {
                Say(pc, 131, "我最珍贵的东西$R;" +
                    "现在由我的伙伴他们守护着$R;" +
                    "$P提示是……$R;" +
                    "$R『巨大的战舰』……鸣鸣…$R;");
                return;
            }
            if (mask.Test(MZTYSFlags.獲得泰迪皇冠) && mask.Test(MZTYSFlags.提示泰迪皇冠))//_6a25)
            {
                Say(pc, 131, "你又来了$R;" +
                    "够了，别再这样！$R;");
                return;
            }
            Say(pc, 131, "哈哈哈$R;" +
                "我是海盗老大，$R;" +
                "什么，这里有什么东西？$R;" +
                "这是秘密$R;" +
                "这里没有到泰迪岛的秘密入口$R;" +
                "$R知道了就离开吧$R;");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10002000
{
    public class S11000112 : Event
    {
        public S11000112()
        {
            this.EventID = 11000112;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<RLSXYFlags> mask = new BitMask<RLSXYFlags>(pc.CMask["RLSXY"]);
            BitMask<Job2X_06> Job2X_06_mask = pc.CMask["Job2X_06"];
            BitMask<Crystal> Crystal_mask = pc.CMask["Crystal"];

            if (Crystal_mask.Test(Crystal.開始收集) &&
                !Crystal_mask.Test(Crystal.水之精靈) &&
                CountItem(pc, 10014300) >= 1)
            {
                if (!Crystal_mask.Test(Crystal.光之精靈) &&
                    !Crystal_mask.Test(Crystal.暗之精靈) &&
                    !Crystal_mask.Test(Crystal.炎之精靈) &&
                    !Crystal_mask.Test(Crystal.土之精靈) &&
                    !Crystal_mask.Test(Crystal.風之精靈) &&
                    CountItem(pc, 10014300) >= 1)
                {
                    Crystal_mask.SetValue(Crystal.水之精靈, true);
                    Say(pc, 131, "您好$R;" +
                        "$R我叫希尔达！是水之精灵$R;" +
                        "$R什么事？$R;" +
                        "$P…$R;" +
                        "$R想在『水晶』上注入力量？$R;" +
                        "$R怎么知道『水晶』里可以$R;" +
                        "注入精灵力量的呀$R;" +
                        "好厉害啊！$R;" +
                        "$P那么开始吧！$R;");
                    PlaySound(pc, 3120, false, 100, 50);
                    ShowEffect(pc, 4034);
                    Say(pc, 131, "『水晶』里注入力量了$R;" +
                        "…?$R;" +
                        "不能再增加力量了$R;");
                    return;
                }
                Say(pc, 131, "您好$R;" +
                    "$R我叫希尔达！是水灵精灵$R;" +
                    "$R什么事呢？$R;" +
                    "$P…$R;" +
                    "$R想在『水晶』上注入力量？$R;" +
                    "…?$R;" +
                    "哎呀！$R;" +
                    "好像还有别的精灵力量耶$R;" +
                    "$P在这里不能$R;" +
                    "注入我的力量呀$R;");
                return;
            }

            if (!mask.Test(RLSXYFlags.水之精靈第一次對話))
            {
                Say(pc, 131, "您好！$R;" +
                    "$R我叫希尔达！是水之精灵$R;" +
                    "$R什么事呢？$R;");
                mask.SetValue(RLSXYFlags.水之精靈第一次對話, true);
            }
            else
            {
                Say(pc, 131, "您好$R;" +
                    "找我有什么事呢？$R;");
            }

            if (Job2X_06_mask.Test(Job2X_06.朝拜水之精靈))
            {
                Say(pc, 131, "请成为最棒的精灵师哦！$R;");
                return;
            }

            if (Job2X_06_mask.Test(Job2X_06.進階轉職開始))
            {
                Job2X_06_mask.SetValue(Job2X_06.朝拜水之精靈, true);
                //_3A96 = true;
                Say(pc, 131, "最近也有巡礼的人呀？$R;" +
                    "$P那么开始吗？$R;" +
                    "$P希望您得到水灵的保护哦…$R;");
                ShowEffect(pc, 4034);
                return;
            }

            if (CountItem(pc, 60091051) >= 1 && CountItem(pc, 10012200) >= 1)
            {
                Say(pc, 131, "啊，是『水蓝宝石』呀$R;" +
                    "用水蓝宝石交换$R;" +
                    "在『猎人战弓』里$R;" +
                    "注入水之力是吗？$R;");
                switch (Select(pc, "怎么办呢？", "", "注入水之力", "放弃"))
                {
                    case 1:
                        if (CheckInventory(pc, 60091053, 1))
                        {
                            PlaySound(pc, 3068, false, 100, 50);
                            Wait(pc, 2000);
                            TakeItem(pc, 60091051, 1);
                            TakeItem(pc, 10012200, 1);
                            GiveItem(pc, 60091053, 1);
                            Say(pc, 131, "得到了『猎人战弓（水）』$R;");
                            Say(pc, 131, "『水蓝宝石』！太谢谢您了！$R;" +
                                "请再来玩呀！$R;");
                            return;
                        }
                        Say(pc, 131, "行李太多了唷$R;");
                        break;
                    case 2:
                        Say(pc, 131, "哎呀…我误会您了$R;");
                        break;
                }
                return;
            }
            if (CountItem(pc, 10011203) >= 1 && CountItem(pc, 10026400) >= 1)
            {
                Say(pc, 131, "『水之召唤石』$R;" +
                    "$R为了交换召唤石，$R;" +
                    "在『木箭』上注入水之力是吗？$R;");
                switch (Select(pc, "怎么办呢?", "", "注入水之力", "放弃"))
                {
                    case 1:
                        PlaySound(pc, 3053, false, 100, 50);
                        Wait(pc, 3000);
                        int MJ;
                        MJ = 0;
                        while (CountItem(pc, 10026400) > MJ)
                        {
                            MJ++;
                        }
                        if (MJ > 199)
                        {
                            if (CheckInventory(pc, 10026504, 200))
                            {
                                TakeItem(pc, 10011203, 1);
                                TakeItem(pc, 10026400, 200);
                                GiveItem(pc, 10026504, 200);
                                Say(pc, 131, "『木箭』变成了『水之箭』$R;");
                                Say(pc, 131, "突然…突然！$R;" +
                                    "$R不能再做了$R;" +
                                    "如果想再做的话$R;" +
                                    "把水之召唤石拿来吧$R;");
                                return;
                            }
                            Say(pc, 131, "行李太多了唷$R;");
                            return;
                        }
                        TakeItem(pc, 10011203, 1);
                        TakeItem(pc, 10026400, (ushort)MJ);
                        GiveItem(pc, 10026504, (ushort)MJ);
                        Say(pc, 131, "『木箭』变成了『水之箭』$R;");
                        Say(pc, 131, "谢谢您给我『水灵召唤石』$R;" +
                            "请再来玩呀！$R;");
                        break;
                    case 2:
                        Say(pc, 131, "哎呀…我误会您了$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "…?$R;" +
                "什么也没有$R;");
        }
    }
}

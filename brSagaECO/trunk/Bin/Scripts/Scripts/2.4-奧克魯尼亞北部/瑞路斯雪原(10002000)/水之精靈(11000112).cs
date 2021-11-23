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
                        "$R我叫希爾達！是水靈精靈$R;" +
                        "$R什麼事？$R;" +
                        "$P…$R;" +
                        "$R想在『水晶』上注入力量？$R;" +
                        "$R怎麼知道『水晶』裡可以$R;" +
                        "注入精靈力量的呀$R;" +
                        "知道了嗎？好厲害啊！$R;" +
                        "$P那麼開始吧！$R;");
                    PlaySound(pc, 3120, false, 100, 50);
                    ShowEffect(pc, 4034);
                    Say(pc, 131, "『水晶』裡注入力量了$R;" +
                        "…?$R;" +
                        "不能再增加力量了$R;");
                    return;
                }
                Say(pc, 131, "您好$R;" +
                    "$R我叫希爾達！是水靈精靈$R;" +
                    "$R什麼事呢？$R;" +
                    "$P…$R;" +
                    "$R想在『水晶』上注入力量？$R;" +
                    "…?$R;" +
                    "哎呀！$R;" +
                    "好像還有別的精靈力量耶$R;" +
                    "$P在這裡不能$R;" +
                    "注入我的力量呀$R;");
                return;
            }

            if (!mask.Test(RLSXYFlags.水之精靈第一次對話))
            {
                Say(pc, 131, "您好！$R;" +
                    "$R我叫希爾達！是水靈精靈$R;" +
                    "$R什麼事呢？$R;");
                mask.SetValue(RLSXYFlags.水之精靈第一次對話, true);
            }
            else
            {
                Say(pc, 131, "您好$R;" +
                    "找我有什麼事呢？$R;");
            }

            if (Job2X_06_mask.Test(Job2X_06.朝拜水之精靈))
            {
                Say(pc, 131, "請成為最棒的元素術師唷！$R;");
                return;
            }

            if (Job2X_06_mask.Test(Job2X_06.進階轉職開始))
            {
                Job2X_06_mask.SetValue(Job2X_06.朝拜水之精靈, true);
                //_3A96 = true;
                Say(pc, 131, "最近也有巡禮的人呀？$R;" +
                    "有！$R;" +
                    "$P那麼開始嗎？$R;" +
                    "$P希望您得到水靈保護唷…$R;");
                ShowEffect(pc, 4034);
                return;
            }

            if (CountItem(pc, 60091051) >= 1 && CountItem(pc, 10012200) >= 1)
            {
                Say(pc, 131, "啊，是『藍玉』呀$R;" +
                    "為了交換藍玉$R;" +
                    "在『獵弓』裡$R;" +
                    "注入水之力是嗎？$R;");
                switch (Select(pc, "怎麼辦呢？", "", "注入水之力", "放棄"))
                {
                    case 1:
                        if (CheckInventory(pc, 60091053, 1))
                        {
                            PlaySound(pc, 3068, false, 100, 50);
                            Wait(pc, 2000);
                            TakeItem(pc, 60091051, 1);
                            TakeItem(pc, 10012200, 1);
                            GiveItem(pc, 60091053, 1);
                            Say(pc, 131, "得到了『獵弓（水靈）』$R;");
                            Say(pc, 131, "『藍玉』！太謝謝您了！$R;" +
                                "請再來玩呀！$R;");
                            return;
                        }
                        Say(pc, 131, "行李太多了唷$R;");
                        break;
                    case 2:
                        Say(pc, 131, "哎呀…我誤會您了$R;");
                        break;
                }
                return;
            }
            if (CountItem(pc, 10011203) >= 1 && CountItem(pc, 10026400) >= 1)
            {
                Say(pc, 131, "『水靈召喚石』$R;" +
                    "$R為了交換召喚石，$R;" +
                    "在『木箭』上注入水之力是嗎？$R;");
                switch (Select(pc, "怎麼辦呢?", "", "注入水之力", "放棄"))
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
                                Say(pc, 131, "『木箭』變成了『水靈之箭』$R;");
                                Say(pc, 131, "突然…突然！$R;" +
                                    "$R不能再做了$R;" +
                                    "如果想再做的話$R;" +
                                    "把水之召喚石拿來吧$R;");
                                return;
                            }
                            Say(pc, 131, "行李太多了唷$R;");
                            return;
                        }
                        TakeItem(pc, 10011203, 1);
                        TakeItem(pc, 10026400, (ushort)MJ);
                        GiveItem(pc, 10026504, (ushort)MJ);
                        Say(pc, 131, "『木箭』變成了『水靈之箭』$R;");
                        Say(pc, 131, "謝謝您給我『水靈召喚石』$R;" +
                            "請再來玩呀！$R;");
                        break;
                    case 2:
                        Say(pc, 131, "哎呀…我誤會您了$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "…?$R;" +
                "什麼也沒有$R;");
        }
    }
}

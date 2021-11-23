using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30082000
{
    public class S11000299 : Event
    {
        public S11000299()
        {
            this.EventID = 11000299;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<AYEFlags> mask = pc.CMask["AYE"];
            BitMask<Sinker> mask_01 = pc.AMask["Sinker"];


            if (CountItem(pc, 10020762) >= 1 && mask_01.Test(Sinker.收到不明的合金))//_7a96)
            {
                Say(pc, 131, "那就辛苦您了$R;");
                return;
            }
            if (mask_01.Test(Sinker.未收到合成測試報告))//_7a98)
            {
                if (CheckInventory(pc, 10020762, 1))
                {
                    mask_01.SetValue(Sinker.未收到合成測試報告, false);
                    //_7a98 = false;
                    GiveItem(pc, 10020762, 1);
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 131, "得到『合成測試報告』$R;");
                    Say(pc, 131, "那就辛苦您了$R;");
                    return;
                }
                Say(pc, 131, "行李太多了，整理後再來吧$R;");
                mask_01.SetValue(Sinker.未收到合成測試報告, true);
                //_7a98 = true;
                return;
            }
            if (CountItem(pc, 10043083) >= 1 && mask_01.Test(Sinker.收到不明的合金))//_7a96)
            {
                Say(pc, 131, "是您啊$R;" +
                    "這麼快就回來了。$R;" +
                    "太感謝了。$R;" +
                    "$R什麼？他有信給我？$R;");
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "轉交了『給社長的信』。$R;");
                TakeItem(pc, 10043083, 1);
                Say(pc, 131, "啊，$R;" +
                    "要重新開始？$R;" +
                    "…$R;" +
                    "$R如果是迪澳曼特所拜託的$R;" +
                    "又不能拒絕，怎麼辦呢？$R;" +
                    "$P怎麼也好是關乎您的名譽地位的$R;" +
                    "這是整理好的合成試驗報告，$R;" +
                    "請幫我交給農夫行會特派員吧。$R;");
                if (CheckInventory(pc, 10020762, 1))
                {
                    mask_01.SetValue(Sinker.未收到合成測試報告, false);
                    //_7a98 = false;
                    GiveItem(pc, 10020762, 1);
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 131, "得到『合成測試報告』$R;");
                    Say(pc, 131, "那就辛苦您了$R;");
                    return;
                }
                Say(pc, 131, "行李太多了，整理後再來吧$R;");
                mask_01.SetValue(Sinker.未收到合成測試報告, true);
                //_7a98 = true;
                return;
            }
            if (mask_01.Test(Sinker.收到不明的合金))//_7a96)
            {
                Say(pc, 131, "一定要小心啊，$R;" +
                    "轉交給迪澳曼特。$R;" +
                    "也別忘了『紫水晶』唷。$R;" +
                    "$R如果出什麼事的話$R;" +
                    "就馬上回來吧。$R;" +
                    "拜託了$R;");
                return;
            }
            if (mask_01.Test(Sinker.未收到不明的合金))//_7a95)
            {
                if (CheckInventory(pc, 10011202, 1))
                {
                    mask_01.SetValue(Sinker.未收到不明的合金, false);
                    mask_01.SetValue(Sinker.收到不明的合金, true);
                    //_7a95 = false;
                    //_7a96 = true;
                    GiveItem(pc, 10011202, 1);
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 131, "得到『不明的合金』$R;");
                    Say(pc, 131, "還有這個也拜託了。$R;" +
                        "從迪澳曼特那裡收到了訂單，$R;" +
                        "要製造裝飾品。$R;" +
                        "其中一個材料『紫水晶』$R;" +
                        "還沒買到呢。$R;" +
                        "可不可以幫我找到『紫水晶』呢？$R;" +
                        "合金是很貴重的，$R;" +
                        "一定要小心啊，$R;" +
                        "$R如果出什麼事的話$R;" +
                        "就馬上回來吧。$R;" +
                        "拜託了$R;");
                    return;
                }
                Say(pc, 131, "行李太多了，整理後再來吧$R;");
                mask_01.SetValue(Sinker.未收到不明的合金, true);
                //_7a95 = true;
                return;
            }
            if (mask_01.Test(Sinker.拒絕幫忙) && mask_01.Test(Sinker.收到合成藥))//_7a94 && _7a93)
            {
                Say(pc, 131, "是嗎？改變心意了嗎？$R;");
                switch (Select(pc, "幫忙嗎？", "", "讓我幫您吧！", "拒絕"))
                {
                    case 1:
                        Say(pc, 131, "謝謝您啊$R;" +
                            "那麼就幫我把這個合金$R;" +
                            "轉交給摩根商人行會總部的$R;" +
                            "迪澳曼特吧$R;");
                        if (CheckInventory(pc, 10011202, 1))
                        {
                            mask_01.SetValue(Sinker.未收到不明的合金, false);
                            mask_01.SetValue(Sinker.收到不明的合金, true);
                            //_7a95 = false;
                            //_7a96 = true;
                            GiveItem(pc, 10011202, 1);
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 131, "得到『不明的合金』$R;");
                            Say(pc, 131, "還有這個也拜託了。$R;" +
                                "從迪澳曼特那裡收到了訂單，$R;" +
                                "要製造裝飾品。$R;" +
                                "其中一個材料『紫水晶』$R;" +
                                "還沒買到呢。$R;" +
                                "可不可以幫我找到『紫水晶』呢？$R;" +
                                "合金是很貴重的，$R;" +
                                "一定要小心啊，$R;" +
                                "$R如果出什麼事的話$R;" +
                                "就馬上回來吧。$R;" +
                                "拜託了$R;");
                            return;
                        }
                        Say(pc, 131, "行李太多了，整理後再來吧$R;");
                        mask_01.SetValue(Sinker.未收到不明的合金, true);
                        //_7a95 = true;
                        break;
                    case 2:
                        mask_01.SetValue(Sinker.拒絕幫忙, true);
                        //_7a94 = true;
                        Say(pc, 131, "是嗎？$R;");
                        break;
                }
                return;
            }
            if (mask_01.Test(Sinker.收到合成藥))//_7a93)
            {
                Say(pc, 131, "您是誰啊?$R;" +
                    "什麼?您從農夫行會來的？$R;" +
                    "啊，就等您呢$R;" +
                    "終於平安的拿回來了！$R;");
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "轉交『合成藥』$R;");
                Say(pc, 131, "來試驗一下$R;" +
                    "您先等等唷$R;");
                TakeItem(pc, 10000510, 1);
                //FADE OUT WHITE
                Wait(pc, 6000);
                //FADE IN
                Say(pc, 131, "合成實驗結束了$R;" +
                    "不好意思$R;" +
                    "都忘了您在這裡等著了$R;" +
                    "$P啊！還有事跟您說$R;" +
                    "還有件事拜託您，$R;" +
                    "怎麼樣？$R;");
                switch (Select(pc, "幫忙嗎？", "", "讓我幫您吧！", "拒絕"))
                {
                    case 1:
                        Say(pc, 131, "謝謝您啊$R;" +
                            "那麼就幫我把這個合金$R;" +
                            "轉交給摩根商人行會總部的$R;" +
                            "迪澳曼特吧$R;");
                        if (CheckInventory(pc, 10011202, 1))
                        {
                            mask_01.SetValue(Sinker.未收到不明的合金, false);
                            mask_01.SetValue(Sinker.收到不明的合金, true);
                            //_7a95 = false;
                            //_7a96 = true;
                            GiveItem(pc, 10011202, 1);
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 131, "得到『不明的合金』$R;");
                            Say(pc, 131, "還有這個也拜託了。$R;" +
                                "從迪澳曼特那裡收到了訂單，$R;" +
                                "要製造裝飾品。$R;" +
                                "其中一個材料『紫水晶』$R;" +
                                "還沒買到呢。$R;" +
                                "可不可以幫我找到『紫水晶』呢？$R;" +
                                "合金是很貴重的，$R;" +
                                "一定要小心啊，$R;" +
                                "$R如果出什麼事的話$R;" +
                                "就馬上回來吧。$R;" +
                                "拜託了$R;");
                            return;
                        }
                        Say(pc, 131, "行李太多了，整理後再來吧$R;");
                        mask_01.SetValue(Sinker.未收到不明的合金, true);
                        //_7a95 = true;
                        break;
                    case 2:
                        mask_01.SetValue(Sinker.拒絕幫忙, true);
                        //_7a94 = true;
                        Say(pc, 131, "是嗎？$R;");
                        break;
                }
                return;
            }
            

            if (pc.Fame < 100 && !mask.Test(AYEFlags.與鋼鐵工廠老闆對話))//_0c38)
            {
                Say(pc, 131, "您是誰啊？$R;");
                Say(pc, 131, pc.Name + "?$R;" +
                    "沒聽說過，信不過的感覺阿$R;" +
                    "回去吧。$R;");
                return;
            }
            if (pc.Fame > 99 && !mask.Test(AYEFlags.與鋼鐵工廠老闆對話))//_0c38)
            {
                mask.SetValue(AYEFlags.與鋼鐵工廠老闆對話, true);
                //_0c38 = true;
                Say(pc, 131, "那裡的大工廠都$R;" +
                    "把礦工們的鐵買斷了。$R;" +
                    "這裡連鐵的影子都看不見阿$R;" +
                    "$R哼！氣死了$R;" +
                    "所以有事要拜託您，$R;" +
                    "如果在地牢裡見到『鹽鐵』，$R;" +
                    "『冰鐵』，『鬼鐵』，『鏡鐵』的話$R;" +
                    "都拿到我這個鋼鐵工廠好嗎？$R;" +
                    "$P一定會給您報酬喔。$R;" +
                    "$R我會通知負責人的$R;" +
                    "只要把道具拿過來$R;" +
                    "給負責人就行了。$R;" +
                    "拜託了$R;");
                return;
            }
            Say(pc, 131, "喂！$R你不幹活在幹嘛阿？$R;");
            Say(pc, 11000300, 131, "啊，對不起。$R;");
        }
    }
}
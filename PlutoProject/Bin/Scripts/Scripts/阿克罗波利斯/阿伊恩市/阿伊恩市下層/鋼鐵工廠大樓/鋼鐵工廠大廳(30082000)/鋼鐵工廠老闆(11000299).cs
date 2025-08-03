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
                    Say(pc, 131, "得到『合成测试报告』$R;");
                    Say(pc, 131, "那就辛苦您了$R;");
                    return;
                }
                Say(pc, 131, "行李太多了，整理后再来吧$R;");
                mask_01.SetValue(Sinker.未收到合成測試報告, true);
                //_7a98 = true;
                return;
            }
            if (CountItem(pc, 10043083) >= 1 && mask_01.Test(Sinker.收到不明的合金))//_7a96)
            {
                Say(pc, 131, "是您啊$R;" +
                    "这么快就回来了。$R;" +
                    "太感谢了。$R;" +
                    "$R什么？他有信给我？$R;");
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "转交了『给社长的信』。$R;");
                TakeItem(pc, 10043083, 1);
                Say(pc, 131, "啊，$R;" +
                    "要重新开始？$R;" +
                    "…$R;" +
                    "$R如果是迪奥曼特所拜托的$R;" +
                    "又不能拒绝，怎么办呢？$R;" +
                    "$P怎么也好是关乎您的名誉地位的$R;" +
                    "这是整理好的合成试验报告，$R;" +
                    "请帮我交给农夫行会特派员吧。$R;");
                if (CheckInventory(pc, 10020762, 1))
                {
                    mask_01.SetValue(Sinker.未收到合成測試報告, false);
                    //_7a98 = false;
                    GiveItem(pc, 10020762, 1);
                    PlaySound(pc, 2040, false, 100, 50);
                    Say(pc, 131, "得到『合成测试报告』$R;");
                    Say(pc, 131, "那就辛苦您了$R;");
                    return;
                }
                Say(pc, 131, "行李太多了，整理后再来吧$R;");
                mask_01.SetValue(Sinker.未收到合成測試報告, true);
                //_7a98 = true;
                return;
            }
            if (mask_01.Test(Sinker.收到不明的合金))//_7a96)
            {
                Say(pc, 131, "一定要小心啊，$R;" +
                    "转交给迪奥曼特。$R;" +
                    "也别忘了『紫水晶』。$R;" +
                    "$R如果出什么事的话$R;" +
                    "就马上回来吧。$R;" +
                    "拜托了$R;");
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
                    Say(pc, 131, "得到『未知的合金』$R;");
                    Say(pc, 131, "还有这个也拜託了。$R;" +
                        "从迪奥曼特那里收到了订单，$R;" +
                        "要制造装饰品。$R;" +
                        "其中一个材料『紫水晶』$R;" +
                        "还没买到呢。$R;" +
                        "可不可以帮我找到『紫水晶』呢？$R;" +
                        "合金是很贵重的，$R;" +
                        "一定要小心啊，$R;" +
                        "$R如果出什么事的话$R;" +
                        "就马上回来吧。$R;" +
                        "拜托了$R;");
                    return;
                }
                Say(pc, 131, "行李太多了，整理后再来吧$R;");
                mask_01.SetValue(Sinker.未收到不明的合金, true);
                //_7a95 = true;
                return;
            }
            if (mask_01.Test(Sinker.拒絕幫忙) && mask_01.Test(Sinker.收到合成藥))//_7a94 && _7a93)
            {
                Say(pc, 131, "是吗？改变心意了吗？$R;");
                switch (Select(pc, "帮忙吗？", "", "让我帮您吧！", "拒绝"))
                {
                    case 1:
                        Say(pc, 131, "谢谢您啊$R;" +
                            "那么就帮我把这个合金$R;" +
                            "转交给摩戈商人行会总部的$R;" +
                            "迪奥曼特吧$R;");
                        if (CheckInventory(pc, 10011202, 1))
                        {
                            mask_01.SetValue(Sinker.未收到不明的合金, false);
                            mask_01.SetValue(Sinker.收到不明的合金, true);
                            //_7a95 = false;
                            //_7a96 = true;
                            GiveItem(pc, 10011202, 1);
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 131, "得到『未知的合金』$R;");
                            Say(pc, 131, "还有这个也拜托了。$R;" +
                                "从迪奥曼特那裡收到了订单，$R;" +
                                "要制造装饰品。$R;" +
                                "其中一个材料『紫水晶』$R;" +
                                "还没买到呢。$R;" +
                                "可不可以帮我找到『紫水晶』呢？$R;" +
                                "合金是很贵重的，$R;" +
                                "一定要小心啊，$R;" +
                                "$R如果出什么事的话$R;" +
                                "就马上回来吧。$R;" +
                                "拜托了$R;");
                            return;
                        }
                        Say(pc, 131, "行李太多了，整理后再来吧$R;");
                        mask_01.SetValue(Sinker.未收到不明的合金, true);
                        //_7a95 = true;
                        break;
                    case 2:
                        mask_01.SetValue(Sinker.拒絕幫忙, true);
                        //_7a94 = true;
                        Say(pc, 131, "是吗？$R;");
                        break;
                }
                return;
            }
            if (mask_01.Test(Sinker.收到合成藥))//_7a93)
            {
                Say(pc, 131, "您是谁啊?$R;" +
                    "什么?您从农夫行会来的？$R;" +
                    "啊，就等您呢$R;" +
                    "终于平安的拿回来了！$R;");
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 131, "转交『合成药』$R;");
                Say(pc, 131, "来试验一下$R;" +
                    "您先等等$R;");
                TakeItem(pc, 10000510, 1);
                //FADE OUT WHITE
                Wait(pc, 6000);
                //FADE IN
                Say(pc, 131, "合成实验结束了$R;" +
                    "不好意思$R;" +
                    "都忘了您在这里等著了$R;" +
                    "$P啊！还有事跟您说$R;" +
                    "还有件事拜托您，$R;" +
                    "怎么样？$R;");
                switch (Select(pc, "帮忙吗？", "", "让我帮您吧！", "拒绝"))
                {
                    case 1:
                        Say(pc, 131, "谢谢您啊$R;" +
                            "那么就帮我把这个合金$R;" +
                            "转交给摩戈商人行会总部的$R;" +
                            "迪奥曼特吧$R;");
                        if (CheckInventory(pc, 10011202, 1))
                        {
                            mask_01.SetValue(Sinker.未收到不明的合金, false);
                            mask_01.SetValue(Sinker.收到不明的合金, true);
                            //_7a95 = false;
                            //_7a96 = true;
                            GiveItem(pc, 10011202, 1);
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 131, "得到『未知的合金』$R;");
                            Say(pc, 131, "还有这个也拜托了。$R;" +
                                "从迪奥曼特那裡收到了订单，$R;" +
                                "要制造装饰品。$R;" +
                                "其中一个材料『紫水晶』$R;" +
                                "还没买到呢。$R;" +
                                "可不可以帮我找到『紫水晶』呢？$R;" +
                                "合金是很贵重的，$R;" +
                                "一定要小心啊，$R;" +
                                "$R如果出什么事的话$R;" +
                                "就马上回来吧。$R;" +
                                "拜托了$R;");
                            return;
                        }
                        Say(pc, 131, "行李太多了，整理后再来吧$R;");
                        mask_01.SetValue(Sinker.未收到不明的合金, true);
                        //_7a95 = true;
                        break;
                    case 2:
                        mask_01.SetValue(Sinker.拒絕幫忙, true);
                        //_7a94 = true;
                        Say(pc, 131, "是吗？$R;");
                        break;
                }
                return;
            }
            

            if (pc.Fame < 100 && !mask.Test(AYEFlags.與鋼鐵工廠老闆對話))//_0c38)
            {
                Say(pc, 131, "您是谁啊？$R;");
                Say(pc, 131, pc.Name + "?$R;" +
                    "没听说过，信不过的感觉阿$R;" +
                    "回去吧。$R;");
                return;
            }
            if (pc.Fame > 99 && !mask.Test(AYEFlags.與鋼鐵工廠老闆對話))//_0c38)
            {
                mask.SetValue(AYEFlags.與鋼鐵工廠老闆對話, true);
                //_0c38 = true;
                Say(pc, 131, "那裡的大工厂都$R;" +
                    "把矿工们的铁买断了。$R;" +
                    "这里连铁的影子都看不见啊$R;" +
                    "$R哼！气死了$R;" +
                    "所以有事要拜托您，$R;" +
                    "如果在地牢裡见到『炎铁』，$R;" +
                    "『冰铁』，『鬼铁』，『镜铁』的话$R;" +
                    "都拿到我这个钢铁工厂好吗？$R;" +
                    "$P一定会给您报酬喔。$R;" +
                    "$R我会通知负责人的$R;" +
                    "只要把道具拿过来$R;" +
                    "给负责人就行了。$R;" +
                    "拜托了$R;");
                return;
            }
            Say(pc, 131, "喂！$R你不干活在干嘛啊？$R;");
            Say(pc, 11000300, 131, "啊，对不起。$R;");
        }
    }
}
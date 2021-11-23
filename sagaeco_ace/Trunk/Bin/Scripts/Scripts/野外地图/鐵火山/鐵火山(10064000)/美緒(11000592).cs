using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10064000
{
    public class S11000592 : Event
    {
        public S11000592()
        {
            this.EventID = 11000592;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Job2X_03> mask = pc.CMask["Job2X_03"];
            
            if (mask.Test(Job2X_03.第四個問題回答正確))//_4A08)
            {
                if (mask.Test(Job2X_03.第四個問題回答正確) && CountItem(pc, 10000352) >= 1)
                {
                    Say(pc, 131, "我看一下，$R;" +
                        "药已经搜集好了吧？$R;" +
                        "$R尽快回去好吗？$R;" +
                        "$P小心啊$R;");
                    return;
                }
                if (CheckInventory(pc, 10000352, 1))
                {
                    GiveItem(pc, 10000352, 1);
                    Say(pc, 131, "得到『暗杀者的秘药4』一个$R;");
                    Say(pc, 131, "这也算是修行之一吧！$R;");
                    mask.SetValue(Job2X_03.未獲得暗殺者的內服藥4, false);
                    //_4A72 = false;
                    //_4A08 = true;
                    return;
                }
                mask.SetValue(Job2X_03.未獲得暗殺者的內服藥4, true);
                //_4A72 = true;
                Say(pc, 131, "…行李太多了，整理后再来吧$R;");
                return;
            }
            if (mask.Test(Job2X_03.第四個問題回答錯誤))//_4A07)
            {
                Say(pc, 131, "…$R;" +
                    "$P…$R;" +
                    "$R给我闪一边去。$R;");
                return;
            }
            if (mask.Test(Job2X_03.未獲得暗殺者的內服藥4))//_4A72)
            {
                if (CheckInventory(pc, 10000352, 1))
                {
                    GiveItem(pc, 10000352, 1);
                    Say(pc, 131, "得到『暗杀者的秘药4』一个$R;");
                    Say(pc, 131, "这也算是修行之一吧！$R;");
                    mask.SetValue(Job2X_03.未獲得暗殺者的內服藥4, false);
                    //_4A72 = false;
                    //_4A08 = true;
                    return;
                }
                mask.SetValue(Job2X_03.未獲得暗殺者的內服藥4, true);
                //_4A72 = true;
                Say(pc, 131, "…行李太多了，整理后再来吧$R;");
                return;
            }
            if (mask.Test(Job2X_03.第一個問題回答正確) &&
                mask.Test(Job2X_03.第二個問題回答正確) &&
                mask.Test(Job2X_03.第三個問題回答正確))//_4A03 && _4A02 && _4A01)
            {
                Say(pc, 131, "…$R;" +
                    "$P…$R;" +
                    "$R好热啊…$R;" +
                    "$P…$R;" +
                    "$P啊，热死了$R;" +
                    "这没有什么办法吗？$R;");
                switch (Select(pc, "怎么办呢？", "", "诺森比较凉快", "脱衣服就行", "心静自然凉"))
                {
                    case 1:
                        mask.SetValue(Job2X_03.第四個問題回答錯誤, true);
                        //_4A07 = true;
                        Say(pc, 131, "…$R;" +
                            "$P…$R;" +
                            "$R那里不是凉快$R;" +
                            "而是冷啊$R;");
                        break;
                    case 2:
                        mask.SetValue(Job2X_03.第四個問題回答錯誤, true);
                        //_4A07 = true;
                        Say(pc, 131, "…$R;" +
                            "$P…$R;" +
                            "$R啊，疯了啊？$R;");
                        break;
                    case 3:
                        Say(pc, 131, "…$R;" +
                            "$P…$R;" +
                            "$R心静自然凉？$R;" +
                            "什么话？$R;" +
                            "$P拿着这个！$R;");
                        if (CheckInventory(pc, 10000352, 1))
                        {
                            GiveItem(pc, 10000352, 1);
                            Say(pc, 131, "得到『暗杀者的秘药4』一个$R;");
                            Say(pc, 131, "这也算是修行之一吧！$R;");
                            mask.SetValue(Job2X_03.未獲得暗殺者的內服藥4, false);
                            mask.SetValue(Job2X_03.第四個問題回答正確, true);
                            //_4A72 = false;
                            //_4A08 = true;
                            return;
                        }
                        mask.SetValue(Job2X_03.未獲得暗殺者的內服藥4, true);
                        //_4A72 = true;
                        Say(pc, 131, "…行李太多了，整理后再来吧$R;");
                        break;
                }
                return;
            }
            if (mask.Test(Job2X_03.刺客轉職開始))//_4A00)
            {
                Say(pc, 131, "…$R;" +
                    "$P…$R;" +
                    "$R好烦啊……$R;");
                return;
            }

            Say(pc, 131, "…$R;" +
                "$P…$R;" +
                "$R好熱啊…$R;");
            
        }
    }
}
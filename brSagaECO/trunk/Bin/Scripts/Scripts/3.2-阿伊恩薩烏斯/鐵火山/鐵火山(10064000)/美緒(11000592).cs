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
                        "藥已經搜集好了吧？$R;" +
                        "$R盡快回去好嗎？$R;" +
                        "$P小心啊$R;");
                    return;
                }
                if (CheckInventory(pc, 10000352, 1))
                {
                    GiveItem(pc, 10000352, 1);
                    Say(pc, 131, "得到『暗殺者的內服藥4』一個$R;");
                    Say(pc, 131, "這也算是修行之一吧！$R;");
                    mask.SetValue(Job2X_03.未獲得暗殺者的內服藥4, false);
                    //_4A72 = false;
                    //_4A08 = true;
                    return;
                }
                mask.SetValue(Job2X_03.未獲得暗殺者的內服藥4, true);
                //_4A72 = true;
                Say(pc, 131, "…行李太多了，整理後再來吧$R;");
                return;
            }
            if (mask.Test(Job2X_03.第四個問題回答錯誤))//_4A07)
            {
                Say(pc, 131, "…$R;" +
                    "$P…$R;" +
                    "$R給我閃一邊去。$R;");
                return;
            }
            if (mask.Test(Job2X_03.未獲得暗殺者的內服藥4))//_4A72)
            {
                if (CheckInventory(pc, 10000352, 1))
                {
                    GiveItem(pc, 10000352, 1);
                    Say(pc, 131, "得到『暗殺者的內服藥4』一個$R;");
                    Say(pc, 131, "這也算是修行之一吧！$R;");
                    mask.SetValue(Job2X_03.未獲得暗殺者的內服藥4, false);
                    //_4A72 = false;
                    //_4A08 = true;
                    return;
                }
                mask.SetValue(Job2X_03.未獲得暗殺者的內服藥4, true);
                //_4A72 = true;
                Say(pc, 131, "…行李太多了，整理後再來吧$R;");
                return;
            }
            if (mask.Test(Job2X_03.第一個問題回答正確) &&
                mask.Test(Job2X_03.第二個問題回答正確) &&
                mask.Test(Job2X_03.第三個問題回答正確))//_4A03 && _4A02 && _4A01)
            {
                Say(pc, 131, "…$R;" +
                    "$P…$R;" +
                    "$R好熱啊…$R;" +
                    "$P…$R;" +
                    "$P啊，熱死了$R;" +
                    "這沒有什麼辦法嗎？$R;");
                switch (Select(pc, "怎麼辦呢？", "", "諾頓比較涼快", "脫衣服就行", "心靜自然涼"))
                {
                    case 1:
                        mask.SetValue(Job2X_03.第四個問題回答錯誤, true);
                        //_4A07 = true;
                        Say(pc, 131, "…$R;" +
                            "$P…$R;" +
                            "$R那裡不是涼快$R;" +
                            "而是冷啊$R;");
                        break;
                    case 2:
                        mask.SetValue(Job2X_03.第四個問題回答錯誤, true);
                        //_4A07 = true;
                        Say(pc, 131, "…$R;" +
                            "$P…$R;" +
                            "$R啊，瘋了啊？$R;");
                        break;
                    case 3:
                        Say(pc, 131, "…$R;" +
                            "$P…$R;" +
                            "$R心靜自然涼？$R;" +
                            "什麼話？$R;" +
                            "$P拿著這個！$R;");
                        if (CheckInventory(pc, 10000352, 1))
                        {
                            GiveItem(pc, 10000352, 1);
                            Say(pc, 131, "得到『暗殺者的內服藥4』一個$R;");
                            Say(pc, 131, "這也算是修行之一吧！$R;");
                            mask.SetValue(Job2X_03.未獲得暗殺者的內服藥4, false);
                            mask.SetValue(Job2X_03.第四個問題回答正確, true);
                            //_4A72 = false;
                            //_4A08 = true;
                            return;
                        }
                        mask.SetValue(Job2X_03.未獲得暗殺者的內服藥4, true);
                        //_4A72 = true;
                        Say(pc, 131, "…行李太多了，整理後再來吧$R;");
                        break;
                }
                return;
            }
            if (mask.Test(Job2X_03.刺客轉職開始))//_4A00)
            {
                Say(pc, 131, "…$R;" +
                    "$P…$R;" +
                    "$R好煩啊……$R;");
                return;
            }

            Say(pc, 131, "…$R;" +
                "$P…$R;" +
                "$R好熱啊…$R;");
            
        }
    }
}
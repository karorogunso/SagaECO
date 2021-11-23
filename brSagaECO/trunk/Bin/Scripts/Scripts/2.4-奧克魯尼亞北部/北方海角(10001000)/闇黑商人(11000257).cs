using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10001000
{
    public class S11000257 : Event
    {
        public S11000257()
        {
            this.EventID = 11000257;
        }

        public override void OnEvent(ActorPC pc)
        {

            BitMask<Job2X_03> mask = pc.CMask["Job2X_03"];

            if (mask.Test(Job2X_03.第四個問題回答錯誤))//_4A07)
            {
                if (mask.Test(Job2X_03.第三個問題回答正確) && CountItem(pc, 10000351) >= 1)
                {
                    mask.SetValue(Job2X_03.第四個問題回答錯誤, false);
                    //_4A07 = false;
                    Say(pc, 135, "放心吧！我可以給的提示就是$R;" +
                        "『變涼快的辦法』！$R;");
                    return;
                }
                if (CheckInventory(pc, 10000351, 1))
                {
                    GiveItem(pc, 10000351, 1);
                    Say(pc, 131, "得到1個『暗殺者的內服藥3』！$R;");
                    Say(pc, 135, "我可以給的提示就是$R;" +
                        "『變涼快的辦法』！$R;");
                    mask.SetValue(Job2X_03.未獲得暗殺者的內服藥3, false);
                    mask.SetValue(Job2X_03.第四個問題回答錯誤, false);
                    //_4A71 = false;
                    //_4A03 = true;
                    //_4A07 = false;
                    Say(pc, 135, "放心吧！我可以給的提示就是$R;" +
                        "『變涼快的辦法』！$R;");
                    return;
                }
                mask.SetValue(Job2X_03.未獲得暗殺者的內服藥3, true);
                //_4A71 = true;
                Say(pc, 135, "您的行李太滿了啦$R;" +
                    "我無法給您物品啊$R;");
                return;
            }
            if (mask.Test(Job2X_03.第三個問題回答正確))//_4A03)
            {
                if (mask.Test(Job2X_03.第三個問題回答正確) && CountItem(pc, 10000351) >= 1)
                {
                    mask.SetValue(Job2X_03.第四個問題回答錯誤, false);
                    //_4A07 = false;
                    Say(pc, 135, "放心吧！我可以給的提示就是$R;" +
                        "『變涼快的辦法』！$R;");
                    return;
                }
                if (CheckInventory(pc, 10000351, 1))
                {
                    GiveItem(pc, 10000351, 1);
                    Say(pc, 131, "得到1個『暗殺者的內服藥3』！$R;");
                    Say(pc, 135, "我可以給的提示就是$R;" +
                        "『變涼快的辦法』！$R;");
                    mask.SetValue(Job2X_03.未獲得暗殺者的內服藥3, false);
                    mask.SetValue(Job2X_03.第四個問題回答錯誤, false);
                    //_4A71 = false;
                    //_4A03 = true;
                    //_4A07 = false;
                    Say(pc, 135, "放心吧！我可以給的提示就是$R;" +
                        "『變涼快的辦法』！$R;");
                    return;
                }
                mask.SetValue(Job2X_03.未獲得暗殺者的內服藥3, true);
                //_4A71 = true;
                Say(pc, 135, "您的行李太滿了啦$R;" +
                    "我無法給您物品啊$R;");
                return;
            }
            if (mask.Test(Job2X_03.第三個問題回答錯誤))//_4A06)
            {
                Say(pc, 135, "請重新聽過提示然後再過來吧！$R;");
                return;
            }
            if (mask.Test(Job2X_03.未獲得暗殺者的內服藥3))//_4A71)
            {
                if (CheckInventory(pc, 10000351, 1))
                {
                    GiveItem(pc, 10000351, 1);
                    Say(pc, 131, "得到1個『暗殺者的內服藥3』！$R;");
                    Say(pc, 135, "我可以給的提示就是$R;" +
                        "『變涼快的辦法』！$R;");
                    mask.SetValue(Job2X_03.未獲得暗殺者的內服藥3, false);
                    mask.SetValue(Job2X_03.第三個問題回答正確, true);
                    mask.SetValue(Job2X_03.第四個問題回答錯誤, false);
                    //_4A71 = false;
                    //_4A03 = true;
                    //_4A07 = false;
                    Say(pc, 135, "放心吧！我可以給的提示就是$R;" +
                        "『變涼快的辦法』！$R;");
                    return;
                }
                mask.SetValue(Job2X_03.未獲得暗殺者的內服藥3, true);
                //_4A71 = true;
                Say(pc, 135, "您的行李太滿了啦$R;" +
                    "我無法給您物品啊$R;");
                return;
            }
            if (mask.Test(Job2X_03.第一個問題回答正確) && mask.Test(Job2X_03.第二個問題回答正確))//_4A02 && _4A01)
            {
                Say(pc, 135, "嘿嘿嘿……終於到我了嗎？$R;" +
                    "$P來吧！請隨便，不用客氣！$R;" +
                    "最近過的怎麽樣啊？$R;");
                switch (Select(pc, "想回答什麼？", "", "就那樣", "忙啊", "微妙啊"))
                {
                    case 1:
                        Say(pc, 135, "好，就是YES的意思啊$R;" +
                            "$R就拿這個走吧$R;");
                        if (CheckInventory(pc, 10000351, 1))
                        {
                            GiveItem(pc, 10000351, 1);
                            Say(pc, 131, "得到1個『暗殺者的內服藥3』！$R;");
                            Say(pc, 135, "我可以給的提示就是$R;" +
                                "『變涼快的辦法』！$R;");
                            mask.SetValue(Job2X_03.未獲得暗殺者的內服藥3, false);
                            mask.SetValue(Job2X_03.第三個問題回答正確, true);
                            mask.SetValue(Job2X_03.第四個問題回答錯誤, false);
                            //_4A71 = false;
                            //_4A03 = true;
                            //_4A07 = false;
                            Say(pc, 135, "放心吧！我可以給的提示就是$R;" +
                                "『變涼快的辦法』！$R;");
                            return;
                        }
                        mask.SetValue(Job2X_03.未獲得暗殺者的內服藥3, true);
                        //_4A71 = true;
                        Say(pc, 135, "您的行李太滿了啦$R;" +
                            "我無法給您物品啊$R;");
                        break;
                    case 2:
                        mask.SetValue(Job2X_03.第三個問題回答錯誤, true);
                        //_4A06 = true;
                        Say(pc, 135, "不管是什麽，慢慢來吧！$R;");
                        break;
                    case 3:
                        mask.SetValue(Job2X_03.第三個問題回答錯誤, true);
                        //_4A06 = true;
                        Say(pc, 135, "嗯$R;");
                        break;
                }
                return;
            }
            if (mask.Test(Job2X_03.刺客轉職開始))//_4A00)
            {
                Say(pc, 135, "嗯？暗號？$R;" +
                    "你是什麽意思啊？$R;");
                return;
            }

            Say(pc, 135, "嘿嘿嘿……$R;" +
                "$R進入諾頓王國會不會很困難啊？$R;");
            switch (Select(pc, "想買那個嗎？", "", "買", "不買"))
            {
                case 1:
                    OpenShopBuy(pc, 81);
                    Say(pc, 135, "哎呀！謝謝！$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}

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
                    Say(pc, 135, "放心吧！我可以给的提示就是$R;" +
                        "『变凉快的办法』！$R;");
                    return;
                }
                if (CheckInventory(pc, 10000351, 1))
                {
                    GiveItem(pc, 10000351, 1);
                    Say(pc, 131, "得到1个『暗杀者的秘药3』！$R;");
                    Say(pc, 135, "我可以给的提示就是$R;" +
                        "『变凉快的办法』！$R;");
                    mask.SetValue(Job2X_03.未獲得暗殺者的內服藥3, false);
                    mask.SetValue(Job2X_03.第四個問題回答錯誤, false);
                    //_4A71 = false;
                    //_4A03 = true;
                    //_4A07 = false;
                    Say(pc, 135, "放心吧！我可以给的提示就是$R;" +
                        "『变凉快的办法』！$R;");
                    return;
                }
                mask.SetValue(Job2X_03.未獲得暗殺者的內服藥3, true);
                //_4A71 = true;
                Say(pc, 135, "您的行李太满了啦$R;" +
                    "我无法给您物品啊$R;");
                return;
            }
            if (mask.Test(Job2X_03.第三個問題回答正確))//_4A03)
            {
                if (mask.Test(Job2X_03.第三個問題回答正確) && CountItem(pc, 10000351) >= 1)
                {
                    mask.SetValue(Job2X_03.第四個問題回答錯誤, false);
                    //_4A07 = false;
                    Say(pc, 135, "放心吧！我可以给的提示就是$R;" +
                        "『变凉快的办法』！$R;");
                    return;
                }
                if (CheckInventory(pc, 10000351, 1))
                {
                    GiveItem(pc, 10000351, 1);
                    Say(pc, 131, "得到1个『暗杀者的秘药3』！$R;");
                    Say(pc, 135, "我可以给的提示就是$R;" +
                        "『变凉快的办法』！$R;");
                    mask.SetValue(Job2X_03.未獲得暗殺者的內服藥3, false);
                    mask.SetValue(Job2X_03.第四個問題回答錯誤, false);
                    //_4A71 = false;
                    //_4A03 = true;
                    //_4A07 = false;
                    Say(pc, 135, "放心吧！我可以给的提示就是$R;" +
                        "『变凉快的办法』！$R;");
                    return;
                }
                mask.SetValue(Job2X_03.未獲得暗殺者的內服藥3, true);
                //_4A71 = true;
                Say(pc, 135, "您的行李太满了啦$R;" +
                    "我无法给您物品啊$R;");
                return;
            }
            if (mask.Test(Job2X_03.第三個問題回答錯誤))//_4A06)
            {
                Say(pc, 135, "请重新听过提示然后再过来吧！$R;");
                return;
            }
            if (mask.Test(Job2X_03.未獲得暗殺者的內服藥3))//_4A71)
            {
                if (CheckInventory(pc, 10000351, 1))
                {
                    GiveItem(pc, 10000351, 1);
                    Say(pc, 131, "得到1个『暗杀者的秘药3』！$R;");
                    Say(pc, 135, "我可以给的提示就是$R;" +
                        "『变凉快的办法』！$R;");
                    mask.SetValue(Job2X_03.未獲得暗殺者的內服藥3, false);
                    mask.SetValue(Job2X_03.第三個問題回答正確, true);
                    mask.SetValue(Job2X_03.第四個問題回答錯誤, false);
                    //_4A71 = false;
                    //_4A03 = true;
                    //_4A07 = false;
                    Say(pc, 135, "放心吧！我可以给的提示就是$R;" +
                        "『变凉快的办法』！$R;");
                    return;
                }
                mask.SetValue(Job2X_03.未獲得暗殺者的內服藥3, true);
                //_4A71 = true;
                Say(pc, 135, "您的行李太满了啦$R;" +
                    "我无法给您物品啊$R;");
                return;
            }
            if (mask.Test(Job2X_03.第一個問題回答正確) && mask.Test(Job2X_03.第二個問題回答正確))//_4A02 && _4A01)
            {
                Say(pc, 135, "嘿嘿嘿……终于到我了吗？$R;" +
                    "$P来吧！请随便，不用客气！$R;" +
                    "最近过的怎么样啊？$R;");
                switch (Select(pc, "想回答什么？", "", "就那样", "忙啊", "微妙啊"))
                {
                    case 1:
                        Say(pc, 135, "好，就是YES的意思啊$R;" +
                            "$R就拿这个走吧$R;");
                        if (CheckInventory(pc, 10000351, 1))
                        {
                            GiveItem(pc, 10000351, 1);
                            Say(pc, 131, "得到1个『暗杀者的秘药3』！$R;");
                            Say(pc, 135, "我可以给的提示就是$R;" +
                                "『变凉快的办法』！$R;");
                            mask.SetValue(Job2X_03.未獲得暗殺者的內服藥3, false);
                            mask.SetValue(Job2X_03.第三個問題回答正確, true);
                            mask.SetValue(Job2X_03.第四個問題回答錯誤, false);
                            //_4A71 = false;
                            //_4A03 = true;
                            //_4A07 = false;
                            Say(pc, 135, "放心吧！我可以给的提示就是$R;" +
                                "『变凉快的办法』！$R;");
                            return;
                        }
                        mask.SetValue(Job2X_03.未獲得暗殺者的內服藥3, true);
                        //_4A71 = true;
                        Say(pc, 135, "您的行李太满了啦$R;" +
                            "我无法给您物品啊$R;");
                        break;
                    case 2:
                        mask.SetValue(Job2X_03.第三個問題回答錯誤, true);
                        //_4A06 = true;
                        Say(pc, 135, "不管是什么，慢慢來吧！$R;");
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
                Say(pc, 135, "嗯？暗号？$R;" +
                    "你是什么意思啊？$R;");
                return;
            }

            Say(pc, 135, "嘿嘿嘿……$R;" +
                "$R进入诺森王国会不会很困难啊？$R;");
            switch (Select(pc, "想买那个吗？", "", "买", "不买"))
            {
                case 1:
                    OpenShopBuy(pc, 81);
                    Say(pc, 135, "哎呀！谢谢！$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}

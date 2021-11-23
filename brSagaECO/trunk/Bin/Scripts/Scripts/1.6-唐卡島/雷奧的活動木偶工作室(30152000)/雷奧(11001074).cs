using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30152000
{
    public class S11001074 : Event
    {
        public S11001074()
        {
            this.EventID = 11001074;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Tangka> Tangka_mask = pc.CMask["Tangka"];

            if (CountItem(pc, 10016350) >= 1)
            {
                if (!Tangka_mask.Test(Tangka.雷奧第一次對話))
                {
                    Tangka_mask.SetValue(Tangka.雷奧第一次對話, true);
                    //_2b43 = true;
                    Say(pc, 131, "哇，這是『神奇木材』呀？$R;" +
                        "在哪發現的?$R;" +
                        "這是活動木偶皮諾的材料。$R;" +
                        "不是用這個木材作成的玩偶$R;" +
                        "$R就不能注入皮諾的靈魂。$R;" +
                        "本以為唐卡只有幾個皮諾，$R;" +
                        "做新的皮諾是不可能的…$R;" +
                        "但對住在唐卡的技術者來說$R;" +
                        "做皮諾是他們最大的驕傲呀。$R;" +
                        "求求您，能不能給我機會$R;" +
                        "幫我做做『活動木偶皮諾』呀。$R;");
                }
                switch (Select(pc, "怎麼辦呢？", "", "什麼也不做。", "做活動木偶皮諾"))
                {
                    case 1:
                        Say(pc, 131, "是嗎？太遺憾了…$R;");
                        break;
                    case 2:
                        Say(pc, 131, "真的嗎？謝謝！！！$R;" +
                            "$P除了這個木材還需要$R;" +
                            "『洋鐵部件』$R;" +
                            "『螺絲』$R;" +
                            "『釘子』$R;" +
                            "『原木』$R;" +
                            "$P『核果』$R;" +
                            "『伏特』$R;" +
                            "『布』$R;" +
                            "『線』$R;" +
                            "『針』$R;" +
                            "這些都是必要的呀！！$R;");
                        if (CountItem(pc, 10018000) >= 1 &&
                            CountItem(pc, 10028300) >= 1 &&
                            CountItem(pc, 10023300) >= 1 &&
                            CountItem(pc, 10023900) >= 1 &&
                            CountItem(pc, 10030500) >= 1 &&
                            CountItem(pc, 10038400) >= 1 &&
                            CountItem(pc, 10021300) >= 1 &&
                            CountItem(pc, 10019800) >= 1 &&
                            CountItem(pc, 10019700) >= 1)
                        {
                            Say(pc, 131, "看樣子材料也已經收集好了$R;");
                            switch (Select(pc, "怎麼辦呢？", "", "什麼也不做", "交出材料"))
                            {
                                case 1:
                                    Say(pc, 131, "是嗎？太遺憾了…$R;");
                                    break;
                                case 2:
                                    Say(pc, 131, "挺好的，說吧！$R;" +
                                        "$R該您幫忙了！$R;");
                                    Say(pc, 131, "總算輪到我了。$R;" +
                                        "我的煙花早就準備好了$R;");
                                    Fade(pc, FadeType.Out, FadeEffect.Black);
                                    Wait(pc, 2000);
                                    Fade(pc, FadeType.In, FadeEffect.Black);
                                    TakeItem(pc, 10018000, 1);
                                    TakeItem(pc, 10028300, 1);
                                    TakeItem(pc, 10023300, 1);
                                    TakeItem(pc, 10023900, 1);
                                    TakeItem(pc, 10030500, 1);
                                    TakeItem(pc, 10038400, 1);
                                    TakeItem(pc, 10021300, 1);
                                    TakeItem(pc, 10019800, 1);
                                    TakeItem(pc, 10019700, 1);
                                    TakeItem(pc, 10016350, 1);
                                    GiveItem(pc, 10027000, 1);
                                    Say(pc, 131, "好了，好了，都好了$R;" +
                                        "沒想到我也能製作出活動木偶皮諾…$R;" +
                                        "$R謝謝！！$R;" +
                                        "真不知該怎麼感謝您呀$R;");
                                    Say(pc, 131, "得到了『活動木偶皮諾』$R;");
                                    break;
                            }
                            return;
                        }
                        Say(pc, 131, "都收集到了？$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "不是這個呀。$R;" +
                "錯了，也不是這個$R;" +
                "不可能是這個樣子，$R;" +
                "$P做出這種活動木偶，$R;" +
                "我哪像專業呀，簡直是初學者嘛。$R;" +
                "$R哇，我完了，$R;" +
                "$P對不起，今天的營業到此結束$R;" +
                "$R這條街有叫莉塔的女人$R;" +
                "是活動木偶工匠，去那裡吧！$R;" +
                "$P那個女人的作品比我做的更幽雅，$R更漂亮唷。$R;" +
                "$R哇，我現在沒救了。$R;");
        }
    }
}
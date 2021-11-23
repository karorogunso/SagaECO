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
                    Say(pc, 131, "哇，这是『神奇木材』呀？$R;" +
                        "在哪发现的?$R;" +
                        "这是活动木偶皮诺的材料。$R;" +
                        "不是用这个木材作成的玩偶$R;" +
                        "$R就不能注入皮诺的灵魂。$R;" +
                        "本以为唐卡只有几个皮诺，$R;" +
                        "做新的皮诺是不可能的…$R;" +
                        "但对住在唐卡的技术者来说$R;" +
                        "做皮诺是他们最大的骄傲呀。$R;" +
                        "求求您，能不能给我机会$R;" +
                        "帮我做做『活动木偶皮诺』呀。$R;");
                }
                switch (Select(pc, "怎么办呢？", "", "什么也不做。", "做活动木偶皮诺"))
                {
                    case 1:
                        Say(pc, 131, "是吗？太遗憾了…$R;");
                        break;
                    case 2:
                        Say(pc, 131, "真的吗？谢谢！！！$R;" +
                            "$P除了这个木材还需要$R;" +
                            "『洋铁部件』$R;" +
                            "『螺丝』$R;" +
                            "『钉子』$R;" +
                            "『原木』$R;" +
                            "$P『螺母』$R;" +
                            "『螺丝钉』$R;" +
                            "『布』$R;" +
                            "『线』$R;" +
                            "『针』$R;" +
                            "这些都是必要的呀！！$R;");
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
                            Say(pc, 131, "看样子材料也已经收集好了$R;");
                            switch (Select(pc, "怎么办呢？", "", "什么也不做", "交出材料"))
                            {
                                case 1:
                                    Say(pc, 131, "是吗？太遗憾了…$R;");
                                    break;
                                case 2:
                                    Say(pc, 131, "挺好的，说吧！$R;" +
                                        "$R该您帮忙了！$R;");
                                    Say(pc, 131, "总算轮到我了。$R;" +
                                        "我的烟花早就准备好了$R;");
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
                                        "没想到我也能制作出活动木偶皮诺…$R;" +
                                        "$R谢谢！！$R;" +
                                        "真不知该怎么感谢您呀$R;");
                                    Say(pc, 131, "得到了『活动木偶皮诺』$R;");
                                    break;
                            }
                            return;
                        }
                        Say(pc, 131, "都收集到了？$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "不是这个呀。$R;" +
                "错了，也不是这个$R;" +
                "不可能是这个样子，$R;" +
                "$P做出这种活动木偶，$R;" +
                "我哪像专业呀，简直是初学者嘛。$R;" +
                "$R哇，我完了，$R;" +
                "$P对不起，今天的营业到此结束$R;" +
                "$R这条街有叫莉塔的女人$R;" +
                "是活动木偶工匠，去那里吧！$R;" +
                "$P那个女人的作品比我做的更幽雅，$R更漂亮哦。$R;" +
                "$R哇，我现在没救了。$R;");
        }
    }
}
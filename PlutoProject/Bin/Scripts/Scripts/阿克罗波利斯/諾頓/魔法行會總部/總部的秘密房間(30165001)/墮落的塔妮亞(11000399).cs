using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30165001
{
    public class S11000399 : Event
    {
        public S11000399()
        {
            this.EventID = 11000399;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<FallenTitantia> FallenTitantia_mask = pc.CMask["FallenTitantia"];
            if (FallenTitantia_mask.Test(FallenTitantia.大导师告知可以回来了))
            {
                if (!FallenTitantia_mask.Test(FallenTitantia.任务完成))
                {
                    FallenTitantia_mask.SetValue(FallenTitantia.任务完成, true); 
                    //_4a63 = true;
                    Say(pc, 11000399, 131, "上次多谢了$R;" +
                        "$R跟大导师商量后，$R;" +
                        "决定暂时待在总部。$R;" +
                        "$P现在跟$R;" +
                        "研究光属性魔法的集团$R;" +
                        "做一些事情$R;" +
                        "$R不过有些事情不太顺利呀$R;" +
                        "$P对了$R;" +
                        "$R这是泰达尼亚服装，$R;" +
                        "想不想买呢？$R;" +
                        "$P对我来说已经没有用了，$R;" +
                        "卖了它，可以得到暂时的生活费阿$R;");
                }
                Say(pc, 11000399, 131, "有没有喜欢的呢？$R;");
                if (pc.Job > (PC_JOB)40 && pc.Job < (PC_JOB)51)
                {
                    OpenShopBuy(pc, 120);
                    return;
                }
                if (pc.Job > (PC_JOB)50 && pc.Job < (PC_JOB)61)
                {
                    OpenShopBuy(pc, 121);
                    return;
                }
                if (pc.Job > (PC_JOB)60 && pc.Job < (PC_JOB)71)
                {
                    OpenShopBuy(pc, 122);
                    return;
                }
                if (pc.Job > (PC_JOB)70 && pc.Job < (PC_JOB)81)
                {
                    OpenShopBuy(pc, 123);
                    return;
                }
                Say(pc, 11000399, 131, "没有什么要卖给您的东西$R;");
                return;
            }
            if (FallenTitantia_mask.Test(FallenTitantia.给花))
            {
                Say(pc, 11000399, 131, "…$R;" +
                    "$P对不起$R;" +
                    "哭了一会儿，心里好受多了$R;" +
                    "$R可以的话，下次再来吧$R;" +
                    "看到您我就有精神了$R;");
                return;
            }
            if (CountItem(pc, 10005300) >= 1 &&
                FallenTitantia_mask.Test(FallenTitantia.告知需要花))
            {
                FallenTitantia_mask.SetValue(FallenTitantia.给花, true);
                //_4a62 = true;
                TakeItem(pc, 10005300, 1);
                Say(pc, 11000399, 131, "真漂亮阿$R;" +
                    "$R像开在泰达尼亚的花$R;" +
                    "$P…$R;" +
                    "$P谢谢$R;" +
                    "这是在安慰我的吧$R;" +
                    "$P我爱了不该爱的人$R;" +
                    "被泰达尼亚赶出来了$R;" +
                    "$R即使这样我也要勇敢的爱$R;" +
                    "违背感情是不可能的$R;" +
                    "$P被流放的那天$R;" +
                    "就下定了决心，决不后悔！$R;" +
                    "$P我太懦弱了$R;" +
                    "离开的时间越长，$R;" +
                    "越思念故乡阿…$R;" +
                    "$P…$R;" +
                    "$P想回到泰达尼亚$R;" +
                    "呜…呜呜……$R;");
                Wait(pc, 1000);
                Fade(pc, FadeType.Out, FadeEffect.Black);
                //FADE OUT BLACK
                Wait(pc, 4000);
                Fade(pc, FadeType.In, FadeEffect.Black);
                //FADE IN
                Wait(pc, 1000);
                Say(pc, 11000399, 131, "…$R;" +
                    "$P对不起$R;" +
                    "哭了一会儿，心里好受多了$R;" +
                    "$R可以的话，下次再来吧$R;" +
                    "看到您我就有精神了$R;");
                return;
            }
            Say(pc, 11000399, 131, "…$R;" +
                "$P我是从泰达尼亚流放到这里来的$R;" +
                "犯了不该犯的禁忌呀$R;" +
                "$P我堕落了$R;" +
                "请不要靠近我吧$R;");
            Warp(pc, 30163000, 12, 4);
        }
    }
}
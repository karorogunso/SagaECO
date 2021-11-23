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
                    Say(pc, 11000399, 131, "上次多謝了$R;" +
                        "$R跟大導師商量後，$R;" +
                        "決定暫時待在總部。$R;" +
                        "$P現在跟$R;" +
                        "研究光屬性魔法的集團$R;" +
                        "做一些事情$R;" +
                        "$R不過有些事情不太順利呀$R;" +
                        "$P對了$R;" +
                        "$R這是塔妮亞服裝，$R;" +
                        "想不想買呢？$R;" +
                        "$P對我來說已經沒有用了，$R;" +
                        "賣了它，可以得到暫時的生活費阿$R;");
                }
                Say(pc, 11000399, 131, "有沒有喜歡的呢？$R;");
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
                Say(pc, 11000399, 131, "沒有什麼要賣給您的東西$R;");
                return;
            }
            if (FallenTitantia_mask.Test(FallenTitantia.给花))
            {
                Say(pc, 11000399, 131, "…$R;" +
                    "$P對不起$R;" +
                    "哭了一會兒，心裡好受多了$R;" +
                    "$R可以的話，下次再來吧$R;" +
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
                    "$R像開在塔妮亞的花$R;" +
                    "$P…$R;" +
                    "$P謝謝$R;" +
                    "這是在安慰我的吧$R;" +
                    "$P我愛了不該愛的人$R;" +
                    "被塔妮亞趕出來了$R;" +
                    "$R即使這樣我也要勇敢的愛$R;" +
                    "違背感情是不可能的$R;" +
                    "$P被流放的那天$R;" +
                    "就下定了決心，決不後悔！$R;" +
                    "$P我太懦弱了$R;" +
                    "離開的時間越長，$R;" +
                    "越思念故鄉阿…$R;" +
                    "$P…$R;" +
                    "$P想回到塔妮亞$R;" +
                    "嗚…嗚嗚……$R;");
                Wait(pc, 1000);
                Fade(pc, FadeType.Out, FadeEffect.Black);
                //FADE OUT BLACK
                Wait(pc, 4000);
                Fade(pc, FadeType.In, FadeEffect.Black);
                //FADE IN
                Wait(pc, 1000);
                Say(pc, 11000399, 131, "…$R;" +
                    "$P對不起$R;" +
                    "哭了一會兒，心裡好受多了$R;" +
                    "$R可以的話，下次再來吧$R;" +
                    "看到您我就有精神了$R;");
                return;
            }
            Say(pc, 11000399, 131, "…$R;" +
                "$P我是從塔妮亞流放到這裡來的$R;" +
                "犯了不該犯的禁忌呀$R;" +
                "$P我墮落了$R;" +
                "請不要靠近我吧$R;");
            Warp(pc, 30163000, 12, 4);
        }
    }
}
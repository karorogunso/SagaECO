using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30162000
{
    public class S11000223 : Event
    {
        public S11000223()
        {
            this.EventID = 11000223;

            this.leastQuestPoint = 2;

            this.gotNormalQuest = "把垃圾扔到垃圾桶裡$R;" +
                                  "系統會自動點算數量的$R;" +
                                  "$R道具太重，$R;" +
                                  "不能一次扔掉的話，就分幾次好了$R;" +
                                  "那就拜託了$R;";

            this.questCompleted = "辛苦了$R;" +
                                  "$R領取報酬吧$R;";

            this.questCanceled = "做事不能半途而廢呀$R;";

            this.questFailed = "任務失敗了$R;";

            this.notEnoughQuestPoint = "這個任務$R;" +
                                       "消耗任務點數『2』$R;" +
                                       "$R下次再來吧$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 10043303) > 0)
            {
                万圣节(pc);
                return;
            }
            if (pc.Gender == PC_GENDER.FEMALE)
            {
                Say(pc, 131, "為什麼女人會在這裡！$R;" +
                    "滾出去！$R;");
                Warp(pc, 10065000, 32, 6);
                return;
            } 
            Say(pc, 131, "我們守護著諾頓王國！$R;" +
                 "不能輸給女人呀！$R;");
            switch (Select(pc, "怎麼辦呢？", "", "買王國軍專用道具", "清潔計劃", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 88);
                    Say(pc, 131, "不要賣給女人阿$R;");
                    break;
                case 2:
                    Say(pc, 131, "這個任務$R;" +
                        "消耗任務點數『2』$R;");
                    HandleQuest(pc, 46);
                    break;
            }
        }
        void 万圣节(ActorPC pc)
        {
           
            TakeItem(pc, 10043303, 1);
            GiveItem(pc, 10013200, 1);
            Say(pc, 131, "なっ、なぜ女がここにいる！？$R;" +
            "出て行きなさい！$R;" +
            "$Pえっ、これを俺に？$R;" +
            "$R本当ですか！$R;" +
            "差し入れなんて、照れるなぁ。$R;" +
            "$Pで、どうしたんだい？$R;" +
            "$R悩みがあるなら、お兄さんが$R;" +
            "なんでも聞いちゃうぞ♪$R;" +
            "$P……。$R;" +
            "$P破結界石？$R;" +
            "んー、本当は一般人にあげちゃ$R;" +
            "ダメなんだけど、君ならいっか！$R;" +
            "$Rはい！$R;", "男僧兵");
            PlaySound(pc, 2040, false, 100, 50);
            Say(pc, 0, 131, "『破結界石』を手に入れた！$R;", " ");
            Say(pc, 131, "さ、一応ここは$R;" +
            "女性立ち入り禁止だから$R;" +
            "帰りなさい。$R;", "男僧兵");
            Warp(pc, 10065000, 32, 6);
        }
    }
}

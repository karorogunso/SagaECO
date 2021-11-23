using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20080012
{
    public class S11000904 : Event
    {
        public S11000904()
        {
            this.EventID = 11000904;

            this.notEnoughQuestPoint = "調查順利進行$R;" +
    "現在沒什麼要幫忙的$R;" +
    "以後再來吧$R;";
            this.leastQuestPoint = 3;
            this.questFailed = "失敗了嗎？$R;" +
    "不要太介意喔$R;" +
    "安全回來，已經是最大的成果了$R;";
            this.alreadyHasQuest = "不好意思$R;" +
    "任務還沒結束唷!$R;";
            this.gotNormalQuest = "這裡給『隊伍』$R;" +
    "介紹擊退任務的喔$R;" +
    "$P隊伍裡，只要有一人接受相同$R;" +
    "任務的話$R;" +
    "就可以共享擊退魔物的數量$R;" +
    "$R齊心協力!加油吧！$R;" +
    "$R那…多多指教了!$R;";
            this.questCompleted = "辛苦了$R;" +
    "好像成功了$R;" +
    "$R請收下報酬吧$R;";
            this.questCanceled = "是嗎？$R;" +
    "沒辦法了$R;";
            this.questTooEasy = "覺得危險就不要勉強$R;" +
    "請馬上撤退明白了嗎？$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<INSD> mask = pc.CMask["INSD"];

            if (pc.Fame < 10)
            {
                Say(pc, 131, "哦?$R;" +
                    "對您來說在這裡調查$R;" +
                    "不是有點困難嗎？$R;" +
                    "$R想調查的話$R;" +
                    "請累積多一點經驗後$R;" +
                    "再來吧！$R;");
                return;
            }

            if (!mask.Test(INSD.東軍團員第一次對話))//_0c67)
            {
                mask.SetValue(INSD.東軍團員第一次對話, true);
                //_0c67 = true;
                Say(pc, 131, "謝謝您的幫忙$R;" +
                    "$R這裡是地下遺跡的入口$R;" +
                    "遺跡的東、西、北邊都有入口$R;" +
                    "離開時請使用南方入口吧$R;" +
                    "$P這次需要您幫忙的是$R;" +
                    "擊退妨礙遺跡調查工作的魔物$R;" +
                    "是擊退任務吧$R;");
            }

            else
            {
                Say(pc, 131, "因為裡面的魔物$R;" +
                    "調查工作正面臨困難阿$R;" +
                    "$R到底從哪兒冒出來的呢？$R;");
                Say(pc, 131, "請您幫幫我們吧$R;");
            }

            switch (Select(pc, "做什麼呢？", "", "任務服務台", "什麼也不做"))
            {
                case 1:
                    Say(pc, 131, "消耗任務點數「3」$R;");

                    HandleQuest(pc, 38);
                    break;
                case 2:
                    break;
            }
        }
    }
}
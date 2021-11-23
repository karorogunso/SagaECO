using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30100000
{
    public class S11000766 : Event
    {
        public S11000766()
        {
            this.EventID = 11000766;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*
            if (pc.Account.GMLevel == 1)
            {
                Call(EVT1100076625);
                return;
            }//*
            if (!_0c54)
            {
                _0c54 = true;
                Say(pc, 131, "我是從帕斯特來的阿$R;" +
                    "希望得到阿高普路斯軍的幫助$R;" +
                    "$P有目擊者報稱，$R在東部森林發現大型魔物阿$R;" +
                    "目擊報告顯示$R;" +
                    "古城周圍的魔物數量$R;" +
                    "有逐漸增加的趨勢呀$R;" +
                    "$R我軍為了打敗它們，打算突襲$R;" +
                    "$R懇請各位鼎力支持唷$R;");
            }
            else
            {
                Say(pc, 131, "現在，綠色防備軍$R;" +
                    "正計劃掃蕩帕斯特的$R;" +
                    "東部森林和南部古城$R;" +
                    "周圍的魔物唷$R;" +
                    "$R請各位協力相助吧！$R;");
            }//*/
            //EVT1100076601
            Say(pc, 131, "參加作戰嗎？$R;" +
                "$R『軍團適用』的任務$R;" +
                "只有軍團成員才能參加喔$R;" +
                "請注意，不是相同隊伍$R;" +
                "但所屬同一個軍團也可以唷$R;" +
                "$P『隊伍適用』的任務$R;" +
                "只有隊伍成員才能參加阿$R;" +
                "請多加注意唷$R;");
            //EVT1100076602
            switch (Select(pc, "做什麼呢？", "", "任務服務台", "什麼也不做"))
            {
                case 1:
                    //GOTO EVT1100076603;
                    break;
                case 2:
                    break;
            }
        }
    }
}
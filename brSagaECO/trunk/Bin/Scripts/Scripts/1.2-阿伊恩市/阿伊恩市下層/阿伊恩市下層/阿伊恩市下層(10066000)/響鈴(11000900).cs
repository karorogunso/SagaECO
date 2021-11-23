using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10066000
{
    public class S11000900 : Event
    {
        public S11000900()
        {
            this.EventID = 11000900;

            this.leastQuestPoint = 3;
            this.notEnoughQuestPoint = "現在不需要實驗對象，$R;" +
    "下次再來吧$R;";
            this.questFailed = "失敗了？$R;" +
    "$R原來您經不起意外阿$R;" +
    "可以保住小命，$R已經是不幸中之大幸了$R;";
            this.questTooHard = "以您現在的實力太勉強了。$R;" +
    "就算不執行任務$R;" +
    "如果是隊員的話，也可以去看看$R;" +
    "去看看嗎？$R;";
            this.questTooEasy = "您看起來很強啊，$R;" +
    "只要有勇氣$R;" +
    "應該不會倒下吧$R;";
            this.gotNormalQuest = "這裡有人數限制$R;" +
    "最多可以有8人進去$R;" +
    "$R隊伍中如果有人接受一樣任務的話$R;" +
    "可以共享擊退魔物的數量唷$R;" +
    "$P超過限定時間$R;" +
    "就會自動回到這裡$R;" +
    "所以不要擔心啊$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<AYEFlags> mask = pc.CMask["AYEFlags"];
            if (!mask.Test(AYEFlags.響鈴第一次對話))//_0c56)
            {
                mask.SetValue(AYEFlags.響鈴第一次對話, true);
                //_0c56 = true;
                Say(pc, 131, "我真的是天才啊$R;" +
                    "能夠造這樣的東西，很棒吧？$R;" +
                    "$R哦？$R;" +
                    "正好！在那邊的您啊！$R;" +
                    "您要不要成為實驗對象啊？$R;" +
                    "$P什麼實驗？$R;" +
                    "只是暫時去趟$R;" +
                    "假想空間的實驗。放心吧！$R;" +
                    "$R怎麼樣？挺有趣吧？$R;");
            }
            else
            {
                Say(pc, 131, "呵呵，您太厲害了$R;" +
                    "$R試驗對象總是受歡迎的阿$R;");
            }
            Say(pc, 131, "怎麼樣?要做一次嗎？$R;");

            switch (Select(pc, "做什麼呢？", "", "任務服務台", "什麼也不做"))
            {
                case 1:
                    //HandleQuest(pc, 37);
                    break;
                case 2:
                    break;
            }
        }
    }
}
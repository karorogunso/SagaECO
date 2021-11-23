using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001021 : Event
    {
        public S11001021()
        {
            this.EventID = 11001021;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 10000305) >= 1)
            {
                Say(pc, 131, "啊，那個是不是$R;" +
                    "『健康營養飲料』呀？$R;" +
                    "$R不好意思，能不能給我一個呢？$R;");
                switch (Select(pc, "怎麼辦呢？", "", "不給", "好阿"))
                {
                    case 1:
                        break;
                    case 2:
                        if (CheckInventory(pc, 10018301, 1))
                        {
                            TakeItem(pc, 10000305, 1);
                            GiveItem(pc, 10018301, 1);
                            Say(pc, 131, "謝謝$R;" +
                                "這是表示感謝的禮物，$R;" +
                                "請您收下唷$R;");
                            Say(pc, 0, 131, "嚯嚯！$R;");
                            Say(pc, 131, "您不知道嗎？$R;" +
                                "我的翅膀能變成金。$R;" +
                                "$R如果不會使用可以問別人喔。$R;");
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 0, 131, "得到了『火焰的翅膀』$R;");
                            return;
                        }
                        Say(pc, 131, "謝謝$R;" +
                            "想向您道謝，$R;" +
                            "不過行李看起來太多了。$R;" +
                            "不好意思，能不能減少一點行李後$R;" +
                            "再來找我呢？$R;");
                        break;
                }
                return;
            }
            if (pc.Marionette != null)
            {
                Say(pc, 131, "這個身體曾經帶著我周遊列國，$R;" +
                    "可是時光飛逝啊，$R;" +
                    "$R能不能賞我『健康營養飲料』呢？$R;");
                return;
            }
            Say(pc, 131, "現在還剩一點，$R;" +
                "加油唷$R;");
        }
    }
}
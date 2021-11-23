using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30152000
{
    public class S11001076 : Event
    {
        public S11001076()
        {
            this.EventID = 11001076;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 10022309) >= 10 &&
                CountItem(pc, 10001400) >= 1 &&
                CountItem(pc, 10014750) >= 1 &&
                CountItem(pc, 10014650) >= 20)
            {
                Say(pc, 131, "不好意思能不能給我$R;" +
                    "『原油』10個$R;" +
                    "『特效藥』1個$R;" +
                    "『打火石』1個$R;" +
                    "『堅硬鵝卵石』20個？$R;");
                switch (Select(pc, "怎麼辦呢？", "", "不給", "給他吧"))
                {
                    case 1:
                        break;
                    case 2:
                        if (CheckInventory(pc, 10035600, 1))
                        {
                            TakeItem(pc, 10022309, 10);
                            TakeItem(pc, 10001400, 1);
                            TakeItem(pc, 10014750, 1);
                            TakeItem(pc, 10014650, 20);
                            GiveItem(pc, 10035600, 1);
                            Say(pc, 131, "嗯，不能白拿人家的，$R有點不好意思，$R;");
                            Say(pc, 131, "嘩！！$R;");
                            Say(pc, 131, "給您這個吧，$R;" +
                                "不，不要用那種表情。$R;" +
                                "會長馬上出來，沒關係的。$R;");
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 0, 131, "得到了『毒蜥蜴尾巴』$R;");
                            Say(pc, 131, "先放入堅硬鵝卵石，$R;" +
                                "然後上面倒入原油，$R;" +
                                "再用打火石點火，等一會兒，$R;" +
                                "$P等火變弱了，在石頭上一邊灑特效藥，$R一邊走在石頭上，$R;" +
                                "$P這樣就像我們的聖地$R;" +
                                "阿伊恩薩烏斯的鐵火山$R熊熊燃燒起來的感覺。$R;" +
                                "$R那種溫暖的感覺…$R;" +
                                "$P要您回去的時候訂單就完成喔。$R;" +
                                "$R哇，真讓人期待呀$R;");
                            return;
                        }
                        Say(pc, 131, "嗯，不能白拿人家的，$R有點不好意思，$R;" +
                            "$R我會有所表示的，$R;" +
                            "不過減輕行李後，再來吧。$R;");
                        break;
                }
                return;
            }
            if (pc.Marionette != null)
            {
                Say(pc, 131, "看樣子那小子墜入愛河了。$R;" +
                    "對象可能是住在這裡$R;" +
                    "同樣是活動木偶工匠…$R;" +
                    "到現在他只知道幹活，$R我還以為不懂得談戀愛呢…$R;" +
                    "該我出馬了。$R;" +
                    "『特效藥』1個$R;" +
                    "『打火石』1個$R;" +
                    "『堅硬鵝卵石』20個$R;" +
                    "愛之注文。$R;" +
                    "$P要是有的話，分給我一點吧？$R;");
                return;
            }
            Say(pc, 131, "有什麼事呢？$R;");
        }
    }
}
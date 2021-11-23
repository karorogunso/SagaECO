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
                Say(pc, 131, "不好意思能不能给我$R;" +
                    "『原油』10个$R;" +
                    "『爆炸药』1个$R;" +
                    "『燧石』1个$R;" +
                    "『坚硬的石块』20个？$R;");
                switch (Select(pc, "怎么办呢？", "", "不给", "给他吧"))
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
                            Say(pc, 131, "嗯，不能白拿人家的，$R有点不好意思，$R;");
                            Say(pc, 131, "哗！！$R;");
                            Say(pc, 131, "给您这个吧，$R;" +
                                "不，不要用那种表情。$R;" +
                                "会长马上出来，没关系的。$R;");
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 0, 131, "得到了『毒蜥蜴的尾巴』$R;");
                            Say(pc, 131, "先放入坚硬的石块，$R;" +
                                "然后上面倒入原油，$R;" +
                                "再用燧石点火，等一会儿，$R;" +
                                "$P等火变弱了，在石头上一边洒爆炸药，$R一边走在石头上，$R;" +
                                "$P这样就像我们的圣地$R;" +
                                "艾恩萨乌斯的铁火山$R熊熊燃烧起来的感觉。$R;" +
                                "$R那种温暖的感觉…$R;" +
                                "$P要您回去的时候订单就完成喔。$R;" +
                                "$R哇，真让人期待呀$R;");
                            return;
                        }
                        Say(pc, 131, "嗯，不能白拿人家的，$R有点不好意思，$R;" +
                            "$R我会有所表示的，$R;" +
                            "不过减轻行李后，再来吧。$R;");
                        break;
                }
                return;
            }
            if (pc.Marionette != null)
            {
                Say(pc, 131, "看样子那小子坠入爱河了。$R;" +
                    "对象可能是住在这里$R;" +
                    "同样是活动木偶工匠…$R;" +
                    "到现在他只知道干活，$R我还以为不懂得谈恋爱呢…$R;" +
                    "该我出马了。$R;" +
                    "『爆炸药』1个$R;" +
                    "『燧石』1个$R;" +
                    "『坚硬的石块』20个$R;" +
                    "爱之注文。$R;" +
                    "$P要是有的话，分给我一点吧？$R;");
                return;
            }
            Say(pc, 131, "有什么事呢？$R;");
        }
    }
}
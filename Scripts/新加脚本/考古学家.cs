
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaDB.Actor;
using SagaMap.Mob;
using SagaDB.Mob;
using SagaMap.ActorEventHandlers;
namespace Exploration
{
    public class S11003588 : Event
    {
        public S11003588()
        {
            this.EventID = 11003588;
        }
        public override void OnEvent(ActorPC pc)
        {
            //ChangeMessageBox(pc);
            if (pc.AStr["通天塔西瓜"] != DateTime.Now.ToString("yyyy-MM-dd"))
            {
                Say(pc, 0, "口好渴啊…这种天气，要是有西瓜吃就好了。", "考古学家");
                return;
            }

            if (pc.AStr["通天塔西瓜"] == DateTime.Now.ToString("yyyy-MM-dd"))
            {
                switch(pc.CInt["通天塔考古学家技能点任务"])
                {
                    case 0:
                        Say(pc, 0, "啊，你是刚才给我们西瓜的冒险者。$R这年头像你这样热心的冒险家可不多见了啊。", "考古学家");
                        Say(pc, 0, "虽然刚才也提到了，重新介绍一下，我是克里斯，$R这位是我的助手，莉可。", "考古学家·克里斯");
                        Say(pc, 11003589, 159, "你好~！", "助手·莉可");
                        Say(pc, 0, "我们在探查这座塔周围的矿石，寻找古代的矿石「鬼神铁」，$R要是您对此也有兴趣的话，可以击打周围的矿石，获取「含金属的石头」$R然后将它交给我的助手进行鉴定。", "考古学家·克里斯");
                        Say(pc, 0, "如果您有幸获得「鬼神铁」，并愿意将它交给我的话。$R我会给你一份特别的礼物的。", "考古学家·克里斯");
                        pc.CInt["通天塔考古学家技能点任务"] = 1;
                        break;
                    case 1:
                        if ((CountItem(pc, 10015706) == 0))
                        {
                            Say(pc, 0, "「鬼神铁」是很稀有的……不是那么简单就能获得的……$R只能一个一个地鉴定「含金属的石头」……", "考古学家·克里斯");
                            Say(pc, 0, "要是没有耐心，可干不好考古的活啊。", "考古学家·克里斯");
                            return;
                        }
                        if ((CountItem(pc, 10015706) > 0) && pc.CInt["通天塔考古学家技能点获得"] == 0)
                        {
                            Say(pc, 0, "这……这是，传说中的「鬼神铁」？", "考古学家·克里斯");
                            Say(pc, 0, "你的运气可真好啊，这可是传说中百里无一的稀有矿石。", "考古学家·克里斯");
                            Say(pc, 0, "那么，你愿意把它交给我吗？如果不愿意，我也不会强求你的。", "考古学家·克里斯");
                            switch (Select(pc, "把「鬼神铁」交出去吗？", "", "给你", "自己留着"))
                            {
                                case 1:
                                    PlaySound(pc, 4008, false, 100, 50);
                                    TakeItem(pc, 10015706, 1);
                                    Say(pc, 0, "真的愿意给我吗？太感激了。$R那么……这是我的一点心意，请收好。", "考古学家·克里斯");
                                    pc.SkillPoint3 += 1;
                                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendPlayerInfo();
                                    ShowEffect(pc, 4131);
                                    pc.CInt["通天塔考古学家技能点任务"] = 2;
                                    pc.CInt["通天塔考古学家技能点获得"] = 1;
                                    Wait(pc, 1000);
                                    Say(pc, 0, "获得了1点技能点", " ");
                                    return;
                                case 2:
                                    Say(pc, 0, "嗯……毕竟这是你发现的东西，但你要是改变心意了可以随时回来找我。", "考古学家·克里斯");
                                    return;
                            }
                        }
                        break;
                    default:
                        Say(pc, 0, "塔内的守护者实在是太强力了，所以我们只能在塔的周围进行研究。$R是在是非常遗憾的一件事。", "艾力克");
                        break;
                }
            }
        }
    }
}

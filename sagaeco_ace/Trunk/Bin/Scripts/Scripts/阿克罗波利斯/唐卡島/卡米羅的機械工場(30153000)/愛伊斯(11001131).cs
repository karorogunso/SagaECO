using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30153000
{
    public class S11001131 : Event
    {
        public S11001131()
        {
            this.EventID = 11001131;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 10013350) >= 5 && CountItem(pc, 10009107) >= 5 && CountItem(pc, 10011600) >= 5 && CountItem(pc, 10014500) >= 5)
            {
                Say(pc, 255, "能不能把您的$R;" +
                    "『天青石碎片』5个$R;" +
                    "『月之碎片』5个$R;" +
                    "『心之碎片』5个$R;" +
                    "『水晶的碎片』5个$R;" +
                    "$R给我呢？$R;");
                switch (Select(pc, "怎么做呀？", "", "不给", "给他吧"))
                {
                    case 1:
                        break;
                    case 2:
                        if (CheckInventory(pc, 10011000, 1))
                        {
                            TakeItem(pc, 10013350, 5);
                            TakeItem(pc, 10009107, 5);
                            TakeItem(pc, 10011600, 5);
                            TakeItem(pc, 10014500, 5);
                            GiveItem(pc, 10011000, 1);
                            Say(pc, 255, "谢谢！$R;" +
                                "$R报答的东西虽小，$R请收下『奇怪的水晶』吧。$R;" +
                                "$P这是冰精灵族的生命源泉哦。$R;" +
                                "我们冰精灵是在水晶里诞生的。$R;" +
                                "$R去我的故乡冰精灵岛吧，$R族长会报答您的。$R;" +
                                "$P旅游途中偶然相遇，$R不过现在想跟他在一起，$R;" +
                                "所以把这个水晶交託给您…$R;");
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 131, "得到了『奇怪的水晶』$R;");
                            return;
                        }
                        Say(pc, 255, "谢谢！$R;" +
                            "$R报答的东西虽小，$R请收下『奇怪的水晶』吧。$R;" +
                            "$R把行李减轻后，再来吧。$R;");
                        break;
                }
                return;
            }
            if (pc.Marionette != null)
            {
                Say(pc, 255, "做实验需要$R;" +
                    "『天青石碎片』5个$R;" +
                    "『月之碎片』5个$R;" +
                    "『心之碎片』5个$R;" +
                    "『水晶的碎片』5个$R;" +
                    "$R如果有的话，能不能分给我？$R;");
                return;
            }
            Say(pc, 255, "欢迎光临~$R;" +
                "找卡米罗先生有事吗？$R;");
        }
    }
}
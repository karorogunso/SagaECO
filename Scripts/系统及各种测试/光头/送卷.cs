
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap;
using SagaMap.Skill;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S9100000391 : Event
    {
        public S9100000391()
        {
            this.EventID = 910000989;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (Select(pc, "确定全服补偿？", "", "yes", "no") == 1)
            {

                foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                {
                    if (item.Character.Name == "空子")
                    {
                        Say(item.Character, 131, "恭喜我们突破30人上线啦！$R（虽然不少多开的）$R$R再说一次！$R请不要多开！不要多开！！！", "羽川柠");
                        Say(item.Character, 131, "好啦..$R送你们一只偷跑的搭档！$R$R请帮忙测试一下$CL搭档系统$CD哦！$R然后有问题请提交到$CR论坛BUG区$CD！$R不要私聊AN，AN已经要猝死了！！$R$R另外2016年即将结束了，$R祝大家2017年一切顺利，$R$CM元旦节快乐$CD！", "羽川柠");
                        GiveItem(item.Character, 10156550, 1);
                        Wait(pc, 1000);
                        Say(item.Character, 131, "得到了库玛一只！$R$R嘎哦！！！！！！！");
                    }
                }
            }
        }
    }
}


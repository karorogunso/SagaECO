using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M11053000
{
    public class S11001475 : Event
    {
        public S11001475()
        {
            this.EventID = 11001475;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "哦？是客人么？$R;" +
            "$R这里真是很清闲呢$R;" +
            "我不生产人鱼...$R;" +
            "我只是大自然的搬运工$R;" +
            "真是、人们为什么不来了$R;" +
            "准备着!为泰达尼亚事业而奋斗!$R;" +
            "这里还能买东西哦！$R;", "美人鱼搬运工");
            Say(pc, 131, "我现在很闲，不知道有什么能帮忙的吗？$R;", "美人鱼搬运工");
            switch (Select(pc, "要做什么呢", "", ",没什么", "任务服务台", "买东西", "卖东西"))
            {

                case 2:
                    HandleQuest(pc, 6);
                    break;
                case 3:
                    OpenShopBuy(pc, 4);
                    break;
                case 4:
                    OpenShopSell(pc, 4);
                    break;

            }

        }
    }
}


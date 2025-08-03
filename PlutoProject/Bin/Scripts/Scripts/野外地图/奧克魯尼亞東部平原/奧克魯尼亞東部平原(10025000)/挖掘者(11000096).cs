using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
//所在地圖:奧克魯尼亞東部平原(10025000) NPC基本信息:挖掘者(11000096) X:31 Y:121
namespace SagaScript.M10025000
{
    public class S11000096 : Event
    {
        public S11000096()
        {
            this.EventID = 11000096;

            this.questTransportDest = "呵呵呵$R;" +
                "是不是有东西要转交给这位叔叔呢?$R;" +
                "谢谢$R;";
            this.questTransportSource = "您可以把东西帮我转交给对方吗?$R;" +
                "那就拜托了，嘿嘿嘿$R;";
            this.questTransportCompleteSrc = "这么快就将物品转交给对方了?$R;" +
                "非常谢谢阿!$R;" +
                "$R请去任务服务台领取报酬吧！;";
            this.questTransportCompleteDest = "嘿嘿嘿，在做什么呢?";
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Global.Random.Next(0, 1))
            {
                case 0:
                    Say(pc, 11000096, 131, "这附近的魔物比较弱一些，$R;" +
                                           "所以身为初心者的您，$R;" +
                                           "在这附近赚取经验值，$R;" +
                                           "是个不错的选择呢。$R;", "挖掘者");
                    break;

                case 1:
                    Say(pc, 11000096, 131, "传闻某个地方存在着「大地精灵」，$R;" +
                                           "一直呵护着大地，$R;" +
                                           "要不要去找看看呢?$R;", "挖掘者");
                    break;
            }
        }
    }
}

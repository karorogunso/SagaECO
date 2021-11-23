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
                "是不是有東西要轉交給這位叔叔呢?$R;" +
                "謝謝$R;";
            this.questTransportSource = "您可以把東西幫我轉交給對方嗎?$R;" +
                "那就拜託了，嘿嘿嘿$R;";
            this.questTransportCompleteSrc = "這麼快就將物品轉交給對方了?$R;" +
                "非常謝謝阿!$R;" +
                "$R請去任務服務台領取報酬吧！;";
            this.questTransportCompleteDest = "嘿嘿嘿，在做什麼呢?";
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Global.Random.Next(0, 1))
            {
                case 0:
                    Say(pc, 11000096, 131, "這附近的魔物比較弱一些，$R;" +
                                           "所以身為初心者的您，$R;" +
                                           "在這附近賺取經驗值，$R;" +
                                           "是個不錯的選擇呢。$R;", "挖掘者");
                    break;

                case 1:
                    Say(pc, 11000096, 131, "傳聞某個地方存在著「大地精靈」，$R;" +
                                           "一直呵護著大地，$R;" +
                                           "要不要去找看看呢?$R;", "挖掘者");
                    break;
            }
        }
    }
}

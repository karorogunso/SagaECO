using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10001000
{
    public class S11000172 : Event
    {
        public S11000172()
        {
            this.EventID = 11000172;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "这里开始是『北极熊』$R;" +
                "栖息的危险地带$R;" +
                "$R赶紧回去比较好!$R;" +
                "$P……我只是警告而已$R;" +
                "$R因为不是禁止出入的地区$R;" +
                "所以无法阻止$R;");
            switch (Select(pc, "问什么吗?", "", "『北极熊』是什么?", "此外的情报?", "没什么好问的"))
            {
                case 1:
                    Say(pc, 131, "北极熊是非常残暴的白熊$R;" +
                        "力气大而且爱攻击人$R;" +
                        "$R每年都有被害者，你也小心点的好$R;");
                    break;
                case 2:

                    Say(pc, 131, "偶尔能发现比北极熊更大的脚印$R;" +
                        "$R那到底是什么脚印呢?$R;");
                    break;
            }
        }
    }
}

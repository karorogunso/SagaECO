using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞東部平原(10025001) NPC基本信息:帳篷(11000957) X:118 Y:137
namespace SagaScript.M10025001
{
    public class S11000957 : Event
    {
        public S11000957()
        {
            this.EventID = 11000957;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000954, 0, "啊啊，那「帐篷」?$R;" +
                                 "$R是冒险家那家伙设置的，$R;" +
                                 "应该还在里面吧?$R;" +
                                 "$R体力都恢复好了，我们才出来的。", "矿工前辈");

            switch (Select(pc, "要进到「帐篷」里面吗?", "", "进去", "不进去"))
            {
                case 1:
                    Warp(pc, 30200000, 2, 5);
                    break;
                    
                case 2:
                    break;
            }
        }
    }
}

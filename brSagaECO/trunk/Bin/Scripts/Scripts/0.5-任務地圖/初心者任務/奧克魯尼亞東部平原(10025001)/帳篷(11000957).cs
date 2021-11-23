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
            Say(pc, 11000954, 0, "啊啊，那「帳篷」?$R;" +
                                 "$R是冒險家那傢伙設置的，$R;" +
                                 "應該還在裡面吧?$R;" +
                                 "$R體力都恢復好了，我們才出來的。", "礦工前輩");

            switch (Select(pc, "要進到「帳篷」裡面嗎?", "", "進去", "不進去"))
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

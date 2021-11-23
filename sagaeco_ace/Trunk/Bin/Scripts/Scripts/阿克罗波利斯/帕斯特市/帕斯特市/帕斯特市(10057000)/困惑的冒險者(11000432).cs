using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000432 : Event
    {
        public S11000432()
        {
            this.EventID = 11000432;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 190, "我们三个一起到处冒险时$R;" +
                "$R有一个人到这个森林里后$R;" +
                "被困而不能出来$R;" +
                "处在进退两难的地步$R;");
            Say(pc, 11000431, 190, "可以的话去酒馆接受任务好吗？$R;" +
                "$R贸易家会给您丰富的报酬的$R;");
            Say(pc, 190, "什么！我给？$R;");
        }
    }
}
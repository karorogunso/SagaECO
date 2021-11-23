using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21003000
{
    public class S11001477 : Event
    {
        public S11001477()
        {
            this.EventID = 11001477;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "要保存什么呢？$R;", "美人鱼商人");
            Select(pc, "要保存东西么？", "", "买东西", "卖东西", "存钱", "取钱", "使用仓库（使用费1000金币）", "没什么事情");
        }


    }
}



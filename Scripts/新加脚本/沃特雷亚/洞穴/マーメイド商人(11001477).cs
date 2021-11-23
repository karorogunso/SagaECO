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
            Say(pc, 131, "要保存什麽呢？$R;", "美人魚商人");
            Select(pc, "要保存東西么？", "", "買東西", "賣東西", "存錢", "取錢", "使用倉庫（使用費1000金幣）", "沒什麼事情");
        }


    }
}



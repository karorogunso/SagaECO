using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30157000
{
    public class S11001052 : Event
    {
        public S11001052()
        {
            this.EventID = 11001052;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "來的好！", "", "買東西", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 178);
                    break;
                case 2:
                    break;
            }
        }
    }
}
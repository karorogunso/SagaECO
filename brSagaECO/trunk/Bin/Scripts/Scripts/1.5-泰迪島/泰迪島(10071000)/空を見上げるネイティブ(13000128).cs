using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10071000
{
    public class S13000128 : Event
    {
        public S13000128()
        {
            this.EventID = 13000128;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "どうした？", "", "星を詰める", "買い物をする", "何もしない"))
            {
                case 1:
                    Synthese(pc, 2025, 3);
                    break;
                case 2:
                    OpenShopBuy(pc, 189); 
                    break;
            }


        }
    }
}